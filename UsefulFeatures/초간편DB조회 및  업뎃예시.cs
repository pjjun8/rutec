using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        DB db;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB(this);
                db.DBConnection();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DBConnectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = db.DBReadSelect();
                gridControl2.DataSource = db.DBReadSelect2();
                gridControl3.DataSource = db.DBAdapterSelect();
                gridControl4.DataSource = db.DBAdapterSelect2();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
==================================================================
==================================================================

using DevExpress.Utils.About;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1
{
    /// <summary>
    /// 데이터 모델 클래스 (바인딩할 때 사용)
    /// </summary>
    public class ProductInfo
    {
        public string ProductCode { get; set; }
        public string ProductionDate { get; set; }
        public string ExpiryDate { get; set; }
        public string ProductionNumber { get; set; }
        public string LotNum { get; set; }
        public string BarCode { get; set; }
        public string ProductionTime { get; set; }
        public string LabelStatus { get; set; }
    }
    public class DB
    {
        /// <summary>
        /// DB접속을 위한 전역객체 
        /// </summary>
        public SqlConnection conn;
        public SqlCommand cmd;

        Form1 form1;
        public DB()
        {
        }
        public DB(Form1 form1)
        {
            this.form1 = form1;
        }
        /// <summary>
        /// DB연결실행 함수
        /// </summary>
        public void DBConnection()
        {
            /// <summary>
            /// DB접속을 위한 DB정보 
            /// </summary>
            string uid = "sa";
            string password = "fnxpr2020@)@)";
            string database = "SANGWON";
            string server = "61.100.180.71,14337";
            string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }
        /// <summary>
        /// SqlDataReader로 DB조회
        /// </summary>
        /// <returns></returns>
        public DataTable DBReadSelect()
        {
            using (DataTable dataTable = new DataTable())
            {
                try
                {
                    string sql = "select * from PInformation";
                    using (cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        dataTable.Load(read);
                    }
                    //MessageBox.Show($"{dataTable}");
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return dataTable;
            }
        }
        public DataTable DBReadSelect2()
        {
            using (DataTable dataTable = new DataTable())
            {
                try
                {
                    string sql = "select * from product";
                    using (cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        dataTable.Load(read);
                    }
                    //MessageBox.Show($"{dataTable}");
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return dataTable;
            }
        }
        /// <summary>
        /// SqlDataAdapter로 DB조회
        /// </summary>
        /// <returns></returns>
        public DataTable DBAdapterSelect()
        {
            using (DataTable dataTable = new DataTable())
            {
                try
                {
                    string sql = "select * from PInformation";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return dataTable;
            }
        }
        public DataTable DBAdapterSelect2()
        {
            using (DataTable dataTable = new DataTable())
            {
                try
                {
                    string sql = "select * from product";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return dataTable;
            }
        }
    }
}
================================================================================
================================================================================
try
{
    string sql = @"
        INSERT INTO PInformation 
        (productCode, productionDate, expiryDate, productionNumber, lotNum, barCode, productionTime, labelStatus) 
        VALUES (@code, @pDate, @eDate, @pNumber, @lotNum, @barCode, GETDATE(), @labelStatus)";

    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        // 일자 변환
        string productionDateTime = CalcDay("제조일자");
        string expiryDateTime = CalcDay("유효기간");

        getBarCodeNum = $"01{gtin}10{LotNumTextEdit.Text}#11{productionDateTime}17{expiryDateTime}21{productionNumber}";
        barCodeNum = $"01{gtin}10{LotNumTextEdit.Text}11{productionDateTime}17{expiryDateTime}21{productionNumber}";

        cmd.Parameters.Add("@code", SqlDbType.NVarChar, 50).Value = codeTextEdit.Text;
        cmd.Parameters.Add("@pDate", SqlDbType.Date).Value = ProductionDateDateEdit.Text;
        cmd.Parameters.Add("@eDate", SqlDbType.Date).Value = ExpiryDateDateEdit.Text;
        cmd.Parameters.Add("@pNumber", SqlDbType.Int).Value = productionNumber;
        cmd.Parameters.Add("@lotNum", SqlDbType.NVarChar, 50).Value = LotNumTextEdit.Text;
        cmd.Parameters.Add("@barCode", SqlDbType.NVarChar, 100).Value = barCodeNum;
        cmd.Parameters.Add("@labelStatus", SqlDbType.NVarChar, 20).Value = "라벨발행";

        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            printSuccessFlag = true;
            MessageBox.Show("커밋 완료!");
        }
        else
        {
            MessageBox.Show("커밋 실패!");
        }
    }
}
catch (Exception ex)
{
    MessageBox.Show($"DB 저장 중 오류 발생: {ex.Message}");
}
