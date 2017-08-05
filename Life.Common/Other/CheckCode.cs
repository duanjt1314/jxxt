using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;

namespace Life.Common
{
    /// <summary>
    /// 验证码类
    /// </summary>
    public class Rand
    {
        #region 生成随机数字
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        public static String Number(Int32 Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static String Number(Int32 Length, Boolean Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            String result = "";
            System.Random random = new Random();
            for (Int32 i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        #endregion

        #region 生成随机字母与数字
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static String Str(Int32 Length)
        {
            return Str(Length, false);
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static String Str(Int32 Length, Boolean Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            String result = "";
            Int32 n = Pattern.Length;
            System.Random random = new Random(~unchecked((Int32)DateTime.Now.Ticks));
            for (Int32 i = 0; i < Length; i++)
            {
                Int32 rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion

        #region 生成随机纯字母随机数
        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static String Str_char(Int32 Length)
        {
            return Str_char(Length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static String Str_char(Int32 Length, Boolean Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            String result = "";
            Int32 n = Pattern.Length;
            System.Random random = new Random(~unchecked((Int32)DateTime.Now.Ticks));
            for (Int32 i = 0; i < Length; i++)
            {
                Int32 rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// 验证图片类
    /// </summary>
    public class YZMHelper
    {
        #region 私有字段
        private String text;
        private Bitmap image;
        private Int32 letterCount = 4;   //验证码位数
        private Int32 letterWidth = 16;  //单个字体的宽度范围
        private Int32 letterHeight = 20; //单个字体的高度范围
        private static Byte[] randb = new Byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        private Font[] fonts = 
        {
           new Font(new FontFamily("Times New Roman"),10 +Next(1),System.Drawing.FontStyle.Regular),
           new Font(new FontFamily("Georgia"), 10 + Next(1),System.Drawing.FontStyle.Regular),
           new Font(new FontFamily("Arial"), 10 + Next(1),System.Drawing.FontStyle.Regular),
           new Font(new FontFamily("Comic Sans MS"), 10 + Next(1),System.Drawing.FontStyle.Regular)
        };
        #endregion

        #region 公有属性
        /// <summary>
        /// 验证码
        /// </summary>
        public String Text
        {
            get { return this.text; }
        }

        /// <summary>
        /// 验证码图片
        /// </summary>
        public Bitmap Image
        {
            get { return this.image; }
        }
        #endregion

        #region 构造函数
        public YZMHelper()
        {
            //HttpContext.Current.Response.Expires = 0;
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            //HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            //HttpContext.Current.Response.CacheControl = "no-cache";
            this.text = Rand.Number(4);
            CreateImage();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        private static Int32 Next(Int32 max)
        {
            rand.GetBytes(randb);
            Int32 value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        private static Int32 Next(Int32 min, Int32 max)
        {
            Int32 value = Next(max - min) + min;
            return value;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 绘制验证码
        /// </summary>
        public void CreateImage()
        {
            Int32 int_ImageWidth = this.text.Length * letterWidth;
            Bitmap image = new Bitmap(int_ImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            for (Int32 i = 0; i < 2; i++)
            {
                Int32 x1 = Next(image.Width - 1);
                Int32 x2 = Next(image.Width - 1);
                Int32 y1 = Next(image.Height - 1);
                Int32 y2 = Next(image.Height - 1);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            Int32 _x = -12, _y = 0;
            for (Int32 int_index = 0; int_index < this.text.Length; int_index++)
            {
                _x += Next(12, 16);
                _y = Next(-2, 2);
                String str_char = this.text.Substring(int_index, 1);
                str_char = Next(1) == 1 ? str_char.ToLower() : str_char.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(_x, _y);
                g.DrawString(str_char, fonts[Next(fonts.Length - 1)], newBrush, thePos);
            }
            for (Int32 i = 0; i < 10; i++)
            {
                Int32 x = Next(image.Width - 1);
                Int32 y = Next(image.Height - 1);
                image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            image = TwistImage(image, true, Next(1, 3), Next(4, 6));
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, int_ImageWidth - 1, (letterHeight - 1));
            this.image = image;
        }

        /// <summary>
        /// 字体随机颜色
        /// </summary>
        public Color GetRandomColor()
        {
            Random RandomNum_First = new Random((Int32)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((Int32)DateTime.Now.Ticks);
            Int32 int_Red = RandomNum_First.Next(180);
            Int32 int_Green = RandomNum_Sencond.Next(180);
            Int32 int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, Boolean bXDir, Double dMultValue, Double dPhase)
        {
            Double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            Double dBaseAxisLen = bXDir ? (Double)destBmp.Height : (Double)destBmp.Width;
            for (Int32 i = 0; i < destBmp.Width; i++)
            {
                for (Int32 j = 0; j < destBmp.Height; j++)
                {
                    Double dx = 0;
                    dx = bXDir ? (PI * (Double)j) / dBaseAxisLen : (PI * (Double)i) / dBaseAxisLen;
                    dx += dPhase;
                    Double dy = Math.Sin(dx);
                    Int32 nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (Int32)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (Int32)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }
        #endregion
    }
}
