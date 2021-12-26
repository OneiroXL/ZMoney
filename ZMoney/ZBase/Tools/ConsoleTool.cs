using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Tools
{
    /// <summary>
    /// 控制台帮助类
    /// </summary>
    public class ConsoleTool
    {

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="con"></param>
        public static void WriteLine(string con)
        {
            Console.WriteLine(con);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// 输出内容 可设置是否清除当前行 延迟清除当前行时间(0 为不延迟)
        /// </summary>
        /// <param name="con"></param>
        public static void WriteLine(string con, bool isClearCurrentConsoleLine = false, int delayMillisecond = 0)
        {
            WriteLine(con);
            if (isClearCurrentConsoleLine)
            {
                if (delayMillisecond > 0)
                {
                    Thread.Sleep(delayMillisecond);
                }
                ClearCurrentConsoleLine();
            }
        }


        /// <summary>
        /// 输出内容 可设置颜色
        /// </summary>
        /// <param name="con"></param>
        public static void WriteLine(string con, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            WriteLine(con);
        }

        /// <summary>
        /// 输出内容 可设置颜色 可设置是否清除当前行 延迟清除当前行时间(0 为不延迟)
        /// </summary>
        /// <param name="con"></param>
        public static void WriteLine(string con, ConsoleColor consoleColor, bool isClearCurrentConsoleLine = false,int delayMillisecond = 0)
        {
            Console.ForegroundColor = consoleColor;
            WriteLine(con);
            if (isClearCurrentConsoleLine)
            {
                if (delayMillisecond > 0) 
                {
                    Thread.Sleep(delayMillisecond);
                }
                ClearCurrentConsoleLine();
            }
        }


        #region 清除当前行
        /// <summary>
        /// 清除当前行
        /// </summary>
        private static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        #endregion

    }
}
