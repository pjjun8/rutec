
namespace Product_information_production_Project
{
    partial class XtraForm2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.productCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productDateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.expiryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionNumberName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lotNumName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionTimeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateSelectionButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ProductionTimeEndDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.ProductionTimeStartDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.TCPConnectButton = new DevExpress.XtraEditors.SimpleButton();
            this.IPTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.TCPDisConnectButton = new DevExpress.XtraEditors.SimpleButton();
            this.ScannerOnButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.PortTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.testTextEdit = new DevExpress.XtraEditors.MemoEdit();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.HandScannerTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.HandScannerButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeEndDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeEndDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeStartDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeStartDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IPTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandScannerTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(12, 87);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(884, 487);
            this.gridControl1.TabIndex = 15;
            this.gridControl1.TabStop = false;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.productCodeName,
            this.productDateName,
            this.expiryName,
            this.productionNumberName,
            this.lotNumName,
            this.barCodeName,
            this.productionTimeName,
            this.labelStatusName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.lotNumName, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.productCodeName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // productCodeName
            // 
            this.productCodeName.Caption = "제품코드";
            this.productCodeName.FieldName = "productCodeField";
            this.productCodeName.Name = "productCodeName";
            this.productCodeName.Visible = true;
            this.productCodeName.VisibleIndex = 0;
            // 
            // productDateName
            // 
            this.productDateName.Caption = "제조일자";
            this.productDateName.FieldName = "productDateField";
            this.productDateName.Name = "productDateName";
            this.productDateName.Visible = true;
            this.productDateName.VisibleIndex = 1;
            // 
            // expiryName
            // 
            this.expiryName.Caption = "유효기간";
            this.expiryName.FieldName = "expiryField";
            this.expiryName.Name = "expiryName";
            this.expiryName.Visible = true;
            this.expiryName.VisibleIndex = 2;
            // 
            // productionNumberName
            // 
            this.productionNumberName.Caption = "일련번호";
            this.productionNumberName.FieldName = "productionNumberField";
            this.productionNumberName.Name = "productionNumberName";
            this.productionNumberName.Visible = true;
            this.productionNumberName.VisibleIndex = 3;
            // 
            // lotNumName
            // 
            this.lotNumName.Caption = "제조번호";
            this.lotNumName.FieldName = "lotNumField";
            this.lotNumName.Name = "lotNumName";
            this.lotNumName.Visible = true;
            this.lotNumName.VisibleIndex = 4;
            // 
            // barCodeName
            // 
            this.barCodeName.Caption = "바코드";
            this.barCodeName.FieldName = "barCodeField";
            this.barCodeName.Name = "barCodeName";
            this.barCodeName.Visible = true;
            this.barCodeName.VisibleIndex = 5;
            // 
            // productionTimeName
            // 
            this.productionTimeName.Caption = "발행시간";
            this.productionTimeName.FieldName = "productionTimeField";
            this.productionTimeName.Name = "productionTimeName";
            this.productionTimeName.Visible = true;
            this.productionTimeName.VisibleIndex = 6;
            // 
            // labelStatusName
            // 
            this.labelStatusName.Caption = "라벨상태";
            this.labelStatusName.FieldName = "labelStatusField";
            this.labelStatusName.Name = "labelStatusName";
            this.labelStatusName.Visible = true;
            this.labelStatusName.VisibleIndex = 7;
            // 
            // DateSelectionButton
            // 
            this.DateSelectionButton.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateSelectionButton.Appearance.Options.UseFont = true;
            this.DateSelectionButton.Location = new System.Drawing.Point(411, 10);
            this.DateSelectionButton.Name = "DateSelectionButton";
            this.DateSelectionButton.Size = new System.Drawing.Size(100, 45);
            this.DateSelectionButton.TabIndex = 3;
            this.DateSelectionButton.Text = "조회";
            this.DateSelectionButton.Click += new System.EventHandler(this.DateSelectionButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.ProductionTimeEndDateEdit);
            this.panelControl1.Controls.Add(this.ProductionTimeStartDateEdit);
            this.panelControl1.Controls.Add(this.DateSelectionButton);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(562, 67);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(255, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 25);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "~";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(36, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 25);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "발행일자 :";
            // 
            // ProductionTimeEndDateEdit
            // 
            this.ProductionTimeEndDateEdit.EditValue = null;
            this.ProductionTimeEndDateEdit.Location = new System.Drawing.Point(276, 18);
            this.ProductionTimeEndDateEdit.Name = "ProductionTimeEndDateEdit";
            this.ProductionTimeEndDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductionTimeEndDateEdit.Properties.Appearance.Options.UseFont = true;
            this.ProductionTimeEndDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionTimeEndDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionTimeEndDateEdit.Size = new System.Drawing.Size(129, 32);
            this.ProductionTimeEndDateEdit.TabIndex = 2;
            // 
            // ProductionTimeStartDateEdit
            // 
            this.ProductionTimeStartDateEdit.EditValue = null;
            this.ProductionTimeStartDateEdit.Location = new System.Drawing.Point(120, 18);
            this.ProductionTimeStartDateEdit.Name = "ProductionTimeStartDateEdit";
            this.ProductionTimeStartDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductionTimeStartDateEdit.Properties.Appearance.Options.UseFont = true;
            this.ProductionTimeStartDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionTimeStartDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionTimeStartDateEdit.Size = new System.Drawing.Size(129, 32);
            this.ProductionTimeStartDateEdit.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(525, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 45);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "표 엑셀변환";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // TCPConnectButton
            // 
            this.TCPConnectButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TCPConnectButton.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCPConnectButton.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TCPConnectButton.Appearance.Options.UseBackColor = true;
            this.TCPConnectButton.Appearance.Options.UseFont = true;
            this.TCPConnectButton.Appearance.Options.UseForeColor = true;
            this.TCPConnectButton.Location = new System.Drawing.Point(225, 13);
            this.TCPConnectButton.Name = "TCPConnectButton";
            this.TCPConnectButton.Size = new System.Drawing.Size(94, 45);
            this.TCPConnectButton.TabIndex = 6;
            this.TCPConnectButton.Text = "스캐너 연결";
            this.TCPConnectButton.Click += new System.EventHandler(this.TCPConnectButton_Click);
            // 
            // IPTextEdit
            // 
            this.IPTextEdit.EditValue = "192.168.100.50";
            this.IPTextEdit.Location = new System.Drawing.Point(68, 5);
            this.IPTextEdit.Name = "IPTextEdit";
            this.IPTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPTextEdit.Properties.Appearance.Options.UseFont = true;
            this.IPTextEdit.Size = new System.Drawing.Size(136, 26);
            this.IPTextEdit.TabIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.TCPDisConnectButton);
            this.panelControl2.Controls.Add(this.ScannerOnButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.PortTextEdit);
            this.panelControl2.Controls.Add(this.IPTextEdit);
            this.panelControl2.Controls.Add(this.TCPConnectButton);
            this.panelControl2.Location = new System.Drawing.Point(580, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(624, 67);
            this.panelControl2.TabIndex = 11;
            // 
            // TCPDisConnectButton
            // 
            this.TCPDisConnectButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TCPDisConnectButton.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCPDisConnectButton.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TCPDisConnectButton.Appearance.Options.UseBackColor = true;
            this.TCPDisConnectButton.Appearance.Options.UseFont = true;
            this.TCPDisConnectButton.Appearance.Options.UseForeColor = true;
            this.TCPDisConnectButton.Location = new System.Drawing.Point(325, 13);
            this.TCPDisConnectButton.Name = "TCPDisConnectButton";
            this.TCPDisConnectButton.Size = new System.Drawing.Size(94, 45);
            this.TCPDisConnectButton.TabIndex = 7;
            this.TCPDisConnectButton.Text = "스캐너 해제";
            this.TCPDisConnectButton.Click += new System.EventHandler(this.TCPDisConnectButton_Click);
            // 
            // ScannerOnButton
            // 
            this.ScannerOnButton.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.ScannerOnButton.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScannerOnButton.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ScannerOnButton.Appearance.Options.UseBackColor = true;
            this.ScannerOnButton.Appearance.Options.UseFont = true;
            this.ScannerOnButton.Appearance.Options.UseForeColor = true;
            this.ScannerOnButton.Location = new System.Drawing.Point(425, 13);
            this.ScannerOnButton.Name = "ScannerOnButton";
            this.ScannerOnButton.Size = new System.Drawing.Size(94, 45);
            this.ScannerOnButton.TabIndex = 8;
            this.ScannerOnButton.Text = "제품감지";
            this.ScannerOnButton.Click += new System.EventHandler(this.ScannerOnButton_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(3, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(59, 25);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Port  :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 25);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "IP    :";
            // 
            // PortTextEdit
            // 
            this.PortTextEdit.EditValue = "9004";
            this.PortTextEdit.Location = new System.Drawing.Point(68, 37);
            this.PortTextEdit.Name = "PortTextEdit";
            this.PortTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortTextEdit.Properties.Appearance.Options.UseFont = true;
            this.PortTextEdit.Size = new System.Drawing.Size(136, 26);
            this.PortTextEdit.TabIndex = 5;
            // 
            // testTextEdit
            // 
            this.testTextEdit.Location = new System.Drawing.Point(902, 204);
            this.testTextEdit.Name = "testTextEdit";
            this.testTextEdit.Size = new System.Drawing.Size(302, 370);
            this.testTextEdit.TabIndex = 13;
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(1014, 85);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.toggleSwitch1.Properties.OffText = "제품검수";
            this.toggleSwitch1.Properties.OnText = "검수초기화";
            this.toggleSwitch1.Size = new System.Drawing.Size(169, 27);
            this.toggleSwitch1.TabIndex = 14;
            this.toggleSwitch1.Toggled += new System.EventHandler(this.toggleSwitch1_Toggled);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(905, 85);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(101, 25);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "스캐너 모드 :";
            // 
            // HandScannerTextEdit
            // 
            this.HandScannerTextEdit.Location = new System.Drawing.Point(902, 131);
            this.HandScannerTextEdit.Name = "HandScannerTextEdit";
            this.HandScannerTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.HandScannerTextEdit.Properties.Appearance.Options.UseFont = true;
            this.HandScannerTextEdit.Size = new System.Drawing.Size(302, 30);
            this.HandScannerTextEdit.TabIndex = 10;
            this.HandScannerTextEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandScannerTextEdit_KeyPress);
            // 
            // HandScannerButton
            // 
            this.HandScannerButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HandScannerButton.Appearance.Options.UseFont = true;
            this.HandScannerButton.Location = new System.Drawing.Point(1005, 167);
            this.HandScannerButton.Name = "HandScannerButton";
            this.HandScannerButton.Size = new System.Drawing.Size(97, 31);
            this.HandScannerButton.TabIndex = 11;
            this.HandScannerButton.Text = "수동 검사";
            this.HandScannerButton.Click += new System.EventHandler(this.HandScannerButton_Click);
            // 
            // XtraForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1216, 586);
            this.ControlBox = false;
            this.Controls.Add(this.HandScannerButton);
            this.Controls.Add(this.HandScannerTextEdit);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.testTextEdit);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "XtraForm2";
            this.Load += new System.EventHandler(this.XtraForm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeEndDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeEndDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeStartDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionTimeStartDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IPTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HandScannerTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn productDateName;
        private DevExpress.XtraGrid.Columns.GridColumn productCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn expiryName;
        private DevExpress.XtraGrid.Columns.GridColumn productionNumberName;
        private DevExpress.XtraGrid.Columns.GridColumn lotNumName;
        private DevExpress.XtraGrid.Columns.GridColumn barCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn productionTimeName;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton DateSelectionButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit ProductionTimeEndDateEdit;
        private DevExpress.XtraEditors.DateEdit ProductionTimeStartDateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton TCPConnectButton;
        private DevExpress.XtraEditors.TextEdit IPTextEdit;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit PortTextEdit;
        private DevExpress.XtraEditors.SimpleButton ScannerOnButton;
        private DevExpress.XtraGrid.Columns.GridColumn labelStatusName;
        private DevExpress.XtraEditors.SimpleButton TCPDisConnectButton;
        private DevExpress.XtraEditors.MemoEdit testTextEdit;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit HandScannerTextEdit;
        private DevExpress.XtraEditors.SimpleButton HandScannerButton;
    }
}