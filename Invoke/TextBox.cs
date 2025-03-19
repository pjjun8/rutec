/// <summary>
        /// UI 스레드에서 textBox 업데이트
        /// </summary>
        /// <param name="messge"></param>
        private void UpdateTextBox(string messge)
        {
            if (testTextEdit.InvokeRequired)
            {
                testTextEdit.Invoke(new Action<string>(UpdateTextBox), messge);
            }
            else
            {
                testTextEdit.Text += messge + Environment.NewLine;
            }
        }
