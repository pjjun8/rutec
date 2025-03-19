/// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            gridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정
        }
