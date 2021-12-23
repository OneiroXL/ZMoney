using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Tools
{
    public class AppSettingHelper
    {
        private static readonly object objLock = new object();
        private static AppSettingHelper instance = null;

        private IConfigurationRoot Config { get; }

        private AppSettingHelper()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        public static AppSettingHelper GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new AppSettingHelper();
                    }
                }
            }

            return instance;
        }

        #region 获取配置文件信息
        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }
        #endregion

    }
}
