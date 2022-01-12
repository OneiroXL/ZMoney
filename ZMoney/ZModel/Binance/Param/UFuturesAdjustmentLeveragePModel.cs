﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Param
{
    /// <summary>
    /// 合约调整杠杆接口
    /// </summary>
    public class UFuturesAdjustmentLeveragePModel
    {
        /// <summary>
        /// 交易对
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { set; get; }
        /// <summary>
        /// 目标杠杆倍数：1 到 125 整数
        /// </summary>
        [JsonProperty(PropertyName = "leverage")]
        public int Leverage { set; get; }
    }
}
