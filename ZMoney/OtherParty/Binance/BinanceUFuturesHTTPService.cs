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
    public class BinanceUFuturesHTTPService : BinanceHTTPServiceBase
    {
        /// <summary>
        /// API地址
        /// </summary>
        public override string APIAddress => AppSettingHelper.GetConfig("Binance:FuturesAddress");

        #region PING
        /// <summary>
        /// PING
        /// </summary>
        public bool Ping()
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
        public UFuturesTradeOrderRModel FuturesTradeOrder(UFuturesTradeOrderPModel futuresTradeOrderPModel)
        {
            //地址
            string url = "/fapi/v1/order";

            string res = HandleWebRequest(APIAddress + url, futuresTradeOrderPModel, RequestMethodTypeEnum.POST);
            //请求
            UFuturesTradeOrderRModel futuresTradeOrderRModel = JsonConvert.DeserializeObject<UFuturesTradeOrderRModel>(res);

            return futuresTradeOrderRModel;
        }

        #endregion

        #region 查询订单信息

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="spotTradeOrderParam"></param>
        public UFuturesQueryFuturesOrderRModel QueryFuturesOrder(UFuturesQueryFuturesOrderPModel uFuturesQueryFuturesOrderPModel)
        {
            //地址
            string url = "/fapi/v1/order";

            string res = HandleWebRequest(APIAddress + url, uFuturesQueryFuturesOrderPModel, RequestMethodTypeEnum.GET);
            //请求
            UFuturesQueryFuturesOrderRModel uFuturesQueryFuturesOrderRModel = JsonConvert.DeserializeObject<UFuturesQueryFuturesOrderRModel>(res);

            return uFuturesQueryFuturesOrderRModel;
        }

        #endregion

        #region 现货取消订单
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="uFuturesCancelOrderPModel"></param>
        public UFuturesCancelOrderRModel CancelOrder(UFuturesCancelOrderPModel uFuturesCancelOrderPModel)
        {
            //地址
            string url = "/fapi/v1/order";

            string res = HandleWebRequest(APIAddress + url, uFuturesCancelOrderPModel, RequestMethodTypeEnum.DELETE);
            //请求
            UFuturesCancelOrderRModel uFuturesCancelOrderRModel = JsonConvert.DeserializeObject<UFuturesCancelOrderRModel>(res);

            return uFuturesCancelOrderRModel;
        }
        #endregion


        #region 调整杠杆
        /// <summary>
        /// 调整杠杆
        /// </summary>
        /// <param name="uFuturesAdjustmentLeveragePModel"></param>
        public UFuturesAdjustmentLeverageRModel AdjustmentLeverage(UFuturesAdjustmentLeveragePModel uFuturesAdjustmentLeveragePModel)
        {
            //地址
            string url = "/fapi/v1/leverage";

            string res = HandleWebRequest(APIAddress + url, uFuturesAdjustmentLeveragePModel, RequestMethodTypeEnum.POST);
            //请求
            UFuturesAdjustmentLeverageRModel uFuturesAdjustmentLeverageRModel = JsonConvert.DeserializeObject<UFuturesAdjustmentLeverageRModel>(res);

            return uFuturesAdjustmentLeverageRModel;
        }

        #endregion

        #region 查询用户余额
        /// <summary>
        /// 调整杠杆
        /// </summary>
        /// <param name="uFuturesAdjustmentLeveragePModel"></param>
        public Dictionary<string, UFuturesQueryBalanceRModel> QueryBalance()
        {
            //地址
            string url = "/fapi/v2/balance";

            string res = HandleWebRequest(APIAddress + url, new UFuturesQueryBalancePModel(), RequestMethodTypeEnum.GET);
            //请求
            List<UFuturesQueryBalanceRModel> uFuturesQueryBalanceRModelList = JsonConvert.DeserializeObject<List<UFuturesQueryBalanceRModel>>(res);

            return uFuturesQueryBalanceRModelList.ToDictionary(p => p.Asset, p => p);
        }


        #endregion
    }
}
