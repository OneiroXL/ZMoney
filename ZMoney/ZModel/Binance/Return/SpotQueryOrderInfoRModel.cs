using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 查询订单返回信息
    /// </summary>
    public class SpotQueryOrderInfoRModel
    {
        /// <summary>
        /// 交易对
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public long OrderId { get; set; }

        /// <summary>
        ///  OCO订单ID，否则为 -1
        /// </summary>
        [JsonProperty(PropertyName = "orderListId")]
        public long OrderListId { set; get; }

        /// <summary>
        /// 用户自定义订单
        /// </summary>
        [JsonProperty(PropertyName = "clientOrderId")]
        public string ClientOrderId { set; get; }

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { set; get; }

        /// <summary>
        /// 用户设置的原始订单数量
        /// </summary>
        [JsonProperty(PropertyName = "origQty")]
        public decimal OrigQty { set; get; }

        /// <summary>
        /// 交易的订单数量
        /// </summary>
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQty { set; get; }

        /// <summary>
        /// 累计交易的金额
        /// </summary>
        [JsonProperty(PropertyName = "cummulativeQuoteQty")]
        public decimal CummulativeQuoteQty { set; get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { set; get; }

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
        /// 订单方向，买还是卖
        /// </summary>
        [JsonProperty(PropertyName = "side")]
        public string Side { set; get; }

        /// <summary>
        /// 止损价格
        /// </summary>
        [JsonProperty(PropertyName = "stopPrice")]
        public decimal stopPrice { set; get; }

        /// <summary>
        /// 冰山数量
        /// </summary>
        [JsonProperty(PropertyName = "icebergQty")]
        public decimal IcebergQty { set; get; }

        /// <summary>
        /// 订单时间
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public double Time { set; get; }

        /// <summary>
        ///  最后更新时间
        /// </summary>
        [JsonProperty(PropertyName = "updateTime")]
        public double UpdateTime { set; get; }

        /// <summary>
        /// 订单是否出现在orderbook中
        /// </summary>
        [JsonProperty(PropertyName = "isWorking")]
        public string IsWorking { set; get; }

        /// <summary>
        /// 原始的交易金额
        /// </summary>
        [JsonProperty(PropertyName = "origQuoteOrderQty")]
        public decimal OrigQuoteOrderQty { set; get; }
    }
}
