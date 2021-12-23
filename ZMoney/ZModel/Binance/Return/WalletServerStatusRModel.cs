using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 服务期状态返回model
    /// </summary>
    public class WalletServerStatusRModel
    {
        /// <summary>
        /// 状态 0为正常
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; } = string.Empty;
    }
}
