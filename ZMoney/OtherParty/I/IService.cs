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
        string APIKey { get; }

        /// <summary>
        /// APISecret
        /// </summary>
        string APISecret { get; }

    }
}
