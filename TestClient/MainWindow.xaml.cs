using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using testAssistant.config;
using testAssistant.constant;
using testAssistant.domain;
using testAssistant.util;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string excuteProgram = null;
        public MainWindow() {
            InitializeComponent();
            //this.WindowStartupLocation = WindowStartupLocation.CenterScreen;//在中间显示
            // 读取配置信息
            InfoContext.init();
            
        }

        
        
        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            bool isRun = true;
           try
            {
                // 创建对应文件夹
                dataInit();
                createDir();
                // 修改测试其配置文件
                modifyConfig();
                // todo 对测试的机型进行记录
                // 启动程序
                startSimulatorProgram(excuteProgram);
            } catch(Exception ex)
            {
                isRun = false;
            }
            exitEAPProgram(isRun);
        }
        private void LoadSavedDevices()
        {
            tester.ItemsSource = new List<string>();
        }

        private void getDeviceInfo(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (tester.SelectedItem != null)
            {
                string selectedDeviceInfo = tester.SelectedItem.ToString();
                
            }
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        private void startSimulatorProgram(string excuteProgram)
        {
            if (string.IsNullOrEmpty(excuteProgram))
            {
                return;
            }
            if (FileUtil.checkFile(excuteProgram))
            {
                Thread.Sleep(2000);
                System.Diagnostics.Process.Start(excuteProgram);
            }
        }
        private void exitEAPProgram(bool isSuccess)
        {
            if (isSuccess)
            {
                Thread.Sleep(2000);
                Environment.Exit(1);
            }
        }
        #region 业务处理

        private void dataInit() {
            
            DeviceInfo info = InfoContext.currentInfo;
            info.deviceOwner = deviceOwner.Text;
            info.deviceCode = deviceCode.Text;
            info.deviceId = deviceId.Text;
            info.ip = ip.Text;
            info.port = port.Text;
        }

        private void createDir() {
            if (StringUtil.isEmpties(InfoContext.currentInfo.deviceOwner, InfoContext.currentInfo.deviceCode,
                    InfoContext.currentInfo.ip)) {
                throw new Exception("设备信息不能为空");
            }

            string path = Path.Combine(InfoContext.setting.baseDir, StringUtil.getDevice(InfoContext.currentInfo.deviceOwner),
                InfoContext.currentInfo.deviceCode);
            if (FileUtil.checkDir(path)) {
                return;
            }
            // 创建设备目录
            FileUtil.createDir(path);
            /*InfoContext.logPath = Path.Combine(path, "log");*/
            FileUtil.createDir(InfoContext.currentInfo.LogPath);// 创建日志目录

        }

        private void modifyConfig() {
            // 找到tester配置文件
            var tmp = InfoContext.virtualMachine.tester[0];
            // 获取启动路径
            excuteProgram = Path.Combine(tmp[Tester.dir], tmp[Tester.executeProgram]);
            string configPath = Path.Combine(tmp[Tester.dir], tmp[Tester.configName]);
            if (!FileUtil.checkFile(configPath)) {
                throw new Exception("无法找到模拟器配置文件");
            }

            var conf = new XmlFile(configPath);
            // 修改配置内容
            var node = InfoContext.virtualMachine.testerDemo[tmp[Tester.name]];
            if (node == null) {
                throw new Exception("需要复制的模板为空，请检查");
            }

            var infos = InfoContext.currentInfo.getDeviceInfo();
            string nodeName = StringUtil.getLabel(InfoContext.currentInfo);
            conf.copyNode(XmlMatcher.Sece, node, nodeName,false, xmlNode => {
                if (infos.ContainsKey(xmlNode.Name)) {
                    return infos[xmlNode.Name];
                }

                return null;
            });
            conf.save();
        }
        #endregion
        
    }
}