using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Product_information_production_Project
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
            xrBarCode1.CanGrow = false;  // 자동 크기 확장 방지
            xrBarCode1.CanShrink = false; // 자동 크기 축소 방지
            xrBarCode1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;  // 중앙 정렬
        }

    }
}
