using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinforsysGlassMap
{
    public class DefectPointEventArgs : EventArgs
    {
        public List<int> PointList { get; set; }
        public bool Cancel = false;
    }

    public class DefectTracePointEventArgs : EventArgs
    {
        public List<int> PointList { get; set; }
        public bool Cancel = false;
    }
}
