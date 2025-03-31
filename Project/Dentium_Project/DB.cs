using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.ViewInfo.BaseListBoxViewInfo;

namespace Dentium_Project
{
    class DB
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
            string password = "dentium@0522!";
            string database = "DENTIUM_POP";
            string server = "125.141.193.229";
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
            finally
            {

            }
        }
        static async Task Main()
        {
            Console.WriteLine("작업 시작...");

            // 비동기 메서드 호출 (await 사용)
            DoWorkAsync();

            Console.WriteLine("작업 완료!");
        }

        static async Task DoWorkAsync()
        {
            Console.WriteLine("3초 동안 작업 실행 중...");
            await Task.Delay(3000); // 3초 대기 (비동기)
            Console.WriteLine("작업 끝!");
        }
        /// <summary>
        /// 모델명, 수량 가져오는 프로시저 실행
        /// </summary>
        public BindingList<ExcelData> DBSelect(List<string> list)
        {
            try
            {
                form1.insertlist.Clear();
                foreach (string lot in list)
                {
                    string sql = $"exec parksangwon {lot}";
                    using (cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool valueFlag = false;
                        while (reader.Read())
                        {
                            valueFlag = true;
                            form1.insertlist.Add(new ExcelData
                            {
                                ProductLot = lot,
                                ModelName = reader["ITEM_TYPE_NAME"].ToString(),
                                QTY = reader["PROD_QTY"].ToString(),
                            });
                        }
                        if (!valueFlag)
                        {
                            MessageBox.Show("입력한 LOT의 생산정보가 없습니다.");
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            return form1.insertlist;
        }

    }

}
