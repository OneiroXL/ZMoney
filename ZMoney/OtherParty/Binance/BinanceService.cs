using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZModel.Binance.Param;
using ZModel.Binance.Return;
using ZOtherParty.I;
using static ZBase.ZEnum.BinanceEnum;
using static ZBase.ZEnum.WebEnum;

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

        #region 创建WEB客户端
        /// <summary>
        /// 创建WEB客户端
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestMethodTypeEnum">请求方式枚举</param>
        /// <param name="isNeedParamModel">是否需要参数</param>
        public static string HandleWebRequest(string url, object paramModel , RequestMethodTypeEnum requestMethodTypeEnum,bool isNeedParamModel = true)
        {
            //参数字典
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic["timestamp"] = DateTimeTool.GetTimeStamp(DateTime.Now).ToString();
            if (paramModel != null) 
            {
                paramDic = ReflexionHelper.ClassFieldJsonPropertyToDictionary(paramModel);
            }
            //签名
            paramDic["signature"] = EncryptiontTool.HMACSHA256Encrypt(WebTool.AssembleXWFUParam(paramDic), APISecret);

            //请求头
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            //返回数据
            string resposeStr = string.Empty;
            //请求方式
            switch (requestMethodTypeEnum) 
            {
                case RequestMethodTypeEnum.POST:
                    {
                        headerDic["Content-Type"] = "application/json";
                        headerDic["X-MBX-APIKEY"] = APIKey;

                        resposeStr = WebTool.Post(url, paramDic, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.GET:
                    {
                        headerDic["Content-Type"] = "application/x-www-form-urlencoded";
                        headerDic["X-MBX-APIKEY"] = APIKey;

                        resposeStr = WebTool.Get(url, isNeedParamModel ? paramDic : null, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.PUT:
                    {
                        headerDic["Content-Type"] = "application/x-www-form-urlencoded";
                        headerDic["X-MBX-APIKEY"] = APIKey;
                    }
                    break;
                case RequestMethodTypeEnum.DELETE:
                    {
                        headerDic["Content-Type"] = "application/x-www-form-urlencoded";
                        headerDic["X-MBX-APIKEY"] = APIKey;
                    }
                    break;
            }

            return resposeStr;
        }

        #endregion

        #region PING
        /// <summary>
        /// PING
        /// </summary>
        public static bool Ping()
        {
            //地址
            string url = "/api/v3/ping";

            var res = HandleWebRequest(APIAddress + url, null, RequestMethodTypeEnum.GET, false);
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
            string res = HandleWebRequest(APIAddress + url, null, RequestMethodTypeEnum.GET);
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

            string res = HandleWebRequest(APIAddress + url, null, RequestMethodTypeEnum.GET);
            //请求
            ServerTimeRModel serverTimeRModel = JsonConvert.DeserializeObject<ServerTimeRModel>(res);

            if (serverTimeRModel == null) 
            {
                return DateTime.MinValue;
            } 

            return DateTimeTool.GetDateTime(serverTimeRModel.ServerTime);
        }

        #endregion

        #region 现货下单
        /// <summary>
        /// 现货下单
        /// </summary>
        /// <param name="spotTradeOrderParam"></param>
        public static void SpotTradeOrder(SpotTradeOrderPModel spotTradeOrderParam) 
        {
            spotTradeOrderParam = new SpotTradeOrderPModel();

            spotTradeOrderParam.Symbol = "ETHUSDT";
            spotTradeOrderParam.Side = SPOTSideEuum.BUY;
            spotTradeOrderParam.Price = 2500m;
            spotTradeOrderParam.Quantity;

            //地址
            string url = "/api/v3/order";

            string res = HandleWebRequest(APIAddress + url, null, RequestMethodTypeEnum.POST);
            //请求
            SpotTradeOrderRModel spotTradeOrderRModel = JsonConvert.DeserializeObject<SpotTradeOrderRModel>(res);

        }

        #endregion

        #region MyRegion

        #endregion
    }
}
