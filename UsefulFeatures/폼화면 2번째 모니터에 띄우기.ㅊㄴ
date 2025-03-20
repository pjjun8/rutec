try
            {
                Form1 frm = new Form1();
                Rectangle secondaryScreenBounds = Screen.AllScreens[1].WorkingArea;

                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point((int)(secondaryScreenBounds.Right * 0.58), (secondaryScreenBounds.Bottom/5));

                Application.Run(frm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
