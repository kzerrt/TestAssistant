using System.Collections.Generic;
using System.Xml;

namespace testAssistant.domain
{
    /// <summary>
    /// 模拟器信息
    /// </summary>
    public class VirtualMachine
    {
        public List<Dictionary<string, string>> tester = new List<Dictionary<string, string>>(5);
        public Dictionary<string, XmlNode> testerDemo = new Dictionary<string, XmlNode>(5);

    }
}