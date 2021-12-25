using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Param
{
    /// <summary>
    /// 现货取消订单
    /// </summary>
    public class SpotCancelOrderPModel
    {
        /// <summary>
        /// 交易对 symbol STRING  YES
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// OrigClientOrderId STRING  NO 客户自定义的唯一订单ID。 如果未发送，则自动生成
        /// </summary>
        [JsonProperty(PropertyName = "origClientOrderId")]
        public string? OrigClientOrderId { set; get; }

        /// newClientOrderId STRING  NO 客户自定义的唯一订单ID。 如果未发送，则自动生成
        /// </summary>
        [JsonProperty(PropertyName = "newClientOrderId")]
        public string? NewClientOrderId { set; get; }
    }
}
