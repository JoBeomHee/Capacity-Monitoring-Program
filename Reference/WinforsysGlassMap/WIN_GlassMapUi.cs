using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using static WinforsysGlassMap.MapData;

namespace WinforsysGlassMap
{
    public class WIN_GlassMapUi : Control
    {
        #region " Properties & Variables "

        public string processType = string.Empty;

        public event EventHandler<DefectPointEventArgs> DefectClickEvent;
        public event EventHandler<DefectTracePointEventArgs> DefectTraceClickEvent;

        public Object DetailData { get; set; }

        public MapProperties mProperties = new MapProperties();
        public MapData mData = null;

        public double MouseCoord_X = 0;
        public double MouseCoord_Y = 0;

        public RawData rData = null;

        private bool IsMapStateOk = false;

        private Form dragFrm = null;
        private Point clickPoint = new Point(0, 0);

        // TMS_Defect_Monitor 에서 사용하는 변수
        public String m_countPnl = "";
        private bool flagB7_GlassZoomForm = false;

        public string defectMapPath = "";

        public Defect_Map_Info Map_Infor { get; set; }

        #endregion " Properties & Variables End"

        #region " Create & Load "

        public WIN_GlassMapUi()
        {
            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.FixedWidth, true);

            this.Dock = DockStyle.Fill;

            this.SizeChanged += WinforsysGlassMap_SizeChanged;

            this.MouseClick += WinforsysGlassMap_MouseClick;

            //Drag Form 관련 기능
            this.MouseDown += WinforsysGlassMap_MouseDown;
            this.MouseMove += WinforsysGlassMap_MouseMove;
            this.MouseUp += WinforsysGlassMap_MouseUp;

            mData = new MapData(null, this.mProperties);
        }

        public WIN_GlassMapUi(bool flag)
        {
            this.flagB7_GlassZoomForm = flag;

            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.FixedWidth, true);

            this.Dock = DockStyle.Fill;

            this.SizeChanged += WinforsysGlassMap_SizeChanged;

            this.MouseClick += WinforsysGlassMap_MouseClick;

            //Drag Form 관련 기능
            this.MouseDown += WinforsysGlassMap_MouseDown;
            this.MouseMove += WinforsysGlassMap_MouseMove;
            this.MouseUp += WinforsysGlassMap_MouseUp;

            mData = new MapData(null, this.mProperties);
        }

        #endregion " Create & Load End"

        #region "  Public methode "

        public void clearMouseEvent()
        {
            this.MouseDown -= WinforsysGlassMap_MouseDown;
            this.MouseMove -= WinforsysGlassMap_MouseMove;
            this.MouseUp -= WinforsysGlassMap_MouseUp;
            this.MouseClick -= WinforsysGlassMap_MouseClick;
        }

        public void Reset(RawData rawData, MapProperties.Module_TYPE type)
        {
            try
            {
                this.rData = rawData;
                this.mData = new MapData(rData, this.mProperties);

                this.mProperties.ModuleType = type;
                this.mProperties.SetColorType();
                this.mData.Translate(new Point(this.Size.Width, this.Size.Height));

                DoubleBuffer();

                IsMapStateOk = true;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Can not create Glass Map.\nReason - {0}", ex.Message);
                //MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Draw_ErrorMessage(msg);

                IsMapStateOk = false;
            }
        }
        public void Reset(string product_Id, MapProperties.Module_TYPE type)
        {
            try
            {
                this.rData = new RawData();
                this.rData.Load(product_Id);
                this.mData = new MapData(rData, this.mProperties);

                this.mProperties.ModuleType = type;
                this.mProperties.SetColorType();
                this.mData.Translate(new Point(this.Size.Width, this.Size.Height));

                //DoubleBuffer();

                IsMapStateOk = true;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Can not create Glass Map.\nReason - {0}", ex.Message);
                //MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Draw_ErrorMessage(msg);

                IsMapStateOk = false;
            }
        }

        public void Reset(string product_Id, string dept, MapProperties.Module_TYPE type)
        {
            try
            {
                this.rData = new RawData();
                this.rData.Load(product_Id, dept);
                this.mData = new MapData(rData, this.mProperties);

                this.mProperties.ModuleType = type;
                this.mProperties.SetColorType();
                this.mData.Translate(new Point(this.Size.Width, this.Size.Height));

                //DoubleBuffer();

                IsMapStateOk = true;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Can not create Glass Map.\nReason - {0}", ex.Message);
                //MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Draw_ErrorMessage(msg);

                IsMapStateOk = false;
            }
        }

        public void ResetFromSeq(string glsSeq, MapProperties.Module_TYPE type)
        {
            try
            {
                this.rData = new RawData();
                this.rData.LoadFormGlsSeq(glsSeq);
                this.mData = new MapData(rData, this.mProperties);

                this.mProperties.ModuleType = type;
                this.mProperties.SetColorType();
                this.mData.Translate(new Point(this.Size.Width, this.Size.Height));

                //DoubleBuffer();

                IsMapStateOk = true;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Can not create Glass Map.\nReason - {0}", ex.Message);
                //MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Draw_ErrorMessage(msg);

                IsMapStateOk = false;
            }
        }

        public void Defect_ADD(List<MapData.DEF_INFO> defList)
        {
            if (IsMapStateOk == false) return;

            mData.DefList = defList;

            mData.Translate(new Point(this.Size.Width, this.Size.Height));
        }

        public void SetDefect(int seq, Color defColor, Point defSize)
        {
            for (int i = 0; i < mData.DefList.Count; ++i)
            {
                MapData.DEF_INFO def = mData.DefList[i];

                if (def.Seq == seq)
                {
                    def.DEF_Color = defColor;
                    def.Manual_Size = defSize.X;

                    break;
                }
            }
        }

        public Image GetMapImage()
        {
            Bitmap bMap = null;

            using (Graphics g = this.CreateGraphics())
            {
                Bitmap memBitMap = new Bitmap(this.Width, this.Height);

                Draw_Map(memBitMap);

                g.DrawImageUnscaled(memBitMap, 0, 0);

                bMap = memBitMap;
            }

            return (Image)bMap;
        }

        public void ReDrawMap()
        {
            if (IsMapStateOk == false)
            {
                Draw_ErrorMessage("Need to Map Data");
                return;
            }

            mProperties.SetColorType();
            mData.Translate(new Point(this.Size.Width, this.Size.Height));

            DoubleBuffer();
        }

        public void AddPinPoint(PinPointData data)
        {
            mData.PinPointDataList.Add(data);
        }

        public List<PinPointData> GetPinPointData()
        {
            return mData.PinPointDataList;
        }

        #endregion "  Public methode End"

        #region "  Private methode "

        private void DoubleBuffer()
        {
            try
            {
                if (mData == null) return;

                if (this.Width == 0 || this.Height == 0) return;

                using (Graphics g = this.CreateGraphics())
                {
                    Bitmap memBitMap = new Bitmap(this.Width, this.Height);

                    Draw_Map(memBitMap);

                    g.DrawImageUnscaled(memBitMap, 0, 0);

                    memBitMap.Dispose();
                    memBitMap = null;
                }
            }
            catch (Exception ex)
            {
                Draw_ErrorMessage(ex.Message);
            }
        }

        private void Draw_Map(Bitmap memBitMap)
        {
            Draw_Gls(memBitMap);
            Draw_CELL(memBitMap);

            if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.Defect)
            {
                Draw_DEF(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.Bubble)
            {
                Draw_Bubble(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.PinPoint)
            {
                Draw_PinPoint(memBitMap);

                if (mData.DefRegionList != null)
                    Draw_DefRegion(memBitMap);

                if (mData.DefList != null)
                    Draw_DEF(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.PnlVoltage)
            {
                Draw_Voltage(memBitMap);
                Draw_DEF(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.EPM)
            {
                Draw_EPMGlass(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.Pnl_Color)
            {
                Draw_Pnl_Color(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.NEW_Bubble)
            {
                Draw_New_Bubble(memBitMap);
            }
            else if (mProperties.MapType == MapProperties.GLS_MAP_TYPE.Contour)
            {
                Draw_Pnl_Color(memBitMap);
                if(rData.PNL_NAME_DATA.Count >0)
                {
                    List<CELL_INFO> PanelNameList = new List<CELL_INFO>();
                    foreach (RawData.P_RAW data in rData.PNL_NAME_DATA)
                    {
                        CELL_INFO info = mData.Translate_Cell(data);

                        info.Cell_Text = data.PNL_ID;
                        info.TAG = data.TAG;

                        PanelNameList.Add(info);
                    }
                    Draw_PanelName(memBitMap, PanelNameList);
                }                
            }
        }

        private void Draw_ErrorMessage(string msg)
        {
            using (Graphics g = this.CreateGraphics())
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                SolidBrush pnlIdBr = new SolidBrush(Color.Purple);

                SolidBrush mapBackBr = new SolidBrush(mProperties.Colors.MapBackgroundColor);
                g.FillRectangle(mapBackBr, this.ClientRectangle);

                float fSize = 12F;
                if (fSize == 0) fSize = 1;
                Font ft = new Font(this.Font.FontFamily, fSize);

                g.DrawString(msg, ft, pnlIdBr, this.ClientRectangle, format);
            }
        }

        private void Draw_Gls(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                SolidBrush mapBackBr = new SolidBrush(mProperties.Colors.MapBackgroundColor);
                g.FillRectangle(mapBackBr, this.ClientRectangle);

                Pen linePen = new Pen(mProperties.Colors.GlassLineColor);
                g.DrawRectangle(linePen, mData.GLS_Rect);
            }
        }

        private void Draw_CELL(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                Pen linePen = new Pen(mProperties.Colors.CellLineColor);

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                //SolidBrush pnlIdBr = new SolidBrush(Color.Purple);
                SolidBrush pnlIdBr = new SolidBrush(Color.FromArgb(100, 150, 150, 0));

                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO info = mData.CellList[i];
                    g.DrawRectangle(linePen, info.Cell_Rect);

                    float fSize = (int)(info.Cell_Rect.Size.Width / 3.5);

                    if (fSize > 0 && fSize * 2 >= info.Cell_Rect.Height) fSize = info.Cell_Rect.Size.Width / (float)5.5;
                    if (fSize == 0) fSize = 1;

                    Font ft = new Font(this.Font.FontFamily, fSize);

                    string cid = info.Cell_Text;
                    if (info.Cell_Text.Length > 5)
                        cid = string.Format("{0}\r\n{1}", info.Cell_Text.Substring(0, 3), info.Cell_Text.Substring(3, 2));

                    if (this.mProperties.MapType != MapProperties.GLS_MAP_TYPE.PinPoint && 
                        this.mProperties.MapType != MapProperties.GLS_MAP_TYPE.Contour)
                    {
                        g.DrawString(cid, ft, pnlIdBr, info.Cell_Rect, format);
                    }
                    else if(this.mProperties.MapType == MapProperties.GLS_MAP_TYPE.Contour)
                    {
                        g.DrawString("", ft, pnlIdBr, info.Cell_Rect, format);
                    }
                    else
                    {
                        g.DrawString(cid, ft, pnlIdBr, info.Cell_Rect, format);
                    }
                }

                SolidBrush CheckBr = new SolidBrush(Color.Green);

                double GLS_width = mData.GLS_Rect.Width;
                double GLS_height = mData.GLS_Rect.Height;

                int glsXpoint = mData.GLS_Rect.X;
                int glsYpoint = mData.GLS_Rect.Y;

                double Ploygon_with = 0f;
                double Ploygon_height = 0f;

                if (GLS_width != 0) //GLS_width가 0인 경우, 0 나눌때 에러 발생
                {
                    Ploygon_with = GLS_width / mData.CellList[0].Cell_Rect.Size.Width * 0.8;
                    Ploygon_height = GLS_height - Ploygon_with;
                }

                Point[] PolyP =
                {
                    new Point ((int)(GLS_width - Ploygon_with) + glsXpoint ,  (int)GLS_height + glsYpoint),
                    new Point ((int)GLS_width + glsXpoint , (int)Ploygon_height + glsYpoint),
                    new Point ((int)GLS_width + glsXpoint , (int)GLS_height + glsYpoint)
                };

                g.FillPolygon(CheckBr, PolyP);
            }
        }

        private void Draw_DEF(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                if (mData.DefList == null) return;

                for (int i = 0; i < mData.DefList.Count; ++i)
                {
                    MapData.DEF_INFO def = mData.DefList[i];

                    if (def.Visible == false) continue;

                    SolidBrush br = new SolidBrush(def.DEF_Color);

                    if (def.DEF_SHARF == E_DFT_SHARF.CIRCLE)
                        g.FillEllipse(br, def.DEF_Rect);
                    else if (def.DEF_SHARF == E_DFT_SHARF.SQUARE)
                        g.FillRectangle(br, def.DEF_Rect);
                    else if (def.DEF_SHARF == E_DFT_SHARF.TRIANGLE)
                    {
                        Point[] points = new Point[] {
                            new Point(def.DEF_Rect.X, def.DEF_Rect.Y + def.DEF_Rect.Height),
                            new Point(def.DEF_Rect.X + (def.DEF_Rect.Width / 2), def.DEF_Rect.Y),
                            new Point(def.DEF_Rect.X + def.DEF_Rect.Width, def.DEF_Rect.Y + def.DEF_Rect.Height) };
                        g.FillPolygon(br, points);
                    }                     
                }
            }
        }

        private void Draw_Bubble(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                Image bubbleImg = null;
                SolidBrush valueBrush = null;

                if (mProperties.ColorType == MapProperties.GLS_COLOR_TYPE.White)
                {
                    valueBrush = new SolidBrush(Color.Red);
                    bubbleImg = Properties.Resources.BG_Bubble02;
                }
                else
                {
                    valueBrush = new SolidBrush(Color.Blue);
                    bubbleImg = Properties.Resources.BG_Bubble;
                }

                //bubble Map 비율 가져오기
                double totalCount = 0;
                double maxCount = 0;

                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO info = mData.CellList[i];

                    if (info.DefList.Count > maxCount) maxCount = info.DefList.Count;

                    totalCount += info.DefList.Count;
                }

                double maxRate = Math.Round(maxCount / totalCount, 4) * 100;
                int sizePoint = (int)Math.Ceiling((double)mData.CellList[0].Cell_Rect.Height / maxRate);

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO info = mData.CellList[i];

                    if (info.DefList.Count == 0) continue;

                    double m = Math.Round((double)info.DefList.Count / totalCount, 4) * 100;

                    Size bSize = new Size((int)(sizePoint * m), (int)(sizePoint * m));

                    Rectangle pRect = info.Cell_Rect;

                    if (bSize.Width > pRect.Width) bSize.Width = pRect.Width;
                    if (bSize.Height > pRect.Height) bSize.Height = pRect.Height;

                    Point center = new Point(pRect.X + pRect.Width / 2, pRect.Y + pRect.Height / 2);

                    Rectangle bubbleRect = new Rectangle(center.X - bSize.Width / 2, center.Y - bSize.Height / 2, bSize.Width, bSize.Height);
                    g.DrawImage(bubbleImg, bubbleRect);

                    //값 넣기
                    float fSize = pRect.Size.Width / 2;
                    if (fSize == 0) fSize = 1;
                    else if (fSize > 10) fSize = 10;

                    Font ft = new Font(this.Font.FontFamily, fSize);
                    g.DrawString(info.DefList.Count.ToString(), ft, valueBrush, pRect, format);
                }
            }
        }

        private void Draw_New_Bubble(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                List<MapData.DEF_INFO> bubbleList = new List<MapData.DEF_INFO>();

                for (int i = 0; i < mData.DefList.Count; ++i)
                {
                    MapData.DEF_INFO def = mData.DefList[i];

                    bool isContain = false;

                    for (int k = 0; k < bubbleList.Count; ++k)
                    {
                        if (Math.Abs(bubbleList[k].Raw_Point_X - def.Raw_Point_X) < 1000 && Math.Abs(bubbleList[k].Raw_Point_Y - def.Raw_Point_Y) < 1000)
                        {
                            if (def.DEF_Code == "X")
                            {
                                bubbleList[k].Manual_Size += 1;
                                bubbleList[k].X_Cnt += 1;
                            }
                            else if (def.DEF_Code == "O" || def.DEF_Code == "0")
                            {
                                bubbleList[k].Manual_Size += 1;
                                bubbleList[k].O_Cnt += 1;
                            }
                            else if (def.DEF_Code == "F")
                            {
                                bubbleList[k].Manual_Size += 1;
                                bubbleList[k].F_Cnt += 1;
                            }

                            if (bubbleList[k].X_Cnt > 0 || bubbleList[k].F_Cnt > 0)
                            {
                                int t_cnt = bubbleList[k].O_Cnt + bubbleList[k].X_Cnt + bubbleList[k].F_Cnt;
                                double OK_Rate = (bubbleList[k].O_Cnt / (double)t_cnt);
                                double NO_Rate = (bubbleList[k].X_Cnt / (double)(bubbleList[k].X_Cnt + bubbleList[k].F_Cnt));
                                Color DftColor = Color.FromArgb(255, 255, 255, 255);
                                if (OK_Rate > 0.5)
                                {
                                    if (NO_Rate > 0.5)
                                        DftColor = Color.FromArgb(255, 255, 153, 153);
                                    else
                                        DftColor = Color.FromArgb(255, 102, 204, 255);
                                }
                                else
                                {
                                    if (NO_Rate > 0.5)
                                        DftColor = Color.FromArgb(255, 255, 51, 51);
                                    else
                                        DftColor = Color.FromArgb(255, 51, 51, 255);
                                }

                                bubbleList[k].DEF_Color = DftColor;
                            }


                            bubbleList[k].MergeList.Add(def.MergeList[0]);

                            isContain = true;
                            break;
                        }
                    }

                    if (isContain == false)
                    {
                        def.Manual_Size = 0;
                        def.DEF_Color = Color.Silver;

                        if (def.DEF_Code == "X")
                        {
                            def.Manual_Size += 10;
                            def.X_Cnt = 1;
                            def.DEF_Color = Color.Red;
                        }
                        else if (def.DEF_Code == "F")
                        {
                            def.Manual_Size += 10;
                            def.F_Cnt = 1;
                            def.DEF_Color = Color.Blue;
                        }
                        else if (def.DEF_Code == "O" || def.DEF_Code == "0")
                        {
                            def.Manual_Size += 10;
                            def.O_Cnt = 1;
                        }

                        bubbleList.Add(def);
                    }
                }

                mData.DefList = bubbleList;
                mProperties.MapType = MapProperties.GLS_MAP_TYPE.Defect;
                //mData.Translate(new Point(this.Size.Width, this.Size.Height));
                //Draw_DEF(map);
            }
        }

        private void Draw_PinPoint(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                if (mData.PinPointDataList == null) return;

                for (int i = 0; i < mData.PinPointDataList.Count; ++i)
                {
                    PinPointData pData = mData.PinPointDataList[i];

                    Color tmpColor = pData.Point_Color;
                    tmpColor = Color.FromArgb(150, pData.Point_Color.R, pData.Point_Color.G, pData.Point_Color.B);
                    Pen linePen = new Pen(pData.Point_Color);
                    SolidBrush br = new SolidBrush(tmpColor);

                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    for (int k = 0; k < pData.PinPointList.Count; ++k)
                    {
                        Rectangle rect = pData.PinPointList[k];

                        if (pData.RawDataList[k].Visible == false) continue;

                        //직사각형태일때는 무조건 사각형으로 그리도록 변경 LJK_20180202
                        if (pData.Point_Shape == 0 && rect.Width == rect.Height)
                        {
                            g.FillEllipse(br, rect);
                        }
                        else if (pData.Point_Shape == 2 && rect.Width == rect.Height)
                        {
                            Point a = new Point(rect.X, rect.Y + rect.Height);
                            Point b = new Point(rect.X + (rect.Width / 2), rect.Y);
                            Point c = new Point(rect.X + rect.Width, rect.Y + rect.Height);

                            Point[] points = new Point[] { a, b, c, };
                            g.FillPolygon(br, points);
                        }
                        else
                        {
                            g.FillRectangle(br, rect);
                        }

                        float fSize = this.Width / 100;
                        if (fSize == 0) fSize = 1;
                        Font ft = new Font(this.Font.FontFamily, fSize);

                        string eqpName = pData.Equipment_Name;

                        Point sPoint = new Point(rect.X + (rect.Width), rect.Y + (rect.Height / 2));
                        SizeF strSize = g.MeasureString(eqpName, ft);

                        Rectangle strRect = new Rectangle(sPoint.X, sPoint.Y, (int)strSize.Width, (int)strSize.Height);

                        if (this.mProperties.SHOW_PINPOINT == true)
                            g.DrawString(eqpName, ft, br, strRect, format);
                    }
                }
            }
        }

        private void Draw_Voltage(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                SolidBrush penBr = new SolidBrush(Color.White);

                Dictionary<string, int> avgList = new Dictionary<string, int>();

                int min = int.MaxValue;
                int max = int.MinValue;

                //계산하기
                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO cell = mData.CellList[i];

                    int val = 0;
                    int cnt = 0;

                    for (int k = 0; k < mData.PnlValueList.Count; ++k)
                    {
                        MapData.PNL_VALUE vol = mData.PnlValueList[k];

                        if (cell.Cell_Text == vol.PNL_ID)
                        {
                            val += (int)vol.PNL_VAL_01;
                            cnt++;
                        }
                    }

                    if (cnt > 0)
                    {
                        int avg = val / cnt;

                        avgList.Add(cell.Cell_Text, avg);

                        min = Math.Min(avg, min);
                        max = Math.Max(avg, max);
                    }
                }

                int degree = (255 - 60) / (max - min);

                //값 뿌려주기
                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO cell = mData.CellList[i];

                    foreach (var tmp in avgList)
                    {
                        if (tmp.Key == cell.Cell_Text)
                        {
                            int avg = tmp.Value;

                            int rv = ((avg - min) * degree + 30);
                            int bv = 255 - ((avg - min) * degree + 30);

                            if (rv < 0) rv = 30;
                            if (rv > 255) rv = 255;
                            if (bv < 0) bv = 30;
                            if (bv > 255) bv = 255;

                            SolidBrush br = new SolidBrush(Color.FromArgb(190, bv, bv, 255));
                            g.FillRectangle(br, cell.Cell_Rect);

                            float fSize = cell.Cell_Rect.Size.Width / 2;
                            if (fSize == 0) fSize = 1;
                            else if (fSize > 10) fSize = 10;

                            Font ft = new Font(this.Font.FontFamily, fSize);
                            g.DrawString(avg.ToString(), ft, penBr, cell.Cell_Rect, format);
                        }
                    }
                }
            }
        }

        private void Draw_EPMGlass(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                Dictionary<string, int> avgList = new Dictionary<string, int>();

                //값 넣어주기
                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO cell = mData.CellList[i];

                    for (int k = 0; k < mData.PnlValueList.Count; ++k)
                    {
                        MapData.PNL_VALUE vol = mData.PnlValueList[k];

                        if (cell.Cell_Text == vol.PNL_ID)
                        {
                            SolidBrush penBr = new SolidBrush(Color.Black);

                            SolidBrush br = new SolidBrush(Color.FromArgb(190, 255, 255, 255));
                            g.FillRectangle(br, cell.Cell_Rect);

                            float fSize = cell.Cell_Rect.Size.Width / 3;
                            if (fSize == 0) fSize = 1;
                            else if (fSize > 13) fSize = 13;

                            if (vol.PNL_VAL_02 > 0)
                            {
                                penBr = new SolidBrush(Color.Red);

                                if (mProperties.SHOW_EPM_COUNT == true)
                                {
                                    double unit = cell.Cell_Rect.Width / vol.PNL_VAL_03;
                                    Rectangle rect = new Rectangle(cell.Cell_Rect.X + 2, cell.Cell_Rect.Y + 2, (int)(unit * vol.PNL_VAL_02) - 2, cell.Cell_Rect.Height - 2);
                                    g.FillRectangle(Brushes.Blue, rect);
                                }
                            }

                            if (mProperties.SHOW_EPM_VALUE == true)
                            {
                                Font ft = new Font(this.Font.FontFamily, fSize);
                                g.DrawString(vol.PNL_VAL_01.ToString(), ft, penBr, cell.Cell_Rect, format);
                            }
                            else if (mProperties.SHOW_EPM_COUNT == true)
                            {
                                Font ft = new Font(this.Font.FontFamily, fSize);
                                g.DrawString(vol.PNL_VAL_02.ToString(), ft, penBr, cell.Cell_Rect, format);
                            }

                            break;
                        }
                    }
                }

            }
        }

        private void Draw_Pnl_Color(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                Dictionary<string, int> avgList = new Dictionary<string, int>();

                //값 넣어주기
                for (int i = 0; i < mData.CellList.Count; ++i)
                {
                    MapData.CELL_INFO cell = mData.CellList[i];

                    if (mData.PnlValueList == null) continue;

                    for (int k = 0; k < mData.PnlValueList.Count; ++k)
                    {
                        MapData.PNL_VALUE vol = mData.PnlValueList[k];                   

                        //if (cell.TAG != null && cell.TAG != vol.TAG) continue;
                        
                        if (cell.Cell_Text == vol.PNL_ID)
                        {
                            //cell.TAG = vol.TAG;

                            SolidBrush penBr = new SolidBrush(Color.Black);

                            if(vol.PNL_VAL_01 != 0 && mProperties.MapType == MapProperties.GLS_MAP_TYPE.Pnl_Color)
                                penBr = new SolidBrush(Color.White);

                            if (vol.PNL_COLOR == null)
                                vol.PNL_COLOR = Color.Transparent;

                            SolidBrush br = new SolidBrush(vol.PNL_COLOR);
                            g.FillRectangle(br, cell.Cell_Rect);

                            float fSize = (int)(cell.Cell_Rect.Size.Width / 3.5);
                            if (fSize == 0) fSize = 1;
                            else if (fSize > 13) fSize = 13;

                            Font ft = new Font(this.Font.FontFamily, fSize);


                            string pnl_id = string.Empty;
                            if (vol.PNL_ID.Contains("_") == true && vol.PNL_ID.Length > 2)
                                pnl_id = vol.PNL_ID.Substring(2, vol.PNL_ID.Length - 2);
                            else
                                pnl_id = vol.PNL_ID;

                            string Pnl_Value = string.Empty;

                            if (mProperties.MapType != MapProperties.GLS_MAP_TYPE.Pnl_Color)
                            {
                                if(vol.Sub_Classify == "RATE")
                                    Pnl_Value = vol.PNL_VAL_01.ToString() + " %";
                                else
                                    Pnl_Value = vol.PNL_VAL_01.ToString();
                            }                                
                            else if (vol.PNL_VAL_01 != 0)
                                Pnl_Value = vol.PNL_VAL_01.ToString();                              
                            else if (pnl_id.Length > 4)
                                Pnl_Value = string.Format("{0}\r\n{1}", pnl_id.Substring(0, 3), pnl_id.Substring(3, 2));
                            else
                                Pnl_Value = pnl_id;

                            g.DrawString(Pnl_Value, ft, penBr, cell.Cell_Rect, format);

                            break;
                        }
                    }
                }
            }
        }

        private void Draw_PanelName(Bitmap map, List<CELL_INFO> PanelNameList)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                //SolidBrush pnlIdBr = new SolidBrush(Color.Purple);
                SolidBrush pnlIdBr = new SolidBrush(Color.Black);

                for (int i = 0; i < PanelNameList.Count; ++i)
                {
                    MapData.CELL_INFO info = PanelNameList[i];

                    float fSize = (int)(info.Cell_Rect.Size.Width / 3);
                    if (fSize == 0) fSize = 1;
                    Font ft = new Font(this.Font.FontFamily, fSize);

                    string cid = info.Cell_Text;
                    if (info.Cell_Text.Length > 5)
                        cid = string.Format("{0}\r\n{1}", info.Cell_Text.Substring(0, 3), info.Cell_Text.Substring(3, 2));                   
                    g.DrawString(cid, ft, pnlIdBr, info.Cell_Rect, format);
                }
            }
        }

        private void Draw_DefRegion(Bitmap map)
        {
            using (Graphics g = Graphics.FromImage(map))
            {
                for (int i = 0; i < mData.DefRegionList.Count; ++i)
                {
                    MapData.DEF_REGION data = mData.DefRegionList[i];

                    Pen linePen = new Pen(Color.Red);
                    SolidBrush br = new SolidBrush(Color.FromArgb(90, 255, 0, 0));

                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.FillEllipse(br, data.Rect);
                }
            }
        }

        private void ShowDetailViewer(Rectangle rect)
        {
            List<int> rowNoList = new List<int>();

            if (this.mData.DefList != null && this.mData.DefList.Count > 0)
            {
                int no = 0;

                foreach (MapData.DEF_INFO def in mData.DefList)
                {
                    if (def.Visible && rect.IntersectsWith(def.DEF_Rect) && def.DEF_Color.Name != "0")
                    {
                        rowNoList.Add(no);
                    }

                    no++;
                }
            }

            if (rowNoList.Count > 0)
            {
                if (this.dragFrm != null)
                {
                    this.dragFrm.Close();
                    this.dragFrm = null;
                }

                DefectPointEventArgs args = new DefectPointEventArgs();
                args.PointList = rowNoList;
                OnDefectPointEvent(args);
            }
        }

        private void ShowTraceViewer(Rectangle rect)
        {
            List<int> rowNoList = new List<int>();

            if (this.mData.DefList != null && this.mData.DefList.Count > 0)
            {
                int no = 0;

                foreach (MapData.DEF_INFO def in mData.DefList)
                {
                    if (def.Visible && rect.IntersectsWith(def.DEF_Rect) && def.DEF_Color.Name != "0")
                    {
                        rowNoList.Add(no);
                    }

                    no++;
                }
            }

            if (rowNoList.Count > 0)
            {
                if (this.dragFrm != null)
                {
                    this.dragFrm.Close();
                    this.dragFrm = null;
                }

                DefectTracePointEventArgs args = new DefectTracePointEventArgs();
                args.PointList = rowNoList;
                OnDefectTracePointEvent(args);
            }
        }

        public void ShowZoomMap()
        {
            GlassMapZoomForm frm = new GlassMapZoomForm();
            frm.DefectClickEvent += this.DefectClickEvent;
            frm.DefectTraceClickEvent += this.DefectTraceClickEvent;

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.GlassRawData = this.rData;
            frm.DefectList = this.mData.DefList;
            frm.PnlVoltageList = this.mData.PnlValueList;
            frm.PinPointList = this.mData.PinPointDataList;
            frm.DefRegionList = this.mData.DefRegionList;
            frm.mProperties = this.mProperties;

            frm.ShowDialog();
        }

        private void ShowPopup()
        {
            ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();

            ToolStripMenuItem item01 = new ToolStripMenuItem("Show Zoom Map", Properties.Resources.Btn_OpenFile);
            item01.Click += item01_Click;

            ToolStripMenuItem item02 = new ToolStripMenuItem("Show Detail Viewer", Properties.Resources.Menu_History);
            item02.Click += item02_Click;

            ToolStripMenuItem item03 = new ToolStripMenuItem("Show Defect Trace Viewer", Properties.Resources.Menu_History);
            item03.Click += item03_Click;

            if (this.mProperties.SHOW_ZOOM_MAP == false)
                menu.Items.Add(item01);

            menu.Items.Add(item02);
            menu.Items.Add(item03);

            menu.Show(MousePosition);
        }

        void item01_Click(object sender, EventArgs e)
        {
            ShowZoomMap();
        }

        void item02_Click(object sender, EventArgs e)
        {
            Rectangle tmpRect = new Rectangle();

            if (this.dragFrm != null)
                tmpRect = new Rectangle(this.PointToClient(this.dragFrm.Location), this.dragFrm.Size);
            else
                tmpRect = new Rectangle(clickPoint, new Size(1, 1));

            ShowDetailViewer(tmpRect);
        }

        void item03_Click(object sender, EventArgs e)
        {
            Rectangle tmpRect = new Rectangle();

            if(this.dragFrm != null)
            {
                tmpRect = new Rectangle(this.PointToClient(this.dragFrm.Location), this.dragFrm.Size);
            }
            else
            {
                tmpRect = new Rectangle(clickPoint, new Size(1, 1));
            }

            ShowTraceViewer(tmpRect);
        }

        #endregion "  Private methode End"

        #region " Map Events "

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ReDrawMap();
        }

        void WinforsysGlassMap_SizeChanged(object sender, EventArgs e)
        {
            DoubleBuffer();
        }

        void WinforsysGlassMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dragFrm != null && (this.dragFrm.Size.Width > 1 || this.dragFrm.Size.Height > 1))
            {
                return;
            }
            else
            {
                if (this.dragFrm != null)
                {
                    this.dragFrm.Close();
                    this.dragFrm = null;
                }
            }

            Point p = new Point(e.X, e.Y);
            List<int> rowNoList = new List<int>();

            if (this.mData.DefList != null && this.mData.DefList.Count > 0)
            {
                int no = 0;

                foreach (MapData.DEF_INFO def in this.mData.DefList)
                {
                    if (def.Visible && def.DEF_Rect.Contains(p))
                    {
                        rowNoList.Add(no);
                    }

                    no++;
                }
            }

            if (rowNoList.Count > 0)
            {
                DefectPointEventArgs args = new DefectPointEventArgs();
                args.PointList = rowNoList;
                OnDefectPointEvent(args);

                DefectTracePointEventArgs traceArgs = new DefectTracePointEventArgs();
                traceArgs.PointList = rowNoList;
                OnDefectTracePointEvent(traceArgs);
            }
        }

        void WinforsysGlassMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.mProperties.ModuleType != MapProperties.Module_TYPE.MONITOR)
                return;

            if (this.mProperties.SHOW_DETAIL_VIEWER == false) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                clickPoint = new Point(e.X, e.Y);

                ShowPopup();

                return;
            }

            this.dragFrm = new Form();
            this.dragFrm.ShowInTaskbar = false;
            this.dragFrm.Tag = true;
            this.dragFrm.FormBorderStyle = FormBorderStyle.None;
            this.dragFrm.BackColor = Color.Silver;
            this.dragFrm.Opacity = 0.7f;
            this.dragFrm.Show();

            this.dragFrm.Size = new Size(1, 1);
            this.dragFrm.Location = this.PointToScreen(new Point(e.X, e.Y));
        }

        void WinforsysGlassMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mProperties.SHOW_ZOOM_MAP == true)
            {
                this.MouseCoord_X = Math.Round((e.X - mData.GLS_Center.X) / mData._ZoomRate, 2);
                this.MouseCoord_Y = Math.Round((e.Y - mData.GLS_Center.Y) * -1 / mData._ZoomRate, 2);

                //this.MouseCoord_X = Math.Round((e.X - mData.GLS_Rect.Width - mData.GLS_Rect.X) * -1 / mData._ZoomRate, 2);
                //this.MouseCoord_Y = Math.Round((e.Y - mData.GLS_Rect.Height - mData.GLS_Rect.Y) * -1 / mData._ZoomRate, 2);
            }

            if (this.mProperties.ModuleType != MapProperties.Module_TYPE.MONITOR)
                return;

            if (this.dragFrm == null || (bool)this.dragFrm.Tag == false) return;

            Point endPoint = this.PointToScreen(new Point(e.X, e.Y));

            int width = endPoint.X - this.dragFrm.Location.X;
            int height = endPoint.Y - this.dragFrm.Location.Y;

            this.dragFrm.Size = new Size(width, height);
        }

        void WinforsysGlassMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.mProperties.ModuleType != MapProperties.Module_TYPE.MONITOR)
                return;

            if (this.dragFrm != null)
            {
                if (this.dragFrm.Width == 1 && this.dragFrm.Height == 1)
                {
                    this.dragFrm.Close();
                    this.dragFrm = null;
                }

                this.dragFrm.Tag = false;
                this.dragFrm.Deactivate += (object sender2, EventArgs e2) =>
                {
                    this.dragFrm.Close();
                    this.dragFrm = null;
                };

                this.dragFrm.MouseClick += (object sender2, MouseEventArgs e2) =>
                {
                    if (e2.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        ShowPopup();
                    }
                };
            }
        }

        protected virtual void OnDefectPointEvent(DefectPointEventArgs e)
        {
            EventHandler<DefectPointEventArgs> handler = DefectClickEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDefectTracePointEvent(DefectTracePointEventArgs e)
        {
            EventHandler<DefectTracePointEventArgs> handler = DefectTraceClickEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }


        #endregion " Map Events End"
    }
}
