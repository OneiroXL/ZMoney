using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZModel.Binance.Param;
using ZModel.Binance.Return;
using ZOtherParty.Binance;
using static ZBase.ZEnum.BinanceEnum;

namespace ZCore.ReapProfitJustSell
{
    /// <summary>
    /// 获利就卖
    /// </summary>
    public class ReapProfitJustSellCore
    {
        #region 币安现货
        /// <summary>
        /// 币安现货
        /// </summary>
        public static void SPOTBinance(bool isDebug) 
        {
            //赚了就卖策略
            ConsoleTool.WriteLine("ReapProfitJustSellCore Start:", ConsoleColor.Magenta);

            //交易对(每次交易需要10U以上)
            string symbol = "SANDUSDT";
            decimal mixPrice = 10m;

            //币安现货服务
            BinanceSPOTHTTPService binanceSPOTHTTPService = new BinanceSPOTHTTPService();

            //获取交易对平均价格
            QuerySymbolAvgPricePModel querySymbolAvgPricePModel = new QuerySymbolAvgPricePModel();
            querySymbolAvgPricePModel.Symbol = symbol;
            QuerySymbolAvgPriceRModel querySymbolAvgPriceRModel = binanceSPOTHTTPService.SpotQuerySymbolAvgPrice(querySymbolAvgPricePModel);
            ConsoleTool.WriteLine(String.Format("当前交易对:{0},分钟数:{1},当前交易对平均价格:{2}", symbol, querySymbolAvgPriceRModel.Mins, querySymbolAvgPriceRModel.Price), ConsoleColor.Yellow);

            //获取实时价格
            QuerySymbolNewestPricePModel querySymbolNewestPricePModel = new QuerySymbolNewestPricePModel();
            querySymbolNewestPricePModel.Symbol = symbol;
            QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = binanceSPOTHTTPService.SpotQuerySymbolNewestPrice(querySymbolNewestPricePModel);
            ConsoleTool.WriteLine(String.Format("当前交易对:{0},当前交易对最新价格:{1}", querySymbolNewestPriceRModel.Symbol, querySymbolNewestPriceRModel.Price), ConsoleColor.Yellow);

            //计算价格和数量
            decimal quantity = Math.Ceiling(mixPrice / querySymbolNewestPriceRModel.Price);

            

            //现货下单(买入)
            SpotTradeOrderPModel buySpotTradeOrderPModel = new SpotTradeOrderPModel();

            buySpotTradeOrderPModel.Symbol = symbol;
            buySpotTradeOrderPModel.Side = SPOTSideEuum.BUY;
            buySpotTradeOrderPModel.Price = querySymbolNewestPriceRModel.Price;
            buySpotTradeOrderPModel.Quantity = quantity;
            buySpotTradeOrderPModel.NewOrderRespType = NewOrderRespTypeEnum.FULL;
            buySpotTradeOrderPModel.Type = OrderTypesEnum.LIMIT;
            buySpotTradeOrderPModel.TimeInForce = TimeInForceEnum.FOK;
            SpotTradeOrderRModel buySpotTradeOrderRModel = isDebug ? new SpotTradeOrderRModel() { Status = SPOTTradestatusEnum.FILLED } : binanceSPOTHTTPService.SpotTradeOrder(buySpotTradeOrderPModel);

            //计算买入手续费
            var buyCommission = buySpotTradeOrderRModel.SpotTradeOrderFillsModelList.Sum(p => p.Commission) * querySymbolNewestPriceRModel.Price;

            ConsoleTool.WriteLine(String.Format("当前买入交易对:{0},当前交易对交易价格:{1},当前交易对交易数量{2},交易状态:{3},交易订单号:{4},交易手续费：{5}", symbol, buySpotTradeOrderRModel.Price, buySpotTradeOrderRModel.ExecutedQty, buySpotTradeOrderRModel.Status, buySpotTradeOrderRModel.OrderId, buyCommission), ConsoleColor.Yellow);


            //如果成交
            if (buySpotTradeOrderRModel.Status == SPOTTradestatusEnum.FILLED)
            {
                //监控
                while (true)
                {
                    //获取
                    querySymbolNewestPriceRModel = binanceSPOTHTTPService.SpotQuerySymbolNewestPrice(querySymbolNewestPricePModel);
                    ConsoleTool.WriteLine(string.Format("当前交易对:{0},当前交易对买入价格{1},当前交易对最新价格:{2},差价:{3},估算获利:{4}", symbol, buySpotTradeOrderPModel.Price, querySymbolNewestPriceRModel.Price, querySymbolNewestPriceRModel.Price - buySpotTradeOrderPModel.Price, querySymbolNewestPriceRModel.Price * buySpotTradeOrderPModel.Quantity - buySpotTradeOrderPModel.Price * buySpotTradeOrderPModel.Quantity - buyCommission), ConsoleColor.DarkYellow, true, 500);

                    //当获利时
                    if (querySymbolNewestPriceRModel.Price - buySpotTradeOrderPModel.Price > 0.025m)
                    {
                        //现货下单(卖出)
                        SpotTradeOrderPModel sellSpotTradeOrderParam = new SpotTradeOrderPModel();

                        sellSpotTradeOrderParam.Symbol = symbol;
                        sellSpotTradeOrderParam.Side = SPOTSideEuum.SELL;
                        sellSpotTradeOrderParam.NewOrderRespType = NewOrderRespTypeEnum.FULL;
                        sellSpotTradeOrderParam.Type = OrderTypesEnum.LIMIT;
                        sellSpotTradeOrderParam.TimeInForce = TimeInForceEnum.FOK;
                        sellSpotTradeOrderParam.Quantity = quantity;
                        sellSpotTradeOrderParam.Price = querySymbolNewestPriceRModel.Price;


                        SpotTradeOrderRModel sellSpotTradeOrderRModel = new SpotTradeOrderRModel();
                        decimal sellCommission = 0;
                        while (true) 
                        {
                            //卖出变量信息
                            //sellSpotTradeOrderParam.Price -= 0.0005m;


                            //卖出
                            sellSpotTradeOrderRModel = isDebug ? new SpotTradeOrderRModel() { Status = SPOTTradestatusEnum.FILLED } : binanceSPOTHTTPService.SpotTradeOrder(sellSpotTradeOrderParam);
                            //计算卖出手续费
                            sellCommission = sellSpotTradeOrderRModel.SpotTradeOrderFillsModelList.Sum(p => p.Commission);
                            ConsoleTool.WriteLine(string.Format("当前卖出交易对:{0},当前交易对交易价格:{1},交易状态:{2},交易订单号:{3},交易手续费：{4}", sellSpotTradeOrderRModel.Symbol, sellSpotTradeOrderRModel.Price, sellSpotTradeOrderRModel.Status, sellSpotTradeOrderRModel.OrderId, sellCommission), ConsoleColor.Yellow);

                            if (sellSpotTradeOrderRModel.Status == SPOTTradestatusEnum.FILLED) 
                            {
                                break;
                            }

                            //睡眠一秒
                            Thread.Sleep(500);
                        }

                        //打印结果
                        ConsoleTool.WriteLine($@"
                            当前卖出交易对:{symbol},
                            当前交易对买入价格:{buySpotTradeOrderPModel.Price},
                            当前交易对买入数量:{buySpotTradeOrderPModel.Quantity},
                            当前交易对卖出价格:{sellSpotTradeOrderParam.Price},
                            当前交易对卖出数量:{sellSpotTradeOrderParam.Quantity},
                            交易手续费：{buyCommission + sellCommission},
                            获利:{sellSpotTradeOrderParam.Price * sellSpotTradeOrderParam.Quantity - buySpotTradeOrderPModel.Price * buySpotTradeOrderPModel.Quantity - buyCommission - sellCommission}".Trim()
                        , ConsoleColor.Red);

                        break;
                    }
                }
            }
            else
            {
                //睡眠一秒
                Thread.Sleep(2000);

                //取消下单
                SpotCancelOrderPModel spotCancelOrderPModel = new SpotCancelOrderPModel();

                spotCancelOrderPModel.Symbol = symbol;
                spotCancelOrderPModel.OrderId = buySpotTradeOrderRModel.OrderId;
                spotCancelOrderPModel.OrigClientOrderId = buySpotTradeOrderRModel.ClientOrderId;

                SpotCancelOrderRModel spotCancelOrderRModel = isDebug ? new SpotCancelOrderRModel() : binanceSPOTHTTPService.SpotCancelOrder(spotCancelOrderPModel);
                Console.WriteLine("取消当前交易对:{0},交易订单号:{1},交易状态:{2},", symbol, spotCancelOrderRModel.OrderId, spotCancelOrderRModel.Status);
            }

            //结束
            Console.WriteLine("HOOOOOOOOOOOOOOOOOOO 完成交易");
        }

        #endregion



    }
}
