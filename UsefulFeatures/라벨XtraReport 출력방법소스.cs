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
            //dt.Load(list);
            foreach (var item in list)
            {
                dt.Rows.Add(item.ProductLot, item.QTY, item.ModelName);
                
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
            rpt.Print();

            //DataTable 일떄 형식
            //XtraReport_table rpt = new XtraReport_table();

            //rpt.DataSource = gridControl1.DataSource;
            //rpt.Print();
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
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
                for (int i = 0; gridView1.RowCount - 1 > i; i++)
                {

                    xtraReport1.Parameters["LOT"].Value = gridView1.GetRowCellValue(i, "ProductLot").ToString();
                    xtraReport1.Parameters["QTY"].Value = gridView1.GetRowCellValue(i, "QTY").ToString();
                    xtraReport1.Parameters["ModelName"].Value = gridView1.GetRowCellValue(i, "ModelName").ToString();
                    xtraReport1.Parameters["Holder"].Value = gridView1.GetRowCellValue(i, "Holder").ToString();
                    xtraReport1.Parameters["Up_JIG"].Value = gridView1.GetRowCellValue(i, "Up_JIG").ToString();
                    xtraReport1.Parameters["Down_JIG"].Value = gridView1.GetRowCellValue(i, "Down_JIG").ToString();
                    xtraReport1.Parameters["Memo"].Value = gridView1.GetRowCellValue(i, "Memo").ToString();

                    xtraReport1.CreateDocument();
                    xtraReport1.Print();
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


