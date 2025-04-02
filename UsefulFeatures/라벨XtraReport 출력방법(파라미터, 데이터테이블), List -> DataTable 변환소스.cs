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
==================================================================================
==================================================================================
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
                MessageBox.Show("정확한 입력값이 아닙니다.");
                //MessageBox.Show(ex.ToString());
            }
        }
        private void LabelOutPutButton_Click(object sender, EventArgs e)
        {
            PrintParametersLabel();
        }
