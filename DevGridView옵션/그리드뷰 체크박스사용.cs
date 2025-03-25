//- 디자이너에서 대상 GridControl의 'Run Designer'를 클릭한 뒤에 'Views' 메뉴에서 'OptionSelection' 속성을 선택하고 하위에 속성을 바꿔주자
    - **MultiSelection**: True
    - **MultiSelectionMode**: CheckBoxRowSelect

        private void getSelectedItems()
        {
            //선택된 아이템 리스트로 가져오기
            List<object> list = new List<object>();
            foreach (int id in gridView.GetSelectedRows())
            {
                list.Add(gridView.GetRow(id) as object);
            }
        }
