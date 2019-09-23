
using System;
namespace UCNL_FW_Update
{
    #region Enums

    public enum DEV_TYPE
    {
        REDBASE = 0,
        REDNODE = 1,
        REDNAV = 2,
        REDGTR = 3,
        REDLINE = 4,
        NATRIX = 11,
        UNKNOWN
    }

    public enum UCNL_SRV_CMD
    {
        DEV_INFO_GET = 0,
        FW_UPDATE_INVOKE = 1,
        DEV_INFO_VAL = 2,
        NAK = 3,
        ACK = 4,
        UNKNOWN
    }

    public enum UCNL_FW_ACTION
    {
        IDLE = 0,
        REQUEST_DEVICE_INFO = 1,
        INVOKE_FW_REQ = 2,
        PORT_REOPENING = 3,
        FW_UPDATE_INIT = 4,
        FW_BLOCK = 5,


        UNKNOWN
    }

    #endregion

    public static class Utils
    {
        public static string BCDVersionToStr(int versionData)
        {
            return string.Format("{0}.{1}", (versionData >> 0x08).ToString(), (versionData & 0xff).ToString("X2"));
        }


        public static string ParseDevInfoStr(string src)
        {
            // 0-REDWAVE-512-TANTRA [IRIDIUM]-512-17002F001451353232333438
            // deviceType
            // sysmoniker
            // sysversion
            // coremoniker
            // coreversion
            // serial number
            var splits = src.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            DEV_TYPE dType = (DEV_TYPE)Enum.Parse(typeof(DEV_TYPE), splits[0]);
            string sysMoniker = splits[1];
            string sysVersion = BCDVersionToStr(int.Parse(splits[2]));
            string coreMoniker = splits[3];
            string coreVersion = BCDVersionToStr(int.Parse(splits[4]));
            string serialNumber = splits[5];


            return string.Format("Device Type: {0}\r\nSystem: {1} v.{2}\r\nCore: {3} v.{4}\r\nS/N: {5}\r\n",
                dType,
                sysMoniker,
                sysVersion,
                coreMoniker,
                coreVersion,
                serialNumber);      
        }
    }
}
