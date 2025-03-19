/// <summary>
        /// 패널 내 모든 텍스트박스를 초기화하는 함수
        /// 패널 초기화 메시지 사용시 ClearTextBoxesFlag = true 를 설정해줘야함
        /// 미사용시 ClearTextBoxesFlag = false
        /// </summary>
        /// <param name="control"></param>
        private void ClearTextBoxes(Control control)
        {
            DialogResult result;
            if (ClearTextBoxesFlag == true)
            {
                result = MessageBox.Show("패널을 초기화 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                ClearTextBoxesFlag = false;
            }

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (Control c in control.Controls)
                    {
                        if (c is TextBox textBox)
                        {
                            textBox.Clear(); // 텍스트 지우기
                        }
                        else if (c is CheckBox checkBox)
                        {
                            checkBox.Checked = false;
                        }
                        else if (c is DevExpress.XtraEditors.CalcEdit calcEdit)
                        {
                            calcEdit.EditValue = 0;
                        }


                        else if (c.HasChildren)
                        {
                            ClearTextBoxes(c); // 자식 컨트롤이 있으면 재귀 호출
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if(ClearTextBoxesFlag == true)
                {
                    MessageBox.Show("패널이 초기화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBoxesFlag = false;
                }
            }
            else
            {
                MessageBox.Show("패널 초기화가 취소되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
