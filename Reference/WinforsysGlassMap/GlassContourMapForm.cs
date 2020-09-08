using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinforsysGlassMap.MapData;
using static WinforsysGlassMap.RawData;

namespace WinforsysGlassMap
{
    public partial class GlassContourMapForm : Form
    {
        #region " Properties & Variables "

        public string processType = string.Empty;
        public MapProperties mProperties { get; set; }

        public RawData GlassRawData { get; set; }
        public List<PinPointData> PinPointList { get; set; }
        public List<MapData.DEF_INFO> DefectList { get; set; }
        public List<MapData.PNL_VALUE> PnlVoltageList { get; set; }
        public List<MapData.DEF_REGION> DefRegionList { get; set; }

        public event EventHandler<DefectPointEventArgs> DefectClickEvent;

        private WIN_GlassMapUi _GlsMap = null;

        public string m_countPnl = "";
        private bool flagColor = false;
        private bool flagTitle = false;

        //Defect Filter 내용을 저장
        public string DefectType = string.Empty;
        public List<string> DefectFilterList = new List<string>();

        public MapData mData = null;
        public Image FormImage = null;

        private bool allChecked = false;

        private string comboColor = string.Empty;

        public class SheetData
        {
            public string Code = string.Empty;
            public double Qty = 0;
            public Color Color = Color.Transparent;
            public bool Visible = true;
        }

        #endregion " Properties & Variables End"
        public GlassContourMapForm()
        {
            InitializeComponent();
            this.Load += GlassClororZoomForm_Load; ;

            this.uiRdo_Black.CheckedChanged += uiRdo_Black_CheckedChanged;
            this.uiRdo_White.CheckedChanged += uiRdo_Black_CheckedChanged;

            this.uiRdo_ALL.CheckedChanged += UiRdo_Defect_CheckedChanged;
            this.uiRdo_Full_Code.CheckedChanged += UiRdo_Defect_CheckedChanged;
            this.uiRdo_DFT.CheckedChanged += UiRdo_Defect_CheckedChanged;
            this.uiRdo_DFTCODE.CheckedChanged += UiRdo_Defect_CheckedChanged;

            this.uiCk_DFT_QTY.CheckedChanged += UiRdo_Defect_CheckedChanged;

            this.uiBtn_SaveDefectMap.Click += uiBtn_SaveDefectMap_Click;

            this.uiCcb_CheckColor.SelectedIndexChanged += uiCcb_CheckColor_SelectedIndexChanged;

            this.FormClosed += GlassContourMapForm_FormClosed; ;
        }

        private void UiRdo_Defect_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string tmpType = CheckDefectType();
            if (DefectType != tmpType || sender == uiCk_DFT_QTY)
            {
                DefectType = tmpType;
                uiFPan_GlassMap.Controls.Clear();
                InitFrm(Defect_Filter());
            }

            this.Cursor = Cursors.Default;
        }

        private void uiCcb_CheckColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ColoredComboBox cb = sender as ColoredComboBox;
            comboColor = cb.Items[cb.SelectedIndex].ToString();

            string tmpType = CheckDefectType();

            DefectType = tmpType;
            uiFPan_GlassMap.Controls.Clear();
            InitFrm(Defect_Filter());

            this.Cursor = Cursors.Default;
        }

        void GlassContourMapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mProperties.MapType = MapProperties.GLS_MAP_TYPE.Defect;
        }

        void GlassClororZoomForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            mProperties.SHOW_ZOOM_MAP = false;
            DefectType = CheckDefectType();
            
            uiCcb_CheckColor.SelectedIndex = 0;

            if (uiFPan_GlassMap.Controls.Count == 0)
                InitFrm(Defect_Filter());

            this.Cursor = Cursors.Default;
        }

        private string CheckDefectType()
        {
            string Dft_Type = string.Empty;
            if (uiRdo_ALL.Checked == true)
            {
                Dft_Type = "ALL";

            }
            else if (uiRdo_Full_Code.Checked == true)
            {
                Dft_Type = "FULL_CODE";

            }
            else if (uiRdo_DFT.Checked == true)
            {
                Dft_Type = "DFT";
            }
            else
            {
                Dft_Type = "DFT_CODE";
            }
            return Dft_Type;
        }

        private List<string> Defect_Filter()
        {
            List<string> dft_List = new List<string>();
            for (int row = 0; row < DefectFilterList.Count; row++)
            {
                string dft_Code = DefectFilterList[row];
                if (uiRdo_ALL.Checked == true)
                {
                    dft_Code = "MERGE";
                }
                else if (uiRdo_Full_Code.Checked == true || DefectFilterList[row].Length < 8)
                {
                    dft_Code = DefectFilterList[row].ToString();
                }
                else if (uiRdo_DFT.Checked == true)
                {
                    dft_Code = DefectFilterList[row].Substring(4, 2);
                }
                else
                {
                    if (DefectFilterList[row].Length >= 8)
                    {
                        dft_Code = DefectFilterList[row].Substring(4, 4);
                    }
                }
                if (dft_List.Contains(dft_Code) == false)
                    dft_List.Add(dft_Code);
            }

            if (dft_List.Count == 0)
                return null;
            else
                return dft_List;
        }

        #region "  Public methode "

        public void Reset(RawData rawData, MapProperties.Module_TYPE type)
        {
            if (_GlsMap == null)
                InitFrm(Defect_Filter());

            _GlsMap.Reset(rawData, type);
        }

        public Image GetImage()
        {
            Bitmap bMap = null;

            using (Graphics g = this.CreateGraphics())
            {
                uiFPan_GlassMap.Controls[0].BackgroundImage = _GlsMap.GetMapImage();
                _GlsMap.Visible = false;

                Bitmap memBitMap = new Bitmap(this.uiTpnl_Main.Width, this.uiTpnl_Main.Height);
                this.uiTpnl_Main.DrawToBitmap(memBitMap, new Rectangle(0, 0, this.uiTpnl_Main.Width, this.uiTpnl_Main.Height));
                g.DrawImageUnscaled(memBitMap, 0, 0);
                bMap = memBitMap;

                _GlsMap.Visible = true;
                uiFPan_GlassMap.Controls[0].BackgroundImage = null;
            }

            this.FormImage = (Image)bMap;
            return this.FormImage;
        }

        #endregion "  Public methode End"

        #region "  Private methode "

        private void InitFrm(List<string> defectList)
        {
            for (int Lcnt = 0; Lcnt < defectList.Count; Lcnt++)
            {
                createMap(ref uiFPan_GlassMap, DefectType, defectList[Lcnt]);
            }

            uiFPan_GlassMap.AutoScroll = true;
        }

        private void createMap(ref FlowLayoutPanel FlowPanel, string Dft_Type, string DefectCode)
        {
            //Map 설정
            WIN_GlassMapUi GlsMap = new WIN_GlassMapUi();
            SubmapUi mapUi = new SubmapUi();

            mapUi.Title = string.Format("{0}_{1}", Dft_Type, DefectCode);

            mapUi.SubControl = GlsMap;
            mapUi.Size = new System.Drawing.Size(450, 450);

            GlsMap.clearMouseEvent();
            GlsMap.Dock = DockStyle.None;
            GlsMap.Size = new Size(450, 450);
            GlsMap.mProperties = this.mProperties;

            if (GlsMap.mProperties == null)
                GlsMap.mProperties = new MapProperties();

            GlsMap.mProperties.SHOW_ZOOM_MAP = true;

            if (uiRdo_White.Checked == true)
                GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.White;
            else
                GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.Black;

            GlsMap.DefectClickEvent += this.DefectClickEvent;
            GlsMap.DoubleClick += GlsMap_DoubleClick;

            if (mProperties == null)
                mProperties = GlsMap.mProperties;

            FlowPanel.Controls.Add(mapUi);

            this.Reset(ref GlsMap, this.mProperties.ModuleType, Dft_Type, DefectCode);
        }

        private void GlsMap_DoubleClick(object sender, EventArgs e)
        {
            GlassMapZoomForm frm = new GlassMapZoomForm();
            frm.DefectClickEvent += this.DefectClickEvent;

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.GlassRawData = (sender as WIN_GlassMapUi).rData;
            frm.DefectList = new List<DEF_INFO>();
            frm.PnlVoltageList = (sender as WIN_GlassMapUi).mData.PnlValueList;
            frm.PinPointList = (sender as WIN_GlassMapUi).mData.PinPointDataList;
            frm.DefRegionList = (sender as WIN_GlassMapUi).mData.DefRegionList;
            frm.mProperties = (sender as WIN_GlassMapUi).mProperties;
            frm.PnlNameList = (sender as WIN_GlassMapUi).rData.PNL_NAME_DATA;
            frm.ShowDialog();
        }

        private void Reset(ref WIN_GlassMapUi GlsMap, MapProperties.Module_TYPE mType, string Dft_Type, string DefectCode)
        {
            GlsMap.Reset(this.GlassRawData, mType);

            GlsMap.mData.DefList = Defect_Filter(this.DefectList, Dft_Type, DefectCode);
            GlsMap.mProperties.MapType = MapProperties.GLS_MAP_TYPE.Contour;
            GlsMap.ReDrawMap();
            if (GlsMap.rData.PNL_NAME_DATA == null || GlsMap.rData.PNL_NAME_DATA.Count == 0)
            {
                GlsMap.rData.PNL_NAME_DATA = new List<P_RAW>();
                GlsMap.rData.PNL_NAME_DATA = GetPanelName(GlsMap.rData.PNL_RAW_DATA);
            }

            GlsMap.mData.PnlValueList = GetPanelList(GlsMap.mData.DefList, GlsMap);
            GlsMap.mProperties.MapType = MapProperties.GLS_MAP_TYPE.Contour;
            //_GlsMap.mData.DefRegionList = this.DefRegionList;
            GlsMap.mData.Raw_Size_X += 20.00F;
            GlsMap.mData.Raw_Size_Y += 20.00F;
            GlsMap.ReDrawMap();
        }

        private List<DEF_INFO> Defect_Filter(List<DEF_INFO> df_List, string Dft_Type, string DefectCode)
        {
            List<DEF_INFO> DefList = new List<DEF_INFO>();
            for (int Lcnt = 0; Lcnt < df_List.Count; Lcnt++)
            {
                DEF_INFO def = df_List[Lcnt];

                if (def.Visible == false) continue;

                if (Dft_Type == "ALL")
                {
                    DefList.Add(def);
                }
                else if (Dft_Type == "FULL_CODE" || def.DEF_Code.Length < 6)
                {
                    if (def.DEF_Code == DefectCode)
                        DefList.Add(def);
                }
                else if (Dft_Type == "DFT")
                {
                    if (def.DEF_Code.Substring(4, 2) == DefectCode)
                        DefList.Add(def);
                }
                else
                {
                    if (def.DEF_Code.Length >= 8)
                    {
                        if (def.DEF_Code.Substring(4, 4) == DefectCode)
                            DefList.Add(def);
                    }
                }
            }
            return DefList;
        }
        private List<P_RAW> GetPanelName(List<P_RAW> PnlData)
        {
            double PNL_SIZE_X = PnlData[2].PNL_SIZE_X;
            double PNL_SIZE_Y = PnlData[2].PNL_SIZE_Y;
            List<P_RAW> PanelNameList = new List<P_RAW>();
            Dictionary<double, string> PanelXName = new Dictionary<double, string>();
            Dictionary<double, string> PanelYName = new Dictionary<double, string>();

            //각 Panel의 좌표 값을 가지고 온다. 
            for (int Lcnt = 0; Lcnt < PnlData.Count; Lcnt++)
            {
                if (PnlData[Lcnt].PNL_POINT_X == 0 || PnlData[Lcnt].PNL_POINT_Y == 0) continue;
                if (PanelXName.ContainsKey(PnlData[Lcnt].PNL_POINT_X) == false)
                    PanelXName.Add(PnlData[Lcnt].PNL_POINT_X, PnlData[Lcnt].PNL_ID);

                if (PanelYName.ContainsKey(PnlData[Lcnt].PNL_POINT_Y) == false)
                    PanelYName.Add(PnlData[Lcnt].PNL_POINT_Y, PnlData[Lcnt].PNL_ID);
            }
            //기준값 지정
            PanelXName.OrderBy(x => x.Key);
            PanelYName.OrderBy(x => x.Key);
            double MaxX = PanelXName.ElementAt(0).Key;
            double MinX = PanelXName.ElementAt(PanelXName.Count - 1).Key;
            double MaxY = PanelYName.ElementAt(0).Key;
            double MinY = PanelYName.ElementAt(PanelYName.Count - 1).Key;
            int i = 0;
            for (int row = 0; row < PanelYName.Count; row++)
            {
                for (int col = 0; col < PanelXName.Count; col++)
                {
                    double tmpX = PanelXName.ElementAt(col).Key;
                    double tmpY = PanelYName.ElementAt(row).Key;

                    if (tmpX == MaxX)
                    {
                        i++;
                        P_RAW pData = new P_RAW();
                        pData.PNL_ID = PanelYName.ElementAt(row).Value.Substring(0, 1);
                        pData.PNL_POINT_X = tmpX + PNL_SIZE_X * 0.75;
                        pData.PNL_POINT_Y = tmpY;
                        pData.PNL_SIZE_X = PNL_SIZE_X;
                        pData.PNL_SIZE_Y = PNL_SIZE_Y;
                        PanelNameList.Add(pData);
                    }
                    if (tmpX == MinX)
                    {
                        i++;
                        P_RAW pData = new P_RAW();
                        pData.PNL_ID = PanelYName.ElementAt(row).Value.Substring(4, 1);
                        pData.PNL_POINT_X = tmpX - PNL_SIZE_X * 0.75;
                        pData.PNL_POINT_Y = tmpY;
                        pData.PNL_SIZE_X = PNL_SIZE_X;
                        pData.PNL_SIZE_Y = PNL_SIZE_Y;
                        PanelNameList.Add(pData);
                    }
                    if (tmpY == MaxY)
                    {
                        i++;
                        P_RAW pData = new P_RAW();
                        pData.PNL_ID = PanelXName.ElementAt(col).Value.Substring(1, 2);
                        pData.PNL_POINT_X = tmpX;
                        pData.PNL_POINT_Y = tmpY + PNL_SIZE_Y * 0.75;
                        pData.PNL_SIZE_X = PNL_SIZE_X;
                        pData.PNL_SIZE_Y = PNL_SIZE_Y;
                        PanelNameList.Add(pData);
                    }
                    if (tmpY == MinY)
                    {
                        i++;
                        P_RAW pData = new P_RAW();
                        pData.PNL_ID = PanelXName.ElementAt(col).Value.Substring(1, 2);
                        pData.PNL_POINT_X = tmpX;
                        pData.PNL_POINT_Y = tmpY - PNL_SIZE_Y * 0.75;
                        pData.PNL_SIZE_X = PNL_SIZE_X;
                        pData.PNL_SIZE_Y = PNL_SIZE_Y;
                        PanelNameList.Add(pData);
                    }

                }
            }

            return PanelNameList;
        }

        private List<PNL_VALUE> GetPanelList(List<DEF_INFO> df_List, WIN_GlassMapUi GlsMap)
        {
            List<PNL_VALUE> PnlValueList = new List<PNL_VALUE>();
            List<CELL_INFO> Cell_date = GlsMap.mData.CellList;

            double max = Int32.MinValue;
            double min = Int32.MaxValue;
            int Dft_Cnt = 0;

            for (int idx = 0; idx < Cell_date.Count; idx++)
            {
                Dft_Cnt = 0;

                for (int Dcnt = 0; Dcnt < df_List.Count; Dcnt++)
                {
                    Rectangle cRect = Cell_date[idx].Cell_Rect;
                    Point pDef = new Point(df_List[Dcnt].DEF_Rect.X + (df_List[Dcnt].DEF_Rect.Width / 2), df_List[Dcnt].DEF_Rect.Y + (df_List[Dcnt].DEF_Rect.Height / 2));

                    if (cRect.Contains(pDef)) Dft_Cnt++;

                    //if (Cell_date[idx].Cell_Rect.Contains(df_List[Dcnt].DEF_Rect.X, df_List[Dcnt].DEF_Rect.Y) == true)
                    //Rectangle rct = new Rectangle(Cell_date[idx].Cell_Rect.X , Cell_date[idx].Cell_Rect.Y - 4, Cell_date[idx].Cell_Rect.Width + 1, Cell_date[idx].Cell_Rect.Height + 4);
                    
                    //if (rct.Contains(df_List[Dcnt].DEF_Rect.X, df_List[Dcnt].DEF_Rect.Y) == true)
                    //    Dft_Cnt++;
                }

                if (Dft_Cnt > max)
                {
                    max = Dft_Cnt;
                }

                if (Dft_Cnt < min)
                {
                    min = Dft_Cnt;
                }
            }

            for (int Lcnt = 0; Lcnt < Cell_date.Count; Lcnt++)
            {
                if (Cell_date[Lcnt].Cell_Text.Length < 5) continue;
                PNL_VALUE pnlValue = new PNL_VALUE();
                pnlValue.PNL_ID = Cell_date[Lcnt].Cell_Text;
                Dft_Cnt = 0;

                for (int Dcnt = 0; Dcnt < df_List.Count; Dcnt++)
                {
                    Rectangle cRect = Cell_date[Lcnt].Cell_Rect;
                    Point pDef = new Point(df_List[Dcnt].DEF_Rect.X + (df_List[Dcnt].DEF_Rect.Width / 2), df_List[Dcnt].DEF_Rect.Y + (df_List[Dcnt].DEF_Rect.Height / 2));

                    if (cRect.Contains(pDef)) Dft_Cnt++;

                    //Rectangle rct = new Rectangle(Cell_date[Lcnt].Cell_Rect.X, Cell_date[Lcnt].Cell_Rect.Y - 4, Cell_date[Lcnt].Cell_Rect.Width + 1, Cell_date[Lcnt].Cell_Rect.Height + 4);
                    //if (rct.Contains(df_List[Dcnt].DEF_Rect.X + (df_List[Dcnt].DEF_Rect.Width / 2), df_List[Dcnt].DEF_Rect.Y + (df_List[Dcnt].DEF_Rect.Height / 2)) == true)

                    //if (rct.Contains(df_List[Dcnt].DEF_Rect.X, df_List[Dcnt].DEF_Rect.Y) == true)
                    //    Dft_Cnt++;
                }

                if (uiCk_DFT_QTY.Checked == true)
                {
                    pnlValue.PNL_VAL_01 = Dft_Cnt;
                    pnlValue.Sub_Classify = "QTY";
                }
                else
                {
                    pnlValue.PNL_VAL_01 = Math.Round((double)Dft_Cnt / df_List.Count * 100, 1);
                    pnlValue.Sub_Classify = "RATE";
                }

                pnlValue.PNL_COLOR = Pnl_Color(Dft_Cnt, max, min);

                PnlValueList.Add(pnlValue);
            }

            return PnlValueList;
        }     

        private Color Pnl_Color(double Cnt, double max, double min)
        {
            double degree = 0;
            double diff = max - min;

            if (max != 0 || min != 0)
            {
                degree = Math.Truncate((255 / diff) * 10000) / 10000;         
            }

            Color baseColor = Color.FromName(comboColor);

            Color cVal = Color.FromArgb((int)((Cnt - min) * degree), baseColor);

            return cVal;
        }

        #endregion "  Private methode End"

        #region " Form Events "

        void uiRdo_Black_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;

            if (btn.Checked == false) return;

            for (int Cnt = 0; Cnt < this.uiFPan_GlassMap.Controls.Count; Cnt++)
            {
                SubmapUi mapUi = this.uiFPan_GlassMap.Controls[Cnt] as SubmapUi;
                WIN_GlassMapUi map = mapUi.SubControl as WIN_GlassMapUi;
                if (uiRdo_White.Checked == true)
                    map.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.White;
                else
                    map.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.Black;
                map.ReDrawMap();
            }
        }
        public static FolderBrowserDialog GetSaveFolderDialog()
        {
            FolderBrowserDialog saveFolderDlg = new FolderBrowserDialog();
            saveFolderDlg.RootFolder = Environment.SpecialFolder.Desktop;

            return saveFolderDlg;
        }

        public void uiBtn_SaveDefectMap_Click(object sender, EventArgs e)
        {
            if (uiFPan_GlassMap.Controls.Count == 0)
                return;
            bool IsChecked = false;
            for (int i = 0; i < uiFPan_GlassMap.Controls.Count; i++)
            {
                SubmapUi mapUI = uiFPan_GlassMap.Controls[i] as SubmapUi;
                if (mapUI.uiChk_MapCheck.Checked == true)
                {
                    IsChecked = true;
                    break;
                }
            }

            using (FolderBrowserDialog folderDlg = GetSaveFolderDialog())
            {
                if (folderDlg.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string dir = folderDlg.SelectedPath;

                    Form frm = new Form();
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Show();
                    frm.Location = new Point(-10000, -10000);
                    frm.Size = new Size(1500, 1300);

                    for (int i = 0; i < uiFPan_GlassMap.Controls.Count; i++)
                    {
                        try
                        {
                            SubmapUi mapUI = uiFPan_GlassMap.Controls[i] as SubmapUi;

                            if (IsChecked == true && mapUI.uiChk_MapCheck.Checked == false) continue;

                            string filePath = string.Format("{0}/{1}.PNG", dir, mapUI.Title.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("/", "_").Replace(":", "_"));

                            frm.Controls.Clear();
                            SubmapUi S_mapui = new SubmapUi();
                            WIN_GlassMapUi map = new WIN_GlassMapUi();

                            S_mapui.Title = mapUI.Title;
                            S_mapui.tableLayoutPanel1.RowStyles[0].Height = 50;
                            S_mapui.uiLab_Title.Font = new Font("Microsoft YaHei", 13F, FontStyle.Bold);
                            S_mapui.SubControl = map;
                            map.Dock = DockStyle.Fill;
                            S_mapui.Dock = DockStyle.Fill;
                            frm.Controls.Add(S_mapui);

                            map.Reset(mapUI.productID, "BP", MapProperties.Module_TYPE.MONITOR);
                            map.rData.PNL_RAW_DATA = (mapUI.SubControl as WIN_GlassMapUi).rData.PNL_RAW_DATA;
                            map.mData.PnlValueList = (mapUI.SubControl as WIN_GlassMapUi).mData.PnlValueList;
                            map.rData.PNL_NAME_DATA = (mapUI.SubControl as WIN_GlassMapUi).rData.PNL_NAME_DATA;
                            map.mProperties.MapType = MapProperties.GLS_MAP_TYPE.Contour;
                            //_GlsMap.mData.DefRegionList = this.DefRegionList;
                            map.mData.Raw_Size_X += 20.00F;
                            map.mData.Raw_Size_Y += 20.00F;
                            map.mProperties = (mapUI.SubControl as WIN_GlassMapUi).mProperties;
                            map.ReDrawMap();

                            Image image = S_mapui.GetImage_and_Title();

                            image.Save(filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    frm.Close();
                    MessageBox.Show("Download Comple.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #endregion " Form Events End"
    }
}