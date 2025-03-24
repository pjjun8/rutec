using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_information_production_Project
{
    public partial class Form1 : Form
    {
        public static string uid = "sa";
        public static string password = "fnxpr2020@)@)";
        public static string database = "SANGWON";
        public static string server = "61.100.180.71,14337";
        string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
        public string name = "";
        public string code = "";
        public string gtin = "";
        string size = "";
        string unit = "";
        string iaNumber = "";
        string sMethod = "";
        string productionstatus = "";
        //string primaryKey = "";
        bool pageFlag = false;
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt = new DataTable();
        DialogResult result;
        bool ClearTextBoxesFlag = true;
        // 메인 컨트롤러 저장용 리스트
        public static List<Control> mainPanelControls = new List<Control>();
        // 메인 컨트롤러 저장 함수
        private void StoreMainControls()
        {
            mainPanelControls.Clear();
            foreach (Control ctrl in panelControl2.Controls)
            {
                mainPanelControls.Add(ctrl);
            }
        }
        public void RestoreMainControls()
        {
            this.panelControl2.Controls.Clear();

            foreach (Control ctrl in mainPanelControls)
            {
                this.panelControl2.Controls.Add(ctrl);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// DB테이블 조회
        /// </summary>
        private void SelectTable()
        {
            string sql = "select * from product";   // * 수정
            cmd = new SqlCommand(sql, conn);

            /////////////////////////// 함수로 묵어
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                name = reader["productName"].ToString();
                code = reader["productCode"].ToString();
                gtin = reader["productGTIN"].ToString();
                size = reader["productSize"].ToString();
                unit = reader["PUnit"].ToString();
                iaNumber = reader["IANumber"].ToString();
                sMethod = reader["SMethod"].ToString();
                productionstatus = reader["productionstatus"].ToString();
                //gridControl1.DataSource = 
                dt.Rows.Add(code, name, gtin, size, unit, iaNumber, sMethod, productionstatus);
            }

            gridControl1.DataSource = dt;
            reader.Close();
            AutoGridViewCellSize();
            //cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            //gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            gridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정
        }
        private void InsertTable()
        {
            name = nameText.Text;
            code = codeText.Text;
            gtin = gtinText.Text;
            size = sizeText.Text;
            //unit = unitText.Text;//calcEdit1
            unit = PUnitTextEdit.Text + label1.Text;
            iaNumber = iaNumberText.Text;
            sMethod = sMethodText.Text;

            try
            {
                if (!(code == ""))
                {
                    DataTableReset();
                    //dt.Rows.Add(name, code, gtin, size, unit, iaNumber, sMethod);
                    //DataRow row = dt.Rows[0];
                    //name = row["productName"].ToString();
                    //code = row["productCode"].ToString();
                    //gtin = row["productGTIN"].ToString();
                    //size = row["productSize"].ToString();
                    //unit = row["PUnit"].ToString();
                    //iaNumber = row["IANumber"].ToString();
                    //sMethod = row["SMethod"].ToString();
                    string sql = "insert into product (productName, productCode, productGTIN, productSize, PUnit, IANumber, SMethod, productionstatus)" +
                                $"VALUES (@name, @code, @gtin, @size, @unit, @iaNumber, @sMethod, @productionstatus)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@gtin", gtin);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@iaNumber", iaNumber);
                        cmd.Parameters.AddWithValue("@sMethod", sMethod);
                        if (productionCheckBox.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@productionstatus", "생산중");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@productionstatus", "생산중지");
                        }

                        //cmd.ExecuteNonQuery(); // 결과값 return 필요
                        int num = cmd.ExecuteNonQuery();
                        if (num != 0)
                        {
                            MessageBox.Show("커밋 완료!");
                        }//  if (cmd.ExecuteNonQuery() != 0)
                        else
                        {
                            MessageBox.Show($"커밋 실패! {num}");
                        }// else (cmd.ExecuteNonQuery() != 0)
                    }

                    SelectTable();
                    ClearTextBoxesFlag = true;
                    ClearTextBoxes(panelControl1);
                }
                else
                {
                    MessageBox.Show("PK가 공백입니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// 테이블 업데이트 함수
        /// </summary>
        private void UpdateTable()
        {
            try
            {


                string sql = "update product set productName = @name, productGTIN = @gtin, " +
                             "productSize = @size, PUnit = @unit, IANumber = @iaNumber, SMethod = @sMethod, productionstatus = @productionstatus " +
                            $"where productCode = @primaryKey";

                int queryUpdateNum = 0;
                List<string> checkBoxValues = new List<string>();

                foreach (string value in GetSelectedCheckBox())
                {
                    checkBoxValues.Add(value);
                }

                int i = 0;
                foreach (int id in gridView1.GetSelectedRows())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //체크된 그리드뷰의 행의 열 값 가져오기
                        name = gridView1.GetRowCellValue(id, "productNameField").ToString();
                        //code = gridView1.GetRowCellValue(id, "productCode").ToString();
                        gtin = gridView1.GetRowCellValue(id, "productGTINField").ToString();
                        size = gridView1.GetRowCellValue(id, "productSizeField").ToString();
                        unit = gridView1.GetRowCellValue(id, "PUnitField").ToString();
                        iaNumber = gridView1.GetRowCellValue(id, "IANumberField").ToString();
                        sMethod = gridView1.GetRowCellValue(id, "SMethodField").ToString();
                        productionstatus = gridView1.GetRowCellValue(id, "productionStatusField").ToString();

                        //DB에 넣을 값 지정하기
                        cmd.Parameters.AddWithValue("@name", name);
                        //cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@gtin", gtin);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@iaNumber", iaNumber);
                        cmd.Parameters.AddWithValue("@sMethod", sMethod);
                        cmd.Parameters.AddWithValue("@productionstatus", productionstatus);
                        cmd.Parameters.AddWithValue("@primaryKey", checkBoxValues[i++]);
                        queryUpdateNum += cmd.ExecuteNonQuery();
                    }
                }

                if (queryUpdateNum > 0)
                {
                    MessageBox.Show($"{queryUpdateNum}개 행 업데이트성공");
                }
                else
                {

                    MessageBox.Show($"업데이트실패 : 쿼리 실행 실패");
                }
                DataTableReset();
                SelectTable();
                //ClearTextBoxesFlag = true;
                //ClearTextBoxes(panelControl1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// dataTable 초기화 함수
        /// </summary>
        private void DataTableReset()
        {
            try
            {
                dt.Clear();
                dt.Columns.Add("productCodeField");
                dt.Columns.Add("productNameField");
                dt.Columns.Add("productGTINField");
                dt.Columns.Add("productSizeField");
                dt.Columns.Add("PUnitField");
                dt.Columns.Add("IANumberField");
                dt.Columns.Add("SMethodField");
                dt.Columns.Add("productionStatusField");

            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                DataTableReset();
                SelectTable();
                StoreMainControls(); // 폼변환을 위한 컨트롤러 저장
                //conn.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("DB 연결 실패: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 패널 내 모든 텍스트박스를 초기화하는 함수
        /// 패널 초기화 메시지 사용시 ClearTextBoxesFlag = true 를 설정해줘야함
        /// 미사용시 ClearTextBoxesFlag = false
        /// </summary>
        /// <param name="control"></param>
        private void ClearTextBoxes(Control control)
        {
            if (ClearTextBoxesFlag == true)
            {
                result = MessageBox.Show("패널을 초기화 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                ClearTextBoxesFlag = false;
            }

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (Control c in control.Controls)
                    {
                        if (c is TextBox textBox)
                        {
                            textBox.Clear(); // 텍스트 지우기
                        }
                        else if (c is CheckBox checkBox)
                        {
                            checkBox.Checked = false;
                        }
                        else if (c is DevExpress.XtraEditors.CalcEdit calcEdit)
                        {
                            calcEdit.EditValue = 0;
                        }


                        else if (c.HasChildren)
                        {
                            ClearTextBoxes(c); // 자식 컨트롤이 있으면 재귀 호출
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (ClearTextBoxesFlag == true)
                {
                    MessageBox.Show("패널이 초기화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBoxesFlag = false;
                }
            }
            else
            {
                MessageBox.Show("패널 초기화가 취소되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// <summary>
        /// 선택한 테이블 행 삭제 함수
        /// </summary>
        private void DeleteTable()
        {
            try
            {
                string sql = $"delete from product where productCode = @code";
                int rowsAffected = 0;

                foreach (string value in GetSelectedCheckBox())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                        cmd.Parameters.AddWithValue("@code", value);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }

                if (rowsAffected > 0)
                {
                    MessageBox.Show("삭제 성공!");
                }
                else
                {
                    MessageBox.Show("삭제할 데이터가 없습니다.");
                }
                DataTableReset();
                SelectTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ProductInsertButton_Click(object sender, EventArgs e)
        {
            InsertTable();
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// checkBox 사용으로 모두 주석처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //// 현재 행 선택 여부 가져오기
            //bool isSelected = gridView1.IsRowSelected(e.RowHandle);

            //// 선택 상태 반전 (토글)
            //if (isSelected)
            //{
            //    gridView1.UnselectRow(e.RowHandle); // 선택 해제
            //}
            //else
            //{
            //    gridView1.SelectRow(e.RowHandle); // 선택
            //}


            //if (e.RowHandle >= 0) // 유효한 행인지 확인
            //{
            //    primaryKey = gridView1.GetRowCellValue(e.RowHandle, "productCode").ToString();
            //    MessageBox.Show($"선택 한 제품 Code: {primaryKey}");

            //    nameText.Text = gridView1.GetRowCellValue(e.RowHandle, "productName").ToString(); ;
            //    codeText.Text = gridView1.GetRowCellValue(e.RowHandle, "productCode").ToString(); ;
            //    gtinText.Text = gridView1.GetRowCellValue(e.RowHandle, "productGTIN").ToString(); ;
            //    sizeText.Text = gridView1.GetRowCellValue(e.RowHandle, "productSize").ToString(); ;
            //    unitText.Text = gridView1.GetRowCellValue(e.RowHandle, "PUnit").ToString(); ;
            //    iaNumberText.Text = gridView1.GetRowCellValue(e.RowHandle, "IANumber").ToString(); ;
            //    sMethodText.Text = gridView1.GetRowCellValue(e.RowHandle, "SMethod").ToString(); ;
            //}
        }
        /// <summary>
        /// 그리드뷰컨트롤러에 체크박스 기능 사용하기(체크한 행 정보 임시저장)
        /// 체크된 행의 PK값 넘겨줌
        /// </summary>
        /// <returns></returns>
        private List<object> GetSelectedCheckBox()
        {
            try
            {
                //선택된 아이템 리스트로 가져오기
                List<object> list = new List<object>();
                List<object> KeyList = new List<object>();
                foreach (int id in gridView1.GetSelectedRows())
                {
                    list.Add(gridView1.GetRow(id) as object);
                    KeyList.Add(gridView1.GetRowCellValue(id, "productCodeField") as object);
                }
                //gridControl1.DataSource = list; 테스트용 문장
                return KeyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (GetSelectedCheckBox().Count == 0)
            {
                MessageBox.Show("삭제할 행을 선택하세요.");
                return;
            }
            result = MessageBox.Show("패널을 초기화 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DeleteTable();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DataTableReset();
            SelectTable();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

            if (GetSelectedCheckBox().Count == 0)
            {
                MessageBox.Show("수정할 행을 선택하세요.");
                return;
            }
            else
            {
                //result = MessageBox.Show("정말 수정하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                UpdateTable();
            }

        }

        private void PanelClearButton_Click(object sender, EventArgs e)
        {
            //result = MessageBox.Show("정말 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            ClearTextBoxesFlag = true;
            ClearTextBoxes(panelControl1); // 패널 내 텍스트박스 전체 초기화
        }

        private void TestButton_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "productName")  // "Price" 컬럼 전체 배경색 변경
            if (e.Column.Name == productCodeName.Name)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void MainFormButton_Click(object sender, EventArgs e)
        {

        }

        private void 품목마스터ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pageFlag == true)
            {
                RestoreMainControls();
                panelControl3.Visible = true;
                pageFlag = false;
            }
        }

        private void 라벨출력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageFlag = true;    //품목마스터 폼 중복방지
            panelControl3.Visible = false;
            //StoreMainControls();
            this.panelControl2.Controls.Clear();
            XtraForm1 xtraForm1 = new XtraForm1();
            xtraForm1.TopLevel = false;
            this.Controls.Add(xtraForm1);
            xtraForm1.Parent = this.panelControl2;
            xtraForm1.ControlBox = false;
            xtraForm1.Show();
        }

        private void 제품생산조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageFlag = true;    //품목마스터 폼 중복방지
            panelControl3.Visible = false;
            //StoreMainControls();
            this.panelControl2.Controls.Clear();
            XtraForm2 xtraForm2 = new XtraForm2();
            xtraForm2.TopLevel = false;
            this.Controls.Add(xtraForm2);
            xtraForm2.Parent = this.panelControl2;
            xtraForm2.ControlBox = false;
            xtraForm2.Show();
        }

        private void codeText_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void codeText_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 숫자가 아니면 입력 방지
            }
        }

        private void nameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 백스페이스 등 제어 문자 허용
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            // 한글 음절 (가~힣) + 한글 자음/모음 (ㄱ~ㅎ, ㅏ~ㅣ) 허용
            if ((e.KeyChar >= '가' && e.KeyChar <= '힣') ||  // 완성형 한글
                (e.KeyChar >= 'ㄱ' && e.KeyChar <= 'ㅎ') ||  // 자음
                (e.KeyChar >= 'ㅏ' && e.KeyChar <= 'ㅣ') ||    // 모음
                char.IsDigit(e.KeyChar))    // 숫자허용
            {
                return; // 입력 허용
            }
            // 그 외 문자 입력 방지
            e.Handled = true;
        }
        private void gtinText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 숫자가 아니면 입력 방지
            }
        }

        private void sizeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 숫자가 아니면 입력 방지
            }
        }

        private void calcEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 숫자가 아니면 입력 방지
            }
        }

        private void sMethodText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar); // 소문자를 대문자로 변환
                return;
            }
            else if (char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true; // 영,한글문자 외에는 입력 불가능
        }

        private void 품목마스터ToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, 품목마스터ToolStripMenuItem.ContentRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
    }
}
