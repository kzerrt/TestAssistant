using System;
using System.Text;
using System.Text.RegularExpressions;
using testAssistant.domain;

namespace testAssistant.util
{
    public class StringUtil
    {
        private const string chinese = "[\\u4e00-\\u9fa5]+";
        private const string english = "[a-zA-z]+";
        public static bool isEmpties(params string[] str) {
            bool flag = false;
            foreach (string  s in str) {
                if (string.IsNullOrEmpty(s)) {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public static string createCode() {
            Random random = new Random(10);
            StringBuilder si = new StringBuilder();
            for (int i = 0; i < 5; i++) {
                si.Append(random.Next().ToString());
            }
            return si.ToString();
        }

        public static string getDevice(string str) {
            string chinese = getChinese(str);
            string english = getEnglish(str);
            return string.IsNullOrEmpty(chinese) ? english : chinese;
        }
        public static string getChinese(string str) {
            Regex regex = new Regex(chinese);
            string s = string.Empty;
            if (regex.IsMatch(str)) {
                s = regex.Match(str).Value.Trim();
            }

            return s;
        }

        public static string getLabel(DeviceInfo info) {
            if (info == null) {
                return null;
            }

            return getEnglish(info.deviceOwner) + subString(info.deviceCode, 5);
        }
        public static string getEnglish(string str) {
            Regex regex = new Regex(english);
            string s = string.Empty;
            if (regex.IsMatch(str)) {
                s = regex.Match(str).Value.Trim();
            }
            return s;
        }

        public static string subString(string str, int len) {
            if (str.Length <= len) {
                return str;
            }

            return str.Substring(0, len);
        }
        
    }
}