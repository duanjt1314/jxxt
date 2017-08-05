using System.Windows;
using System;

namespace Life.Encrypt
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but1_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtKey.Text))
                this.txtContent.Text = Life.Common.Encrypt.EncryptString(this.txtContent.Text);
            else
            {
                if (this.txtKey.Text.Length != 8)
                {
                    MessageBox.Show("键只能是8位数字或字母", "系统提示");
                    return;
                }
                this.txtContent.Text = Life.Common.Encrypt.EncryptString(this.txtContent.Text, this.txtKey.Text);
            }
            Clipboard.SetText(this.txtContent.Text);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.txtKey.Text))
                    this.txtContent.Text = Life.Common.Encrypt.DecryptString(this.txtContent.Text);
                else
                {
                    if (this.txtKey.Text.Length != 8)
                    {
                        MessageBox.Show("键只能是8位数字或字母", "系统提示");
                        return;
                    }
                    this.txtContent.Text = Life.Common.Encrypt.DecryptString(this.txtContent.Text, this.txtKey.Text);
                }
                Clipboard.SetText(this.txtContent.Text);
            }
            catch (System.Exception)
            {
                MessageBox.Show("解密出错.", "提示");
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but3_Click(object sender, RoutedEventArgs e)
        {
            this.txtContent.Text = Life.Common.MD5Encry.Encry(this.txtContent.Text);
            Clipboard.SetText(this.txtContent.Text);
        }

        /// <summary>
        /// 转为大些
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but4_Click(object sender, RoutedEventArgs e)
        {
            this.txtContent.Text = this.txtContent.Text.ToUpper();
            Clipboard.SetText(this.txtContent.Text);
        }

        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but5_Click(object sender, RoutedEventArgs e)
        {
            this.txtContent.Text = this.txtContent.Text.ToLower();
            Clipboard.SetText(this.txtContent.Text);
        }

        private void but6_Click(object sender, RoutedEventArgs e)
        {
            this.txtContent.Text = Guid.NewGuid().ToString();
            Clipboard.SetText(this.txtContent.Text);
        }

        private void btnCSSFormat_Click(object sender, RoutedEventArgs e)
        {
            String Str = String.Empty;
            String chars = "\t\n\r";
            foreach (var item in this.txtContent.Text)
            {
                if (!chars.Contains(item.ToString())&&item.ToString()!=" ")
                {
                    Str += item.ToString();
                }
                if (item.ToString() == "}" || item.ToString() == "/")
                {
                    Str += "\n";
                }
            }
            this.txtContent.Text = Str;
        }

    }
}
