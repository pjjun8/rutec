private void advBandedGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //입력된 값이 없는지 확인
            ExcelData row = e.Row as ExcelData;
            if (new[] { row.ProductLot, row.ModelName, row.QTY, row.Holder, row.Memo, row.UpJIG_LOT,
                        row.UpJIG_LOT2, row.DownJIG_LOT, row.DownJIG_LOT2}.All(string.IsNullOrWhiteSpace))  //한번에 값 확인 방법
            {
                
                Common.insertlist.Remove(row); // 입력이 없으면 삭제
                advBandedGridView1.RefreshData();
            }
        }
