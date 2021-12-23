using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.ZEnum
{
    public class BinanceEnum
    {
        /// <summary>
        /// 交易状态枚举
        /// </summary>
        public enum TradeStatusEnum 
        {
            /// <summary>
            /// 交易前
            /// </summary>
            PRE_TRADING,

            /// <summary>
            /// 交易中
            /// </summary>
            TRADING,

            /// <summary>
            /// 交易后
            /// </summary>
            POST_TRADING,

            /// <summary>
            /// END_OF_DAY
            /// </summary>
            END_OF_DAY,

            /// <summary>
            /// HALT
            /// </summary>
            HALT,

            /// <summary>
            /// AUCTION_MATCH
            /// </summary>
            AUCTION_MATCH,

            /// <summary>
            /// BREAK
            /// </summary>
            BREAK
        }

        /// <summary>
        /// 现货 订单状态(状态 status)
        /// </summary>
        public enum SPOTTradestatusEnum 
        {
            /// <summary>
            /// 订单被交易引擎接受
            /// </summary>
            NEW,
            /// <summary>
            /// 部分订单被成交
            /// </summary>
            PARTIALLY_FILLED,
            /// <summary>
            /// 订单完全成交
            /// </summary>
            FILLED,
            /// <summary>
            /// 用户撤销了订单
            /// </summary>
            CANCELED,
            /// <summary>
            /// 撤销中(目前并未使用)
            /// </summary>
            PENDING_CANCEL,
            /// <summary>
            /// 订单没有被交易引擎接受，也没被处理
            /// </summary>
            REJECTED,
            /// <summary>
            /// 订单被交易引擎取消, 比如LIMIT FOK 订单没有成交市价单没有完全成交 强平期间被取消的订单交易所维护期间被取消的订单
            /// </summary>
            EXPIRED,
        }

        /// <summary>
        /// OCO 状态 (状态类型集 listStatusType):
        /// </summary>
        public enum SPOTOCOListStatusTypeEnum 
        {
            /// <summary>
            /// 当ListStatus响应失败的操作时使用。 (订单完成或取消订单)
            /// </summary>
            RESPONSE,

            /// <summary>
            /// 当已经下单或者订单有更新时
            /// </summary>
            EXEC_STARTED,
            /// <summary>
            /// 当订单执行结束或者不在激活状态
            /// </summary>
            ALL_DONE
        }

        /// <summary>
        /// OCO 订单状态 (订单状态集 listOrderStatus)
        /// </summary>
        public enum SPOTOCOListOrderStatus 
        {
            /// <summary>
            /// 当已经下单或者订单有更新时
            /// </summary>
            EXECUTING,
            /// <summary>
            /// 当订单执行结束或者不在激活状态
            /// </summary>
            ALL_DONE,
            /// <summary>
            /// 当订单状态响应失败(订单完成或取消订单)
            /// </summary>
            REJECT
        }

        /// <summary>
        /// OCO 选择性委托订单 订单类型(orderTypes, type)
        /// </summary>
        public enum SPOTOCOOrderTypesEnum 
        {
            /// <summary>
            /// 限价单
            /// </summary>
            LIMIT,
            /// <summary>
            /// 市价单
            /// </summary>
            MARKET,
            /// <summary>
            /// 止损单
            /// </summary>
            STOP_LOSS,
            /// <summary>
            /// 限价止损单
            /// </summary>
            STOP_LOSS_LIMIT,
            /// <summary>
            /// 止盈单
            /// </summary>
            TAKE_PROFIT,
            /// <summary>
            /// 限价止盈单
            /// </summary>
            TAKE_PROFIT_LIMIT,
            /// <summary>
            /// 限价只挂单
            /// </summary>
            LIMIT_MAKER
        }

        /// <summary>
        ///OCO 订单返回类型 (newOrderRespType)
        /// </summary>
        public enum SPOTOCONewOrderRespTypeEnum 
        {
            /// <summary>
            /// ACK
            /// </summary>
            ACK,
            /// <summary>
            /// RESULT
            /// </summary>
            RESULT,
            /// <summary>
            /// FULL
            /// </summary>
            FULL
        }

        /// <summary>
        /// 现货 订单方向 (方向 side)
        /// </summary>
        public enum SPOTSideEuum 
        {
            /// <summary>
            /// 买入
            /// </summary>
            BUY,
            /// <summary>
            /// 卖出
            /// </summary>
            SELL
        }

        /// <summary>
        /// 现货 有效方式 (timeInForce):
        /// </summary>
        public enum SPOTTimeInForceEnum
        {
            /// <summary>
            /// 成交为止 订单会一直有效，直到被成交或者取消。
            /// </summary>
            GTC,
            /// <summary>
            /// 无法立即成交的部分就撤销 订单在失效前会尽量多的成交
            /// </summary>
            IOC,
            /// <summary>
            ///  无法全部立即成交就撤销 如果无法全部成交，订单会失效。
            /// </summary>
            FOK
        }

        /// <summary>
        /// 限制种类 (rateLimitType)
        /// </summary>
        public enum SPOTRateLimitTypeEnum
        {
            /// <summary>
            /// 单位时间请求权重之和上限
            /// </summary>
            REQUEST_WEIGHT,

            /// <summary>
            /// 单位时间下单次数限制
            /// </summary>
            ORDERS,

            /// <summary>
            /// 单位时间请求次数上限
            /// </summary>
            RAW_REQUESTS
        }

        /// <summary>
        /// 限制间隔 (interval)
        /// </summary>
        public enum SPOTintervalEnum 
        {
            /// <summary>
            /// 秒
            /// </summary>
            SECOND,

            /// <summary>
            /// MINUTE
            /// </summary>
            MINUTE,

            /// <summary>
            /// 天
            /// </summary>
            DAY
        }
    }
}
