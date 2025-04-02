using DevExpress.Charts.Native;
using DevExpress.Office.Utils;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.Utils.Extensions;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Dentium_Project
{
    
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// DB 접근용 객체
        /// </summary>
        DB db;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            
        }
        /// <summary>
        /// 붙어넣기 이벤트 처리를 위한 커맨드키 함수 오버라이드 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)

        {
            if (keyData == (Keys.Enter)) // Enter Ctrl + V 감지
            {
                //List<string> list = new List<string>();
                //list.Add(new ExcelData { ProductLot = advBandedGridView1.GetRowCellValue(selectLow, "ProductLot").ToString() });
                gridControl1.DataSource = Common.insertlist;
                advBandedGridView1.RefreshData(); // UI 갱신
                int focusedRow = advBandedGridView1.FocusedRowHandle;
                advBandedGridView1.FocusedRowHandle = -1; // 포커스 해제
                advBandedGridView1.FocusedRowHandle = focusedRow;   //포커스 재설정

                //Thread.Sleep(100);

                LOTSelect();
                return true; // 이벤트 처리 완료
            }

            if (keyData == (Keys.Control | Keys.V))
            {
                if (advBandedGridView1.FocusedColumn.FieldName == "ProductLot")
                {
                    PasteExcelDataToGrid(); // 붙여넣기 함수 실행
                    advBandedGridView1.RefreshData(); // UI 갱신
                    return true; // 이벤트 처리 완료
                }
            }
            //return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// 클립보드 값 가져와서 쏴주기 함수
        /// </summary>
        private void PasteExcelDataToGrid()
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    return;
                }

                // 클립보드에서 데이터 가져오기
                string clipboardText = Clipboard.GetText();

                string[] rows = clipboardText.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //string[] test = clipboardText.Split(new[] "\r\n");

                //List<ExcelData> list = new List<ExcelData>();
                
                foreach (string row in rows)
                {
                    if (!(string.IsNullOrWhiteSpace(row)))
                    {
                        Common.insertlist.Add(new ExcelData { ProductLot = row.Trim() });
                    }
                }
                // GridControl에 바인딩
                //gridControl1.DataSource = list;
                //advBandedGridView1.RefreshData();
                LOTSelect();
                //Clipboard.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
        /// <summary>
        /// 입력 LOT으로 DB조회 해서 수량, 제품명 가져오기
        /// </summary>
        private void LOTSelect()
        {
            try
            {
                List<string> list = new List<string>();

                for (int i = 0; i < advBandedGridView1.RowCount - 1; i++)
                {
                    
                    if ((advBandedGridView1.GetRowCellValue(i, "ProductLot") != null) && (advBandedGridView1.GetRowCellValue(i, "ProductLot").ToString() != ""))
                    {
                        
                        string name = advBandedGridView1.GetRowCellValue(i, "ProductLot").ToString();
                        list.Add(name);
                        //MessageBox.Show(name);
                    }
                    else
                    {
                        MessageBox.Show($"{i + 1}행의 입력한 LOT이 없습니다.");
                    }

                }
                gridControl1.DataSource = db.DBSelect(list);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 라벨 프린트를 위한 함수
        /// </summary>
        private void PrintParametersLabel()
        {
            try
            {
                //xtraReport1.RequestParameters = false;
                XtraReport1 xtraReport1 = new XtraReport1();
                for (int i = 0; advBandedGridView1.RowCount - 1 > i; i++)
                {

                    xtraReport1.Parameters["LOT"].Value = advBandedGridView1.GetRowCellValue(i, "ProductLot").ToString();
                    xtraReport1.Parameters["ModelName"].Value = advBandedGridView1.GetRowCellValue(i, "ModelName").ToString();
                    xtraReport1.Parameters["QTY"].Value = advBandedGridView1.GetRowCellValue(i, "QTY").ToString();
                    xtraReport1.Parameters["Holder"].Value = advBandedGridView1.GetRowCellValue(i, "Holder").ToString();
                    xtraReport1.Parameters["Up_JIG"].Value = advBandedGridView1.GetRowCellValue(i, "UpJIG_LOT").ToString();
                    xtraReport1.Parameters["Up_JIG2"].Value = advBandedGridView1.GetRowCellValue(i, "UpJIG_LOT2").ToString();
                    xtraReport1.Parameters["Down_JIG"].Value = advBandedGridView1.GetRowCellValue(i, "DownJIG_LOT").ToString();
                    xtraReport1.Parameters["Down_JIG2"].Value = advBandedGridView1.GetRowCellValue(i, "DownJIG_LOT2").ToString();
                    xtraReport1.Parameters["Memo"].Value = advBandedGridView1.GetRowCellValue(i, "Memo").ToString();

                    
                    //1200 넘은면 2장 발행
                    int qty = int.Parse(xtraReport1.Parameters["QTY"].Value.ToString());
                    if (qty > 1200)
                    {
                        xtraReport1.Parameters["QTY"].Value = "1200";
                        xtraReport1.CreateDocument();
                        xtraReport1.Print();

                        xtraReport1.Parameters["QTY"].Value = (qty - 1200).ToString();
                        xtraReport1.CreateDocument();
                        xtraReport1.Print();
                    }
                    else
                    {
                        xtraReport1.CreateDocument();
                        xtraReport1.Print();
                    }
                    //xtraReport1.ShowPreviewDialog();
                }
            }
            catch
            {
                MessageBox.Show("빈값을 입력해 주세요");
                //MessageBox.Show(ex.ToString());
            }
        }
        private void LabelOutPutButton_Click(object sender, EventArgs e)
        {
            PrintParametersLabel();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            db = new DB();
            db.DBConnection();
            gridControlSetting();

            // 최초 실행 시 빈 데이터 추가
            //Common.insertlist.Add(new ExcelData());

            //GridControl에 바인딩
            gridControl1.DataSource = Common.insertlist;

        }
        private void gridControlSetting()
        {
            //gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            advBandedGridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            advBandedGridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정

            // 컬럼 이동, 크기 조절, 정렬, 필터링 방지
            advBandedGridView1.OptionsCustomization.AllowColumnMoving = false;
            advBandedGridView1.OptionsCustomization.AllowColumnResizing = false;
            advBandedGridView1.OptionsCustomization.AllowSort = false;

            // 밴드 드래그 이동 방지
            advBandedGridView1.OptionsCustomization.AllowBandMoving = false;

            // 밴드 크기 조절 방지
            advBandedGridView1.OptionsCustomization.AllowBandResizing = false;

            // 그룹 패널 숨기기 (AllowFixedGroups 대체)
            advBandedGridView1.OptionsView.ShowGroupPanel = false;

            // 컬럼 필터링 방지 (AllowColumnFiltering 대체)
            advBandedGridView1.OptionsView.ShowAutoFilterRow = false;

            // 사용자가 컬럼 추가/삭제 못하게 막기
            advBandedGridView1.OptionsMenu.EnableColumnMenu = false;
            advBandedGridView1.OptionsMenu.EnableFooterMenu = false;

            // 행 수정 가능하지만 선택/삭제 방지
            advBandedGridView1.OptionsBehavior.Editable = true;
            //advBandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            //advBandedGridView1.OptionsSelection.MultiSelect = false;
            //advBandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LOTSelect();
        }
        
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.RowHandle == gridView1.DataRowCount - 1) // 마지막 행만 추가
            //{
            //    insertlist.Add(new ExcelData { ProductLot = "" });
            //    gridView1.RefreshData();
            //}
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //// 입력된 값이 없는지 확인
            //ExcelData row = e.Row as ExcelData;
            //if (row != null && string.IsNullOrWhiteSpace(row.ProductLot))
            //{
            //    insertlist.Remove(row); // 입력이 없으면 삭제
            //    gridView1.RefreshData();
            //}
        }
        /// <summary>
        /// BindingList 를 DataTable 형식으로 변환
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable(BindingList<ExcelData> list)
        {
            DataTable dt = new DataTable();

            // 간단한 변환 방법! 프로퍼티 정보를 가져와서 DataTable 컬럼 생성
            PropertyInfo[] properties = typeof(ExcelData).GetProperties();
            foreach (var prop in properties)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // BindingList 데이터를 DataTable로 변환
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    // 로우의 빈값 확인
                    if (string.IsNullOrWhiteSpace(prop.GetValue(item)?.ToString()))
                    {
                        if(!(prop.Name == "Memo"))
                        {
                            Common.printFlag = false;
                        }
                    }
                }
                if (int.Parse(item.QTY) > 1200)
                {
                    DataRow row1 = dt.NewRow();
                    row1.ItemArray = row.ItemArray.Clone() as object[]; // 깊은 복사
                    row["QTY"] = "1200";
                    row1["QTY"] = (int.Parse(row1["QTY"].ToString()) - 1200).ToString();
                    dt.Rows.Add(row);
                    dt.Rows.Add(row1);
                }
                else
                {
                    dt.Rows.Add(row);
                }
            }



            //dt.Columns.Add("ProductLot", typeof(string));
            //dt.Columns.Add("QTY", typeof(string));
            //dt.Columns.Add("ModelName", typeof(string));
            //dt.Columns.Add("Holder", typeof(string));
            //dt.Columns.Add("UpJIG_LOT", typeof(string));
            //dt.Columns.Add("UpJIG_LOT2", typeof(string));
            //dt.Columns.Add("DownJIG_LOT", typeof(string));
            //dt.Columns.Add("DownJIG_LOT2", typeof(string));
            //dt.Columns.Add("Memo", typeof(string));
            //dt.Load(list);
            //foreach (var item in list)
            //{
            //    if(int.Parse(item.QTY) > 1200)  //1200넘으면 2장 발행
            //    {
            //        for(int i = 0; i < 2; i++)
            //        {
            //            dt.Rows.Add(item.ProductLot, item.QTY, item.ModelName, item.Holder,
            //                    item.UpJIG_LOT, item.UpJIG_LOT2, item.DownJIG_LOT, item.DownJIG_LOT2, item.Memo);
            //        }
            //    }
            //    else
            //    {
            //        dt.Rows.Add(item.ProductLot, item.QTY, item.ModelName, item.Holder,
            //                    item.UpJIG_LOT, item.UpJIG_LOT2, item.DownJIG_LOT, item.DownJIG_LOT2, item.Memo);
            //    }
            //}
            return dt;
        }
        /// <summary>
        /// 테이블로 변환후 라벨 출력
        /// </summary>
        private void PrintTableLabel()
        {
            XtraReport_table rpt = new XtraReport_table();

            // DataSource 변환
            //BindingList<ExcelData> bindingList = (BindingList<ExcelData>)gridControl1.DataSource;
            DataTable dt = ConvertToDataTable(Common.insertlist);

            rpt.DataSource = dt;
            //rpt.ShowPreviewDialog();
            if(Common.printFlag == true)
            {
                rpt.Print();    // XtraReport.DataSource랑 DataTable이랑 바인딩해서 반복문 안해도 행수 만큼 출력해주는듯?
            }
            else
            {
               MessageBox.Show("빈값을 입력해 주세요");
            }

            //DataTable 일떄 형식
            //XtraReport_table rpt = new XtraReport_table();

            //rpt.DataSource = gridControl1.DataSource;
            //rpt.Print();
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //printOutput setting
            Common.printFlag = true;
            PrintTableLabel();
        }

        private void advBandedGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //입력된 값이 없는지 확인
            ExcelData row = e.Row as ExcelData;
            if (new[] { row.ProductLot, row.ModelName, row.QTY, row.Holder, row.Memo, row.UpJIG_LOT,
                        row.UpJIG_LOT2, row.DownJIG_LOT, row.DownJIG_LOT2}.All(string.IsNullOrWhiteSpace))  //한번에 값 확인 방법
            {
                
                Common.insertlist.Remove(row); // 입력이 없으면 삭제
                advBandedGridView1.RefreshData();
            }
        }

        private void CellDeleteButton_Click(object sender, EventArgs e)
        {
            if (Common.selectLow < 0)
            {
                Common.selectLow = 0;
            }
            DialogResult result;
            result = MessageBox.Show($"{Common.selectLow + 1}행을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                
                try
                {
                    Common.insertlist.RemoveAt(Common.selectLow);
                }
                catch
                {
                    MessageBox.Show("행이 없습니다.");
                }
                
            }
            
            advBandedGridView1.RefreshData();

        }

        private void advBandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 특정 컬럼에서만 이벤트 발생 (예: "ProductLot" 컬럼)
            //if (e.Column.FieldName == "ProductLot")
            //{
            //    Common.newValue = e.Value?.ToString(); // 새로운 값 가져오기
            //    //MessageBox.Show($"[붙여넣기 감지] {e.Column.FieldName}: {newValue}");
            //    PasteExcelDataToGrid(); // 붙여넣기 함수 실행
            //}
        }

        private void CellAllDeleteButton_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show($"전체 행을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {

                try
                {
                    int rowCount = advBandedGridView1.RowCount;
                    if (rowCount <= 1)
                    {
                        MessageBox.Show("행이 없습니다.");
                    }
                    else
                    {
                        //Common.insertlist.RemoveAt(Common.selectLow);
                        Common.insertlist.Clear();
                    }
                }
                catch
                {
                    
                }

            }

            advBandedGridView1.RefreshData();
        }
        private void advBandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            // 클릭된 셀을 바로 편집 상태로 변경
            //advBandedGridView1.FocusedRowHandle = e.RowHandle;
            //advBandedGridView1.FocusedColumn = e.Column;
            advBandedGridView1.ShowEditor(); // 즉시 편집 모드 진입
            Common.selectLow = e.RowHandle;
        }
    }
}
