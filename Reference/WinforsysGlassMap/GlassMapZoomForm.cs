using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinforsysGlassMap
{
    public partial class GlassMapZoomForm : Form
    {
        #region " Properties & Variables "

        public string processType = string.Empty;
        public MapProperties mProperties { get; set; }

        public RawData GlassRawData { get; set; }
        public List<PinPointData> PinPointList { get; set; }
        public List<MapData.DEF_INFO> DefectList { get; set; }
        public List<MapData.PNL_VALUE> PnlVoltageList { get; set; }
        public List<MapData.DEF_REGION> DefRegionList { get; set; }

        public List<RawData.P_RAW> PnlNameList { get; set; }

        public event EventHandler<DefectPointEventArgs> DefectClickEvent;
        public event EventHandler<DefectTracePointEventArgs> DefectTraceClickEvent;

        private WIN_GlassMapUi _GlsMap = null;

        public string m_countPnl = "";
        private bool flagColor = false;
        private bool flagTitle = false;

        public MapData mData = null;
        public Image FormImage = null;

        private bool allChecked = false;

        public class SheetData
        {
            public string Code = string.Empty;
            public double Qty = 0;
            public Color Color = Color.Transparent;
            public bool Visible = true;
        }

        #endregion " Properties & Variables End"

        #region " Create & Load "

        public GlassMapZoomForm()
        {
            InitializeComponent();

            this.Load += GlassMapZoomForm_Load;
            this.SizeChanged += GlassMapZoomForm_SizeChanged;

            this.uiTrackBar.ValueChanged += uiTrackBar_ValueChanged;
            this.uiNum_DefectSize.ValueChanged += uiNum_DefectSize_ValueChanged;

            this.uiRdo_Black.CheckedChanged += uiRdo_Black_CheckedChanged;
            this.uiRdo_White.CheckedChanged += uiRdo_Black_CheckedChanged;

            this.uiLab_DefColor.Click += UiLab_DefColor_Click;

            this.uiBtn_SaveDefectMap.Click += uiBtn_SaveDefectMap_Click;

            this.FormClosed += GlassMapZoomForm_FormClosed;

            this.uiBtn_ContourMap.Click += UiBtn_ContourMap_Click;
        }

        private void UiBtn_ContourMap_Click(object sender, EventArgs e)
        {
            GlassContourMapForm Gcm = new GlassContourMapForm();
            Gcm.mProperties = mProperties;
            Gcm.GlassRawData = GlassRawData;
            Gcm.DefectList = DefectList;

            Gcm.DefectFilterList = Defect_Filter();
            Gcm.ShowDialog();
        }
        private List<string> Defect_Filter()
        {
            List<string> dft_List = new List<string>();
            for (int row = 0; row < fpSpread1_Sheet1.Rows.Count; row++)
            {
                if ((bool)fpSpread1_Sheet1.Cells[row, 3].Value == false) continue;
                string dft_Code = fpSpread1_Sheet1.Cells[row, 0].Value.ToString();
                if (dft_List.Contains(dft_Code) == false)
                    dft_List.Add(dft_Code);
            }
            if (dft_List.Count == 0)
                return null;
            else
                return dft_List;
        }

        public GlassMapZoomForm(bool flagColor, bool flagTitle)
        {
            InitializeComponent();

            this.flagColor = flagColor;
            this.flagTitle = flagTitle;

            this.Load += GlassMapZoomForm_Load;
            this.SizeChanged += GlassMapZoomForm_SizeChanged;

            this.uiTrackBar.ValueChanged += uiTrackBar_ValueChanged;
            this.uiNum_DefectSize.ValueChanged += uiNum_DefectSize_ValueChanged;

            this.uiRdo_Black.CheckedChanged += uiRdo_Black_CheckedChanged;
            this.uiRdo_White.CheckedChanged += uiRdo_Black_CheckedChanged;

            this.uiBtn_SaveDefectMap.Click += uiBtn_SaveDefectMap_Click;

            this.FormClosed += GlassMapZoomForm_FormClosed;

            this.uiBtn_ContourMap.Click += UiBtn_ContourMap_Click;

        }

        void GlassMapZoomForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.mProperties.MapType != MapProperties.GLS_MAP_TYPE.Contour)
                GetImage();

            _GlsMap.mProperties.SHOW_ZOOM_MAP = false;
        }

        void GlassMapZoomForm_Load(object sender, EventArgs e)
        {
            if (_GlsMap == null)
                InitFrm();

            _GlsMap.mData.DefList = this.DefectList;

            InitSpread();
            UpdateSpread();

            if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.Defect)
                uiBtn_ContourMap.Visible = true;
            else
                uiBtn_ContourMap.Visible = false;
        }

        #endregion " Create & Load End"

        #region "  Public methode "

        public void Reset(RawData rawData, MapProperties.Module_TYPE type)
        {
            if (_GlsMap == null)
                InitFrm();

            _GlsMap.Reset(rawData, type);
        }

        public Image GetImage()
        {
            Bitmap bMap = null;

            using (Graphics g = this.CreateGraphics())
            {
                uiPan_GlassMap.BackgroundImage = _GlsMap.GetMapImage();
                _GlsMap.Visible = false;

                Bitmap memBitMap = new Bitmap(this.uiTpnl_Main.Width, this.uiTpnl_Main.Height);
                this.uiTpnl_Main.DrawToBitmap(memBitMap, new Rectangle(0, 0, this.uiTpnl_Main.Width, this.uiTpnl_Main.Height));
                g.DrawImageUnscaled(memBitMap, 0, 0);
                bMap = memBitMap;

                _GlsMap.Visible = true;
                uiPan_GlassMap.BackgroundImage = null;
            }

            this.FormImage = (Image)bMap;
            return this.FormImage;
        }

        public Image GetMapImage()
        {
            return _GlsMap.GetMapImage();
        }

        #endregion "  Public methode End"

        #region "  Private methode "

        private void InitFrm()
        {
            //Map 설정
            _GlsMap = new WIN_GlassMapUi();
            _GlsMap.Dock = DockStyle.None;
            _GlsMap.Size = uiPan_GlassMap.Size;
            _GlsMap.mProperties = this.mProperties;

            if (_GlsMap.mProperties == null)
                _GlsMap.mProperties = new MapProperties();

            _GlsMap.mProperties.SHOW_ZOOM_MAP = true;

            if (uiRdo_White.Checked == true)
                _GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.White;
            else
                _GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.Black;

            _GlsMap.DefectClickEvent += this.DefectClickEvent;
            _GlsMap.DefectTraceClickEvent += this.DefectTraceClickEvent;
            _GlsMap.MouseMove += _GlsMap_MouseMove;

            uiPan_GlassMap.AutoScroll = true;
            uiPan_GlassMap.Controls.Add(_GlsMap);

            //Zoom
            uiTrackBar.Maximum = 30;
            uiTrackBar.Minimum = 10;
            uiTrackBar.TickFrequency = 1;
            uiTrackBar.Value = 10;

            uiLab_ZoomRate.Text = "x1.0";

            if (mProperties == null)
                mProperties = _GlsMap.mProperties;

            uiNum_DefectSize.Value = mProperties.Default_DefectSize;

            this.Reset(this.mProperties.ModuleType);
        }

        private void Reset(MapProperties.Module_TYPE mType)
        {
            _GlsMap.Reset(this.GlassRawData, mType);

            _GlsMap.mData.DefList = this.DefectList;
            _GlsMap.mData.PinPointDataList = this.PinPointList;
            _GlsMap.mData.PnlValueList = this.PnlVoltageList;
            _GlsMap.mData.DefRegionList = this.DefRegionList;
            _GlsMap.rData.PNL_NAME_DATA = this.PnlNameList;

            _GlsMap.ReDrawMap();

            InitLotList();
            InitGlassList();

            uiCcmb_GlassList.SelectedIndexChanged -= UiCcmb_GlassList_SelectedIndexChanged;
            uiCcmb_GlassList.SelectedIndexChanged += UiCcmb_GlassList_SelectedIndexChanged;
        }

        private void InitLotList()
        {
            List<string> lotList = new List<string>();

            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                object pnlId = null;

                if (_GlsMap.mData.DefList[i].DataList == null || _GlsMap.mData.DefList[i].DataList.Count == 0)
                    continue;

                if(_GlsMap.mData.DefList[i].DataList.ContainsKey("PNL ID"))
                    _GlsMap.mData.DefList[i].DataList.TryGetValue("PNL ID", out pnlId);

                if (_GlsMap.mData.DefList[i].DataList.ContainsKey("Panel ID"))
                    _GlsMap.mData.DefList[i].DataList.TryGetValue("Panel ID", out pnlId);

                if (pnlId != null && pnlId.ToString().Length > 10)
                {
                    string lotId = pnlId.ToString().Substring(0, 10);

                    if (lotList.Contains(lotId) == false)
                        lotList.Add(lotId);
                }
            }

            lotList.Sort();
            uiCcmb_LotList.Reset(lotList.ToArray());

            uiCcmb_LotList.SelectedIndexChanged -= UiCcmb_LotList_SelectedIndexChanged;
            uiCcmb_LotList.SelectedIndexChanged += UiCcmb_LotList_SelectedIndexChanged;
        }

        private void InitGlassList()
        {
            List<string> glassList = new List<string>();

            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];
                object pnlId = null;

                if (def.DataList == null || def.DataList.Count == 0)
                    continue;

                if (def.DataList.ContainsKey("PNL ID"))
                    def.DataList.TryGetValue("PNL ID", out pnlId);

                if (def.DataList.ContainsKey("Panel ID"))
                    def.DataList.TryGetValue("Panel ID", out pnlId);

                if (pnlId != null && pnlId.ToString().Length > 12)
                {
                    string glassId = pnlId.ToString().Substring(0, 12);

                    if(uiCcmb_LotList.Text == "(ALL)")
                    {
                        if (glassList.Contains(glassId) == false)
                            glassList.Add(glassId);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(uiCcmb_LotList.Text) == false)
                        {
                            string lotData = uiCcmb_LotList.Text.Replace("'", "");
                            var lots = lotData.Split(',');

                            if (lots.Contains(glassId.Substring(0, 10)))
                                glassList.Add(glassId);
                        }
                    }
                }
            }

            glassList.Sort();

            uiCcmb_GlassList.Reset(glassList.Distinct().ToArray());
        }

        private void UiCcmb_LotList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];
                object pnlId = null;

                if(def.DataList.ContainsKey("PNL ID"))
                    def.DataList.TryGetValue("PNL ID", out pnlId);

                if (def.DataList.ContainsKey("Panel ID"))
                    def.DataList.TryGetValue("Panel ID", out pnlId);

                if (pnlId != null && pnlId.ToString().Length > 10)
                {
                    string lotId = pnlId.ToString().Substring(0, 10);

                    if (uiCcmb_LotList.Text == "(ALL)")
                        def.Visible = def.CodeVisible;
                    else if (uiCcmb_LotList.Text.Contains(lotId) == false)
                        def.Visible = false;
                    else if (uiCcmb_LotList.Text.Contains(lotId) == true)
                        def.Visible = def.CodeVisible;
                }
            }

            uiCcmb_GlassList.SelectedIndexChanged -= UiCcmb_GlassList_SelectedIndexChanged;
            InitGlassList();
            uiCcmb_GlassList.SelectedIndexChanged += UiCcmb_GlassList_SelectedIndexChanged;

            _GlsMap.ReDrawMap();

            this.Cursor = Cursors.Default;
        }

        private void UiCcmb_GlassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];
                object pnlId = null;

                if (def.DataList.ContainsKey("PNL ID"))
                    def.DataList.TryGetValue("PNL ID", out pnlId);

                if (def.DataList.ContainsKey("Panel ID"))
                    def.DataList.TryGetValue("Panel ID", out pnlId);

                if (pnlId != null && pnlId.ToString().Length > 12)
                {
                    string lotId = pnlId.ToString().Substring(0, 10);
                    string glassId = pnlId.ToString().Substring(0, 12);

                    if (uiCcmb_LotList.Text.Contains(lotId) == true &&
                        uiCcmb_GlassList.Text == "(ALL)")
                        def.Visible = def.CodeVisible;
                    else if (uiCcmb_LotList.Text == "(ALL)" &&
                        uiCcmb_GlassList.Text == "(ALL)")
                        def.Visible = def.CodeVisible;
                    else if (uiCcmb_LotList.Text == "(ALL)" &&
                        uiCcmb_GlassList.Text.Contains(glassId) == true)
                        def.Visible = def.CodeVisible;
                    else if (uiCcmb_LotList.Text.Contains(lotId) == true &&
                             uiCcmb_GlassList.Text.Contains(glassId) == true)
                        def.Visible = def.CodeVisible;
                    else
                        def.Visible = false;
                }
            }

            _GlsMap.ReDrawMap();

            this.Cursor = Cursors.Default;
        }

        private void InitSpread()
        {
            fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

            fpSpread1.CellClick -= FpSpread1_CellClick;
            fpSpread1.CellClick += FpSpread1_CellClick;
            fpSpread1.ButtonClicked += FpSpread1_ButtonClicked;

            fpSpread1.SetCursor(FarPoint.Win.Spread.CursorType.Normal, Cursors.Arrow);

            FarPoint.Win.Spread.SheetView sheet = fpSpread1_Sheet1;

            //Sheet1            
            //sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;        

            sheet.RowCount = 0;
            sheet.ColumnHeader.RowCount = 1;
            sheet.ColumnHeader.Rows[0].Height = 35;

            sheet.ColumnHeader.Columns.Count = 4;

            int nCols;
            int nCol = 0;

            nCols = sheet.ColumnHeader.Columns.Count - 1;
            sheet.Columns[0, nCols].Width = 50;

            FarPoint.Win.Spread.CellType.NumberCellType nCell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nCell.DecimalPlaces = 0;
            FarPoint.Win.Spread.CellType.CheckBoxCellType cbCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            sheet.ColumnHeader.Cells[0, nCol++].Value = "DFT Code";
            sheet.Columns[nCol - 1].Width = 85;
            sheet.ColumnHeader.Cells[0, nCol++].Value = "Qty";
            sheet.Columns[nCol - 1].CellType = nCell;
            sheet.ColumnHeader.Cells[0, nCol++].Value = "Color";
            sheet.ColumnHeader.Cells[0, nCol++].Value = "Visible";
            sheet.Columns[nCol - 1].Width = 50;

            sheet.Columns.Get(3).CellType = cbCell;
            sheet.ColumnHeader.Cells[0, 3].Value = true;

            sheet.FrozenColumnCount = 3;

            sheet.Columns[0, nCols].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Columns[0, nCols].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        }

        private void UpdateSpread()
        {
            List<SheetData> defSheetList = new List<SheetData>();

            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];

                SheetData sd = defSheetList.Find(x => x.Code == def.DEF_Code);

                if (sd == null)
                {
                    sd = new SheetData();
                    sd.Code = def.DEF_Code;
                    sd.Color = def.DEF_Color;
                    sd.Visible = def.Visible;
                    sd.Qty = 1;

                    defSheetList.Add(sd);
                }
                else
                {
                    sd.Qty += 1;
                }
            }

            FarPoint.Win.Spread.SheetView sheet = fpSpread1_Sheet1;
            sheet.Rows.Count = 0;

            foreach (SheetData data in defSheetList.OrderByDescending(x => x.Qty))
            {
                int row = sheet.Rows.Count++;

                sheet.Cells[row, 0].Value = data.Code;
                sheet.Cells[row, 1].Value = data.Qty;
                sheet.Cells[row, 2].Value = "COLOR";
                sheet.Cells[row, 2].ForeColor = Color.White;
                sheet.Cells[row, 2].BackColor = data.Color;
                sheet.Cells[row, 3].Value = data.Visible;
            }
        }

        private void Change_DEF_Properties(string code, Color codeColor, bool codeVisible)
        {
            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];

                if (def.DEF_Code == code)
                {
                    def.DEF_Color = codeColor;
                    def.CodeVisible = codeVisible;
                    def.Visible = codeVisible;
                }
            }

            _GlsMap.ReDrawMap();
        }

        private void Change_DEF_Properties(string code, Color codeColor, bool codeVisible, bool reDraw)
        {
            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = _GlsMap.mData.DefList[i];

                if (def.DEF_Code == code)
                {
                    def.DEF_Color = codeColor;
                    def.CodeVisible = codeVisible;
                    def.Visible = codeVisible;
                }
            }
        }

        #endregion "  Private methode End"

        #region " Form Events "
        private void FpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Color
            if (e.Column == 2 && e.ColumnHeader == false)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    fpSpread1_Sheet1.Cells[e.Row, 2].BackColor = colorDialog1.Color;

                    string code = fpSpread1_Sheet1.Cells[e.Row, 0].Value.ToString();
                    Color cColor = (Color)fpSpread1_Sheet1.Cells[e.Row, 2].BackColor;
                    bool visible = (bool)fpSpread1_Sheet1.Cells[e.Row, 3].Value;

                    Change_DEF_Properties(code, cColor, visible);
                }
            }

            // Check 컬럼헤더 클릭
            if (e.ColumnHeader == true && e.Column == 3)
            {
                SetAllCheckBox();
            }
        }

        private void FpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 3)
            {
                string code = fpSpread1_Sheet1.Cells[e.Row, 0].Value.ToString();
                Color cColor = (Color)fpSpread1_Sheet1.Cells[e.Row, 2].BackColor;
                bool visible = (bool)fpSpread1_Sheet1.Cells[e.Row, 3].Value;

                Change_DEF_Properties(code, cColor, visible);
            }
        }

        void uiTrackBar_ValueChanged(object sender, EventArgs e)
        {
            double rate = (double)Math.Round((double)uiTrackBar.Value / 10, 2);
            int sizeX = (int)(uiPan_GlassMap.Size.Width * rate);
            int sizeY = (int)(uiPan_GlassMap.Size.Height * rate);
            Size s = new Size(sizeX, sizeY);
            _GlsMap.Size = s;

            uiLab_ZoomRate.Text = string.Format("x{0:0.0}", rate);

            this.Reset(this.mProperties.ModuleType);
        }

        void GlassMapZoomForm_SizeChanged(object sender, EventArgs e)
        {
            if (uiPan_GlassMap == null || _GlsMap == null) return;

            double rate = uiTrackBar.Value / 10;
            int sizeX = (int)(uiPan_GlassMap.Size.Width * rate);
            int sizeY = (int)(uiPan_GlassMap.Size.Height * rate);
            Size s = new Size(sizeX, sizeY);
            _GlsMap.Size = s;

            this.Reset(this.mProperties.ModuleType);
        }

        void uiNum_DefectSize_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
            {
                _GlsMap.mData.DefList[i].Manual_Size = (int)uiNum_DefectSize.Value;
            }

            _GlsMap.mProperties.Default_DefectSize = (int)uiNum_DefectSize.Value;
            _GlsMap.ReDrawMap();
        }

        void _GlsMap_MouseMove(object sender, MouseEventArgs e)
        {
            uiLab_PosX.Text = string.Format("X [{0}]", _GlsMap.MouseCoord_X);
            uiLab_PosY.Text = string.Format("Y [{0}]", _GlsMap.MouseCoord_Y);
        }

        void uiRdo_Black_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;

            if (btn.Checked == false) return;

            if (uiRdo_White.Checked == true)
                _GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.White;
            else
                _GlsMap.mProperties.ColorType = MapProperties.GLS_COLOR_TYPE.Black;

            _GlsMap.ReDrawMap();
        }

        private void SetAllCheckBox()
        {
            this.Cursor = Cursors.WaitCursor;

            if (allChecked == false && (bool)fpSpread1_Sheet1.Cells[0, 3].Value == false)
            {
                fpSpread1_Sheet1.ColumnHeader.Cells[0, 3].Value = true;

                for (int row = 0; row < fpSpread1_Sheet1.Rows.Count; row++)
                {
                    string code = fpSpread1_Sheet1.Cells[row, 0].Value.ToString();
                    Color cColor = (Color)fpSpread1_Sheet1.Cells[row, 2].BackColor;
                    fpSpread1_Sheet1.Cells[row, 3].Value = true;

                    Change_DEF_Properties(code, cColor, (bool)fpSpread1_Sheet1.Cells[row, 3].Value, false);
                    allChecked = true;
                }
            }
            else
            {
                fpSpread1_Sheet1.ColumnHeader.Cells[0, 3].Value = false;

                for (int row = 0; row < fpSpread1_Sheet1.Rows.Count; row++)
                {
                    string code = fpSpread1_Sheet1.Cells[row, 0].Value.ToString();
                    Color cColor = (Color)fpSpread1_Sheet1.Cells[row, 2].BackColor;
                    fpSpread1_Sheet1.Cells[row, 3].Value = false;

                    Change_DEF_Properties(code, cColor, (bool)fpSpread1_Sheet1.Cells[row, 3].Value, false);
                    allChecked = false;
                }
            }

            _GlsMap.ReDrawMap();

            this.Cursor = Cursors.Default;
        }

        private void UiLab_DefColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < _GlsMap.mData.DefList.Count; ++i)
                {
                    _GlsMap.mData.DefList[i].DEF_Color = colorDialog1.Color;

                    if (i == 0)
                        uiLab_DefColor.BackColor = colorDialog1.Color;
                }

                _GlsMap.ReDrawMap();
            }
        }

        public void uiBtn_SaveDefectMap_Click(object sender, EventArgs e)
        {
            if (uiPan_GlassMap.Controls.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "JPG Files | *.jpg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    WIN_GlassMapUi map = uiPan_GlassMap.Controls[0] as WIN_GlassMapUi;
                    Image image = map.GetMapImage();
                    image.Save(sfd.FileName);

                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Saved Complete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Defect Map to save does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion " Form Events End"
    }
}
