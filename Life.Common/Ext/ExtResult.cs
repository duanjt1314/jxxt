using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Common
{
    /// <summary>
    /// 提交后台返回对象
    /// </summary>
    public class ExtResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public Boolean success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public String msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public Object data { get; set; }
    }
}
