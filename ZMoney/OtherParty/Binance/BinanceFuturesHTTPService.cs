using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZOtherParty.Base.BinanceBase;

namespace ZOtherParty.Binance
{
    /// <summary>
    /// 币安 Futures HTTP 服务
    /// </summary>
    public class BinanceFuturesHTTPService : BinanceHTTPServiceBase
    {
        /// <summary>
        /// API地址
        /// </summary>
        public override string APIAddress => AppSettingHelper.GetConfig("Binance:FuturesAddress");



    }
}
