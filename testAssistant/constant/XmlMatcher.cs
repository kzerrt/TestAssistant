namespace testAssistant.constant
{
    /// <summary>
    /// 用来匹配配置文件中主要设备信息
    /// </summary>
    public class XmlMatcher
    {
        public const string deviceMatcher = "/configuration/deviceInfo";
        /// <summary>
        /// 程序配置信息
        /// </summary>
        public const string settingMatcher = "/configuration/setting";
        /// <summary>
        /// 使用的模拟器信息
        /// </summary>
        public const string tester = "/configuration/virtualMachine/testers";
        public const string demo = "/configuration/virtualMachine/demos";

        public const string usingSece = "/SEComSimulatorConfiguration/LASTSELECT/LASTSEComID";
        public const string Sece = "/SEComSimulatorConfiguration/SEComID";
    }
}