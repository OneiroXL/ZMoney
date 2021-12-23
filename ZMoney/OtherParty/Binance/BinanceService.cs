using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZModel.Binance.Return;
using ZOtherParty.I;

namespace ZOtherParty.Binance
{
    /// <summary>
    /// 币安服务
    /// </summary>
    public class BinanceService : IService
    {
        /// <summary>
        /// APIKey
        /// </summary>
        public static string APIKey { get => "DK3twQRbMOPsZRgkjbsnmgDc0FBnmaVZL5TqkoFYUIZBrpH5wvPSJPvR2GYVcSMG"; }

        /// <summary>
        /// APISecret
        /// </summary>
        public static string APISecret { get => "HlpAyQHVVvzUTi7tfzCnT7PYOFA0iD212YTiomnX8fPx5eBUKVNVx5fqST0qoHql"; }

        /// <summary>
        /// API地址
        /// </summary>
        public static string APIAddress => "https://api1.binance.com";


        #region PING
        /// <summary>
        /// PING
        /// </summary>
        public static bool Ping()
        {
            //地址
            string url = "/api/v3/ping";

            //请求
            var res = WebTool.Get(APIAddress + url);

            if (res != "{}")
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 钱包系统状态
        /// <summary>
        /// 钱包系统状态
        /// </summary>
        public static WalletServerStatusRModel WalletServerStatus()
        {
            string url = "/sapi/v1/system/status";

            var res = WebTool.Get(APIAddress + url);

            return JsonConvert.DeserializeObject<WalletServerStatusRModel>(res);
        }

        #endregion

        #region 获取服务器时间
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            //地址
            string url = "/api/v3/time";
            //请求
            var res = WebTool.Get(APIAddress + url);

            ServerTimeRModel serverTimeRModel = JsonConvert.DeserializeObject<ServerTimeRModel>(res);

            if (serverTimeRModel == null) 
            {
                return DateTime.MinValue;
            } 

            return DateTimeTool.GetDateTime(serverTimeRModel.ServerTime);
        }

        #endregion

        #region 现货下单

        public static void SpotTradeOrder() 
        {

        }

        #endregion

        #region MyRegion

        #endregion
    }
}
