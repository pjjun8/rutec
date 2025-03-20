using DevExpress.Office.Utils;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_information_production_Project
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// DB접속을 위한 전역지정
        /// </summary>
        public static string uid = "sa";
        public static string password = "fnxpr2020@)@)";
        public static string database = "SANGWON";
        public static string server = "61.100.180.71,14337";
        string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";

        /// <summary>
        /// 클릭한 셀의 정보저장을 위한 전역변수
        /// </summary>
        string gtin = "";
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt = new DataTable();

        /// <summary>
        /// 프린트 확인용 플래그
        /// </summary>
        bool printSuccessFlag;
        public XtraForm1()
        {
            InitializeComponent();

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
            catch (Exception ex)
            {

            }

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
            string name, code, gtin, size, unit, iaNumber, sMethod, productionStatus;
            while (reader.Read())
            {
                name = reader["productName"].ToString();
                code = reader["productCode"].ToString();
                gtin = reader["productGTIN"].ToString();
                size = reader["productSize"].ToString();
                unit = reader["PUnit"].ToString();
                iaNumber = reader["IANumber"].ToString();
                sMethod = reader["SMethod"].ToString();
                productionStatus = reader["productionstatus"].ToString();
                dt.Rows.Add(code, name, gtin, size, unit, iaNumber, sMethod, productionStatus);
            }

            gridControl1.DataSource = dt;
            reader.Close();
            AutoGridViewCellSize();

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
        /// <summary>
        /// 텍스트박스 공백 확인 함수
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private int CheckValue(Control control)
        {
            int voidNum = 0;
            foreach (Control c in control.Controls)
            {
                if (c is TextEdit textEdit)
                {
                    if((c.Text is "") || (c.Text is null))
                    {
                        voidNum++;
                    }
                    //textBox.Clear(); // 텍스트 지우기
                }
                else if (c.HasChildren)
                {
                    CheckValue(c); // 자식 컨트롤이 있으면 재귀 호출
                }

            }
            return voidNum;
        }
        /// <summary>
        /// 라벨프린트를 위한함수
        /// </summary>
        private void OutPrint()
        {
            XtraReport1 report = new XtraReport1();
            
            try
            {
                int voidNum = CheckValue(panelControl1);
                if(voidNum == 0)
                {
                    string barCodeNum = PInformationUpdate();   //생산정보 DB업뎃

                    report.Parameters["productName"].Value = nameTextEdit.Text;
                    report.Parameters["productCode"].Value = codeTextEdit.Text;
                    report.Parameters["productSize"].Value = sizeTextEdit.Text;
                    report.Parameters["productUnit"].Value = unitTextEdit.Text;
                    report.Parameters["productionDate"].Value = ProductionDateDateEdit.Text;
                    report.Parameters["expiryDate"].Value = ExpiryDateDateEdit.Text;
                    report.Parameters["IANumber"].Value = IANumTextEdit.Text;
                    report.Parameters["SMethod"].Value = SMethodTextEdit.Text;
                    report.Parameters["lotNum"].Value = LotNumTextEdit.Text;
                    report.Parameters["barCode"].Value = barCodeNum;
                    report.Print();
                }
                else if(printSuccessFlag == false)
                {
                    printSuccessFlag = true;
                    MessageBox.Show("텍스트에 공백이 있습니다.");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// 생산정보DB 업데이트 함수 제품 일련번호 자동 생성후 바코드 텍스트를 위해 넘겨주기
        /// </summary>
        /// <returns></returns>
        private string PInformationUpdate()
        {
            string productionNumber = "";
            int nextProductionNumber = 0;
            //BD저장용 바코드
            string barCodeNum = "";
            //BD넘겨주기위한 바코드
            string getBarCodeNum = "";

            try
            {
                string sql = "select productionNumber from PInformation";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    string readNum = "";
                    while (reader.Read())
                    {
                        readNum = reader["productionNumber"].ToString();
                        if(readNum is null || readNum == "")
                        {
                            productionNumber = "00001";
                        }
                        else if (int.Parse(readNum) >= nextProductionNumber)
                        {
                            nextProductionNumber = int.Parse(readNum) + 1;
                            productionNumber = nextProductionNumber.ToString().PadLeft(5, '0');
                        }
                    }
                    reader.Close();
                    reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        productionNumber = "00001";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                string sql = "insert into PInformation (productCode, productionDate, expiryDate, productionNumber, lotNum, barCode, productionTime, labelStatus) " +   // * 수정
                             "values (@code, @pDate, @eDate, @pNumber, @lotNum, @barCode, GETDATE(), @labelStatus)";   //GETDATE()는 파라미터로 못받음

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // 일자 변환
                    string productionDateTime = CalcDay("제조일자");
                    string expiryDateTime = CalcDay("유효기간");

                    getBarCodeNum = "01" + gtin + "10" + LotNumTextEdit.Text + "#11" + productionDateTime + 
                                 "17" + expiryDateTime + "21" + productionNumber;
                    barCodeNum = "01" + gtin + "10" + LotNumTextEdit.Text + "11" + productionDateTime +
                                 "17" + expiryDateTime + "21" + productionNumber;

                    cmd.Parameters.AddWithValue("@code", codeTextEdit.Text);
                    cmd.Parameters.AddWithValue("@pDate", ProductionDateDateEdit.Text);
                    cmd.Parameters.AddWithValue("@eDate", ExpiryDateDateEdit.Text);
                    cmd.Parameters.AddWithValue("@pNumber", productionNumber);  //일련번호
                    cmd.Parameters.AddWithValue("@lotNum", LotNumTextEdit.Text); // 제조번호
                    cmd.Parameters.AddWithValue("@barCode", barCodeNum);    //바코드 번호
                    cmd.Parameters.AddWithValue("@labelStatus", "라벨발행");
                    //cmd.Parameters.AddWithValue("@productionTime", "CONVERT(VARCHAR(30), GETDATE(), 120)");


                    int num = cmd.ExecuteNonQuery();
                    if (num != 0 && printSuccessFlag == false)
                    {
                        printSuccessFlag = true;
                        MessageBox.Show("커밋 완료!");
                    }//  if (cmd.ExecuteNonQuery() != 0)
                    else if(printSuccessFlag == false)
                    {
                        printSuccessFlag = true;
                        MessageBox.Show($"커밋 실패! {num}");
                    }// else (cmd.ExecuteNonQuery() != 0)
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return getBarCodeNum;
        }//end of PInformationUpdate()
        /// <summary>
        /// 일자 계산해서 텍스트에 뿌려주기 위한 함수 yearNum => 제품 요효기간
        /// </summary>
        /// <param name="yearNum"></param>
        private string CalcDay(string s)
        {

            DateTime dateTime;
            string dateFormat = "yyyy-MM-dd"; // 날짜 형식 지정
            CultureInfo culture = CultureInfo.InvariantCulture;
            string formattedDate = "";

            try
            {
                string inputDate = (s == "제조일자") ? ProductionDateDateEdit.Text : ExpiryDateDateEdit.Text;

                // 입력값이 빈 문자열인지 확인
                if (string.IsNullOrWhiteSpace(inputDate))
                {
                    MessageBox.Show("날짜 입력값이 비어 있습니다.");
                    return formattedDate;
                }

                // 날짜 변환 시도 (예외 발생 방지)
                if (DateTime.TryParseExact(inputDate, dateFormat, culture, DateTimeStyles.None, out dateTime))
                {
                    formattedDate = dateTime.ToString("yy/MM/dd" /* 출력 포맷형식 */);
                    formattedDate = formattedDate.Replace("-", "");
                    //MessageBox.Show($"{formattedDate}");
                }
                else
                {
                    MessageBox.Show($"유효하지 않은 날짜 형식: {inputDate}\n올바른 형식: {dateFormat}");
                    return formattedDate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return formattedDate;
            }

            return formattedDate;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                printSuccessFlag = false;
                for (int printCount = int.Parse(PQuantityTextEdit.Text); printCount > 0; printCount--)
                {
                    OutPrint();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            conn.Open();

            DataTableReset();
            SelectTable();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0) // 유효한 행인지 확인
            {
                //primaryKey = gridView1.GetRowCellValue(e.RowHandle, "productCodeField").ToString();

                nameTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "productNameField").ToString(); ;
                codeTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "productCodeField").ToString(); ;
                sizeTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "productSizeField").ToString(); ;
                unitTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "PUnitField").ToString(); ;
                IANumTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "IANumberField").ToString(); ;
                SMethodTextEdit.Text = gridView1.GetRowCellValue(e.RowHandle, "SMethodField").ToString(); ;

                // 바코드텍스트를 위한 제품 GTIN 추출
                gtin = gridView1.GetRowCellValue(e.RowHandle, "productGTINField").ToString();
            }
        }

        private void ProductionDateTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void MainFormButton_Click(object sender, EventArgs e)
        {

        }

        private void LotNumTextEdit_KeyPress(object sender, KeyPressEventArgs e)
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
            else if (char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                return;
            }
            e.Handled = true; // 영,한글문자 외에는 입력 불가능
        }
    }
}