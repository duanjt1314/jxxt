using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Life.Common
{
    public class MD5Encry
    {
        public static string Encry(string str)
        {
            //实例化MD5对象
            MD5 md5 = new MD5CryptoServiceProvider();
            //根据字符串获得字节集合
            byte[] data = Encoding.Default.GetBytes(str);
            //利用md5的实例获得md5字节的集合
            byte[] md5Data = md5.ComputeHash(data);
            //释放资源
            md5.Clear();
            StringBuilder builder = new StringBuilder();
            //循环遍历字节集合
            for (int i = 0; i < md5Data.Length - 1; i++)
            {
                //toString()方法后可以传参，传入X2表示转换为数字和英文
                //如果传入d1就表示一位数，d2表示两位数
                builder.Append(md5Data[i].ToString("X2"));
            }
            //返回字符串
            return builder.ToString();

        }
    }
}
