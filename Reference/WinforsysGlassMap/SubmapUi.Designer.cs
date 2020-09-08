namespace WinforsysGlassMap
{
    partial class SubmapUi
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiPan_Main = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiChk_MapCheck = new System.Windows.Forms.CheckBox();
            this.uiLab_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.uiPan_Main, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 413);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // uiPan_Main
            // 
            this.uiPan_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPan_Main.Location = new System.Drawing.Point(3, 33);
            this.uiPan_Main.Name = "uiPan_Main";
            this.uiPan_Main.Size = new System.Drawing.Size(314, 377);
            this.uiPan_Main.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiChk_MapCheck);
            this.panel1.Controls.Add(this.uiLab_Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 24);
            this.panel1.TabIndex = 2;
            // 
            // uiChk_MapCheck
            // 
            this.uiChk_MapCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiChk_MapCheck.AutoSize = true;
            this.uiChk_MapCheck.BackColor = System.Drawing.Color.Black;
            this.uiChk_MapCheck.Font = new System.Drawing.Font("굴림", 9F);
            this.uiChk_MapCheck.Location = new System.Drawing.Point(295, 5);
            this.uiChk_MapCheck.Name = "uiChk_MapCheck";
            this.uiChk_MapCheck.Size = new System.Drawing.Size(15, 14);
            this.uiChk_MapCheck.TabIndex = 2;
            this.uiChk_MapCheck.UseVisualStyleBackColor = false;
            // 
            // uiLab_Title
            // 
            this.uiLab_Title.BackColor = System.Drawing.Color.Black;
            this.uiLab_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLab_Title.ForeColor = System.Drawing.Color.White;
            this.uiLab_Title.Location = new System.Drawing.Point(0, 0);
            this.uiLab_Title.Name = "uiLab_Title";
            this.uiLab_Title.Size = new System.Drawing.Size(314, 24);
            this.uiLab_Title.TabIndex = 1;
            this.uiLab_Title.Text = "label1";
            this.uiLab_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SubmapUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SubmapUi";
            this.Size = new System.Drawing.Size(320, 413);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel uiPan_Main;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label uiLab_Title;
        public System.Windows.Forms.CheckBox uiChk_MapCheck;
    }
}
