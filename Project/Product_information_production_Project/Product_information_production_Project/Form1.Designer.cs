
namespace Product_information_production_Project
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.productNameName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productGTINName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productSizeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IANumberName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SMethodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productionStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nameText = new DevExpress.XtraEditors.TextEdit();
            this.codeText = new DevExpress.XtraEditors.TextEdit();
            this.gtinText = new DevExpress.XtraEditors.TextEdit();
            this.sizeText = new DevExpress.XtraEditors.TextEdit();
            this.iaNumberText = new DevExpress.XtraEditors.TextEdit();
            this.sMethodText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.ProductInsertButton = new DevExpress.XtraEditors.SimpleButton();
            this.DeleteButton = new DevExpress.XtraEditors.SimpleButton();
            this.searchButton = new DevExpress.XtraEditors.SimpleButton();
            this.UpdateButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelClearButton = new DevExpress.XtraEditors.SimpleButton();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.productionCheckBox = new System.Windows.Forms.CheckBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.품목마스터ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.품목마스터ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.라벨출력ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.제품생산조회ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gtinText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iaNumberText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMethodText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(5, 5);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(960, 592);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseClick);
            this.gridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.productNameName,
            this.productCodeName,
            this.productGTINName,
            this.productSizeName,
            this.PUnitName,
            this.IANumberName,
            this.SMethodName,
            this.productionStatusName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.PUnitName, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.productCodeName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle_1);
            // 
            // productNameName
            // 
            this.productNameName.Caption = "제품이름";
            this.productNameName.FieldName = "productNameField";
            this.productNameName.Name = "productNameName";
            this.productNameName.Visible = true;
            this.productNameName.VisibleIndex = 2;
            // 
            // productCodeName
            // 
            this.productCodeName.Caption = "제품코드";
            this.productCodeName.FieldName = "productCodeField";
            this.productCodeName.Name = "productCodeName";
            this.productCodeName.Visible = true;
            this.productCodeName.VisibleIndex = 1;
            // 
            // productGTINName
            // 
            this.productGTINName.Caption = "제품GTIN";
            this.productGTINName.FieldName = "productGTINField";
            this.productGTINName.Name = "productGTINName";
            this.productGTINName.Visible = true;
            this.productGTINName.VisibleIndex = 3;
            // 
            // productSizeName
            // 
            this.productSizeName.Caption = "제품SIZE";
            this.productSizeName.FieldName = "productSizeField";
            this.productSizeName.Name = "productSizeName";
            this.productSizeName.Visible = true;
            this.productSizeName.VisibleIndex = 4;
            // 
            // PUnitName
            // 
            this.PUnitName.Caption = "포장단위";
            this.PUnitName.FieldName = "PUnitField";
            this.PUnitName.Name = "PUnitName";
            this.PUnitName.Visible = true;
            this.PUnitName.VisibleIndex = 5;
            // 
            // IANumberName
            // 
            this.IANumberName.Caption = "품목허가번호";
            this.IANumberName.FieldName = "IANumberField";
            this.IANumberName.Name = "IANumberName";
            this.IANumberName.Visible = true;
            this.IANumberName.VisibleIndex = 6;
            // 
            // SMethodName
            // 
            this.SMethodName.Caption = "보관방법";
            this.SMethodName.FieldName = "SMethodField";
            this.SMethodName.Name = "SMethodName";
            this.SMethodName.Visible = true;
            this.SMethodName.VisibleIndex = 7;
            // 
            // productionStatusName
            // 
            this.productionStatusName.Caption = "생산여부";
            this.productionStatusName.FieldName = "productionStatusField";
            this.productionStatusName.Name = "productionStatusName";
            this.productionStatusName.Visible = true;
            this.productionStatusName.VisibleIndex = 8;
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(118, 81);
            this.nameText.Name = "nameText";
            this.nameText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameText.Properties.Appearance.Options.UseFont = true;
            this.nameText.Size = new System.Drawing.Size(121, 32);
            this.nameText.TabIndex = 3;
            this.nameText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameText_KeyPress);
            // 
            // codeText
            // 
            this.codeText.EditValue = "";
            this.codeText.Location = new System.Drawing.Point(118, 43);
            this.codeText.Name = "codeText";
            this.codeText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeText.Properties.Appearance.Options.UseFont = true;
            this.codeText.Properties.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codeText_Properties_KeyPress);
            this.codeText.Size = new System.Drawing.Size(121, 32);
            this.codeText.TabIndex = 2;
            this.codeText.EditValueChanged += new System.EventHandler(this.codeText_EditValueChanged);
            // 
            // gtinText
            // 
            this.gtinText.Location = new System.Drawing.Point(118, 119);
            this.gtinText.Name = "gtinText";
            this.gtinText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtinText.Properties.Appearance.Options.UseFont = true;
            this.gtinText.Properties.MaxLength = 14;
            this.gtinText.Size = new System.Drawing.Size(121, 32);
            this.gtinText.TabIndex = 4;
            this.gtinText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gtinText_KeyPress);
            // 
            // sizeText
            // 
            this.sizeText.Location = new System.Drawing.Point(118, 158);
            this.sizeText.Name = "sizeText";
            this.sizeText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizeText.Properties.Appearance.Options.UseFont = true;
            this.sizeText.Size = new System.Drawing.Size(121, 32);
            this.sizeText.TabIndex = 5;
            this.sizeText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sizeText_KeyPress);
            // 
            // iaNumberText
            // 
            this.iaNumberText.Location = new System.Drawing.Point(118, 234);
            this.iaNumberText.Name = "iaNumberText";
            this.iaNumberText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iaNumberText.Properties.Appearance.Options.UseFont = true;
            this.iaNumberText.Size = new System.Drawing.Size(121, 32);
            this.iaNumberText.TabIndex = 7;
            // 
            // sMethodText
            // 
            this.sMethodText.Location = new System.Drawing.Point(118, 272);
            this.sMethodText.Name = "sMethodText";
            this.sMethodText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sMethodText.Properties.Appearance.Options.UseFont = true;
            this.sMethodText.Size = new System.Drawing.Size(121, 32);
            this.sMethodText.TabIndex = 8;
            this.sMethodText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sMethodText_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(54, 89);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 19);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "제품명 : ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(40, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 19);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "제품코드 : ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(25, 127);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(87, 19);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "제품 GTIN : ";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(30, 166);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 19);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "제품 SIZE : ";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(40, 204);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 19);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "포장단위 : ";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(12, 242);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(100, 19);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "품목허가번호 : ";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(40, 280);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 19);
            this.labelControl7.TabIndex = 15;
            this.labelControl7.Text = "보관방법 : ";
            // 
            // ProductInsertButton
            // 
            this.ProductInsertButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductInsertButton.Appearance.Options.UseFont = true;
            this.ProductInsertButton.Location = new System.Drawing.Point(5, 425);
            this.ProductInsertButton.Name = "ProductInsertButton";
            this.ProductInsertButton.Size = new System.Drawing.Size(112, 66);
            this.ProductInsertButton.TabIndex = 16;
            this.ProductInsertButton.Text = "품목정보등록";
            this.ProductInsertButton.Click += new System.EventHandler(this.ProductInsertButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Appearance.BackColor = System.Drawing.Color.Brown;
            this.DeleteButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Appearance.Options.UseBackColor = true;
            this.DeleteButton.Appearance.Options.UseFont = true;
            this.DeleteButton.Location = new System.Drawing.Point(113, 4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(102, 33);
            this.DeleteButton.TabIndex = 17;
            this.DeleteButton.Text = "선택행삭제";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Appearance.Options.UseFont = true;
            this.searchButton.Location = new System.Drawing.Point(221, 4);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(102, 32);
            this.searchButton.TabIndex = 18;
            this.searchButton.Text = "품목조회";
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.UpdateButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.Appearance.Options.UseBackColor = true;
            this.UpdateButton.Appearance.Options.UseFont = true;
            this.UpdateButton.Location = new System.Drawing.Point(5, 4);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(102, 33);
            this.UpdateButton.TabIndex = 19;
            this.UpdateButton.Text = "선택행수정";
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.PanelClearButton);
            this.panelControl1.Controls.Add(this.calcEdit1);
            this.panelControl1.Controls.Add(this.productionCheckBox);
            this.panelControl1.Controls.Add(this.nameText);
            this.panelControl1.Controls.Add(this.codeText);
            this.panelControl1.Controls.Add(this.gtinText);
            this.panelControl1.Controls.Add(this.ProductInsertButton);
            this.panelControl1.Controls.Add(this.sizeText);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.iaNumberText);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.sMethodText);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Location = new System.Drawing.Point(971, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(256, 592);
            this.panelControl1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "개";
            // 
            // PanelClearButton
            // 
            this.PanelClearButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelClearButton.Appearance.Options.UseFont = true;
            this.PanelClearButton.Location = new System.Drawing.Point(139, 425);
            this.PanelClearButton.Name = "PanelClearButton";
            this.PanelClearButton.Size = new System.Drawing.Size(112, 66);
            this.PanelClearButton.TabIndex = 23;
            this.PanelClearButton.Text = "패널초기화";
            this.PanelClearButton.Click += new System.EventHandler(this.PanelClearButton_Click);
            // 
            // calcEdit1
            // 
            this.calcEdit1.EditValue = new decimal(new int[] {
            123,
            0,
            0,
            0});
            this.calcEdit1.Location = new System.Drawing.Point(118, 205);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Size = new System.Drawing.Size(100, 20);
            this.calcEdit1.TabIndex = 22;
            this.calcEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.calcEdit1_KeyPress);
            // 
            // productionCheckBox
            // 
            this.productionCheckBox.AutoSize = true;
            this.productionCheckBox.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productionCheckBox.Location = new System.Drawing.Point(87, 343);
            this.productionCheckBox.Name = "productionCheckBox";
            this.productionCheckBox.Size = new System.Drawing.Size(95, 29);
            this.productionCheckBox.TabIndex = 20;
            this.productionCheckBox.Text = "생산여부";
            this.productionCheckBox.UseVisualStyleBackColor = true;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Location = new System.Drawing.Point(12, 46);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1232, 602);
            this.panelControl2.TabIndex = 25;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.품목마스터ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(12, 4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Size = new System.Drawing.Size(106, 39);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 품목마스터ToolStripMenuItem
            // 
            this.품목마스터ToolStripMenuItem.AutoSize = false;
            this.품목마스터ToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue;
            this.품목마스터ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.품목마스터ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.품목마스터ToolStripMenuItem1,
            this.라벨출력ToolStripMenuItem,
            this.제품생산조회ToolStripMenuItem});
            this.품목마스터ToolStripMenuItem.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.품목마스터ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.품목마스터ToolStripMenuItem.Name = "품목마스터ToolStripMenuItem";
            this.품목마스터ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.품목마스터ToolStripMenuItem.Size = new System.Drawing.Size(100, 35);
            this.품목마스터ToolStripMenuItem.Text = "메뉴";
            this.품목마스터ToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.품목마스터ToolStripMenuItem_Paint);
            // 
            // 품목마스터ToolStripMenuItem1
            // 
            this.품목마스터ToolStripMenuItem1.Name = "품목마스터ToolStripMenuItem1";
            this.품목마스터ToolStripMenuItem1.Size = new System.Drawing.Size(204, 26);
            this.품목마스터ToolStripMenuItem1.Text = "품목마스터";
            this.품목마스터ToolStripMenuItem1.Click += new System.EventHandler(this.품목마스터ToolStripMenuItem1_Click);
            // 
            // 라벨출력ToolStripMenuItem
            // 
            this.라벨출력ToolStripMenuItem.Name = "라벨출력ToolStripMenuItem";
            this.라벨출력ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.라벨출력ToolStripMenuItem.Text = "라벨출력";
            this.라벨출력ToolStripMenuItem.Click += new System.EventHandler(this.라벨출력ToolStripMenuItem_Click);
            // 
            // 제품생산조회ToolStripMenuItem
            // 
            this.제품생산조회ToolStripMenuItem.Name = "제품생산조회ToolStripMenuItem";
            this.제품생산조회ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.제품생산조회ToolStripMenuItem.Text = "검수 및 생산조회";
            this.제품생산조회ToolStripMenuItem.Click += new System.EventHandler(this.제품생산조회ToolStripMenuItem_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.searchButton);
            this.panelControl3.Controls.Add(this.DeleteButton);
            this.panelControl3.Controls.Add(this.UpdateButton);
            this.panelControl3.Location = new System.Drawing.Point(649, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(328, 43);
            this.panelControl3.TabIndex = 28;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 660);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CRUD_Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gtinText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iaNumberText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMethodText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit nameText;
        private DevExpress.XtraEditors.TextEdit codeText;
        private DevExpress.XtraEditors.TextEdit gtinText;
        private DevExpress.XtraEditors.TextEdit sizeText;
        private DevExpress.XtraEditors.TextEdit iaNumberText;
        private DevExpress.XtraEditors.TextEdit sMethodText;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton ProductInsertButton;
        private DevExpress.XtraEditors.SimpleButton DeleteButton;
        private DevExpress.XtraEditors.SimpleButton searchButton;
        private DevExpress.XtraEditors.SimpleButton UpdateButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private System.Windows.Forms.CheckBox productionCheckBox;
        private DevExpress.XtraEditors.SimpleButton PanelClearButton;
        private DevExpress.XtraGrid.Columns.GridColumn productCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn productNameName;
        private DevExpress.XtraGrid.Columns.GridColumn productGTINName;
        private DevExpress.XtraGrid.Columns.GridColumn productSizeName;
        private DevExpress.XtraGrid.Columns.GridColumn PUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn IANumberName;
        private DevExpress.XtraGrid.Columns.GridColumn SMethodName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn productionStatusName;
        public DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 품목마스터ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 품목마스터ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 라벨출력ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 제품생산조회ToolStripMenuItem;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

