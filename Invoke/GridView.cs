DataTable dt = new DataTable();
/// <summary>
        /// 그리드뷰 invoke
        /// </summary>
        /// <param name="action"></param>
        /////////////////////////////////test//////////////
        private void UpdateUI(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);  // UI 스레드에서 실행
            }
            else
            {
                action();
            }
        }
        
        //UI 컨트롤을 변경하는 코드
        private void UpdateGridView()
        {
            UpdateUI(() =>
            {
                // UI 컨트롤 변경 코드 (그리드뷰 데이터 변경)
                DataTableReset();
                if (ProductionTimeStartDateEdit.EditValue is null && ProductionTimeEndDateEdit.EditValue is null)
                {
                    SelectTable("전체조회");
                }
                else
                {
                    SelectTable("날짜조회");
                }
                
            });
        }
        ///////////////////////////////////////////////////

/// <summary>
        /// dataTable 초기화 함수
        /// </summary>
        private void DataTableReset()
        {
            try
            {
                dt.Clear();
                dt.Columns.Add("productCodeField");
                dt.Columns.Add("productDateField");
                dt.Columns.Add("expiryField");
                dt.Columns.Add("productionNumberField");
                dt.Columns.Add("lotNumField");
                dt.Columns.Add("barCodeField");
                dt.Columns.Add("productionTimeField"); 
                dt.Columns.Add("labelStatusField");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

/// <summary>
        /// DB테이블 조회
        /// </summary>
        private void SelectTable(string value)
        {
            try
            {
                string sql = "";
                if (value == "전체조회")
                {
                    sql = "select * from PInformation";   // * 수정
                }
                else if (value == "날짜조회")
                {
                    sql = "select * from [SANGWON].[dbo].[PInformation] where " +
                          $"productionTime between '{ProductionTimeStartDateEdit.Text}'/*변수*/ and dateadd(day, 1, '{ProductionTimeEndDateEdit.Text}')/*변수*/";   // * 수정
                }

                cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string code, date, exDate, productionNumber, lotNum, barCode, productionTime, labelStatus;
                    while (reader.Read())
                    {
                        code = reader["productCode"].ToString();
                        date = reader["productionDate"].ToString();
                        exDate = reader["expiryDate"].ToString();
                        productionNumber = reader["productionNumber"].ToString();
                        lotNum = reader["lotNum"].ToString();
                        barCode = reader["barCode"].ToString();
                        productionTime = reader["productionTime"].ToString();
                        labelStatus = reader["labelStatus"].ToString();
                        
                        dt.Rows.Add(code, date, exDate, productionNumber, lotNum, barCode, productionTime, labelStatus);
                    }
                    gridControl1.DataSource = dt;
                }
                AutoGridViewCellSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

