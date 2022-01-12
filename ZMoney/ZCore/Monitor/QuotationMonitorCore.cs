using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZModel.Binance.Param;
using ZModel.Binance.Return;
using ZOtherParty.Binance;

namespace ZCore.Monitor
{
    /// <summary>
    /// 行情监控
    /// </summary>
    public class QuotationMonitorCore
    {
        #region 现货价格监控
        /// <summary>
        /// 现货价格监控
        /// </summary>
        /// <param name="symbol"></param>
        public static void SPOTPriceMonitor(string symbol) 
        {
            //币安现货服务
            BinanceUFuturesHTTPService binanceUFuturesHTTPService = new BinanceUFuturesHTTPService();

            //获取实时价格
            QuerySymbolNewestPricePModel querySymbolNewestPricePModel = new QuerySymbolNewestPricePModel();
            querySymbolNewestPricePModel.Symbol = symbol;
            QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = binanceUFuturesHTTPService.QuerySymbolNewestPrice(querySymbolNewestPricePModel);


            ConsoleTool.WriteLine(String.Format("当前交易对:{0},当前交易对最新价格:{1}", querySymbolNewestPriceRModel.Symbol, querySymbolNewestPriceRModel.Price), ConsoleColor.Yellow);

            EmailTool.SendEmail("871018736@qq.com", $"当前交易对:{symbol}", String.Format("当前交易对:{0},当前交易对最新价格:{1}", querySymbolNewestPriceRModel.Symbol, querySymbolNewestPriceRModel.Price));

        }
        #endregion


        #region 合约价格监控
        /// <summary>
        /// 合约价格监控
        /// </summary>
        /// <param name="symbol"></param>
        public static void FuturesPriceMonitor(string symbol) 
        {

        }

        #endregion
    }
}
