using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.NetCore;
using ZBase.Tools;
using ZModel.Binance.Param;
using ZOtherParty.Binance;

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
            Console.WriteLine("Hello World!");


            BinanceService.Ping();
            var a = BinanceService.WalletServerStatus();
            var b = BinanceService.GetServerTime();
            BinanceService.SpotTradeOrder(new SpotTradeOrderPModel());

            var sadasd =  WebTool.Get("https://www.google.com/");

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
