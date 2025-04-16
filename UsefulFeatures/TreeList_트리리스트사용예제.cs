using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using RUTEC.FORM.FormCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RUTEC.FORM.FormMaster
{
    public partial class FrmRole : RutecBaseForm
    {
        //DB연결용 전역지정
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader rd;
        public FrmRole()
        {
            InitializeComponent();
        }
        /// <summary>
        /// DB 연결
        /// </summary>
        public void DBConnection()
        {
            /// <summary>
            /// DB접속을 위한 DB정보 
            /// </summary>
            string uid = "sa";
            string password = "fnxpr2020@)@)";
            string database = "RUTEC_BASIC";
            string server = "61.100.180.71,14337";
            string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmRole_Load(object sender, EventArgs e)
        {
            // DB연결
            DBConnection();
            GridViewSet();
        }
        /// <summary>
        /// 그리드뷰에 역할 띄우기
        /// </summary>
        private void GridViewSet()
        {
            try
            {
                string sql = "SELECT ROLE_CODE, ROLE_NAME, EXPLAIN, LIVE_CODE FROM [RUTEC_BASIC].[dbo].[ROLE_MST]";
                using (cmd = new SqlCommand(sql, conn))
                using (rd = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(rd);

                    gridControl1.DataSource = dataTable;
                    //gridView1.Columns.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 역할 마다의 권한 조회, 수정
        /// </summary>
        private void TreeListSet(string roleCode)
        {
            try
            {
                string sql =
                $@"SELECT	B.ROLE_CODE, A.MENU_CODE, A.PARENT_CODE, A.MENU_NAME,
                B.PERMIT_CREATE, B.PERMIT_READ,
                B.PERMIT_UPDATE, B.PERMIT_DELETE,
                B.PERMIT_PRINT, PERMIT_EXTERNAL_FILE, B.DELETE_MARK
                FROM	[RUTEC_BASIC].[dbo].[MENU_MST] A join  [RUTEC_BASIC].[dbo].[MENUROLE] B on A.MENU_CODE = B.MENU_CODE
                WHERE	B.ROLE_CODE = '{roleCode}'/*변수*/
                AND	A.DELETE_MARK = 0
                AND	B.DELETE_MARK = 0
                ORDER BY A.MENU_CODE";
                using (cmd = new SqlCommand(sql, conn))
                using (rd = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(rd);

                    // 모든권한 체크용 컬럼생성
                    dataTable.Columns.Add("PERMIT_ALL", typeof(bool));

                    treeList1.DataSource = dataTable;
                    //비교용 테이블 복사
                    DataTable dataTableCopy = dataTable.Copy();

                    // 모든 문자열 컬럼의 MaxLength 제한 제거
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (col.DataType == typeof(string) && col.MaxLength > 0)
                        {
                            col.MaxLength = -1; // 무제한
                        }
                    }

                    // 먼저 기존 컬럼 값 초기화
                    //checkBox 사용을 위한 dataTable의 dataType 변환
                    string[] permitCode = new string[] { "PERMIT_CREATE", "PERMIT_READ", "PERMIT_UPDATE", "PERMIT_DELETE", "PERMIT_PRINT", "PERMIT_EXTERNAL_FILE" };
                    foreach (string value in permitCode)
                    {
                        dataTable.Columns.Remove(value);
                        dataTable.Columns.Add(value, typeof(bool));  // 기본적으로 null 허용
                    }


                    // 체크박스 에디터 생성
                    RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
                    treeList1.RepositoryItems.Add(checkEdit);
                    foreach (TreeListColumn col in treeList1.Columns)
                    {
                        if (col.FieldName == "MENU_NAME")
                            continue;
                        col.ColumnEdit = checkEdit;
                        col.OptionsColumn.AllowEdit = true;
                    }

                    // 테이블 비교하고 원본 테이블 체크 상태 변경
                    int rowHandle = 0;
                    for (int colNum = 0; colNum < permitCode.Length; colNum++)
                    {
                        rowHandle = 0;
                        foreach (DataRow row in dataTableCopy.Rows)
                        {
                            string columnValue = row[permitCode[colNum]].ToString();
                            if (colNum == 0)
                            {
                                dataTable.Rows[rowHandle]["PERMIT_ALL"] = false;    // 기본 해제 상태
                            }
                            if (columnValue == "Y")
                            {
                                dataTable.Rows[rowHandle++][permitCode[colNum]] = true; // 체크 상태
                            }
                            else if (columnValue == "N")
                            {
                                dataTable.Rows[rowHandle++][permitCode[colNum]] = false; // 체크 해제
                            }


                        }

                    }


                    treeList1.KeyFieldName = "MENU_CODE";
                    treeList1.ParentFieldName = "PARENT_CODE";
                    treeList1.Columns["ROLE_CODE"].Visible = false;
                    treeList1.Columns["DELETE_MARK"].Visible = false;
                    treeList1.Columns["MENU_NAME"].Caption = "메뉴 명";
                    treeList1.Columns["PERMIT_READ"].Caption = "조회";
                    treeList1.Columns["PERMIT_CREATE"].Caption = "입력";
                    treeList1.Columns["PERMIT_UPDATE"].Caption = "수정";
                    treeList1.Columns["PERMIT_DELETE"].Caption = "삭제";
                    treeList1.Columns["PERMIT_PRINT"].Caption = "인쇄";
                    treeList1.Columns["PERMIT_EXTERNAL_FILE"].Caption = "외부파일";
                    treeList1.Columns["PERMIT_ALL"].Caption = "모든권한";
                }

                treeList1.ExpandAll(); // 모든 노드 확장

                //데이터 테이블 상태 커밋
                ((DataTable)(treeList1.DataSource)).AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 자식 노드들의 체크박스 상태를 설정하는 메서드
        /// </summary>
        private void SetChildNodesCheckState(TreeListNode parentNode, TreeListColumn column, bool value)
        {
            try
            {
                // 본인, 자식 모든권한 상태 변경
                if (column.FieldName == "PERMIT_ALL")
                {
                    foreach (TreeListNode childNode in parentNode.Nodes)
                    {
                        foreach (TreeListColumn col in treeList1.Columns)
                        {
                            if (col.FieldName != "MENU_NAME")
                            {
                                // 자식 상태 모두 변경
                                treeList1.SetRowCellValue(childNode, col, value);
                                // 본인 상태 모두 변경
                                treeList1.SetRowCellValue(parentNode, col, value);
                            }

                        }

                        // 자식이 또 자식을 갖고 있을 경우 재귀 호출
                        if (childNode.HasChildren)
                        {
                            SetChildNodesCheckState(childNode, column, value);
                        }
                    }
                }
                // 선택한 컬럼만 본인, 자식 권한 변경
                else
                {
                    foreach (TreeListNode childNode in parentNode.Nodes)
                    {
                        treeList1.SetRowCellValue(childNode, column, value);

                        // 자식이 또 자식을 갖고 있을 경우 재귀 호출
                        if (childNode.HasChildren)
                        {
                            SetChildNodesCheckState(childNode, column, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// DB 역할 권한 수정
        /// </summary>
        private void ModifyMenuRole()
        {
            foreach (DataRow row in ((DataTable)treeList1.DataSource).Rows)
            {
                //if (row.RowState == DataRowState.Modified) // 수정된 행만 처리
                //{
                string updateSql = $@"
                        UPDATE [RUTEC_BASIC].[dbo].[MENUROLE]
                        SET 
                            PERMIT_CREATE = @PERMIT_CREATE,
                            PERMIT_READ   = @PERMIT_READ,
                            PERMIT_UPDATE = @PERMIT_UPDATE,
                            PERMIT_DELETE = @PERMIT_DELETE,
                            PERMIT_PRINT  = @PERMIT_PRINT,
                            PERMIT_EXTERNAL_FILE = @PERMIT_EXTERNAL_FILE
                        WHERE MENU_CODE = @MENU_CODE AND ROLE_CODE = @ROLE_CODE";

                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@PERMIT_CREATE", (bool)row["PERMIT_CREATE"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@PERMIT_READ", (bool)row["PERMIT_READ"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@PERMIT_UPDATE", (bool)row["PERMIT_UPDATE"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@PERMIT_DELETE", (bool)row["PERMIT_DELETE"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@PERMIT_PRINT", (bool)row["PERMIT_PRINT"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@PERMIT_EXTERNAL_FILE", (bool)row["PERMIT_EXTERNAL_FILE"] ? "Y" : "N");
                    cmd.Parameters.AddWithValue("@MENU_CODE", row["MENU_CODE"]);
                    cmd.Parameters.AddWithValue("@ROLE_CODE", CodeTextEdit.Text);

                    cmd.ExecuteNonQuery();
                }
                //}
            }
            MessageBox.Show("수정완료");
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            CodeTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "ROLE_CODE").ToString();
            NameTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "ROLE_NAME").ToString();
            ExTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "EXPLAIN").ToString();
            UseComboBoxEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "LIVE_CODE").ToString();
            TreeListSet(CodeTextEdit.Text);
        }
        
        private void treeList1_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
            try
            {
                var value = treeList1.GetRowCellValue(e.Node, e.Column);

                if (value is bool boolValue)
                {
                    // 부모 노드라면 모든 자식에게 값 설정
                    if (e.Node.HasChildren)
                    {
                        SetChildNodesCheckState(e.Node, e.Column, !boolValue);
                    }

                    // 본인 모든권한 상태 변경
                    if (e.Column.FieldName == "PERMIT_ALL")
                    {
                        foreach (TreeListColumn col in treeList1.Columns)
                            if (col.FieldName != "MENU_NAME")
                                treeList1.SetRowCellValue(e.Node, col, !boolValue);
                    }
                    else
                    {
                        // 본인 체크 상태 변경
                        treeList1.SetRowCellValue(e.Node, e.Column, !boolValue);
                    }
                }
                //MessageBox.Show($"선택한 셀의 값: {value}, {value.GetType().ToString()}");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void FrmRole_Action_Search(object sender, EventArgs e)
        {
            GridViewSet();
        }

        private void FrmRole_Action_Modify(object sender, EventArgs e)
        {

        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("수정하시겠습니까?", "경고", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
                ModifyMenuRole();
        }
    }
}
