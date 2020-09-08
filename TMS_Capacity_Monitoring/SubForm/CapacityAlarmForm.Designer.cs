namespace TMS_Capacity_Monitoring
{
    partial class CapacityAlarmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapacityAlarmForm));
            this.uiPn_Alarm = new System.Windows.Forms.Panel();
            this.uiLb_Alarm = new System.Windows.Forms.Label();
            this.uiPn_Alarm.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPn_Alarm
            // 
            this.uiPn_Alarm.Controls.Add(this.uiLb_Alarm);
            this.uiPn_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPn_Alarm.Location = new System.Drawing.Point(0, 0);
            this.uiPn_Alarm.Name = "uiPn_Alarm";
            this.uiPn_Alarm.Size = new System.Drawing.Size(340, 158);
            this.uiPn_Alarm.TabIndex = 0;
            // 
            // uiLb_Alarm
            // 
            this.uiLb_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiLb_Alarm.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uiLb_Alarm.Location = new System.Drawing.Point(0, 0);
            this.uiLb_Alarm.Name = "uiLb_Alarm";
            this.uiLb_Alarm.Size = new System.Drawing.Size(340, 158);
            this.uiLb_Alarm.TabIndex = 1;
            this.uiLb_Alarm.Text = "Warning";
            this.uiLb_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CapacityAlarmForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 158);
            this.Controls.Add(this.uiPn_Alarm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CapacityAlarmForm";
            this.Text = "CapacityAlarmForm";
            this.uiPn_Alarm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel uiPn_Alarm;
        private System.Windows.Forms.Label uiLb_Alarm;
    }
}