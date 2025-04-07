pageFlag = true;    //품목마스터 폼 중복방지
            panelControl3.Visible = false;
            //StoreMainControls();
            this.panelControl2.Controls.Clear();
            XtraForm2 xtraForm2 = new XtraForm2();
            xtraForm2.TopLevel = false;
            this.Controls.Add(xtraForm2);
            xtraForm2.Parent = this.panelControl2;
            xtraForm2.ControlBox = false;
            xtraForm2.Show();
