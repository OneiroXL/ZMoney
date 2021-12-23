using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Tools
{
    /// <summary>
    /// 命令台执行工具
    /// </summary>
    public class ShellCmdExecuterTool
    {
        /// <summary>
        /// 输出数据
        /// </summary>
        StringBuilder outputData = new StringBuilder();

        #region 运行cmd命令
        /// <summary>
        /// 运行cmd命令
        /// </summary>
        /// <param name="cmdList"></param>
        /// <param name="encoding"></param>
        /// <param name="userGroup"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        public static void ExecuteCmd(List<String> cmdList, Encoding encoding = null, String userGroup = null, String userName = null, String userPwd = null, Action<string> errorDataReceivedAction = null,Action<string> outputDataReceivedAction = null)
        {
            //创建程序
            using (Process cmdProcess = new Process()) 
            {
                //设置基础属性
                cmdProcess.StartInfo.FileName = "cmd.exe";
                cmdProcess.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                cmdProcess.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                cmdProcess.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                cmdProcess.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                cmdProcess.StartInfo.CreateNoWindow = true;//不显示程序窗口
                
                //设置编码格式
                if (encoding != null)
                {
                    cmdProcess.StartInfo.StandardOutputEncoding = encoding;
                    cmdProcess.StartInfo.StandardErrorEncoding = encoding;
                }

                //设置启动程序的帐号密码
                if (!String.IsNullOrWhiteSpace(userGroup) && !String.IsNullOrWhiteSpace(userName) && !String.IsNullOrWhiteSpace(userPwd))
                {
                    cmdProcess.StartInfo.Domain = userGroup;
                    cmdProcess.StartInfo.UserName = userName;

                    //SecureString，安全字符，必须是char类型
                    SecureString password = new SecureString();
                    foreach (char c in userPwd.ToCharArray())
                    {
                        password.AppendChar(c);
                    }
                    cmdProcess.StartInfo.Password = password;
                }
                //启动程序
                cmdProcess.Start();
                cmdProcess.StandardInput.AutoFlush = true;

                //写入命令
                foreach (var cmd in cmdList) 
                {
                    //向cmd窗口发送输入信息
                    cmdProcess.StandardInput.WriteLine(cmd);
                }

                cmdProcess.StandardInput.Close();

                //绑定读取数据事件
                cmdProcess.ErrorDataReceived += (sender, dataReceivedEventArgs) =>
                {
                    if (errorDataReceivedAction != null) 
                    {
                        errorDataReceivedAction(dataReceivedEventArgs.Data);
                    }
                };
                cmdProcess.OutputDataReceived += (sender, dataReceivedEventArgs) =>
                {
                    if (outputDataReceivedAction != null)
                    {
                        outputDataReceivedAction(dataReceivedEventArgs.Data);
                    }
                };

                //开始读取输出数据
                cmdProcess.BeginErrorReadLine();
                cmdProcess.BeginOutputReadLine();

                //等待程序执行完退出进程
                cmdProcess.WaitForExit();
            }
        }

        #endregion
    }
}
