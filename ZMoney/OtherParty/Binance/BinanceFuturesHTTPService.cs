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
using static ZBase.ZEnum.WebEnum;

namespace ZOtherParty.Binance
{
    /// <summary>
    /// 币安 Futures HTTP 服务
    /// </summary>
    public class BinanceFuturesHTTPService : BinanceHTTPServiceBase
    {
        /// <summary>
        /// API地址
        /// </summary>
        public override string APIAddress => AppSettingHelper.GetConfig("Binance:FuturesAddress");

        #region PING
        /// <summary>
        /// PING
        /// </summary>
        public bool SpotPing()
        {
            //地址
            string url = "/fapi/v1/ping";

            var res = HandleWebRequest<Object>(APIAddress + url, null, RequestMethodTypeEnum.GET, false);
            if (res != "{}")
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 获取服务器时间
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime QueryServerTime()
        {
            //地址
            string url = "/fapi/v1/time";

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

        #region 查询交易对当前最新价格

        /// <summary>
        /// 查询现易对当前最新价格
        /// </summary>
        /// <param name="querySymbolNewestPricePModel"></param>
        /// <returns></returns>
        public QuerySymbolNewestPriceRModel QuerySymbolNewestPrice(QuerySymbolNewestPricePModel querySymbolNewestPricePModel)
        {
            //地址
            string url = "/fapi/v1/ticker/price";

            string res = HandleWebRequest(APIAddress + url, querySymbolNewestPricePModel, RequestMethodTypeEnum.GET, false);
            //请求
            QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = JsonConvert.DeserializeObject<QuerySymbolNewestPriceRModel>(res);

            return querySymbolNewestPriceRModel;
        }

        #endregion

        #region 合约下单
        /// <summary>
        /// 合约下单
        /// </summary>
        /// <param name="spotTradeOrderParam"></param>
        public FuturesTradeOrderRModel FuturesTradeOrder(FuturesTradeOrderPModel futuresTradeOrderPModel)
        {
            //地址
            string url = "/fapi/v1/order";

            string res = HandleWebRequest(APIAddress + url, futuresTradeOrderPModel, RequestMethodTypeEnum.POST);
            //请求
            FuturesTradeOrderRModel futuresTradeOrderRModel = JsonConvert.DeserializeObject<FuturesTradeOrderRModel>(res);

            return futuresTradeOrderRModel;
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
            string url = "/fapi/v1/order";

            string res = HandleWebRequest(APIAddress + url, spotCancelOrderPModel, RequestMethodTypeEnum.DELETE);
            //请求
            SpotCancelOrderRModel spotCancelOrderRModel = JsonConvert.DeserializeObject<SpotCancelOrderRModel>(res);

            return spotCancelOrderRModel;
        }
        #endregion

    }
}
