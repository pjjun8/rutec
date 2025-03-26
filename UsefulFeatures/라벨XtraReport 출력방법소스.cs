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
