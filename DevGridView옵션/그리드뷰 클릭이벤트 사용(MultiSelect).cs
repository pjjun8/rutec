        private void advBandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            // 클릭된 셀을 바로 편집 상태로 변경
            //advBandedGridView1.FocusedRowHandle = e.RowHandle;
            //advBandedGridView1.FocusedColumn = e.Column;
            advBandedGridView1.ShowEditor(); // 즉시 편집 모드 진입
            Common.selectLow = e.RowHandle;
        }
