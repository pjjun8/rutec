        /// <summary>
        /// DB접속을 위한 전역지정
        /// </summary>
        public static string uid = "sa";
        public static string password = "fnxpr2020@)@)";
        public static string database = "SANGWON";
        public static string server = "61.100.180.71,14337";
        string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt = new DataTable();

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            conn.Open();

            DataTableReset();
            SelectTable();
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
                //MessageBox.Show(ex.ToString());
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
            //cmd.ExecuteNonQuery();
        }

        // DB 업데이트예시 코드
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
