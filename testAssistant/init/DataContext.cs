using testAssistant.constant;
using testAssistant.domain;
using testAssistant.util;

namespace testAssistant.config
{
    /// <summary>
    /// 数据上下文中转
    /// </summary>
    public class InfoContext
    {
        public static  DeviceInfo currentInfo =new DeviceInfo();
        public static readonly Setting setting = new Setting();
        public static readonly VirtualMachine virtualMachine = new VirtualMachine();
        public static string logPath { get; set; }

        public static void init() {
            var configFile =  ConfigUtil.findConfig();
            if (string.IsNullOrEmpty(configFile)) {
                
                return;
            }

            var configXml = new XmlFile(configFile);

            #region 对配置文件中的数据进行读取

            ConfigUtil.initInfo(configXml, setting, XmlMatcher.settingMatcher);
            ConfigUtil.retrieveTester(configXml, virtualMachine);
            #endregion

        }

    }
}