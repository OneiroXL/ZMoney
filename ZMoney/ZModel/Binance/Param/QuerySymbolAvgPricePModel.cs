using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Param
{
    /// <summary>
    /// 获取交易对平均价格
    /// </summary>
    public class QuerySymbolAvgPricePModel
    {
        /// <summary>
        /// 交易对 symbol STRING  YES
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }

    }
}
