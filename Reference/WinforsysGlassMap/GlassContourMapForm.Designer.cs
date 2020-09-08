namespace WinforsysGlassMap
{
    partial class GlassContourMapForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiTpnl_Main = new System.Windows.Forms.TableLayoutPanel();
            this.uiFPan_GlassMap = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiCcb_CheckColor = new WinforsysGlassMap.ColoredComboBox();
            this.uiCk_DFT_QTY = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.uiRdo_DFTCODE = new System.Windows.Forms.RadioButton();
            this.uiRdo_DFT = new System.Windows.Forms.RadioButton();
            this.uiRdo_ALL = new System.Windows.Forms.RadioButton();
            this.uiRdo_Full_Code = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.uiRdo_White = new System.Windows.Forms.RadioButton();
            this.uiRdo_Black = new System.Windows.Forms.RadioButton();
            this.uiBtn_SaveDefectMap = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiTpnl_Main.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 985);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // uiTpnl_Main
            // 
            this.uiTpnl_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.uiTpnl_Main.ColumnCount = 1;
            this.uiTpnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTpnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.uiTpnl_Main.Controls.Add(this.uiFPan_GlassMap, 0, 0);
            this.uiTpnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTpnl_Main.Location = new System.Drawing.Point(1, 64);
            this.uiTpnl_Main.Margin = new System.Windows.Forms.Padding(0);
            this.uiTpnl_Main.Name = "uiTpnl_Main";
            this.uiTpnl_Main.RowCount = 1;
            this.uiTpnl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTpnl_Main.Size = new System.Drawing.Size(996, 920);
            this.uiTpnl_Main.TabIndex = 5;
            // 
            // uiFPan_GlassMap
            // 
            this.uiFPan_GlassMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiFPan_GlassMap.Location = new System.Drawing.Point(4, 4);
            this.uiFPan_GlassMap.Name = "uiFPan_GlassMap";
            this.uiFPan_GlassMap.Size = new System.Drawing.Size(988, 912);
            this.uiFPan_GlassMap.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.uiBtn_SaveDefectMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 54);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiCcb_CheckColor);
            this.groupBox1.Controls.Add(this.uiCk_DFT_QTY);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(501, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 45);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Option";
            // 
            // uiCcb_CheckColor
            // 
            this.uiCcb_CheckColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.uiCcb_CheckColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiCcb_CheckColor.FormattingEnabled = true;
            this.uiCcb_CheckColor.Items.AddRange(new object[] {
            "Red",
            "Blue",
            "Green",
            "Yellow",
            "Orange",
            "DarkRed",
            "Pink",
            "Aqua"});
            this.uiCcb_CheckColor.Location = new System.Drawing.Point(7, 16);
            this.uiCcb_CheckColor.MyColors = new string[] {
        "Red",
        "Blue",
        "Green",
        "Yellow",
        "Orange",
        "DarkRed",
        "Pink",
        "Aqua",
        "X"};
            this.uiCcb_CheckColor.Name = "uiCcb_CheckColor";
            this.uiCcb_CheckColor.Size = new System.Drawing.Size(121, 24);
            this.uiCcb_CheckColor.TabIndex = 7;
            // 
            // uiCk_DFT_QTY
            // 
            this.uiCk_DFT_QTY.AutoSize = true;
            this.uiCk_DFT_QTY.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiCk_DFT_QTY.ForeColor = System.Drawing.Color.White;
            this.uiCk_DFT_QTY.Location = new System.Drawing.Point(138, 20);
            this.uiCk_DFT_QTY.Name = "uiCk_DFT_QTY";
            this.uiCk_DFT_QTY.Size = new System.Drawing.Size(84, 19);
            this.uiCk_DFT_QTY.TabIndex = 6;
            this.uiCk_DFT_QTY.Text = "Defect Qty";
            this.uiCk_DFT_QTY.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.uiRdo_DFTCODE);
            this.groupBox7.Controls.Add(this.uiRdo_DFT);
            this.groupBox7.Controls.Add(this.uiRdo_ALL);
            this.groupBox7.Controls.Add(this.uiRdo_Full_Code);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(190, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(306, 45);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Countour Option";
            // 
            // uiRdo_DFTCODE
            // 
            this.uiRdo_DFTCODE.AutoSize = true;
            this.uiRdo_DFTCODE.ForeColor = System.Drawing.Color.White;
            this.uiRdo_DFTCODE.Location = new System.Drawing.Point(205, 22);
            this.uiRdo_DFTCODE.Name = "uiRdo_DFTCODE";
            this.uiRdo_DFTCODE.Size = new System.Drawing.Size(96, 19);
            this.uiRdo_DFTCODE.TabIndex = 2;
            this.uiRdo_DFTCODE.Text = "Defect+Code";
            this.uiRdo_DFTCODE.UseVisualStyleBackColor = true;
            // 
            // uiRdo_DFT
            // 
            this.uiRdo_DFT.AutoSize = true;
            this.uiRdo_DFT.ForeColor = System.Drawing.Color.White;
            this.uiRdo_DFT.Location = new System.Drawing.Point(139, 22);
            this.uiRdo_DFT.Name = "uiRdo_DFT";
            this.uiRdo_DFT.Size = new System.Drawing.Size(60, 19);
            this.uiRdo_DFT.TabIndex = 2;
            this.uiRdo_DFT.Text = "Defect";
            this.uiRdo_DFT.UseVisualStyleBackColor = true;
            // 
            // uiRdo_ALL
            // 
            this.uiRdo_ALL.AutoSize = true;
            this.uiRdo_ALL.Checked = true;
            this.uiRdo_ALL.ForeColor = System.Drawing.Color.White;
            this.uiRdo_ALL.Location = new System.Drawing.Point(6, 22);
            this.uiRdo_ALL.Name = "uiRdo_ALL";
            this.uiRdo_ALL.Size = new System.Drawing.Size(45, 19);
            this.uiRdo_ALL.TabIndex = 2;
            this.uiRdo_ALL.TabStop = true;
            this.uiRdo_ALL.Text = "ALL";
            this.uiRdo_ALL.UseVisualStyleBackColor = true;
            // 
            // uiRdo_Full_Code
            // 
            this.uiRdo_Full_Code.AutoSize = true;
            this.uiRdo_Full_Code.ForeColor = System.Drawing.Color.White;
            this.uiRdo_Full_Code.Location = new System.Drawing.Point(57, 22);
            this.uiRdo_Full_Code.Name = "uiRdo_Full_Code";
            this.uiRdo_Full_Code.Size = new System.Drawing.Size(76, 19);
            this.uiRdo_Full_Code.TabIndex = 2;
            this.uiRdo_Full_Code.Text = "Full Code";
            this.uiRdo_Full_Code.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.uiRdo_White);
            this.groupBox4.Controls.Add(this.uiRdo_Black);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(8, 4);
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
            // uiBtn_SaveDefectMap
            // 
            this.uiBtn_SaveDefectMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiBtn_SaveDefectMap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiBtn_SaveDefectMap.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.uiBtn_SaveDefectMap.Image = global::WinforsysGlassMap.Properties.Resources.Btn_OpenFile;
            this.uiBtn_SaveDefectMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiBtn_SaveDefectMap.Location = new System.Drawing.Point(845, 6);
            this.uiBtn_SaveDefectMap.Name = "uiBtn_SaveDefectMap";
            this.uiBtn_SaveDefectMap.Size = new System.Drawing.Size(137, 44);
            this.uiBtn_SaveDefectMap.TabIndex = 4;
            this.uiBtn_SaveDefectMap.Text = "       &Save Map(S)";
            this.uiBtn_SaveDefectMap.UseVisualStyleBackColor = true;
            // 
            // GlassContourMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 985);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GlassContourMapForm";
            this.Text = "Contour Map";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.uiTpnl_Main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton uiRdo_White;
        private System.Windows.Forms.RadioButton uiRdo_Black;
        public System.Windows.Forms.Button uiBtn_SaveDefectMap;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel uiTpnl_Main;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton uiRdo_DFTCODE;
        private System.Windows.Forms.RadioButton uiRdo_DFT;
        private System.Windows.Forms.RadioButton uiRdo_Full_Code;
        public System.Windows.Forms.FlowLayoutPanel uiFPan_GlassMap;
        private System.Windows.Forms.CheckBox uiCk_DFT_QTY;
        private System.Windows.Forms.RadioButton uiRdo_ALL;
        private System.Windows.Forms.GroupBox groupBox1;
        private ColoredComboBox uiCcb_CheckColor;
    }
}