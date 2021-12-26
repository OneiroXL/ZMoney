using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.NetCore;
using ZBase.Tools;
using ZCore.ReapProfitJustSell;
using ZModel.Binance.Param;
using ZModel.Binance.Return;
using ZOtherParty.Binance;
using static ZBase.ZEnum.BinanceEnum;

namespace ZMoney
{
    // See https://aka.ms/new-console-template for more information
    public class Program
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BinanceSPOTHTTPService binanceSPOTHTTPService = new BinanceSPOTHTTPService();

            ConsoleTool.WriteLine("币安系统信息:", ConsoleColor.White);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService SpotPing:{0}", binanceSPOTHTTPService.SpotPing()), ConsoleColor.Blue);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService GetSpotServerTime:{0}", binanceSPOTHTTPService.GetSpotServerTime()), ConsoleColor.Blue);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService WalletServerStatus:{0}", binanceSPOTHTTPService.WalletServerStatus().Status), ConsoleColor.Blue);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService QueryExchangeInfo:{0}", binanceSPOTHTTPService.QueryExchangeInfo()), ConsoleColor.Blue);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService QueryExchangeInfo.SymbolModelList.Count:{0}", binanceSPOTHTTPService.QueryExchangeInfo().SymbolModelList.Count), ConsoleColor.Blue);
            ConsoleTool.WriteLine(string.Format("BinanceSPOTHTTPService QueryExchangeInfo.SymbolModelSpotTradingAllowedList.Count:{0}", binanceSPOTHTTPService.QueryExchangeInfo().SymbolModelList.Where(p => p.IsSpotTradingAllowed).Count()), ConsoleColor.Blue);

            ReapProfitJustSellCore.SPOTBinance(true);

            //WebSocket webSocket = new WebSocket("wss://stream.binance.com:9443");

            //webSocket.SetProxy("http://hk18.balala2016.xyz:80", "b5a97e40-0b08-11eb-b38f-a98cdf692636", "");

            //webSocket.OnMessage += (sender, e) =>
            //{
            //    Console.WriteLine("Laputa says: " + e.Data);
            //};

            //webSocket.Connect();
            //var sadas = webSocket.Ping();
            //string a = "asdasds";
            //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(a);

            //webSocket.Send(byteArray);

        }
    }
}
