using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_information_production_Project
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //try
            //{
            //    Application.Run(new Form1());

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
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
        }
    }
}
