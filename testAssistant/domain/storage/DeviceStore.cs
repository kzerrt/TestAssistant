using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace testAssitant.domain.storage
{
    /// <summary>
    /// 设备信息存储
    /// </summary>
    [Serializable]
    public class DeviceStore
    {
        private string id;
        private string deviceOwner;
        private string deviceCode;
        private string deviceNum;
        private string ip;
        private string port;
        private string deviceId;
        private DateTime startTime;
        private DateTime endTime;
        private bool tested;

        public string Id { get; set; }
        public string DeviceOwner { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceNum { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string DeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Tested { get; set; }

    }
}
