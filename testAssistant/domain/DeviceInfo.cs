using System.Collections.Generic;
using System.IO;
using testAssistant.config;
using testAssistant.constant;
using testAssistant.util;

namespace testAssistant.domain
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        public string id { get; set; }
        /// <summary>
        /// 设备商
        /// </summary>
        public  string deviceOwner { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public  string deviceCode{ get; set; }
        /// <summary>
        /// 设备ip
        /// </summary>
        public  string ip{ get; set; }

        public string deviceId
        {
            get;
            set;
        }

        public  string port{ get; set; }

        public string logPath { get; set; }

        public string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(logPath)) {
                    logPath = Path.Combine(InfoContext.setting.baseDir,
                        StringUtil.getDevice(InfoContext.currentInfo.deviceOwner),
                        InfoContext.currentInfo.deviceCode);
                }

                return logPath;
            }
        }
        public string DeviceId
        {
            get
            {
                string s = Constant.deviceId;
                if (!string.IsNullOrEmpty(deviceId)) {
                    s = deviceId;
                }

                return s;
            }
        }

        public string Port
        {
            get
            {
                string s = Constant.devicePort;
                if (!string.IsNullOrEmpty(port)) {
                    s = port;
                }
                return s;
            }
        }

        public Dictionary<string, string> getDeviceInfo() {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("DEVICEID", DeviceId);
            map.Add("DIR", LogPath);
            map.Add("REMOTEIP", ip);
            map.Add("REMOTEPORT", port);
            return map;
        }
    }
    
}