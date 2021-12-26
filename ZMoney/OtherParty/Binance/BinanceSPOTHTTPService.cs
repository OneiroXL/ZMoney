using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZModel.Binance.Param;
using ZModel.Binance.Return;
using ZOtherParty.Base.BinanceBase;
using static ZBase.ZEnum.BinanceEnum;
using static ZBase.ZEnum.WebEnum;

namespace ZOtherParty.Binance
{
    /// <summary>
    /// 币安 SPOT HTTP服务
    /// </summary>
    public class BinanceSPOTHTTPService : BinanceHTTPServiceBase
    {
        /// <summary>
        /// API地址
        /// </summary>
        public override string APIAddress => AppSettingHelper.GetConfig("Binance:SPOTAddress");

        /// <summary>
        /// 交易规范信息
        /// </summary>
        private static Dictionary<DateTime, QueryExchangeInfoRModel> QueryExchangeInfoRModelDic = new Dictionary<DateTime, QueryExchangeInfoRModel>();

        #region 钱包系统状态
        /// <summary>
        /// 钱包系统状态
        /// </summary>
        public WalletServerStatusRModel WalletServerStatus()
        {
            string url = "/sapi/v1/system/status";
            string res = HandleWebRequest<Object>(APIAddress + url, null, RequestMethodTypeEnum.GET);
            return JsonConvert.DeserializeObject<WalletServerStatusRModel>(res);
        }

        #endregion

        #region 现货PING
        /// <summary>
        /// 现货PING
        /// </summary>
        public bool SpotPing()
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

        #region 现货获取服务器时间
        /// <summary>
        /// 现货获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSpotServerTime()
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

        #region 查询交易规范信息
        /// <summary>
        /// 查询交易规范信息
        /// </summary>
        /// <param name="queryExchangeInfoPModel"></param>
        /// <returns></returns>
        public QueryExchangeInfoRModel QueryExchangeInfo() 
        {
            //地址
            string url = "/api/v3/exchangeInfo";

            if (QueryExchangeInfoRModelDic.Count == 0) 
            {
                string res = HandleWebRequest(APIAddress + url, new QueryExchangeInfoPModel(), RequestMethodTypeEnum.GET, false);
                //请求
                QueryExchangeInfoRModel queryExchangeInfoRModel = JsonConvert.DeserializeObject<QueryExchangeInfoRModel>(res);

                QueryExchangeInfoRModelDic[DateTime.Now] = queryExchangeInfoRModel;
            }

            return QueryExchangeInfoRModelDic.FirstOrDefault().Value;
        }

        #endregion

        #region 查询现货交易对当前最新价格

        /// <summary>
        /// 查询现货交易对当前最新价格
        /// </summary>
        /// <param name="querySymbolNewestPricePModel"></param>
        /// <returns></returns>
        public QuerySymbolNewestPriceRModel SpotQuerySymbolNewestPrice(QuerySymbolNewestPricePModel querySymbolNewestPricePModel)
        {
            //地址
            string url = "/api/v3/ticker/price";

            string res = HandleWebRequest(APIAddress + url, querySymbolNewestPricePModel, RequestMethodTypeEnum.GET, false);
            //请求
            QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = JsonConvert.DeserializeObject<QuerySymbolNewestPriceRModel>(res);

            return querySymbolNewestPriceRModel;
        }

        #endregion

        #region 获取现货交易对当前平均价格

        /// <summary>
        /// 获取现货交易对当前平均价格
        /// </summary>
        /// <param name="querySymbolAvgPricePModel"></param>
        /// <returns></returns>
        public QuerySymbolAvgPriceRModel SpotQuerySymbolAvgPrice(QuerySymbolAvgPricePModel querySymbolAvgPricePModel)
        {
            //地址
            string url = "/api/v3/avgPrice";

            string res = HandleWebRequest(APIAddress + url, querySymbolAvgPricePModel, RequestMethodTypeEnum.GET, false);
            //请求
            QuerySymbolAvgPriceRModel querySymbolAvgPriceRModel = JsonConvert.DeserializeObject<QuerySymbolAvgPriceRModel>(res);

            return querySymbolAvgPriceRModel;
        }


        #endregion

        #region 现货下单
        /// <summary>
        /// 现货下单
        /// </summary>
        /// <param name="spotTradeOrderParam"></param>
        public SpotTradeOrderRModel SpotTradeOrder(SpotTradeOrderPModel spotTradeOrderParam) 
        {
            //地址
            string url = "/api/v3/order";

            string res = HandleWebRequest(APIAddress + url, spotTradeOrderParam, RequestMethodTypeEnum.POST);
            //请求
            SpotTradeOrderRModel spotTradeOrderRModel = JsonConvert.DeserializeObject<SpotTradeOrderRModel>(res);

            return spotTradeOrderRModel;
        }

        #endregion

        #region 现货取消订单
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="spotCancelOrderPModel"></param>
        public SpotCancelOrderRModel SpotCancelOrder(SpotCancelOrderPModel spotCancelOrderPModel)
        {
            //地址
            string url = "/api/v3/order";

            string res = HandleWebRequest(APIAddress + url, spotCancelOrderPModel, RequestMethodTypeEnum.DELETE);
            //请求
            SpotCancelOrderRModel spotCancelOrderRModel = JsonConvert.DeserializeObject<SpotCancelOrderRModel>(res);

            return spotCancelOrderRModel;
        }
        #endregion

        #region 查询现货订单信息

        /// <summary>
        /// 查询现货订单信息
        /// </summary>
        /// <param name="spotCancelOrderPModel"></param>
        public SpotQueryOrderInfoRModel SpotQueryOrderInfo(SpotQueryOrderInfoPModel spotQueryOrderInfoPModel)
        {
            //地址
            string url = "/api/v3/order";
            //请求
            string res = HandleWebRequest(APIAddress + url, spotQueryOrderInfoPModel, RequestMethodTypeEnum.GET);
      
            SpotQueryOrderInfoRModel spotQueryOrderInfoRModel = JsonConvert.DeserializeObject<SpotQueryOrderInfoRModel>(res);

            return spotQueryOrderInfoRModel;
        }

        #endregion

    }
}
