using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Life.Common
{
    public class LogHelper
    {
        private static ILog log = null;
        public static ILog Log
        {
            get
            {
                if (log == null)
                {
                    //log4.config表示log4的配置文件
                    String configFileName = ConfigurationManager.AppSettings["LogConfigPath"];
                    if (string.IsNullOrEmpty(configFileName))
                    {
                        configFileName = "config/log4.config";
                    }
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(configFileName));
                    //Log4Name表示配置文件中的日志名称
                    log = LogManager.GetLogger("LifeManager");
                }
                return log;
            }
        }
    }
}
