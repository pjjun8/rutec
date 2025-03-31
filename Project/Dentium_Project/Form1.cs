using DevExpress.Charts.Native;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dentium_Project
{
    
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// DB 접근용 객체
        /// </summary>
        DB db;
        /// <summary>
        /// 셀 입력 바인딩용 리스트
        /// </summary>
        public BindingList<ExcelData> insertlist;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            //gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            advBandedGridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            advBandedGridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정
        }
        /// <summary>
        /// 붙어넣기 이벤트 처리를 위한 커맨드키 함수 오버라이드 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V)) // Ctrl + V 감지
            {
                PasteExcelDataToGrid(insertlist); // 붙여넣기 함수 실행
                return true; // 이벤트 처리 완료
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// 클립보드 값 가져와서 쏴주기 함수
        /// </summary>
        private void PasteExcelDataToGrid(BindingList<ExcelData> list)
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
                    list.Add(new ExcelData { ProductLot = row.Trim() });
                }
                // GridControl에 바인딩
                //gridControl1.DataSource = list;
                //advBandedGridView1.RefreshData();
                LOTSelect();
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

                    xtraReport1.CreateDocument();
                    //1200 넘은면 2장 발행
                    if(int.Parse(xtraReport1.Parameters["QTY"].Value.ToString()) > 1200)
                    {
                        xtraReport1.Print();
                        xtraReport1.Print();
                    }
                    else
                    {
                        xtraReport1.Print();
                    }
                    //xtraReport1.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("정확한 입력값이 아닙니다.");
                //MessageBox.Show(ex.ToString());
            }
        }
        private void LabelOutPutButton_Click(object sender, EventArgs e)
        {
            PrintParametersLabel();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            db = new DB(this);
            db.DBConnection();
            AutoGridViewCellSize();
            
            insertlist = new BindingList<ExcelData>() { AllowNew = true }; //AllowNew 설정
            //GridControl에 바인딩
            gridControl1.DataSource = insertlist;
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
            
            dt.Columns.Add("ProductLot", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("ModelName", typeof(string));
            dt.Columns.Add("Holder", typeof(string));
            dt.Columns.Add("UpJIG_LOT", typeof(string));
            dt.Columns.Add("UpJIG_LOT2", typeof(string));
            dt.Columns.Add("DownJIG_LOT", typeof(string));
            dt.Columns.Add("DownJIG_LOT2", typeof(string));
            dt.Columns.Add("Memo", typeof(string));
            //dt.Load(list);
            foreach (var item in list)
            {
                if(int.Parse(item.QTY) > 1200)  //1200넘으면 2장 발행
                {
                    for(int i = 0; i < 2; i++)
                    {
                        dt.Rows.Add(item.ProductLot, item.QTY, item.ModelName, item.Holder,
                                item.UpJIG_LOT, item.UpJIG_LOT2, item.DownJIG_LOT, item.DownJIG_LOT2, item.Memo);
                    }
                }
                else
                {
                    dt.Rows.Add(item.ProductLot, item.QTY, item.ModelName, item.Holder,
                                item.UpJIG_LOT, item.UpJIG_LOT2, item.DownJIG_LOT, item.DownJIG_LOT2, item.Memo);
                }
            }
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
            DataTable dt = ConvertToDataTable(insertlist);

            rpt.DataSource = dt;
            //rpt.ShowPreviewDialog();
            rpt.Print();    // XtraReport.DataSource랑 DataTable이랑 바인딩해서 반복문 안해도 행수 만큼 출력해주는듯?

            //DataTable 일떄 형식
            //XtraReport_table rpt = new XtraReport_table();

            //rpt.DataSource = gridControl1.DataSource;
            //rpt.Print();
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            PrintTableLabel();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
    public class ExcelData
    {
        public string ProductLot { get; set; }
        public string ModelName { get; set; }
        public string QTY { get; set; }
        public string Holder { get; set; }
        public string UpJIG_LOT { get; set; }
        public string UpJIG_LOT2 { get; set; }
        public string DownJIG_LOT2 { get; set; }
        public string DownJIG_LOT { get; set; }
        public string Memo { get; set; }

    }
}
