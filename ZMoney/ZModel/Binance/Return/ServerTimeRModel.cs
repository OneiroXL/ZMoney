using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 服务器时间
    /// </summary>
    public class ServerTimeRModel
    {
        /// <summary>
        /// 服务器时间
        /// </summary>
        [JsonProperty(PropertyName = "serverTime")]
        public long ServerTime { set; get; }
    }
}
