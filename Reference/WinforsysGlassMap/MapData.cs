using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WinforsysGlassMap
{
    public enum E_DFT_SHARF { CIRCLE, SQUARE, TRIANGLE }
    [Serializable]
    public class MapData : ICloneable
    {
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public MapData Clone()
        {
            return (MapData)this.MemberwiseClone();
        }

        #region " Sub Class "

        [Serializable]
        public class CELL_INFO
        {
            public string Cell_Text { get; set; }

            public Rectangle Cell_Rect { get; set; }

            public List<DEF_INFO> DefList = new List<DEF_INFO>();
            public object TAG = null;
        }

        [Serializable]
        public class DEF_INFO
        {
            public int Seq { get; set; }
            public string Cell_Text { get; set; }

            public double Raw_Point_X { get; set; }
            public double Raw_Point_Y { get; set; }

            public int Manual_Size { get; set; }

            public string DEF_Code { get; set; }

            public E_DFT_SHARF DEF_SHARF = E_DFT_SHARF.CIRCLE;
            public Color DEF_Color { get; set; }
            public Rectangle DEF_Rect { get; set; }
            public Rectangle LINE_DEF_Rect { get; set; }

            public List<string> DetailData { get; set; }

            public Dictionary<string, object> DataList { get; set; }

            public List<object> MergeList = new List<object> ();

            public int MapIndex { get; set; }

            //Buble Map 전용 Item
            public int O_Cnt = 0;
            public int X_Cnt = 0;
            public int F_Cnt = 0;

            public bool Visible = true;
            public bool CodeVisible = true;
        }

        [Serializable]
        public class DEF_REGION
        {
            public double Raw_X { get; set; }
            public double Raw_Y { get; set; }
            public double Raw_Radius { get; set; }            

            public Rectangle Rect { get; set; }
            public object Tag { get; set; }
        }

        [Serializable]
        public class PNL_VALUE
        {
            public string PNL_ID = string.Empty;
            public double PNL_VAL_01 = 0;
            public double PNL_VAL_02 = 0;
            public double PNL_VAL_03 = 0;
            public string Sub_Classify = string.Empty;
            public Color PNL_COLOR = new Color();

            public List<string> DetailData = new List<string>();
            public object TAG = null;
        }

        #endregion " Sub Class End "

        #region " Properties & Variables "

        public string Product_Id = "";
        public string GLS_Id { get; set; }

        public MapProperties GlsProperties { get; set; }

        public Point MapSize { get; set; }
        public RawData GlsRawData { get; set; }

        public double Raw_Size_Y = 1850.00F;
        public double Raw_Size_X = 1500.00F;
        //private double Raw_Size_X = 1850.00F;
        //private double Raw_Size_Y = 1500.00F;

        public Point GLS_Center { get; set; }
        public Rectangle GLS_Rect { get; set; }

        public List<CELL_INFO> CellList = new List<CELL_INFO>();
        public List<DEF_INFO> DefList = new List<DEF_INFO>();
        public List<PNL_VALUE> PnlValueList = new List<PNL_VALUE>();
        public double _ZoomRate = 1;

        public List<PinPointData> PinPointDataList { get; set; }
        public List<DEF_REGION> DefRegionList { get; set; }

        #endregion " Properties & Variables End"

        #region " Create & Load "

        public MapData()
        {
            PinPointDataList = new List<PinPointData>();
            DefRegionList = new List<DEF_REGION>();
        }

        public MapData(RawData rData, MapProperties mProperties)
        {
            GlsRawData = rData;
            GlsProperties = mProperties;

            PinPointDataList = new List<PinPointData>();
            DefRegionList = new List<DEF_REGION>();
        }

        #endregion " Create & Load End"

        #region "  Public methode "

        public void Translate(Point mapSize)
        {
            this.MapSize = mapSize;

            //Map 크기 및 비율 설정
            Translate_Map();

            //Glass 크기 및 위치 설정
            Translate_GLS();

            if (this.GlsRawData == null) return;         

            //Cell 크기 및 위치 설정
            this.CellList = new List<CELL_INFO>();      
          
            foreach (RawData.P_RAW data in this.GlsRawData.PNL_RAW_DATA)
            {
                CELL_INFO info = Translate_Cell(data);                

                info.Cell_Text = data.PNL_ID;
                info.TAG = data.TAG;

                CellList.Add(info);              
            }

            //Defect 크기 및 위치 설정
            if (this.DefList != null)
            {
                foreach (DEF_INFO def in this.DefList)
                {
                    Translate_DEF(def);
                }
            }

            //Pin Point 위치 설정
            if (this.PinPointDataList != null)
            {
                Translate_PinPoint();
            }

            if (this.DefRegionList != null)
            {
                Translate_DefRegion();
            }
        }

        #endregion "  Public methode End"

        #region "  Private methode "

        private void Translate_Map()
        {
            //Glass Map 창과의 간격을 임의로 줌
            int marX = (int)(MapSize.X * 0.01);
            int marY = (int)(MapSize.Y * 0.01);

            if (marX < 1) marX = 1;
            if (marY < 1) marY = 1;

            int nSizeX = MapSize.X - marX;
            int nSizeY = MapSize.Y - marY;

            double rateX = Math.Round((double)nSizeX / this.Raw_Size_X, 4);
            double rateY = Math.Round((double)nSizeY / this.Raw_Size_Y, 4);

            _ZoomRate = rateX > rateY ? rateY : rateX;
        }

        private void Translate_GLS()
        {
            int sizeX = (int)(this.Raw_Size_X * _ZoomRate);
            int sizeY = (int)(this.Raw_Size_Y * _ZoomRate);

            int centerX = (int)(this.MapSize.X / 2);
            int centerY = (int)(this.MapSize.Y / 2);

            int sPointX = centerX - (sizeX / 2);
            int sPointY = centerY - (sizeY / 2);

            this.GLS_Rect = new Rectangle(sPointX, sPointY, sizeX, sizeY);
            this.GLS_Center = new Point(centerX, centerY);
        }

        public CELL_INFO Translate_Cell(RawData.P_RAW data)
        {
            CELL_INFO info = new CELL_INFO();

            double tmpSizeX = 0;
            double tmpSizeY = 0;

            double zr = 0.001;

            tmpSizeX = data.PNL_SIZE_X * zr;
            tmpSizeY = data.PNL_SIZE_Y * zr;

            int cSizeX = (int)(tmpSizeX * _ZoomRate);
            int cSizeY = (int)(tmpSizeY * _ZoomRate);           

            int cCenterX = (int)(data.PNL_POINT_X * _ZoomRate * zr) + this.GLS_Center.X;
            int cCenterY = (int)(data.PNL_POINT_Y * _ZoomRate * -1 * zr) + this.GLS_Center.Y;

            int cSpointX = cCenterX - (cSizeX / 2);
            int cSpointY = cCenterY - (cSizeY / 2);

            int px = (int)(data.PNL_POINT_X * _ZoomRate * zr) + this.GLS_Center.X;  //X축은 오른쪽이 +이다
            int py = (int)(data.PNL_POINT_Y * _ZoomRate * zr * -1) + this.GLS_Center.Y;       //Y축은 위쪽이 - 이다.

            int mx = (int)(data.PNL_MARGIN_X * _ZoomRate * zr);
            int my = (int)(data.PNL_MARGIN_Y * _ZoomRate * zr);

            //tmpSizeX = data.PNL_SIZE_Y * zr;
            //tmpSizeY = data.PNL_SIZE_X * zr;

            //int cSizeX = (int)(tmpSizeX * _ZoomRate);
            //int cSizeY = (int)(tmpSizeY * _ZoomRate);

            //int cCenterX = (int)(data.PNL_POINT_Y * _ZoomRate * zr) + this.GLS_Center.X;
            //int cCenterY = (int)(data.PNL_POINT_X * _ZoomRate * -1 * zr) + this.GLS_Center.Y;

            //int cSpointX = cCenterX - (cSizeX / 2);
            //int cSpointY = cCenterY - (cSizeY / 2);

            //int px = (int)(data.PNL_POINT_Y * _ZoomRate * zr * -1) + this.GLS_Center.X;  //X축은 오른쪽이 +이다
            //int py = (int)(data.PNL_POINT_X * _ZoomRate * zr * -1) + this.GLS_Center.Y;       //Y축은 위쪽이 - 이다.

            //int mx = (int)(data.PNL_MARGIN_Y * _ZoomRate * zr);
            //int my = (int)(data.PNL_MARGIN_X * _ZoomRate * zr);

            if (data.PNL_ORIGIN == "LT") //이것만  B11 기준으로 변경
            {
                cSpointX = px - mx - cSizeX;
                cSpointY = py - my - cSizeY;
            }
            else if (data.PNL_ORIGIN == "LB")
            {
                cSpointX = px - mx - cSizeX;
                cSpointY = py + my;
            }
            else if (data.PNL_ORIGIN == "RT")
            {
                cSpointX = px + mx;
                cSpointY = py - my - cSizeY;
            }
            else if (data.PNL_ORIGIN == "RB")
            {
                cSpointX = px + mx - cSizeX;
                cSpointY = py + my - cSizeY;
            }
            else if (data.PNL_ORIGIN == null || data.PNL_ORIGIN.Length == 0) // LT로 본다
            {
                cSpointX = px - mx;
                cSpointY = py - my - cSizeY;
            }

            if (data.PNL_F_SIZE_X != 0 && data.PNL_F_SIZE_Y != 0)
            {
                cSizeX = (int)(data.PNL_F_SIZE_Y * _ZoomRate * zr);
                cSizeY = (int)(data.PNL_F_SIZE_X * _ZoomRate * zr);
            }

            info.Cell_Rect = new Rectangle(cSpointX, cSpointY, cSizeX, cSizeY);

            return info;
        }        

        private void Translate_DEF(DEF_INFO data)
        {
            double zr = 0.001;
            //해당 Cell 찾기
            CELL_INFO cell = null;

            foreach (CELL_INFO info in this.CellList)
            {
                if (info.Cell_Text == data.Cell_Text)
                {
                    info.DefList.Add(data);
                    cell = info;
                    break;
                }
            }

            int sizeX = data.Manual_Size;
            int sizeY = data.Manual_Size;

            if (data.Manual_Size == 0)
            {
                sizeX = sizeY = GlsProperties.Default_DefectSize;
            }

            if (this.GlsProperties.TYPE_DATA_GATE == false)
            {
                int centerX = (int)(data.Raw_Point_X * _ZoomRate * zr) + this.GLS_Center.X;
                int centerY = (int)(data.Raw_Point_Y * _ZoomRate * -1 * zr) + this.GLS_Center.Y;

                int sPointX = centerX - (sizeX / 2);
                int sPointY = centerY - (sizeY / 2);

                data.DEF_Rect = new Rectangle(sPointX, sPointY, sizeX, sizeY);
            }
            else
            {
                if (cell == null)
                {
                    //throw new System.Exception("Can not found Defect's Cell Information.");
                    return;
                }

                double rX = 0;
                double rY = 0;

                int tmpPointX = 0;
                int tmpPointY = 0;

                double dataSize = 0;
                double gatesize = 0;

                if (this.GlsRawData.PNL_RAW_DATA.Count > 0)
                {
                    dataSize = this.GlsRawData.PNL_RAW_DATA[0].PNL_DATA;
                    gatesize = this.GlsRawData.PNL_RAW_DATA[0].PNL_GATE;
                }

                if (cell.Cell_Rect.Height > cell.Cell_Rect.Width)
                {
                    //높이가 넓이보다 큰 경우 Data - Gate
                    rX = Math.Round((double)cell.Cell_Rect.Height / dataSize, 4);
                    rY = Math.Round((double)cell.Cell_Rect.Width / gatesize, 4);

                    tmpPointX = (int)(data.Raw_Point_Y * rX);
                    tmpPointY = (int)(data.Raw_Point_X * rY);
                }
                else
                {
                    //넓이가 높이가보다 큰 경우 Gate - Data
                    rX = Math.Round((double)cell.Cell_Rect.Width / dataSize, 4);
                    rY = Math.Round((double)cell.Cell_Rect.Height / gatesize, 4);

                    tmpPointX = (int)(data.Raw_Point_X * rX);
                    tmpPointY = (int)(data.Raw_Point_Y * rY);
                }

                int sPointX = -(sizeX / 2) + tmpPointX + cell.Cell_Rect.X;
                int sPointY = -(sizeY / 2) + tmpPointY + cell.Cell_Rect.Y;

                data.DEF_Rect = new Rectangle(sPointX, sPointY, sizeX, sizeY);
            }


            //if (this.GlsProperties.TYPE_DATA_GATE == false)
            //{
            //    int centerX = (int)(data.Raw_Point_Y * _ZoomRate * -1 * zr) + this.GLS_Center.X;
            //    int centerY = (int)(data.Raw_Point_X * _ZoomRate * -1 * zr) + this.GLS_Center.Y;

            //    int sPointX = centerX - (sizeX / 2);
            //    int sPointY = centerY - (sizeY / 2);

            //    data.DEF_Rect = new Rectangle(sPointX, sPointY, sizeX, sizeY);
            //}
            //else
            //{
            //    if (cell == null)
            //    {
            //        //throw new System.Exception("Can not found Defect's Cell Information.");
            //        return;
            //    }

            //    double rX = 0;
            //    double rY = 0;

            //    int tmpPointX = 0;
            //    int tmpPointY = 0;

            //    double dataSize = 0;
            //    double gatesize = 0;

            //    if (this.GlsRawData.PNL_RAW_DATA.Count > 0)
            //    {
            //        dataSize = this.GlsRawData.PNL_RAW_DATA[0].PNL_DATA;
            //        gatesize = this.GlsRawData.PNL_RAW_DATA[0].PNL_GATE;
            //    }

            //    if (cell.Cell_Rect.Height > cell.Cell_Rect.Width)
            //    {
            //        //높이가 넓이보다 큰 경우 Data - Gate
            //        rX = Math.Round((double)cell.Cell_Rect.Height / dataSize, 4);
            //        rY = Math.Round((double)cell.Cell_Rect.Width / gatesize, 4);

            //        tmpPointX = (int)(data.Raw_Point_X * rX);
            //        tmpPointY = (int)(data.Raw_Point_Y * rY);
            //    }
            //    else
            //    {
            //        //넓이가 높이가보다 큰 경우 Gate - Data
            //        rX = Math.Round((double)cell.Cell_Rect.Width / dataSize, 4);
            //        rY = Math.Round((double)cell.Cell_Rect.Height / gatesize, 4);

            //        tmpPointX = (int)(data.Raw_Point_Y * rX);
            //        tmpPointY = (int)(data.Raw_Point_X * rY);
            //    }

            //    int sPointX = -(sizeX / 2) + tmpPointX + cell.Cell_Rect.X;
            //    int sPointY = -(sizeY / 2) + tmpPointY + cell.Cell_Rect.Y;

            //    data.DEF_Rect = new Rectangle(sPointX, sPointY, sizeX, sizeY);
            //}
        }

        private void Translate_PinPoint()
        {
            for (int i = 0; i < this.PinPointDataList.Count; ++i)
            {
                PinPointData pData = this.PinPointDataList[i];
                pData.PinPointList.Clear();

                for (int k = 0; k < pData.RawDataList.Count; ++k)
                {
                    PinPointData.RawData rData = pData.RawDataList[k];

                    int centerX = (int)(rData.Raw_X * _ZoomRate) + this.GLS_Center.X;
                    int centerY = (int)(rData.Raw_Y * _ZoomRate * -1) + this.GLS_Center.Y;

                    int sizeX = (int)(rData.Raw_Width * _ZoomRate);
                    int sizeY = (int)(rData.Raw_Height * _ZoomRate);

                    if (sizeX < 3) sizeX = 3;
                    if (sizeY < 3) sizeY = 3;

                    int spointX = centerX - (sizeX / 2);
                    int spointY = centerY - (sizeY / 2);

                    Rectangle rect = new Rectangle(spointX, spointY, sizeX, sizeY);
                    pData.PinPointList.Add(rect);
                }              
            }
        }

        private void Translate_DefRegion()
        {
            for (int i = 0; i < this.DefRegionList.Count; ++i)
            {
                DEF_REGION def = this.DefRegionList[i];

                int size = (int)(def.Raw_Radius * 2 * _ZoomRate);

                int centerX = (int)(def.Raw_X * _ZoomRate) + this.GLS_Center.X;
                int centerY = (int)(def.Raw_Y * _ZoomRate * -1) + this.GLS_Center.Y;

                int sPointX = centerX - (size / 2);
                int sPointY = centerY - (size / 2);

                def.Rect = new Rectangle(sPointX, sPointY, size, size);
            }
        }

        #endregion "  Private methode End"

        #region " Events "

        #endregion " Events End"
    }
}
