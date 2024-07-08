using System;
using System.IO;
using testAssistant.constant;

namespace testAssistant.util
{
    /// <summary>
    /// 文件管理类
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// 获取当前程序目录
        /// </summary>
        /// <returns></returns>
        public static string getBaseProDir() {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static bool createDir(string path) {
            if (checkFile(path)) {
                return false;
            }

            if (checkDir(path)) {
                return true;
            }

            bool result = true;
            try {
                Directory.CreateDirectory(path);
            }
            catch (Exception e) {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file">文件绝对路径</param>
        /// <returns></returns>
        public static bool checkFile(string file) {
            return File.Exists(file);
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool checkDir(string path) {
            return Directory.Exists(path);
        }
        
    }
}