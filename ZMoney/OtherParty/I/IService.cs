using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOtherParty.I
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// APIKey
        /// </summary>
        static string APIKey { get; } = string.Empty;

        /// <summary>
        /// APISecret
        /// </summary>
        static string APISecret { get; } = string.Empty;

        /// <summary>
        /// API地址
        /// </summary>
        static string APIAddress { get; } = string.Empty;

    }
}
