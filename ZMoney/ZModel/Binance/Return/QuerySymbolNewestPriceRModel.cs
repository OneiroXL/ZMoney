using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 获取交易对最新价格返回数据
    /// </summary>
    public class QuerySymbolNewestPriceRModel
    {
        /// <summary>
        /// 交易对 symbol STRING  YES
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }

        /// <summary>
        /// price   DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { set; get; }
    }
}
