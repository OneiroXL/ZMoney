using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Tools
{
    /// <summary>
    /// WEB工具类
    /// </summary>
    public class WebTool
    {
        #region url为请求的网址，param参数为需要查询的条件（服务端接收的参数，没有则为null）
        /// <summary>
        /// url为请求的网址，param参数为需要查询的条件（服务端接收的参数，没有则为null）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> param = null, Dictionary<string, string> headers = null)
        {
            if (param != null) //有参数的情况下，拼接url
            {
                url = url + "?";
                url = param.Aggregate(url, (current, item) => current + item.Key + "=" + item.Value + "&");
                url = url.Substring(0, url.Length - 1);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;//创建请求
            request.Method = "GET"; //请求方法为GET
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers[item.Key] = item.Value;
                }
            }
            HttpWebResponse res; //定义返回的response
            try
            {
                res = (HttpWebResponse)request.GetResponse(); //此处发送了请求并获得响应
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //响应转化为String字符串
            return content;
        }
        #endregion

        #region url为请求的网址，param为需要传递的参数 (POST)
        /// <summary>
        ///  url为请求的网址，param为需要传递的参数 (POST)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<String, String> param, Dictionary<string, string> headers = null)
        {
            string parameterSt = AssembleXWFUParam(param);


            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/x-www-form-urlencoded";
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers[item.Key] = item.Value;
                }
            }
            byte[] parameterStByte = Encoding.UTF8.GetBytes(parameterSt);
            Stream postStream = request.GetRequestStream();
            postStream.Write(parameterStByte, 0, parameterStByte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串
            return content;
        }

        #endregion

        #region url为请求的网址，param为需要传递的参数 (DELETE)
        /// <summary>
        ///  url为请求的网址，param为需要传递的参数 (DELETE)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string DELETE(string url, Dictionary<String, String> param, Dictionary<string, string> headers = null)
        {
            string parameterSt = AssembleXWFUParam(param);


            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "DELETE"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/x-www-form-urlencoded";
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers[item.Key] = item.Value;
                }
            }
            byte[] parameterStByte = Encoding.UTF8.GetBytes(parameterSt);
            Stream postStream = request.GetRequestStream();
            postStream.Write(parameterStByte, 0, parameterStByte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串
            return content;
        }

        #endregion

        #region url为请求的网址，param为需要传递的参数
        /// <summary>
        /// url为请求的网址，param为需要传递的参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string PostJosn(string url, string param, Dictionary<string, string> headers = null)
        {
            #region 写日记
            string parameterSt = param;
            #endregion

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/json";
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers[item.Key] = item.Value;
                }
            }
            string jsonstring = param;//获得参数的json字符串
            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //获得响应字符串
            return content;
        }
        #endregion

        #region url为请求的网址，param为需要传递的参数 返回文件类型数据

        /// <summary>
        /// url为请求的网址，param为需要传递的参数 返回文件类型数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="paramStr"></param>
        /// <returns></returns>
        public static MemoryStream PostFile(string url, Dictionary<String, object> param, string paramStr = null)
        {

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; //创建请求
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            //request.AllowReadStreamBuffering = true;
            request.MaximumResponseHeadersLength = 1024;
            request.Method = "POST"; //请求方式为post
            request.AllowAutoRedirect = true;
            request.MaximumResponseHeadersLength = 1024;
            request.ContentType = "application/json";

            string jsonstring = string.Empty;
            if (string.IsNullOrEmpty(paramStr))
            {
                if (param != null)
                {
                    jsonstring = JsonConvert.SerializeObject(param);
                }
            }
            else
            {
                jsonstring = paramStr;
            }

            byte[] jsonbyte = Encoding.UTF8.GetBytes(jsonstring);
            Stream postStream = request.GetRequestStream();
            postStream.Write(jsonbyte, 0, jsonbyte.Length);
            postStream.Close();
            //发送请求并获取相应回应数据       
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            Stream resultStream = res.GetResponseStream();
            MemoryStream ms = new MemoryStream();
            byte[] buffer = new byte[1024];
            while (true)
            {
                int sz = resultStream.Read(buffer, 0, 1024);
                if (sz == 0) break;
                ms.Write(buffer, 0, sz);
            }
            //string content = Encoding.UTF8.GetString(ms.ToArray()); 
            return ms;
        }
        #endregion



        #region 获取IP地址
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress(HttpContext context)
        {
            string result = String.Empty;

            string IP = context.Connection.RemoteIpAddress.ToString();
            if (!string.IsNullOrEmpty(IP))
            {
                string[] ips = IP.Split(':');
                result = ips[ips.Length - 1];
            }
            return result;
        }
        #endregion

        #region 组装参数
        /// <summary>
        /// 组装参数
        /// </summary>
        /// <param name="paramDic">参数字典</param>
        /// <returns></returns>
        public static string AssembleXWFUParam(Dictionary<string, string> paramDic) 
        {
            string param = string.Empty;
            param = paramDic.Aggregate(param, (current, item) => current + item.Key + "=" + item.Value + "&");
            return param.Substring(0, param.Length - 1);
        }

        #endregion
    }
}
