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
        
        /// <summary>
        /// 모델명, 수량 가져오는 프로시저 실행
        /// </summary>
        public BindingList<ExcelData> DBSelect(List<string> list)
        {
            try
            {
                //Common.insertlist.Clear();
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
                            //Common.insertlist.Add(new ExcelData
                            //{
                            //    ProductLot = lot,
                            //    ModelName = reader["ITEM_TYPE_NAME"].ToString(),
                            //    QTY = reader["PROD_QTY"].ToString(),
                            //});

                            for (int i = 0; i < Common.insertlist.Count; i++)
                            {
                                if (Common.insertlist[i].ProductLot == lot)
                                {
                                    Common.insertlist[i].ProductLot = lot;
                                    Common.insertlist[i].ModelName = reader["ITEM_TYPE_NAME"].ToString();
                                    Common.insertlist[i].QTY = reader["PROD_QTY"].ToString();
                                }
                            }

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
            
            return Common.insertlist;
        }

    }

}
