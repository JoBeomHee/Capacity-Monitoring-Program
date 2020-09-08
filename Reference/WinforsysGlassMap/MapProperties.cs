using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WinforsysGlassMap
{
    public class MapProperties : ICloneable
    {
        public enum Module_TYPE { MONITOR, RECIPE, ALARM, JPC }
        public enum GLS_MAP_TYPE { Defect, Bubble, PinPoint, PnlVoltage, EPM, Pnl_Color, NEW_Bubble, Contour }
        public enum GLS_COLOR_TYPE { White, Black, Gray, DimmyGray }
        public enum E_GLS_TYPE { BP, TSP, EVEN }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public MapProperties Clone()
        {
            return (MapProperties)this.MemberwiseClone();
        }

        public Module_TYPE ModuleType { get; set; }
        public GLS_MAP_TYPE MapType { get; set; }
        public GLS_COLOR_TYPE ColorType { get; set; }

        public E_GLS_TYPE GLS_TYPE = E_GLS_TYPE.BP;

        public bool TYPE_DATA_GATE = false;
        public bool SHOW_ZOOM_MAP = false;
        public bool SHOW_DETAIL_VIEWER = false;
        public bool SHOW_PINPOINT = false;
        public bool SHOW_EPM_VALUE = false;
        public bool SHOW_EPM_COUNT = false;

        public int Default_DefectSize { get; set; }

        public class ColorValues
        {
            public Color MapBackgroundColor = Color.Black;
            public Color GlassLineColor = Color.Red;
            public Color CellLineColor = Color.Green;
            public Color PanelLineColor = Color.White;
        }

        public ColorValues Colors { get; set; }       

        public MapProperties()
        {
            Colors = new ColorValues();    

            Default_DefectSize = 4;
        }

        public void SetColorType()
        {
            if (this.ColorType == GLS_COLOR_TYPE.White)
            {
                Colors.MapBackgroundColor = Color.White;
                Colors.GlassLineColor = Color.Red;
                Colors.CellLineColor = Color.Black;
                Colors.PanelLineColor = Color.Green;
            }
            else if (this.ColorType == GLS_COLOR_TYPE.Black)
            {
                Colors.MapBackgroundColor = Color.Black;
                Colors.GlassLineColor = Color.Red;
                Colors.CellLineColor = Color.White;
                Colors.PanelLineColor = Color.Green;
            }
            else if (this.ColorType == GLS_COLOR_TYPE.Gray)
            {
                Colors.MapBackgroundColor = Color.Gray;
                Colors.GlassLineColor = Color.Red;
                Colors.CellLineColor = Color.White;
                Colors.PanelLineColor = Color.Green;
            }
        }
    }
}
