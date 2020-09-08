using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Capacity_Monitoring
{
    public enum E_DEPT { BP, EVEN, TSP}
    public static class UserInfo
    {
        public static string USER_ID { get; set; }

        public static E_DEPT DEPT = E_DEPT.BP;

        public static string USER_VIEW { get; set; }
    }
}
