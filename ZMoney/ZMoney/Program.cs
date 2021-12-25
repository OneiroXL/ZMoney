using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.NetCore;
using ZBase.Tools;
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
            Console.WriteLine("BinanceServer PING:{0}", BinanceHTTPService.Ping());
            Console.WriteLine("BinanceServer WalletServerStatus:{0}", BinanceHTTPService.WalletServerStatus());
            Console.WriteLine("BinanceServer Time:{0}", BinanceHTTPService.GetServerTime());


            ////现货下单
            //SpotTradeOrderPModel spotTradeOrderParam = new SpotTradeOrderPModel();

            //spotTradeOrderParam.Symbol = "ETHUSDT";
            //spotTradeOrderParam.Side = SPOTSideEuum.BUY;
            //spotTradeOrderParam.Price = 2500m;
            //spotTradeOrderParam.Quantity = 0.005m;
            //spotTradeOrderParam.NewOrderRespType = NewOrderRespTypeEnum.FULL;
            //spotTradeOrderParam.Type = OrderTypesEnum.LIMIT;
            //spotTradeOrderParam.TimeInForce = TimeInForceEnum.GTC;
            //SpotTradeOrderRModel spotTradeOrderRModel = BinanceService.SpotTradeOrder(spotTradeOrderParam);

            ////查询现货订单信息
            //SpotQueryOrderInfoPModel spotQueryOrderInfoPModel = new SpotQueryOrderInfoPModel();
            //spotQueryOrderInfoPModel.Symbol = "ETHUSDT";
            //spotQueryOrderInfoPModel.OrderId = spotTradeOrderRModel.OrderId;
            //spotQueryOrderInfoPModel.OrigClientOrderId = spotTradeOrderRModel.ClientOrderId;


            //SpotQueryOrderInfoRModel spotQueryOrderInfoRModel = BinanceService.SpotQueryOrderInfo(spotQueryOrderInfoPModel);


            ////取消下单
            //SpotCancelOrderPModel spotCancelOrderPModel = new SpotCancelOrderPModel();

            //spotCancelOrderPModel.Symbol = "ETHUSDT";
            //spotCancelOrderPModel.OrderId = spotTradeOrderRModel.OrderId;
            //spotCancelOrderPModel.OrigClientOrderId = spotTradeOrderRModel.ClientOrderId;

            //SpotCancelOrderRModel spotCancelOrderRModel = BinanceService.SpotCancelOrder(spotCancelOrderPModel);


            //获取交易对平均价格
            //QuerySymbolAvgPricePModel querySymbolAvgPricePModel = new QuerySymbolAvgPricePModel();
            //querySymbolAvgPricePModel.Symbol = "ETHUSDT";

            //while (true)//int i = 0;i <= 10;i++
            //{
            //    Thread.Sleep(1000);

            //    QuerySymbolAvgPriceRModel querySymbolAvgPriceRModel = BinanceService.QuerySymbolAvgPrice(querySymbolAvgPricePModel);

            //    Console.WriteLine($"Mins:{querySymbolAvgPriceRModel.Mins},Price:{querySymbolAvgPriceRModel.Price}");
            //}

            //获取交易对最新价格
            QuerySymbolNewestPricePModel querySymbolNewestPricePModel = new QuerySymbolNewestPricePModel();
            querySymbolNewestPricePModel.Symbol = "ETHUSDT";

            while (true)//int i = 0;i <= 10;i++
            {
                Thread.Sleep(1000);

                QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = BinanceHTTPService.QuerySymbolNewestPrice(querySymbolNewestPricePModel);

                Console.WriteLine($"Symbol:{querySymbolNewestPriceRModel.Symbol},Price:{querySymbolNewestPriceRModel.Price}");
            }


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
