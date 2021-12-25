using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 获取交易对平均价格
    /// </summary>
    public class QuerySymbolAvgPriceRModel
    {
        /// <summary>
        /// 分钟数
        /// </summary>
        [JsonProperty(PropertyName = "mins")]
        public int Mins { set; get; }

        /// <summary>
        /// price   DECIMAL NO
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price { set; get; }
    }
}
