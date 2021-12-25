using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBase.Tools;
using static ZBase.ZEnum.WebEnum;

namespace ZOtherParty.Base.BinanceBase
{
    /// <summary>
    /// 币安HTTP基础类
    /// </summary>
    public abstract class BinanceHTTPServiceBase
    {
        /// <summary>
        /// APIKey
        /// </summary>
        public virtual string APIKey { get => AppSettingHelper.GetConfig("Binance:APIKey"); }

        /// <summary>
        /// APISecret
        /// </summary>
        public virtual string APISecret { get => AppSettingHelper.GetConfig("Binance:APISecret"); }

        /// <summary>
        /// API地址
        /// </summary>
        public virtual string APIAddress => string.Empty;

        #region 处理WEB请求
        /// <summary>
        /// 处理WEB请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestMethodTypeEnum">请求方式枚举</param>
        /// <param name="isNeedSignature">是否需要验签</param>
        public string HandleWebRequest<PT>(string url, PT paramModel, RequestMethodTypeEnum requestMethodTypeEnum, bool isNeedSignature = true)
        {
            //参数字典
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            if (paramModel != null)
            {
                paramDic = ReflexionHelper.ClassFieldJsonPropertyToDictionary(paramModel);
            }

            if (isNeedSignature)
            {
                //当前时间戳
                paramDic["timestamp"] = DateTimeTool.GetTimeStamp(DateTime.Now).ToString().PadRight(13, '0');
                //时间限制
                paramDic["recvWindow"] = "5000";
                //签名
                paramDic["signature"] = EncryptiontTool.HMACSHA256Encrypt(WebTool.AssembleXWFUParam(paramDic), APISecret);
            }

            //请求头
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Content-Type"] = "application/x-www-form-urlencoded";
            headerDic["X-MBX-APIKEY"] = APIKey;
            //返回数据
            string resposeStr = string.Empty;
            //请求方式
            switch (requestMethodTypeEnum)
            {
                case RequestMethodTypeEnum.POST:
                    {
                        resposeStr = WebTool.Post(url, paramDic, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.GET:
                    {
                        resposeStr = WebTool.Get(url, paramDic, headerDic);
                    }
                    break;
                case RequestMethodTypeEnum.PUT:
                    {

                    }
                    break;
                case RequestMethodTypeEnum.DELETE:
                    {
                        resposeStr = WebTool.DELETE(url, paramDic, headerDic);
                    }
                    break;
            }

            return resposeStr;
        }

        #endregion

    }
}
