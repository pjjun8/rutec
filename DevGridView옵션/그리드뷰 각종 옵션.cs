/// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            gridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정
        }

=================================================================================================
=================================================================================================

//DevExpress의 GridView에서 New Item Row를 활성화하면 사용자가 직접 추가할 수 있는 빈 줄을 표시(무조건 1줄 생김)
gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

=================================================================================================
=================================================================================================
🚀 적용 후 동작
✅ 그리드뷰 마지막에 빈 행이 생김
✅ 사용자가 입력하면 행이 추가됨
✅ 빈 행이 입력되지 않으면 자동 삭제됨 (추가적인 로직 필요 시 RowUpdated 이벤트 활용 가능)

이렇게 하면 속성 설정으로만 행 추가가 가능하도록 유지하면서, 데이터도 유지할 수 있어! 😊
// 전역 리스트 (AllowNew 가능하도록 설정)
private BindingList<ExcelData> insertlist;

private void Form1_Load(object sender, EventArgs e)
{
    insertlist = new BindingList<ExcelData>() { AllowNew = true }; // ✅ AllowNew 설정
    gridControl1.DataSource = insertlist;
    gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom; // ✅ 속성 설정
    int focusedRow = advBandedGridView1.FocusedRowHandle;
    advBandedGridView1.FocusedRowHandle = -1; // 포커스 해제
    advBandedGridView1.FocusedRowHandle = focusedRow;   //포커스 재설정
        
}
//BindingList<ExcelData>의 AllowNew = true 설정 → 행 추가 가능
//NewItemRowPosition = Bottom → 마지막 줄에 빈 행 자동 생성
// 그리드뷰에서 포커스 나가야 새 행 추가됨!!!!!!!!!!!!!!!!!!!!!!!!
=================================================================================================
=================================================================================================
