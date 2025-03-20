
namespace Product_information_production_Project
{
    partial class XtraForm1
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.productCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productNameName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productGTINName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productSizeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IANumberName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SMethodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionDateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.expiryDateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionNumberName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PQuantityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lotNumName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PrintButton = new System.Windows.Forms.Button();
            this.PQuantityTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ExpiryDateDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.ProductionDateDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.LotNumTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.SMethodTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.IANumTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.unitTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.sizeTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.codeTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.nameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sharedDictionaryStorage1 = new DevExpress.XtraSpellChecker.SharedDictionaryStorage(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PQuantityTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpiryDateDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpiryDateDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionDateDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionDateDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LotNumTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SMethodTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IANumTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(926, 562);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.productCodeName,
            this.productNameName,
            this.productGTINName,
            this.productSizeName,
            this.PUnitName,
            this.IANumberName,
            this.SMethodName,
            this.productionStatusName,
            this.productionDateName,
            this.expiryDateName,
            this.productionNumberName,
            this.PQuantityName,
            this.lotNumName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.PUnitName, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.productCodeName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // productCodeName
            // 
            this.productCodeName.Caption = "제품코드";
            this.productCodeName.FieldName = "productCodeField";
            this.productCodeName.Name = "productCodeName";
            this.productCodeName.Visible = true;
            this.productCodeName.VisibleIndex = 0;
            // 
            // productNameName
            // 
            this.productNameName.Caption = "제품이름";
            this.productNameName.FieldName = "productNameField";
            this.productNameName.Name = "productNameName";
            this.productNameName.Visible = true;
            this.productNameName.VisibleIndex = 1;
            // 
            // productGTINName
            // 
            this.productGTINName.Caption = "제품GTIN";
            this.productGTINName.FieldName = "productGTINField";
            this.productGTINName.Name = "productGTINName";
            this.productGTINName.Visible = true;
            this.productGTINName.VisibleIndex = 2;
            // 
            // productSizeName
            // 
            this.productSizeName.Caption = "제품SIZE";
            this.productSizeName.FieldName = "productSizeField";
            this.productSizeName.Name = "productSizeName";
            this.productSizeName.Visible = true;
            this.productSizeName.VisibleIndex = 3;
            // 
            // PUnitName
            // 
            this.PUnitName.Caption = "포장단위";
            this.PUnitName.FieldName = "PUnitField";
            this.PUnitName.Name = "PUnitName";
            this.PUnitName.Visible = true;
            this.PUnitName.VisibleIndex = 4;
            // 
            // IANumberName
            // 
            this.IANumberName.Caption = "품목허가번호";
            this.IANumberName.FieldName = "IANumberField";
            this.IANumberName.Name = "IANumberName";
            this.IANumberName.Visible = true;
            this.IANumberName.VisibleIndex = 5;
            // 
            // SMethodName
            // 
            this.SMethodName.Caption = "보관방법";
            this.SMethodName.FieldName = "SMethodField";
            this.SMethodName.Name = "SMethodName";
            this.SMethodName.Visible = true;
            this.SMethodName.VisibleIndex = 6;
            // 
            // productionStatusName
            // 
            this.productionStatusName.Caption = "생산여부";
            this.productionStatusName.FieldName = "productionStatusField";
            this.productionStatusName.Name = "productionStatusName";
            this.productionStatusName.Visible = true;
            this.productionStatusName.VisibleIndex = 7;
            // 
            // productionDateName
            // 
            this.productionDateName.Caption = "제조일자";
            this.productionDateName.FieldName = "productionDateField";
            this.productionDateName.Name = "productionDateName";
            // 
            // expiryDateName
            // 
            this.expiryDateName.Caption = "유효기간";
            this.expiryDateName.FieldName = "expiryDateField";
            this.expiryDateName.Name = "expiryDateName";
            // 
            // productionNumberName
            // 
            this.productionNumberName.Caption = "생산이력";
            this.productionNumberName.FieldName = "productionNumberField";
            this.productionNumberName.Name = "productionNumberName";
            // 
            // PQuantityName
            // 
            this.PQuantityName.Caption = "생산수량";
            this.PQuantityName.FieldName = "PQuantityField";
            this.PQuantityName.Name = "PQuantityName";
            // 
            // lotNumName
            // 
            this.lotNumName.Caption = "로트번호";
            this.lotNumName.FieldName = "lotNumField";
            this.lotNumName.Name = "lotNumName";
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(55, 444);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(139, 74);
            this.PrintButton.TabIndex = 3;
            this.PrintButton.Text = "발행";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // PQuantityTextEdit
            // 
            this.PQuantityTextEdit.Location = new System.Drawing.Point(95, 382);
            this.PQuantityTextEdit.Name = "PQuantityTextEdit";
            this.PQuantityTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PQuantityTextEdit.Properties.Appearance.Options.UseFont = true;
            this.PQuantityTextEdit.Size = new System.Drawing.Size(146, 26);
            this.PQuantityTextEdit.TabIndex = 109;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ExpiryDateDateEdit);
            this.panelControl1.Controls.Add(this.ProductionDateDateEdit);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.LotNumTextEdit);
            this.panelControl1.Controls.Add(this.SMethodTextEdit);
            this.panelControl1.Controls.Add(this.IANumTextEdit);
            this.panelControl1.Controls.Add(this.unitTextEdit);
            this.panelControl1.Controls.Add(this.sizeTextEdit);
            this.panelControl1.Controls.Add(this.codeTextEdit);
            this.panelControl1.Controls.Add(this.nameTextEdit);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.PrintButton);
            this.panelControl1.Controls.Add(this.PQuantityTextEdit);
            this.panelControl1.Location = new System.Drawing.Point(944, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(260, 562);
            this.panelControl1.TabIndex = 7;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // ExpiryDateDateEdit
            // 
            this.ExpiryDateDateEdit.EditValue = null;
            this.ExpiryDateDateEdit.Location = new System.Drawing.Point(95, 318);
            this.ExpiryDateDateEdit.Name = "ExpiryDateDateEdit";
            this.ExpiryDateDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpiryDateDateEdit.Properties.Appearance.Options.UseFont = true;
            this.ExpiryDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExpiryDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExpiryDateDateEdit.Size = new System.Drawing.Size(146, 26);
            this.ExpiryDateDateEdit.TabIndex = 111;
            // 
            // ProductionDateDateEdit
            // 
            this.ProductionDateDateEdit.EditValue = null;
            this.ProductionDateDateEdit.Location = new System.Drawing.Point(95, 286);
            this.ProductionDateDateEdit.Name = "ProductionDateDateEdit";
            this.ProductionDateDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductionDateDateEdit.Properties.Appearance.Options.UseFont = true;
            this.ProductionDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProductionDateDateEdit.Size = new System.Drawing.Size(146, 26);
            this.ProductionDateDateEdit.TabIndex = 8;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(22, 353);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(67, 19);
            this.labelControl10.TabIndex = 110;
            this.labelControl10.Text = "로트번호 :";
            // 
            // LotNumTextEdit
            // 
            this.LotNumTextEdit.Location = new System.Drawing.Point(95, 350);
            this.LotNumTextEdit.Name = "LotNumTextEdit";
            this.LotNumTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumTextEdit.Properties.Appearance.Options.UseFont = true;
            this.LotNumTextEdit.Properties.MaxLength = 20;
            this.LotNumTextEdit.Size = new System.Drawing.Size(146, 26);
            this.LotNumTextEdit.TabIndex = 108;
            this.LotNumTextEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LotNumTextEdit_KeyPress);
            // 
            // SMethodTextEdit
            // 
            this.SMethodTextEdit.Enabled = false;
            this.SMethodTextEdit.Location = new System.Drawing.Point(124, 214);
            this.SMethodTextEdit.Name = "SMethodTextEdit";
            this.SMethodTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SMethodTextEdit.Properties.Appearance.Options.UseFont = true;
            this.SMethodTextEdit.Size = new System.Drawing.Size(117, 26);
            this.SMethodTextEdit.TabIndex = 105;
            // 
            // IANumTextEdit
            // 
            this.IANumTextEdit.Enabled = false;
            this.IANumTextEdit.Location = new System.Drawing.Point(124, 182);
            this.IANumTextEdit.Name = "IANumTextEdit";
            this.IANumTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IANumTextEdit.Properties.Appearance.Options.UseFont = true;
            this.IANumTextEdit.Size = new System.Drawing.Size(117, 26);
            this.IANumTextEdit.TabIndex = 104;
            // 
            // unitTextEdit
            // 
            this.unitTextEdit.Enabled = false;
            this.unitTextEdit.Location = new System.Drawing.Point(124, 150);
            this.unitTextEdit.Name = "unitTextEdit";
            this.unitTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unitTextEdit.Properties.Appearance.Options.UseFont = true;
            this.unitTextEdit.Size = new System.Drawing.Size(117, 26);
            this.unitTextEdit.TabIndex = 103;
            // 
            // sizeTextEdit
            // 
            this.sizeTextEdit.Enabled = false;
            this.sizeTextEdit.Location = new System.Drawing.Point(124, 118);
            this.sizeTextEdit.Name = "sizeTextEdit";
            this.sizeTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizeTextEdit.Properties.Appearance.Options.UseFont = true;
            this.sizeTextEdit.Size = new System.Drawing.Size(117, 26);
            this.sizeTextEdit.TabIndex = 102;
            // 
            // codeTextEdit
            // 
            this.codeTextEdit.Enabled = false;
            this.codeTextEdit.Location = new System.Drawing.Point(124, 86);
            this.codeTextEdit.Name = "codeTextEdit";
            this.codeTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextEdit.Properties.Appearance.Options.UseFont = true;
            this.codeTextEdit.Size = new System.Drawing.Size(117, 26);
            this.codeTextEdit.TabIndex = 101;
            // 
            // nameTextEdit
            // 
            this.nameTextEdit.Enabled = false;
            this.nameTextEdit.Location = new System.Drawing.Point(124, 54);
            this.nameTextEdit.Name = "nameTextEdit";
            this.nameTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextEdit.Properties.Appearance.Options.UseFont = true;
            this.nameTextEdit.Size = new System.Drawing.Size(117, 26);
            this.nameTextEdit.TabIndex = 100;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(26, 217);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(92, 19);
            this.labelControl9.TabIndex = 15;
            this.labelControl9.Text = "보관방법      :";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(23, 185);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(95, 19);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "품목허가번호 :";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(26, 153);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(92, 19);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "제품단위      :";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(26, 121);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(92, 19);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "제품SIZE     :";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(26, 89);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(92, 19);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "제품코드      :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(25, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(93, 19);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "제품명         :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(22, 385);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 19);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "생산수량 :";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(22, 321);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 19);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "유효기간 :";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(22, 289);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 19);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "제조일자 :";
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1216, 586);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "XtraForm1";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PQuantityTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpiryDateDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpiryDateDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionDateDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionDateDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LotNumTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SMethodTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IANumTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn productNameName;
        private DevExpress.XtraGrid.Columns.GridColumn productCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn productGTINName;
        private DevExpress.XtraGrid.Columns.GridColumn productSizeName;
        private DevExpress.XtraGrid.Columns.GridColumn PUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn IANumberName;
        private DevExpress.XtraGrid.Columns.GridColumn SMethodName;
        private DevExpress.XtraGrid.Columns.GridColumn productionStatusName;
        private System.Windows.Forms.Button PrintButton;
        private DevExpress.XtraGrid.Columns.GridColumn productionDateName;
        private DevExpress.XtraGrid.Columns.GridColumn expiryDateName;
        private DevExpress.XtraGrid.Columns.GridColumn productionNumberName;
        private DevExpress.XtraGrid.Columns.GridColumn PQuantityName;
        private DevExpress.XtraEditors.TextEdit PQuantityTextEdit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit nameTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit SMethodTextEdit;
        private DevExpress.XtraEditors.TextEdit IANumTextEdit;
        private DevExpress.XtraEditors.TextEdit unitTextEdit;
        private DevExpress.XtraEditors.TextEdit sizeTextEdit;
        private DevExpress.XtraEditors.TextEdit codeTextEdit;
        private DevExpress.XtraGrid.Columns.GridColumn lotNumName;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit LotNumTextEdit;
        private DevExpress.XtraEditors.DateEdit ProductionDateDateEdit;
        private DevExpress.XtraEditors.DateEdit ExpiryDateDateEdit;
        private DevExpress.XtraSpellChecker.SharedDictionaryStorage sharedDictionaryStorage1;
    }
}