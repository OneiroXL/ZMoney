using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// U本位合约 用户钱包信息
    /// </summary>
    public class UFuturesQueryBalanceRModel
    {
        /// <summary>
        /// 账户唯一识别码
        /// </summary>
        [JsonProperty(PropertyName = "accountAlias")]
        public string AccountAlias { get; set; }

        /// <summary>
        /// 资产
        /// </summary>
        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; set; }

        /// <summary>
        /// 总余额
        /// </summary>
        [JsonProperty(PropertyName = "balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// 全仓余额
        /// </summary>
        [JsonProperty(PropertyName = "crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }

        /// <summary>
        /// 全仓持仓未实现盈亏
        /// </summary>
        [JsonProperty(PropertyName = "crossUnPnl")]
        public decimal CrossUnPnl { get; set; }

        /// <summary>
        /// 下单可用余额
        /// </summary>
        [JsonProperty(PropertyName = "availableBalance")]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// 最大可转出余额
        /// </summary>
        [JsonProperty(PropertyName = "maxWithdrawAmount")]
        public decimal MaxWithdrawAmount { get; set; }

        /// <summary>
        /// 是否可用作联合保证金
        /// </summary>
        [JsonProperty(PropertyName = "marginAvailable")]
        public bool MarginAvailable { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "updateTime")]
        public long UpdateTime { get; set; }

    }
}
