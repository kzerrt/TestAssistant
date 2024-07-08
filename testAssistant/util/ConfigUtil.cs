using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using testAssistant.constant;
using testAssistant.domain;

namespace testAssistant.util
{
    /// <summary>
    /// 获取该程序配置文件信息，寻找优先级
    /// 1. ./conf/*.xml
    /// 2. ./*.xml
    /// 
    /// </summary>
    public class ConfigUtil
    {
        public static string findConfig() {
            string config = null;
            string proPath = FileUtil.getBaseProDir();
            config = Path.Combine(proPath, "conf", Constant.proConfig);
            if (FileUtil.checkFile(config)) {
                return config;
            }
            config = Path.Combine(proPath, Constant.proConfig);
            if (FileUtil.checkFile(config)) {
                return config;
            }
            return null;
        }
        public static void initInfo(XmlFile conf, Object info, string matcher) {
            if (info == null) {
                return;
            }
            XmlNode deviceInfoNode = conf.findNodeByPattern(matcher);
            if (deviceInfoNode == null || !deviceInfoNode.HasChildNodes) {
                return;
            }

            var type = info.GetType();
            var properties = type.GetProperties();
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (XmlNode childNode in deviceInfoNode.ChildNodes) {
                if (childNode.NodeType == XmlNodeType.Comment) {
                    continue;
                }
                if (string.IsNullOrEmpty(childNode.InnerText) || map.ContainsKey(childNode.Name)) {
                    continue;
                }
                map.Add(childNode.Name, childNode.InnerText);
            }

            foreach (PropertyInfo propertyInfo in properties) {
                if (map.ContainsKey(propertyInfo.Name)) {
                    propertyInfo.SetValue(info, map[propertyInfo.Name]);
                }
            }
        }

        /// <summary>
        /// 解析模拟器配置信息
        /// </summary>
        /// <param name="conf"></param>
        /// <param name="test"></param>
        /// <exception cref="Exception"></exception>
        public static void retrieveTester(XmlFile conf, VirtualMachine test) {
            if (conf == null) {
                return;
            }

            XmlNode tester = conf.findNodeByPattern(XmlMatcher.tester);
            XmlNode demo = conf.findNodeByPattern(XmlMatcher.demo);

            if (tester == null || demo == null) {
                throw new Exception("配置文件，测试器信息未找到");
            }

            Dictionary<string, string> tmp;
            foreach (XmlNode childNode in tester.ChildNodes) {
                tmp = new Dictionary<string, string>(4);
                foreach (XmlNode item in childNode.ChildNodes) {
                    if (item.NodeType == XmlNodeType.Comment) {
                        continue;
                    }
                    tmp.Add(item.Name, item.InnerText);
                }
                test.tester.Add(tmp);
            }

            XmlAttribute name;
            foreach (XmlNode item in demo.ChildNodes) {
                name = item.Attributes[0];
                if (name == null || string.IsNullOrEmpty(name.Value)) {
                    continue;
                }
                test.testerDemo.Add(name.Value ,item.Clone());
            }
        }

    }
}