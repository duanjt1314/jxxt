using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Life.Common.Email
{
    /// <summary>
    /// 邮件操作公共类
    /// </summary>
    public class SOCNetSendMail
    {
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        /// <param name="outboxUser">发件人</param>
        /// <param name="recipientUsres">收件人集合</param>
        /// <param name="contxt">发送内容</param>
        /// <param name="title">标题</param>
        /// <param name="isBodyHtml">是否为Html</param>
        /// <param name="emailFiles">附件</param>
        /// <returns>是否发送成功</returns>
        public static Boolean Sender(EmailType emailType, EmailUser outboxUser, List<EmailUser> recipientUsres, String contxt, String title = "", Boolean isBodyHtml = true, params String[] emailFiles)
        {
            Boolean isMessage = false;

            //得到SmtpClient对象
            SmtpClient smtpClient = GetSender(emailType, outboxUser);            

            //得到MailMessage对象
            MailMessage mailMessage = GetMailMessage(outboxUser, recipientUsres, contxt, title, isBodyHtml, emailFiles);
            
            try
            {
                smtpClient.Send(mailMessage);
                return isMessage = true;
            }
            catch (Exception e)
            {
                isMessage = false;
                throw e;
            }

        }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        /// <param name="outboxUser">发件人</param>
        /// <param name="recipientUsres">收件人集合</param>
        /// <param name="contxt">发送内容</param>
        /// <param name="title">标题</param>
        /// <param name="isBodyHtml">是否为Html</param>
        /// <param name="emailFiles">附件</param>
        /// <returns>是否发送成功</returns>
        public static Boolean Sender(EmailType emailType, EmailUser outboxUser, String recipientUsres, String contxt, String title = "", Boolean isBodyHtml = true, params String[] emailFiles)
        {
            Boolean isMessage = false;

            //得到SmtpClient对象
            SmtpClient smtpClient = GetSender(emailType, outboxUser);

            //得到MailMessage对象
            MailMessage mailMessage = GetMailMessage(outboxUser, new List<EmailUser>() { new EmailUser() { UserName = recipientUsres } }, contxt, title, isBodyHtml, emailFiles);

            try
            {
                smtpClient.Send(mailMessage);
                return isMessage = true;
            }
            catch (Exception e)
            {
                isMessage = false;
                throw e;
            }

        }

        /// <summary>
        /// 异部发送邮件 异部发送完成以后需要调用SendCompleted事件返回发送已经完成
        /// 
        /// 注:使用异步发送邮件,需在.aspx页面头部命令行中设置Async="true",否则报错
        /// smtpClient.SendCompleted += new SendCompletedEventHandler(sc_SendCompleted);//邮件异步发送完成后响应事件
        /// 
        /// void smtpClient_SendCompleted(Object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        /// {   //异步发送完成时响应的事件
        ///     Page.Response.Write("<script>alert('邮件已发送完成')</script>"); 
        /// }
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        /// <param name="outboxUser">发件人</param>
        /// <param name="recipientUsres">收件人集合</param>
        /// <param name="contxt">发送内容</param>
        /// <param name="title">标题</param>
        /// <param name="isBodyHtml">是否为Html</param>
        /// <param name="emailFiles">附件</param>
        /// <returns>SmtpClient 对象</returns>
        public static SmtpClient AsyncSender(EmailType emailType, EmailUser outboxUser, List<EmailUser> recipientUsres, String contxt, String title = "", Boolean isBodyHtml = true, params String[] emailFiles)
        {
            //得到SmtpClient对象
            SmtpClient smtpClient = GetSender(emailType, outboxUser);

            //得到MailMessage对象
            MailMessage mailMessage = GetMailMessage(outboxUser, recipientUsres, contxt, title, isBodyHtml, emailFiles);

            try
            {
                smtpClient.SendAsync(mailMessage, "");
                return smtpClient;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        /// <summary>
        /// 异部发送邮件 异部发送完成以后需要调用SendCompleted事件返回发送已经完成
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        /// <param name="outboxUser">发件人</param>
        /// <param name="recipientUsres">收件人集合</param>
        /// <param name="contxt">发送内容</param>
        /// <param name="title">标题</param>
        /// <param name="isBodyHtml">是否为Html</param>
        /// <param name="emailFiles">附件</param>
        /// <returns>SmtpClient 对象</returns>
        public static SmtpClient AsyncSender(EmailType emailType, EmailUser outboxUser, String recipientUsres, String contxt, String title = "", Boolean isBodyHtml = true, params String[] emailFiles)
        {
            //得到SmtpClient对象
            SmtpClient smtpClient = GetSender(emailType,  outboxUser);

            //得到MailMessage对象
            MailMessage mailMessage = GetMailMessage(outboxUser,new List<EmailUser>(){ new EmailUser() { UserName = recipientUsres }}, contxt, title, isBodyHtml, emailFiles);

            try
            {
                smtpClient.SendAsync(mailMessage, "");
                return smtpClient;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 实例化MailMessage对象
        /// </summary>
        /// <param name="outboxUser">发件人</param>
        /// <param name="recipientUsres">收件人集合</param>
        /// <param name="contxt">发送内容</param>
        /// <param name="title">标题</param>
        /// <param name="isBodyHtml">是否为Html</param>
        /// <param name="emailFiles">附件</param>
        /// <returns>实例化后MailMessage对象</returns>
        private static MailMessage GetMailMessage(EmailUser outboxUser, List<EmailUser> recipientUsres, String contxt, String title = "", Boolean isBodyHtml = true, params String[] emailFiles)
        {
            if (recipientUsres.Count <= 0)
                return null;

            MailMessage mailMessage = new MailMessage();

            //发件人地址
            mailMessage.From = new MailAddress(outboxUser.UserName,"生活费管理系统");

            //标题
            mailMessage.Subject = title;

            //内容
            mailMessage.Body = contxt;

            //是否是Html
            mailMessage.IsBodyHtml = isBodyHtml;

            //收件人
            for (Int32 i = 0; i < recipientUsres.Count; i++)
                mailMessage.To.Add(new MailAddress(recipientUsres[i].UserName));

            //邮件附件
            foreach (String str in emailFiles)
                mailMessage.Attachments.Add(new Attachment(str));//Attachment参数:包含用于创建此附件的文件路径

            mailMessage.Priority = MailPriority.High;//设置此电子邮件的优先级

            return mailMessage;
        }

        /// <summary>
        /// 实例化一个SmtpClient对象
        /// </summary>
        /// <param name="emailType">邮箱类型</param>
        /// <param name="emailUser">邮箱帐户</param>
        /// <returns>实例化后SmtpClient对象</returns>
        private static SmtpClient GetSender(EmailType emailType, EmailUser outboxUser)
        {
            SmtpClient sender = null;

            switch (emailType)
            {
                //QQ邮箱
                case EmailType.QQ:
                    {
                        sender = CreateSender("smtp.qq.com", 25, false, outboxUser);
                    }
                    break;
                //163邮箱
                case EmailType.Mail163:
                    {
                        sender = CreateSender("smtp.163.com", 25, true, outboxUser);
                    }
                    break;
                //Gmail邮箱
                case EmailType.Gmail:
                    {
                        sender = CreateSender("smtp.gmail.com", 25, true, outboxUser);
                    }
                    break;
                //Hotmail邮箱
                case EmailType.Hotmail:
                    {
                        sender = CreateSender("smtp.live.com", 25, true, outboxUser);
                    }
                    break;
            }

            return sender;
        }

        /// <summary>
        /// 实例化一个SmtpClient对象
        /// </summary>
        /// <param name="host">主机名称或IP地址</param>
        /// <param name="port">SMTP端口</param>
        /// <param name="enableSsl">是否套用加密连接（必写）</param>
        /// <param name="emailUser">邮箱帐户</param>
        /// <returns>实例化后SmtpClient对象</returns>
        private static SmtpClient CreateSender(String host, Int32 port, Boolean enableSsl, EmailUser outboxUser)
        {
            return new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(outboxUser.UserName, outboxUser.UserPwd)
            };
        }
    }    
}
