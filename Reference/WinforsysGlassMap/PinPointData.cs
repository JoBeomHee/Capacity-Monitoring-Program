using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinforsysGlassMap
{
    [Serializable()]
    public class PinPointData
    {
        public class RawData
        {
            public string Product_Id { get; set; } //Recipe Manager에서 사용하기 위해 임시로 추가

            public bool Visible = false;
            public bool IsMatched = false;
            public double Raw_X { get; set; }
            public double Raw_Y { get; set; }
            public double Raw_Width { get; set; }
            public double Raw_Height { get; set; }
            public string DefCode { get; set; }
        }
        
        public List<RawData> RawDataList { get; set; }
        public List<Rectangle> PinPointList { get; set; }

        public string Product_Id { get; set; }
        public string Process_Name { get; set; }
        public string Equipment_Name { get; set; }

        public int Point_Shape { get; set; }
        public Color Point_Color { get; set; }

        public int MatchedCnt = 0;

        public PinPointData()
        {
            RawDataList = new List<RawData>();
            PinPointList = new List<Rectangle>();
        }
    }
}
