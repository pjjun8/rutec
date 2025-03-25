 /// <summary>
        /// 그리드뷰컨트롤러에 체크박스 기능 사용하기(체크한 행 정보 임시저장)
        /// 체크된 행의 PK값 넘겨줌
        /// </summary>
        /// <returns></returns>
        private List<object> GetSelectedCheckBox()
        {
            try
            {
                //선택된 아이템 리스트로 가져오기
                List<object> list = new List<object>();
                List<object> KeyList = new List<object>();
                foreach (int id in gridView1.GetSelectedRows())
                {
                    list.Add(gridView1.GetRow(id) as object);
                    KeyList.Add(gridView1.GetRowCellValue(id, "productCodeField") as object);
                }
                //gridControl1.DataSource = list; 테스트용 문장
                return KeyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
