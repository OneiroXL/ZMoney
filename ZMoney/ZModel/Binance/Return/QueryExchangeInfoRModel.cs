using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModel.Binance.Return
{
    /// <summary>
    /// 交易规范信息返回信息
    /// </summary>
    public class QueryExchangeInfoRModel
    {
        /// <summary>
        /// 时区
        /// </summary>
        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { set; get; }

        /// <summary>
        /// 服务器时间
        /// </summary>
        [JsonProperty(PropertyName = "serverTime")]
        public long ServerTime { set; get; }

        /// <summary>
        /// 交易对信息
        /// </summary>
        [JsonProperty(PropertyName = "symbols")]
        public List<SymbolModel> SymbolModelList { set; get; }
    }

    /// <summary>
    /// 交易对信息
    /// </summary>
    public class SymbolModel 
    {
        /// <summary>
        /// 交易对
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { set; get; }

        /// <summary>
        /// 基础资产
        /// </summary>
        [JsonProperty(PropertyName = "baseAsset")]
        public string BaseAsset { set; get; }

        /// <summary>
        /// 基础资产精度
        /// </summary>
        [JsonProperty(PropertyName = "baseAssetPrecision")]
        public int BaseAssetPrecision { set; get; }

        /// <summary>
        /// 精度资产
        /// </summary>
        [JsonProperty(PropertyName = "quoteAsset")]
        public string QuoteAsset { set; get; }

        /// <summary>
        /// 精度资产
        /// </summary>
        [JsonProperty(PropertyName = "quotePrecision")]
        public int quotePrecision { set; get; }

        /// <summary>
        /// 精度资产精度
        /// </summary>
        [JsonProperty(PropertyName = "quoteAssetPrecision")]
        public int QuoteAssetPrecision { set; get; }

        /// <summary>
        /// 订单类型列表
        /// </summary>
        [JsonProperty(PropertyName = "orderTypes")]
        public List<string> OrderTypeList { set; get; }

        /// <summary>
        /// 是否可以冰山
        /// </summary>
        [JsonProperty(PropertyName = "icebergAllowed")]
        public bool IcebergAllowed { set; get; }

        /// <summary>
        /// OCO 允许
        /// </summary>
        [JsonProperty(PropertyName = "ocoAllowed")]
        public bool OCOAllowed { set; get; }

        /// <summary>
        /// 是否可以现货交易
        /// </summary>
        [JsonProperty(PropertyName = "isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { set; get; }

        /// <summary>
        /// isMarginTradingAllowed
        /// </summary>
        [JsonProperty(PropertyName = "isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { set; get; }

        /// <summary>
        /// 权限
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public List<string> PermissionList { set; get;}
    }
}
