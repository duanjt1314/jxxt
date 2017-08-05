using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Common
{
    public class StringManager
    {
        /// <summary>
        /// 验证字符串中是否存在中文
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static bool isChina(string msg)
        {
            string temp;
            for (int i = 0; i < msg.Length; i++)
            {
                temp = msg.Substring(i, 1);
                byte[] ser = Encoding.GetEncoding("gb2312").GetBytes(temp);
                if (ser.Length == 2)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 截取指定长度的字符串，中文算两个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string CutString(string str, int count)
        {
            string temp,result="";
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                temp = str.Substring(i, 1);
                byte[] ser = Encoding.GetEncoding("gb2312").GetBytes(temp);
                if (ser.Length == 2)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }

                if (num <= count)
                {
                    result += temp;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}
