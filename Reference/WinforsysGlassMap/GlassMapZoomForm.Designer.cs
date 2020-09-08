namespace WinforsysGlassMap
{
    partial class GlassMapZoomForm
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
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaGrayscale");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlassMapZoomForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiTpnl_Main = new System.Windows.Forms.TableLayoutPanel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.uiPan_GlassMap = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.uiCcmb_LotList = new Winforsys.JKControl.CheckComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.uiLab_DefColor = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.uiRdo_White = new System.Windows.Forms.RadioButton();
            this.uiRdo_Black = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uiLab_PosY = new System.Windows.Forms.Label();
            this.uiLab_PosX = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uiNum_DefectSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiLab_ZoomRate = new System.Windows.Forms.Label();
            this.uiTrackBar = new System.Windows.Forms.TrackBar();
            this.uiBtn_ContourMap = new System.Windows.Forms.Button();
            this.uiBtn_SaveDefectMap = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.uiCcmb_GlassList = new Winforsys.JKControl.CheckComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiTpnl_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiNum_DefectSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTrackBar)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.uiTpnl_Main, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1317, 985);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // uiTpnl_Main
            // 
            this.uiTpnl_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.uiTpnl_Main.ColumnCount = 2;
            this.uiTpnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTpnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 306F));
            this.uiTpnl_Main.Controls.Add(this.fpSpread1, 0, 0);
            this.uiTpnl_Main.Controls.Add(this.uiPan_GlassMap, 0, 0);
            this.uiTpnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTpnl_Main.Location = new System.Drawing.Point(1, 64);
            this.uiTpnl_Main.Margin = new System.Windows.Forms.Padding(0);
            this.uiTpnl_Main.Name = "uiTpnl_Main";
            this.uiTpnl_Main.RowCount = 1;
            this.uiTpnl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTpnl_Main.Size = new System.Drawing.Size(1315, 920);
            this.uiTpnl_Main.TabIndex = 5;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.fpSpread1.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.DimGray;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.Silver;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.Silver;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.DimGray;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.LightGray;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            this.fpSpread1.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.fpSpread1.HorizontalScrollBar.TabIndex = 2;
            this.fpSpread1.Location = new System.Drawing.Point(1011, 4);
            this.fpSpread1.Name = "fpSpread1";
            namedStyle1.BackColor = System.Drawing.Color.Gainsboro;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.NoteIndicatorColor = System.Drawing.Color.Red;
            namedStyle1.Renderer = generalCellType1;
            this.fpSpread1.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1});
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(300, 912);
            this.fpSpread1.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Grayscale;
            this.fpSpread1.TabIndex = 2;
            this.fpSpread1.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.fpSpread1.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.Black;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.DimGray;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.Silver;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.Silver;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.DimGray;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.LightGray;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            this.fpSpread1.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.fpSpread1.VerticalScrollBar.TabIndex = 3;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderGrayscale";
            this.fpSpread1_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaGrayscale";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderGrayscale";
            this.fpSpread1_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerGrayscale";
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // uiPan_GlassMap
            // 
            this.uiPan_GlassMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPan_GlassMap.Location = new System.Drawing.Point(1, 1);
            this.uiPan_GlassMap.Margin = new System.Windows.Forms.Padding(0);
            this.uiPan_GlassMap.Name = "uiPan_GlassMap";
            this.uiPan_GlassMap.Size = new System.Drawing.Size(1006, 918);
            this.uiPan_GlassMap.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.uiBtn_ContourMap);
            this.panel1.Controls.Add(this.uiBtn_SaveDefectMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1309, 54);
            this.panel1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.uiCcmb_LotList);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(720, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(133, 45);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Lot List";
            // 
            // uiCcmb_LotList
            // 
            this.uiCcmb_LotList.DropDownHeight = 1;
            this.uiCcmb_LotList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiCcmb_LotList.DropDownWidth = 1;
            this.uiCcmb_LotList.EventArgs_Checked = null;
            this.uiCcmb_LotList.FormattingEnabled = true;
            this.uiCcmb_LotList.IntegralHeight = false;
            this.uiCcmb_LotList.Location = new System.Drawing.Point(6, 18);
            this.uiCcmb_LotList.Name = "uiCcmb_LotList";
            this.uiCcmb_LotList.Size = new System.Drawing.Size(121, 23);
            this.uiCcmb_LotList.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.uiLab_DefColor);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(624, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(90, 45);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Defect Color";
            // 
            // uiLab_DefColor
            // 
            this.uiLab_DefColor.BackColor = System.Drawing.Color.Red;
            this.uiLab_DefColor.Location = new System.Drawing.Point(7, 20);
            this.uiLab_DefColor.Name = "uiLab_DefColor";
            this.uiLab_DefColor.Size = new System.Drawing.Size(78, 19);
            this.uiLab_DefColor.TabIndex = 0;
            this.uiLab_DefColor.Text = "Color";
            this.uiLab_DefColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.uiRdo_White);
            this.groupBox4.Controls.Add(this.uiRdo_Black);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(442, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(176, 45);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Map Color";
            // 
            // uiRdo_White
            // 
            this.uiRdo_White.AutoSize = true;
            this.uiRdo_White.Checked = true;
            this.uiRdo_White.ForeColor = System.Drawing.Color.White;
            this.uiRdo_White.Location = new System.Drawing.Point(81, 20);
            this.uiRdo_White.Name = "uiRdo_White";
            this.uiRdo_White.Size = new System.Drawing.Size(56, 19);
            this.uiRdo_White.TabIndex = 2;
            this.uiRdo_White.TabStop = true;
            this.uiRdo_White.Text = "White";
            this.uiRdo_White.UseVisualStyleBackColor = true;
            // 
            // uiRdo_Black
            // 
            this.uiRdo_Black.AutoSize = true;
            this.uiRdo_Black.ForeColor = System.Drawing.Color.White;
            this.uiRdo_Black.Location = new System.Drawing.Point(6, 20);
            this.uiRdo_Black.Name = "uiRdo_Black";
            this.uiRdo_Black.Size = new System.Drawing.Size(53, 19);
            this.uiRdo_Black.TabIndex = 2;
            this.uiRdo_Black.Text = "Black";
            this.uiRdo_Black.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.uiLab_PosY);
            this.groupBox3.Controls.Add(this.uiLab_PosX);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(280, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(156, 45);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cursor Point";
            // 
            // uiLab_PosY
            // 
            this.uiLab_PosY.AutoSize = true;
            this.uiLab_PosY.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiLab_PosY.ForeColor = System.Drawing.Color.White;
            this.uiLab_PosY.Location = new System.Drawing.Point(81, 19);
            this.uiLab_PosY.Name = "uiLab_PosY";
            this.uiLab_PosY.Size = new System.Drawing.Size(14, 15);
            this.uiLab_PosY.TabIndex = 1;
            this.uiLab_PosY.Text = "Y";
            // 
            // uiLab_PosX
            // 
            this.uiLab_PosX.AutoSize = true;
            this.uiLab_PosX.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiLab_PosX.ForeColor = System.Drawing.Color.White;
            this.uiLab_PosX.Location = new System.Drawing.Point(6, 19);
            this.uiLab_PosX.Name = "uiLab_PosX";
            this.uiLab_PosX.Size = new System.Drawing.Size(15, 15);
            this.uiLab_PosX.TabIndex = 1;
            this.uiLab_PosX.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.uiNum_DefectSize);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(190, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(84, 45);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Defect Size";
            // 
            // uiNum_DefectSize
            // 
            this.uiNum_DefectSize.Location = new System.Drawing.Point(6, 16);
            this.uiNum_DefectSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.uiNum_DefectSize.Name = "uiNum_DefectSize";
            this.uiNum_DefectSize.Size = new System.Drawing.Size(42, 23);
            this.uiNum_DefectSize.TabIndex = 4;
            this.uiNum_DefectSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiLab_ZoomRate);
            this.groupBox1.Controls.Add(this.uiTrackBar);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Zoom Rate";
            // 
            // uiLab_ZoomRate
            // 
            this.uiLab_ZoomRate.AutoSize = true;
            this.uiLab_ZoomRate.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiLab_ZoomRate.ForeColor = System.Drawing.Color.White;
            this.uiLab_ZoomRate.Location = new System.Drawing.Point(149, 19);
            this.uiLab_ZoomRate.Name = "uiLab_ZoomRate";
            this.uiLab_ZoomRate.Size = new System.Drawing.Size(30, 15);
            this.uiLab_ZoomRate.TabIndex = 1;
            this.uiLab_ZoomRate.Text = "x3.0";
            // 
            // uiTrackBar
            // 
            this.uiTrackBar.AutoSize = false;
            this.uiTrackBar.Location = new System.Drawing.Point(5, 16);
            this.uiTrackBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiTrackBar.Name = "uiTrackBar";
            this.uiTrackBar.Size = new System.Drawing.Size(147, 22);
            this.uiTrackBar.TabIndex = 0;
            // 
            // uiBtn_ContourMap
            // 
            this.uiBtn_ContourMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiBtn_ContourMap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiBtn_ContourMap.ForeColor = System.Drawing.Color.Red;
            this.uiBtn_ContourMap.Image = global::WinforsysGlassMap.Properties.Resources.Menu_GlassMap;
            this.uiBtn_ContourMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiBtn_ContourMap.Location = new System.Drawing.Point(1015, 4);
            this.uiBtn_ContourMap.Name = "uiBtn_ContourMap";
            this.uiBtn_ContourMap.Size = new System.Drawing.Size(154, 44);
            this.uiBtn_ContourMap.TabIndex = 4;
            this.uiBtn_ContourMap.Text = "          Contour Map";
            this.uiBtn_ContourMap.UseVisualStyleBackColor = true;
            this.uiBtn_ContourMap.Visible = false;
            // 
            // uiBtn_SaveDefectMap
            // 
            this.uiBtn_SaveDefectMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiBtn_SaveDefectMap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiBtn_SaveDefectMap.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.uiBtn_SaveDefectMap.Image = ((System.Drawing.Image)(resources.GetObject("uiBtn_SaveDefectMap.Image")));
            this.uiBtn_SaveDefectMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiBtn_SaveDefectMap.Location = new System.Drawing.Point(1171, 4);
            this.uiBtn_SaveDefectMap.Name = "uiBtn_SaveDefectMap";
            this.uiBtn_SaveDefectMap.Size = new System.Drawing.Size(135, 44);
            this.uiBtn_SaveDefectMap.TabIndex = 4;
            this.uiBtn_SaveDefectMap.Text = "       &Save Map(S)";
            this.uiBtn_SaveDefectMap.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.uiCcmb_GlassList);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(859, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(150, 45);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Glass List";
            // 
            // uiCcmb_GlassList
            // 
            this.uiCcmb_GlassList.DropDownHeight = 1;
            this.uiCcmb_GlassList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiCcmb_GlassList.DropDownWidth = 1;
            this.uiCcmb_GlassList.EventArgs_Checked = null;
            this.uiCcmb_GlassList.FormattingEnabled = true;
            this.uiCcmb_GlassList.IntegralHeight = false;
            this.uiCcmb_GlassList.Location = new System.Drawing.Point(6, 18);
            this.uiCcmb_GlassList.Name = "uiCcmb_GlassList";
            this.uiCcmb_GlassList.Size = new System.Drawing.Size(138, 23);
            this.uiCcmb_GlassList.TabIndex = 0;
            // 
            // GlassMapZoomForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1317, 985);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GlassMapZoomForm";
            this.Text = "Zoom Map";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.uiTpnl_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiNum_DefectSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTrackBar)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar uiTrackBar;
        private System.Windows.Forms.Panel uiPan_GlassMap;
        private System.Windows.Forms.Label uiLab_ZoomRate;
        private System.Windows.Forms.NumericUpDown uiNum_DefectSize;
        private System.Windows.Forms.Label uiLab_PosY;
        private System.Windows.Forms.Label uiLab_PosX;
        private System.Windows.Forms.RadioButton uiRdo_White;
        private System.Windows.Forms.RadioButton uiRdo_Black;
        public System.Windows.Forms.Button uiBtn_SaveDefectMap;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label uiLab_DefColor;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel uiTpnl_Main;
        private System.Windows.Forms.GroupBox groupBox6;
        private Winforsys.JKControl.CheckComboBox uiCcmb_LotList;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public System.Windows.Forms.Button uiBtn_ContourMap;
        private System.Windows.Forms.GroupBox groupBox7;
        private Winforsys.JKControl.CheckComboBox uiCcmb_GlassList;
    }
}