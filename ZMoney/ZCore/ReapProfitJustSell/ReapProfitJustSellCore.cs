using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using ZBase.ZConstant;
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
        public static void SPOTBinance(string symbol, bool isDebug = true) 
        {
            //赚了就卖策略(搬砖一段时间内可以赚取的最小利润)
            ConsoleTool.WriteLine($"ReapProfitJustSellCore Start:{symbol}", ConsoleColor.Magenta);

            //币安现货服务
            BinanceSPOTHTTPService binanceSPOTHTTPService = new BinanceSPOTHTTPService();

            //累计收益
            decimal cumulativeProfit = 0M;

            //计时器
            Stopwatch sw = new Stopwatch();

            //开始
            while (true) 
            {
                //开始计时
                sw.Start();

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
                decimal quantity = Math.Ceiling(BinanceConstant.MinTradingVolume / querySymbolNewestPriceRModel.Price);

                //现货下单(买入)
                SpotTradeOrderPModel buySpotTradeOrderPModel = new SpotTradeOrderPModel();

                buySpotTradeOrderPModel.Symbol = symbol;
                buySpotTradeOrderPModel.Side = SideEuum.BUY;
                buySpotTradeOrderPModel.Price = querySymbolNewestPriceRModel.Price;
                buySpotTradeOrderPModel.Quantity = quantity;
                buySpotTradeOrderPModel.NewOrderRespType = NewOrderRespTypeEnum.RESULT;
                buySpotTradeOrderPModel.Type = OrderTypesEnum.LIMIT;
                buySpotTradeOrderPModel.TimeInForce = TimeInForceEnum.FOK;
                SpotTradeOrderRModel buySpotTradeOrderRModel = isDebug ? new SpotTradeOrderRModel() { Status = TradeOrderStatusEnum.FILLED,Price = querySymbolNewestPriceRModel.Price } : binanceSPOTHTTPService.SpotTradeOrder(buySpotTradeOrderPModel);

                //计算买入手续费
                var buyCommission = buySpotTradeOrderRModel.Status == TradeOrderStatusEnum.FILLED ? quantity * BinanceConstant.SPOTServiceCharge * buySpotTradeOrderRModel.Price : 0;
                ConsoleTool.WriteLine(String.Format("当前买入交易对:{0},当前交易对交易价格:{1},当前交易对交易数量{2},交易状态:{3},交易订单号:{4},交易手续费：{5}", symbol, buySpotTradeOrderRModel.Price, buySpotTradeOrderRModel.ExecutedQty, buySpotTradeOrderRModel.Status, buySpotTradeOrderRModel.OrderId, buyCommission), ConsoleColor.Yellow);

                //如果成交
                if (buySpotTradeOrderRModel.Status == TradeOrderStatusEnum.FILLED)
                {
                    //收益系数 并不是越大越好 要找到一个适合值(这个值 之后通过算法寻找)
                    decimal profitCoefficient = 0.015m;

                    //现货下单(卖出) 止盈单
                    SpotTradeOrderPModel sellSpotTradeOrderParam = new SpotTradeOrderPModel();

                    sellSpotTradeOrderParam.Symbol = symbol;
                    sellSpotTradeOrderParam.Side = SideEuum.SELL;
                    sellSpotTradeOrderParam.NewOrderRespType = NewOrderRespTypeEnum.FULL;
                    sellSpotTradeOrderParam.Type = OrderTypesEnum.LIMIT;
                    sellSpotTradeOrderParam.TimeInForce = TimeInForceEnum.GTC;
                    sellSpotTradeOrderParam.Quantity = quantity;
                    sellSpotTradeOrderParam.Price = buySpotTradeOrderRModel.Price + profitCoefficient;

                    //卖出
                    SpotTradeOrderRModel sellSpotTradeOrderRModel = isDebug ? new SpotTradeOrderRModel() { Status = TradeOrderStatusEnum.NEW,Price = sellSpotTradeOrderParam.Price.Value, CummulativeQuoteQty = (sellSpotTradeOrderParam.Quantity * sellSpotTradeOrderParam.Price).Value } : binanceSPOTHTTPService.SpotTradeOrder(sellSpotTradeOrderParam);
                    //计算卖出手续费
                    decimal sellCommission = sellSpotTradeOrderRModel.CummulativeQuoteQty * BinanceConstant.SPOTServiceCharge;
                    ConsoleTool.WriteLine(string.Format("当前委托卖出交易对:{0},当前交易对交易价格:{1},交易状态:{2},交易订单号:{3},交易手续费：{4}", sellSpotTradeOrderRModel.Symbol, sellSpotTradeOrderRModel.Price, sellSpotTradeOrderRModel.Status, sellSpotTradeOrderRModel.OrderId, sellCommission), ConsoleColor.Yellow);

                    //监控
                    while (true)
                    {
                        //获取最新价格
                        querySymbolNewestPriceRModel = binanceSPOTHTTPService.SpotQuerySymbolNewestPrice(querySymbolNewestPricePModel);
                        //差价
                        var priceDifference = querySymbolNewestPriceRModel.Price - buySpotTradeOrderPModel.Price ;
                        ConsoleTool.WriteLine(string.Format("当前交易对:{0},当前交易对买入价格{1},当前交易对最新价格:{2},差价:{3},获利:{4}", symbol, buySpotTradeOrderPModel.Price, querySymbolNewestPriceRModel.Price, priceDifference, querySymbolNewestPriceRModel.Price * buySpotTradeOrderPModel.Quantity - buySpotTradeOrderPModel.Price * buySpotTradeOrderPModel.Quantity - buyCommission - sellCommission), ConsoleColor.DarkYellow, priceDifference >= profitCoefficient ? false : true, 500);

                        //当获利时
                        if (priceDifference >= profitCoefficient)
                        {
                            //获取订单信息
                            SpotQueryOrderInfoPModel spotQueryOrderInfoPModel = new SpotQueryOrderInfoPModel();
                            spotQueryOrderInfoPModel.OrderId = sellSpotTradeOrderRModel.OrderId;
                            spotQueryOrderInfoPModel.Symbol = symbol;
                            spotQueryOrderInfoPModel.OrigClientOrderId = sellSpotTradeOrderRModel.ClientOrderId;

                            SpotQueryOrderInfoRModel spotQueryOrderInfoRModel = isDebug ? new SpotQueryOrderInfoRModel() { Status = TradeOrderStatusEnum.FILLED,Price = sellSpotTradeOrderParam.Price.Value } : binanceSPOTHTTPService.SpotQueryOrderInfo(spotQueryOrderInfoPModel);

                            ConsoleTool.WriteLine(string.Format("当前卖出交易对:{0},当前交易对交易价格:{1},交易状态:{2},交易订单号:{3},交易手续费：{4}", symbol, spotQueryOrderInfoRModel.Price, spotQueryOrderInfoRModel.Status, spotQueryOrderInfoRModel.OrderId, sellCommission), ConsoleColor.Yellow);

                            //打印结果
                            ConsoleTool.WriteLine($@"
                            当前交易对:{symbol},
                            当前交易对买入价格:{buySpotTradeOrderPModel.Price},
                            当前交易对买入数量:{buySpotTradeOrderPModel.Quantity},
                            当前交易对卖出价格:{sellSpotTradeOrderParam.Price},
                            当前交易对卖出数量:{sellSpotTradeOrderParam.Quantity},
                            交易手续费：{buyCommission + sellCommission},
                            获利:{sellSpotTradeOrderParam.Price * sellSpotTradeOrderParam.Quantity - buySpotTradeOrderPModel.Price * buySpotTradeOrderPModel.Quantity - buyCommission - sellCommission}".Trim(' ')
                            , ConsoleColor.Cyan);

                            //累加累计收益
                            cumulativeProfit += (sellSpotTradeOrderParam.Price * sellSpotTradeOrderParam.Quantity - buySpotTradeOrderPModel.Price * buySpotTradeOrderPModel.Quantity - buyCommission - sellCommission).Value;

                            //停止计时
                            sw.Stop();
                            ConsoleTool.WriteLine($"当前交易用时:{sw.Elapsed.TotalSeconds}秒",ConsoleColor.Red);

                            //跳出
                            break;
                        }
                    }
                }

                ConsoleTool.WriteLine($"累计收益:{cumulativeProfit}",ConsoleColor.DarkMagenta);
                ConsoleTool.WriteLine("OOOOOOHOHOHHOHOHOOH 完成交易");
            }
        }

        #endregion

        #region 币安合约
        /// <summary>
        /// 币安合约
        /// </summary>
        public static void FuturesBinance(string symbol, bool isDebug = true)
        {
            //赚了就卖策略(搬砖一段时间内可以赚取的最小利润)
            ConsoleTool.WriteLine($"ReapProfitJustSellCore Start:{symbol}", ConsoleColor.Magenta);

            //币安现货服务
            BinanceUFuturesHTTPService binanceUFuturesHTTPService = new BinanceUFuturesHTTPService();

            //杠杆
            int lever = 10;

            //基础币
            string baseAssert = "USDT";

            //获取账户余额
            var futuresQueryBalanceDic = binanceUFuturesHTTPService.QueryBalance();

            //累计收益
            decimal cumulativeProfit = 0M;

            //计时器
            Stopwatch sw = new Stopwatch();

            //开始
            while (true)
            {
                //开始计时
                sw.Start();

                //获取实时价格
                QuerySymbolNewestPricePModel querySymbolNewestPricePModel = new QuerySymbolNewestPricePModel();
                querySymbolNewestPricePModel.Symbol = symbol;
                QuerySymbolNewestPriceRModel querySymbolNewestPriceRModel = binanceUFuturesHTTPService.QuerySymbolNewestPrice(querySymbolNewestPricePModel);
                ConsoleTool.WriteLine(String.Format("当前交易对:{0},当前交易对最新价格:{1}", querySymbolNewestPriceRModel.Symbol, querySymbolNewestPriceRModel.Price), ConsoleColor.Yellow);

                //计算价格和数量
                decimal quantity = Math.Round(futuresQueryBalanceDic[baseAssert].AvailableBalance / querySymbolNewestPriceRModel.Price, 3) * lever;

                //现货下单(买入)
                UFuturesTradeOrderPModel buyUFuturesTradeOrderPModel = new UFuturesTradeOrderPModel();

                buyUFuturesTradeOrderPModel.Symbol = symbol;
                buyUFuturesTradeOrderPModel.Side = SideEuum.BUY;
                buyUFuturesTradeOrderPModel.Price = querySymbolNewestPriceRModel.Price;
                buyUFuturesTradeOrderPModel.Quantity = quantity;
                buyUFuturesTradeOrderPModel.NewOrderRespType = NewOrderRespTypeEnum.RESULT;
                buyUFuturesTradeOrderPModel.Type = OrderTypesEnum.LIMIT;
                buyUFuturesTradeOrderPModel.TimeInForce = TimeInForceEnum.FOK;

                UFuturesTradeOrderRModel buyUFuturesTradeOrderRModel = isDebug ? new UFuturesTradeOrderRModel() { Status = TradeOrderStatusEnum.FILLED, Price = querySymbolNewestPriceRModel.Price } : binanceUFuturesHTTPService.FuturesTradeOrder(buyUFuturesTradeOrderPModel);

                //计算买入手续费
                var buyCommission = buyUFuturesTradeOrderRModel.Status == TradeOrderStatusEnum.FILLED ? quantity * buyUFuturesTradeOrderRModel.Price * BinanceConstant.FutureserviceCharge * 2: 0;
                ConsoleTool.WriteLine(String.Format("当前买入交易对:{0},当前交易对交易价格:{1},当前交易对交易数量{2},交易状态:{3},交易订单号:{4},交易手续费：{5}", symbol, buyUFuturesTradeOrderRModel.Price, buyUFuturesTradeOrderRModel.ExecutedQty, buyUFuturesTradeOrderRModel.Status, buyUFuturesTradeOrderRModel.OrderId, buyCommission), ConsoleColor.Yellow);

                //如果成交
                if (buyUFuturesTradeOrderRModel.Status == TradeOrderStatusEnum.FILLED)
                {
                    //收益系数 并不是越大越好 要找到一个适合值(这个值 之后通过算法寻找)
                    decimal profitCoefficient = 5M;

                    //现货下单(卖出) 止盈单
                    UFuturesTradeOrderPModel sellUFuturesTradeOrderPModel = new UFuturesTradeOrderPModel();

                    sellUFuturesTradeOrderPModel.Symbol = symbol;
                    sellUFuturesTradeOrderPModel.Side = SideEuum.SELL;
                    sellUFuturesTradeOrderPModel.NewOrderRespType = NewOrderRespTypeEnum.FULL;
                    sellUFuturesTradeOrderPModel.Type = OrderTypesEnum.LIMIT;
                    sellUFuturesTradeOrderPModel.TimeInForce = TimeInForceEnum.GTC;
                    sellUFuturesTradeOrderPModel.Quantity = quantity;
                    sellUFuturesTradeOrderPModel.Price = buyUFuturesTradeOrderRModel.Price + profitCoefficient;

                    //卖出
                    UFuturesTradeOrderRModel sellUFuturesTradeOrderRModel = isDebug ? new UFuturesTradeOrderRModel() { Status = TradeOrderStatusEnum.NEW, Price = sellUFuturesTradeOrderPModel.Price.Value,  } : binanceUFuturesHTTPService.FuturesTradeOrder(sellUFuturesTradeOrderPModel);
                    //计算卖出手续费
                    decimal sellCommission = (sellUFuturesTradeOrderPModel.Quantity * sellUFuturesTradeOrderPModel.Price).Value * BinanceConstant.FutureserviceCharge * 4;
                    ConsoleTool.WriteLine(string.Format("当前委托卖出交易对:{0},当前交易对交易价格:{1},当前交易对交易数量{2},交易状态:{3},交易订单号:{4},交易手续费：{5}", sellUFuturesTradeOrderRModel.Symbol, sellUFuturesTradeOrderRModel.Price, sellUFuturesTradeOrderPModel.Quantity,sellUFuturesTradeOrderRModel.Status, sellUFuturesTradeOrderRModel.OrderId, sellCommission), ConsoleColor.Yellow);

                    //监控
                    while (true)
                    {
                        //获取最新价格
                        querySymbolNewestPriceRModel = binanceUFuturesHTTPService.QuerySymbolNewestPrice(querySymbolNewestPricePModel);
                        //差价
                        var priceDifference = querySymbolNewestPriceRModel.Price - sellUFuturesTradeOrderPModel.Price;
                        ConsoleTool.WriteLine(string.Format("当前交易对:{0},当前交易对卖出价格{1},当前交易对最新价格:{2},差价:{3},获利:{4}", symbol, sellUFuturesTradeOrderPModel.Price, querySymbolNewestPriceRModel.Price, priceDifference, (querySymbolNewestPriceRModel.Price * sellUFuturesTradeOrderPModel.Quantity - buyUFuturesTradeOrderPModel.Price * buyUFuturesTradeOrderPModel.Quantity) * lever - buyCommission - sellCommission), ConsoleColor.DarkYellow, priceDifference >= profitCoefficient ? false : true, 500);

                        //当获利时
                        if (priceDifference >= profitCoefficient)
                        {
                            //获取订单信息
                            UFuturesQueryFuturesOrderPModel uFuturesQueryFuturesOrderPModel = new UFuturesQueryFuturesOrderPModel();
                            uFuturesQueryFuturesOrderPModel.OrderId = sellUFuturesTradeOrderRModel.OrderId;
                            uFuturesQueryFuturesOrderPModel.Symbol = symbol;
                            uFuturesQueryFuturesOrderPModel.OrigClientOrderId = sellUFuturesTradeOrderRModel.ClientOrderId;

                            UFuturesQueryFuturesOrderRModel uFuturesQueryFuturesOrderRModel = isDebug ? new UFuturesQueryFuturesOrderRModel() { Status = TradeOrderStatusEnum.FILLED, Price = sellUFuturesTradeOrderPModel.Price.Value } : binanceUFuturesHTTPService.QueryFuturesOrder(uFuturesQueryFuturesOrderPModel);

                            ConsoleTool.WriteLine(string.Format("当前卖出交易对:{0},当前交易对交易价格:{1},交易状态:{2},交易订单号:{3},交易手续费：{4}", symbol, uFuturesQueryFuturesOrderRModel.Price, uFuturesQueryFuturesOrderRModel.Status, uFuturesQueryFuturesOrderRModel.OrderId, sellCommission), ConsoleColor.Yellow);

                            //打印结果
                            ConsoleTool.WriteLine($@"
                            当前交易对:{symbol},
                            当前交易对买入价格:{buyUFuturesTradeOrderPModel.Price},
                            当前交易对买入数量:{buyUFuturesTradeOrderPModel.Quantity},
                            当前交易对卖出价格:{sellUFuturesTradeOrderPModel.Price},
                            当前交易对卖出数量:{sellUFuturesTradeOrderPModel.Quantity},
                            交易手续费：{buyCommission + sellCommission},
                            获利:{sellUFuturesTradeOrderPModel.Price * sellUFuturesTradeOrderPModel.Quantity - buyUFuturesTradeOrderPModel.Price * buyUFuturesTradeOrderPModel.Quantity - buyCommission - sellCommission}".Trim(' ')
                            , ConsoleColor.Cyan);

                            //累加累计收益
                            cumulativeProfit += (sellUFuturesTradeOrderPModel.Price * sellUFuturesTradeOrderPModel.Quantity - buyUFuturesTradeOrderPModel.Price * buyUFuturesTradeOrderPModel.Quantity - buyCommission - sellCommission).Value;

                            //停止计时
                            sw.Stop();
                            ConsoleTool.WriteLine($"当前交易用时:{sw.Elapsed.TotalSeconds}秒", ConsoleColor.Red);

                            //跳出
                            break;
                        }
                    }
                }

                ConsoleTool.WriteLine($"累计收益:{cumulativeProfit}", ConsoleColor.DarkMagenta);
                ConsoleTool.WriteLine("OOOOOOHOHOHHOHOHOOH 完成交易");
            }
        }

        #endregion

    }
}
