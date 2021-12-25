﻿using Newtonsoft.Json;
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
        public static string APIKey { get => AppSettingHelper.GetConfig("Binance:APIKey"); }

        /// <summary>
        /// APISecret
        /// </summary>
        public static string APISecret { get => AppSettingHelper.GetConfig("Binance:APISecret"); }

        /// <summary>
        /// API地址
        /// </summary>
        public static string APIAddress => "https://api1.binance.com";

        #region 处理WEB请求
        /// <summary>
        /// 处理WEB请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestMethodTypeEnum">请求方式枚举</param>
        /// <param name="isNeedParamModel">是否需要参数</param>
        public static string HandleWebRequest<PT>(string url, PT paramModel , RequestMethodTypeEnum requestMethodTypeEnum,bool isNeedParamModel = true)
        {
            //参数字典
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            if (paramModel != null) 
            {
                paramDic = ReflexionHelper.ClassFieldJsonPropertyToDictionary(paramModel);
            }
            //当前时间戳
            paramDic["timestamp"] = DateTimeTool.GetTimeStamp(DateTime.Now).ToString().PadRight(13, '0');
            //时间限制
            paramDic["recvWindow"] = "5000";
            //签名
            paramDic["signature"] = EncryptiontTool.HMACSHA256Encrypt(WebTool.AssembleXWFUParam(paramDic), APISecret);


            //请求头
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Content-Type"] = "application/x-www-form-urlencoded";
            headerDic["X-MBX-APIKEY"] = APIKey;
            //返回数据
            string resposeStr = string.Empty;
            //请求方式
            switch (requestMethodTypeEnum) 
            {
                case RequestMethodTypeEnum.POST:
                    {
                        resposeStr = WebTool.Post(url, paramDic, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.GET:
                    {
                        resposeStr = WebTool.Get(url, isNeedParamModel ? paramDic : null, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.PUT:
                    {

                    }
                    break;
                case RequestMethodTypeEnum.DELETE:
                    {

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

            var res = HandleWebRequest<Object>(APIAddress + url, null, RequestMethodTypeEnum.GET, false);
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
            string res = HandleWebRequest<Object>(APIAddress + url, null, RequestMethodTypeEnum.GET);
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

            string res = HandleWebRequest<Object>(APIAddress + url, null, RequestMethodTypeEnum.GET, false);
            //请求
            ServerTimeRModel serverTimeRModel = JsonConvert.DeserializeObject<ServerTimeRModel>(res);

            if (serverTimeRModel == null) 
            {
                return DateTime.MinValue;
            }

            return DateTimeTool.GetDateTime(serverTimeRModel.ServerTime / 1000);
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
            spotTradeOrderParam.Quantity = 0.005m;
            spotTradeOrderParam.NewOrderRespType = NewOrderRespTypeEnum.FULL;
            spotTradeOrderParam.Type = OrderTypesEnum.LIMIT;
            spotTradeOrderParam.TimeInForce = TimeInForceEnum.GTC;

            //地址
            string url = "/api/v3/order";

            string res = HandleWebRequest<SpotTradeOrderPModel>(APIAddress + url, spotTradeOrderParam, RequestMethodTypeEnum.POST);
            //请求
            SpotTradeOrderRModel spotTradeOrderRModel = JsonConvert.DeserializeObject<SpotTradeOrderRModel>(res);

        }

        #endregion

        #region 取消订单

        #endregion
    }
}
