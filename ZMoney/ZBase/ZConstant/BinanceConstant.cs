using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.ZConstant
{
    /// <summary>
    /// 币安 服务常量
    /// </summary>
    public class BinanceConstant
    {
        /// <summary>
        /// 最小交易量
        /// </summary>
        public static readonly decimal MinTradingVolume = 10M;

        /// <summary>
        /// 现货 手续费率
        /// </summary>
        public static readonly decimal SPOTServiceCharge = 1 / 1000M;

        /// <summary>
        /// U本位合约 手续费率
        /// </summary>
        public static readonly decimal UFuturesServiceCharge = 1 / 1000M;
    }

}
