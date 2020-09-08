using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinforsysGlassMap
{
    public partial class SubmapUi : UserControl
    {
        public string Title { get; set; }
        public string GlsID { get; set; }
        public string StepID { get; set; }
        public string productID { get; set; }
        public Control SubControl { get; set; }
        public Image Map_Image { get; set; }
        public String m_countPnl = "";
        public SubmapUi()
        {
            InitializeComponent();
            this.Load += SubImageUI_Load;
        }

        void SubImageUI_Load(object sender, EventArgs e)
        {
            this.uiLab_Title.Text = this.Title;

            if (this.SubControl != null && this.SubControl is WIN_GlassMapUi)
            {
                //this.uiPan_Main.Controls.Add(this.SubControl);
                //this.SubControl.Dock = DockStyle.Fill;

                //this.uiLab_Title.Click += uiLab_Title_Click;
                //this.uiChk_MapCheck.CheckedChanged += uiChk_MapCheck_CheckedChanged;

                //this.SubControl.DoubleClick -= SubControl_DoubleClick;
                //this.SubControl.DoubleClick += SubControl_DoubleClick;

                WIN_GlassMapUi glsMap = this.SubControl as WIN_GlassMapUi;

                this.uiPan_Main.Controls.Add(glsMap);
                glsMap.Dock = DockStyle.Fill;

                this.uiLab_Title.Click += uiLab_Title_Click;
                this.uiChk_MapCheck.CheckedChanged += uiChk_MapCheck_CheckedChanged;

                //glsMap.DoubleClick -= SubControl_DoubleClick;
                //glsMap.DoubleClick += SubControl_DoubleClick;
                
            }

            if (Map_Image != null)
            {
                this.uiPan_Main.BackgroundImage = Map_Image;
                this.uiPan_Main.BackgroundImageLayout = ImageLayout.Stretch;
            }

            SetMapCheckVisible(true);
        }

        void uiChk_MapCheck_CheckedChanged(object sender, EventArgs e)
        {
            uiLab_Title.BackColor = (uiChk_MapCheck.Checked == true) ? Color.DarkOrange : Color.Black;
        }

        void uiLab_Title_Click(object sender, EventArgs e)
        {
            if (uiChk_MapCheck.Visible == true)
            {
                if (uiChk_MapCheck.Checked == true)
                {
                    uiChk_MapCheck.Checked = false;
                    uiLab_Title.BackColor = Color.Black;
                }
                else
                {
                    uiChk_MapCheck.Checked = true;
                    uiLab_Title.BackColor = Color.DarkOrange;
                }
            }
        }

        public void SetMapCheckVisible(bool flag)
        {
            uiChk_MapCheck.Visible = flag;
        }
        
        void SubControl_DoubleClick(object sender, EventArgs e)
        {
            WIN_GlassMapUi map = SubControl as WIN_GlassMapUi;

            GlassMapZoomForm frm = new GlassMapZoomForm(true, true);
            frm.m_countPnl = m_countPnl;
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.GlassRawData = map.mData.GlsRawData;
            if (map.mData.PnlValueList != null)
            {
                frm.PnlVoltageList = map.mData.PnlValueList;
            }
            frm.DefectList = map.mData.DefList;

            frm.mProperties = map.mProperties;

            if (map.mProperties.MapType == MapProperties.GLS_MAP_TYPE.Pnl_Color)
            {
                frm.uiBtn_SaveDefectMap.Click -= frm.uiBtn_SaveDefectMap_Click;
                frm.uiBtn_SaveDefectMap.Click += UiBtn_SaveDefectMap_Click;
            }

            frm.ShowDialog();
            map.ReDrawMap();
        }

        private void UiBtn_SaveDefectMap_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG Files | *.jpg";

            int initWidth = this.Width;
            int initHeight = this.Height;
            int initTableHeight = (int)tableLayoutPanel1.RowStyles[0].Height;
            Font initFnt = this.uiLab_Title.Font;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Width = 1500;
                this.Height = 1850;
                this.tableLayoutPanel1.RowStyles[0].Height = 50;
                if (uiLab_Title.Text.Length < 30)
                    this.uiLab_Title.Font = new Font("Microsoft YaHei", 13F, FontStyle.Bold);
                else
                    this.uiLab_Title.Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold);
                Image image = GetImage_and_Title();
                image.Save(sfd.FileName);

                this.Width = initWidth;
                this.Height = initHeight;
                this.tableLayoutPanel1.RowStyles[0].Height = initTableHeight;
                this.uiLab_Title.Font = initFnt;

                MessageBox.Show("Saved Complete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        public Image GetImage_and_Title()
        {
            /* Form frm = new Form();
             //frm.FormBorderStyle = FormBorderStyle.None;
             frm.Show();
             frm.AutoScroll = true;
             frm.Location = new Point(0, 0);
             frm.Size = new Size(1500, 1100);*/

            if (this.SubControl != null && this.SubControl is WIN_GlassMapUi)
            {
                (this.SubControl as WIN_GlassMapUi).Invalidate();
                (this.SubControl as WIN_GlassMapUi).Refresh();
                Image bMap = (this.SubControl as WIN_GlassMapUi).GetMapImage();

                PictureBox pnl1 = new PictureBox();
                pnl1.Image = bMap;
                this.uiPan_Main.Controls.Add(pnl1);
                pnl1.Dock = DockStyle.Fill;

                /*    this.tableLayoutPanel1.RowStyles[0].Height = 50;
                    this.Height = 1200;
                    this.tableLayoutPanel1.Height = 1200;
                    this.uiLab_Title.Font = new Font("Microsoft YaHei", 13F, FontStyle.Bold);*/

                Bitmap bmp = new Bitmap(this.Width, this.Height);

                this.tableLayoutPanel1.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                PictureBox pb = new PictureBox();
                pb.Image = bmp;
                //  frm.Controls.Add(pb);

                pb.Dock = DockStyle.Fill;
                (this.SubControl as WIN_GlassMapUi).Invalidate();
                (this.SubControl as WIN_GlassMapUi).Refresh();
                //  frm.Close();
                return bmp;
            }
            //frm.Close();
            return null;
        }
    }
}
