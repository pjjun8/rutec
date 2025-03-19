/// <summary>
        /// 컨트롤 ex(텍스트박스, 패널안의 컨트롤러) 공백 확인 함수
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private int CheckValue(Control control)
        {
            int voidNum = 0;
            foreach (Control c in control.Controls)
            {
                if (c is TextEdit textEdit)
                {
                    if((c.Text is "") || (c.Text is null))
                    {
                        voidNum++;
                    }
                    //textBox.Clear(); // 텍스트 지우기
                }
                else if (c.HasChildren)
                {
                    CheckValue(c); // 자식 컨트롤이 있으면 재귀 호출
                }

            }
            return voidNum;
        }
