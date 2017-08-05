using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Common
{
    public class ExtGridRecord
    {
        /// <summary>
        /// 返回的记录
        /// </summary>
        public object rows { get; set; }
        /// <summary>
        /// 总个数
        /// </summary>
        public int total { get; set; }

        public ExtGridRecord()
        {
                
        }

        public ExtGridRecord(object rows,int total)
        {
            this.rows = rows;
            this.total = total;
        }
    }
}
