using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZBase.ZEnum.BinanceEnum;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 查询合约订单信息
    /// </summary>
    public class UFuturesQueryFuturesOrderRModel
    {
        /// <summary>
        ///  平均成交价
        /// </summary>
        [JsonProperty(PropertyName = "avgPrice")]
        public decimal AvgPrice { set; get; }

        /// <summary>
        /// 用户自定义订单
        /// </summary>
        [JsonProperty(PropertyName = "clientOrderId")]
        public string ClientOrderId { set; get; }

        /// <summary>
        /// 用户自定义订单
        /// </summary>
        [JsonProperty(PropertyName = "cumQty")]
        public decimal CumQty { set; get; }

        /// <summary>
        /// 成交量
        /// </summary>
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQty { set; get; }

        /// <summary>
        /// 成交金额
        /// </summary>
        [JsonProperty(PropertyName = "cumQuote")]
        public decimal CumQuote { set; get; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// 用户设置的原始订单数量
        /// </summary>
        [JsonProperty(PropertyName = "origQty")]
        public decimal OrigQty { set; get; }

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { set; get; }

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty(PropertyName = "reduceOnly")]
        public bool ReduceOnly { set; get; }

        /// <summary>
        /// 买卖方向
        /// </summary>
        [JsonProperty(PropertyName = "side")]
        public string Side { set; get; }

        /// <summary>
        /// 持仓方向
        /// </summary>
        [JsonProperty(PropertyName = "positionSide")]
        public string PositionSide { set; get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public TradeOrderStatusEnum Status { set; get; }

        /// <summary>
        ///  触发价，对`TRAILING_STOP_MARKET`无效
        /// </summary>
        [JsonProperty(PropertyName = "stopPrice")]
        public decimal StopPrice { set; get; }

        /// <summary>
        ///  触发价，对`TRAILING_STOP_MARKET`无效
        /// </summary>
        [JsonProperty(PropertyName = "closePosition")]
        public bool ClosePosition { set; get; }

        /// <summary>
        /// 交易对
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// 订单的时效方式
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public double Time { set; get; }

        /// <summary>
        /// 订单的时效方式
        /// </summary>
        [JsonProperty(PropertyName = "timeInForce")]
        public string TimeInForce { set; get; }

        /// <summary>
        ///  订单类型， 比如市价单，现价单等
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; }

        /// <summary>
        ///  订单类型， 比如市价单，现价单等
        /// </summary>
        [JsonProperty(PropertyName = "origType")]
        public string OrigType { set; get; }

        /// <summary>
        /// 跟踪止损激活价格, 仅`TRAILING_STOP_MARKET` 订单返回此字段
        /// </summary>
        [JsonProperty(PropertyName = "activatePrice")]
        public decimal ActivatePrice { set; get; }

        /// <summary>
        /// 跟踪止损回调比例, 仅`TRAILING_STOP_MARKET` 订单返回此字段
        /// </summary>
        [JsonProperty(PropertyName = "priceRate")]
        public decimal PriceRate { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "updateTime")]
        public double UpdateTime { set; get; }

        /// <summary>
        /// 条件价格触发类型
        /// </summary>
        [JsonProperty(PropertyName = "workingType")]
        public string WorkingType { set; get; }

        /// <summary>
        /// 是否开启条件单触发保护
        /// </summary>
        [JsonProperty(PropertyName = "priceProtect")]
        public bool PriceProtect { set; get; }
    }
}
