using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZBase.ZEnum.BinanceEnum;

/*
基于订单 type不同，强制要求某些参数:

类型	强制要求的参数
LIMIT	timeInForce, quantity, price
MARKET	quantity or quoteOrderQty
STOP_LOSS	quantity, stopPrice
STOP_LOSS_LIMIT	timeInForce, quantity, price, stopPrice
TAKE_PROFIT	quantity, stopPrice
TAKE_PROFIT_LIMIT	timeInForce, quantity, price, stopPrice
LIMIT_MAKER	quantity, price
其他信息:

LIMIT_MAKER是LIMIT订单，如果它们立即匹配并成为吃单方将被拒绝。
当触发stopPrice时，STOP_LOSS和TAKE_PROFIT将执行MARKET订单。
任何LIMIT或LIMIT_MAKER类型的订单都可以通过发送icebergQty而成为iceberg订单。
任何带有icebergQty的订单都必须将timeInForce设置为GTC。
使用 quantity 的市价单 MARKET 明确的是用户想用市价单买入或卖出的数量。
比如在BTCUSDT上下一个市价单, quantity用户指明能够买进或者卖出多少BTC。
使用 quoteOrderQty 的市价单MARKET 明确的是通过买入(或卖出)想要花费(或获取)的报价资产数量; 此时的正确报单数量将会以市场流动性和quoteOrderQty被计算出来。
以BTCUSDT为例, quoteOrderQty=100:
下买单的时候, 订单会尽可能的买进价值100USDT的BTC.
下卖单的时候, 订单会尽可能的卖出价值100USDT的BTC.
使用 quoteOrderQty 的市价单MARKET不会突破LOT_SIZE的限制规则; 报单会按给定的quoteOrderQty尽可能接近地被执行。
除非之前的订单已经成交, 不然设置了相同的newClientOrderId订单会被拒绝。
MARKET版本和LIMIT版本针对市场价格触发订单价格规则：

价格高于市价：止损买入，获利卖出
价格低于市价：止损卖出，获利买入
关于 newOrderRespType的三种选择

Response ACK: 返回速度最快，不包含成交信息，信息量最少
Response RESULT:返回速度居中，返回吃单成交的少量信息
Response FULL: 返回速度最慢，返回吃单成交的详细信息
 */
namespace ZModel.Binance.Param
{
    /// <summary>
    /// 现货下单参数
    /// </summary>
    public class SpotTradeOrderPModel
    {
        /// <summary>
        /// 交易对 symbol STRING  YES
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }

        /// <summary>
        /// side    ENUM YES 详见枚举定义：订单方向
        /// </summary>
        [JsonProperty(PropertyName = "side")]
        public SPOTSideEuum Side { set; get; }

        /// <summary>
        /// type    ENUM YES 详见枚举定义：订单类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public OrderTypesEnum Type { set; get; }

        /// <summary>
        /// timeInForce ENUM NO  详见枚举定义：有效方式
        /// </summary>
        [JsonProperty(PropertyName = "timeInForce")]
        public TimeInForceEnum TimeInForce { set; get; }

        /// <summary>
        /// quantity    DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public decimal? Quantity { set; get; }

        /// <summary>
        /// quoteOrderQty DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "quoteOrderQty")]
        public decimal? QuoteOrderQty { set; get; }

        /// <summary>
        /// price   DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal? Price { set; get; }

        /// <summary>
        /// newClientOrderId STRING  NO 客户自定义的唯一订单ID。 如果未发送，则自动生成
        /// </summary>
        [JsonProperty(PropertyName = "newClientOrderId")]
        public string? NewClientOrderId { set; get; }

        /// <summary>
        /// stopPrice   DECIMAL NO  仅 STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, 和TAKE_PROFIT_LIMIT 需要此参数。
        /// </summary>
        [JsonProperty(PropertyName = "stopPrice")]
        public decimal? StopPrice { set; get; }

        /// <summary>
        /// icebergQty DECIMAL NO 仅使用 LIMIT, STOP_LOSS_LIMIT, 和 TAKE_PROFIT_LIMIT 创建新的 iceberg 订单时需要此参数
        /// </summary>
        [JsonProperty(PropertyName = "icebergQty")]
        public decimal? IcebergQty { set; get; }

        /// <summary>
        /// newOrderRespType    ENUM NO  设置响应JSON。 ACK，RESULT或FULL； "MARKET"和" LIMIT"订单类型默认为"FULL"，所有其他订单默认为"ACK"。
        /// </summary>
        [JsonProperty(PropertyName = "newOrderRespType")]
        public NewOrderRespTypeEnum NewOrderRespType { set; get; }
    }
}
