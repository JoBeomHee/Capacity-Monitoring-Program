using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Capacity_Monitoring
{
    public enum DISK { C, D }

    // 공유폴더 접근 관련 에러코드
    public class ERROR_CODE
    {
        public const int NO_ERROR = 0;
        public const int ERROR_NO_NET_OR_BAD_SERVER = 53;
        public const int ERROR_BAD_USER_OR_PASSWORD = 1326;
        public const int ERROR_ACCESS_DENIED = 5;
        public const int ERROR_ALREADY_ASSIGNED = 85;
        public const int ERROR_BAD_DEV_TYPE = 66;
        public const int ERROR_BAD_DEVICE = 1200;
        public const int ERROR_BAD_NET_NAME = 67;
        public const int ERROR_BAD_PROFILE = 1206;
        public const int ERROR_BAD_PROVIDER = 1204;
        public const int ERROR_BUSY = 170;
        public const int ERROR_CANCELLED = 1223;
        public const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        public const int ERROR_DEVICE_ALREADY_REMEMBERED = 1202;
        public const int ERROR_EXTENDED_ERROR = 1208;
        public const int ERROR_INVALID_PASSWORD = 86;
        public const int ERROR_NO_NET_OR_BAD_PATH = 1203;
        public const int ERROR_INVALID_ADDRESS = 487;
        public const int ERROR_NETWORK_BUSY = 54;
        public const int ERROR_UNEXP_NET_ERR = 59;
        public const int ERROR_INVALID_PARAMETER = 87;
        public const int ERROR_MULTIPLE_CONNECTION = 1219;
    }
}
