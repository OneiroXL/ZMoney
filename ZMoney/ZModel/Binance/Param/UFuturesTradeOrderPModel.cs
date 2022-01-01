using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZBase.ZEnum.BinanceEnum;

namespace ZModel.Binance.Param
{
    /// <summary>
    /// 合约下单参数
    /// </summary>
    public class UFuturesTradeOrderPModel
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
        public SideEuum Side { set; get; }

        /// <summary>
        /// 持仓方向，单向持仓模式下非必填，默认且仅可填BOTH;在双向持仓模式下必填,且仅可选择 LONG 或 SHORT
        /// </summary>
        [JsonProperty(PropertyName = "positionSide")]
        public object PositionSide { set; get; }

        /// <summary>
        /// type    ENUM YES 详见枚举定义：订单类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public OrderTypesEnum Type { set; get; }

        /// <summary>
        /// true, false; 非双开模式下默认false；双开模式下不接受此参数； 使用closePosition不支持此参数。
        /// </summary>
        public string ReduceOnly { set; get; }

        /// <summary>
        /// quantity    DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public decimal? Quantity { set; get; }

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
        /// true, false；触发后全部平仓，仅支持STOP_MARKET和TAKE_PROFIT_MARKET；不与quantity合用；自带只平仓效果，不与reduceOnly 合用
        /// </summary>
        [JsonProperty(PropertyName = "closePosition")]
        public string ClosePosition { set; get; }

        /// <summary>
        /// 追踪止损激活价格，仅TRAILING_STOP_MARKET 需要此参数, 默认为下单当前市场价格(支持不同workingType)
        /// </summary>
        [JsonProperty(PropertyName = "activationPrice")]
        public decimal? ActivationPrice { set; get; }

        /// <summary>
        /// 追踪止损回调比例，可取值范围[0.1, 5],其中 1代表1% ,仅TRAILING_STOP_MARKET 需要此参数
        /// </summary>
        [JsonProperty(PropertyName = "callbackRate")]
        public decimal? CallbackRate { set; get; }

        /// <summary>
        /// timeInForce ENUM NO  详见枚举定义：有效方式
        /// </summary>
        [JsonProperty(PropertyName = "timeInForce")]
        public TimeInForceEnum TimeInForce { set; get; }

        /// <summary>
        /// wstopPrice 触发类型: MARK_PRICE(标记价格), CONTRACT_PRICE(合约最新价). 默认 CONTRACT_PRICE
        /// </summary>
        [JsonProperty(PropertyName = "workingType")]
        public string WorkingType { set; get; }

        /// <summary>
        /// 条件单触发保护："TRUE","FALSE", 默认"FALSE". 仅 STOP, STOP_MARKET, TAKE_PROFIT, TAKE_PROFIT_MARKET 需要此参数
        /// </summary>
        [JsonProperty(PropertyName = "priceProtect")]
        public string PriceProtect { set; get; }

        /// <summary>
        /// newOrderRespType    ENUM NO  设置响应JSON。 ACK，RESULT或FULL； "MARKET"和" LIMIT"订单类型默认为"FULL"，所有其他订单默认为"ACK"。
        /// </summary>
        [JsonProperty(PropertyName = "newOrderRespType")]
        public NewOrderRespTypeEnum NewOrderRespType { set; get; }
    }
}
