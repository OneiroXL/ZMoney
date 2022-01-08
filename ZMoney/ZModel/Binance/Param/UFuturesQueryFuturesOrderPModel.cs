using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Param
{
    /// <summary>
    /// 合约查询订单信息
    /// </summary>
    public class UFuturesQueryFuturesOrderPModel
    {
        /// <summary>
        /// 交易对
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }
        /// <summary>
        /// 目标杠杆倍数：1 到 125 整数
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public long OrderId { set; get; }

        /// <summary>
        /// 用户自定义的订单号
        /// </summary>
        [JsonProperty(PropertyName = "origClientOrderId")]
        public string OrigClientOrderId { set; get; }
    }
}
