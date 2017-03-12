/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：JceConfig
 * 版本号：v1.0.0.0
 * 唯一标识：fd26db0d-ee9a-4bb4-8bd7-c56931b5a8db
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 16:17:34
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 16:17:34
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Configs.ConfigFile;

namespace JCE.Core.Configs
{
    /// <summary>
    /// Jce配置类
    /// </summary>
    public sealed class JceConfig
    {
        private const string JceSectionName = "jce";
        private static readonly Lazy<JceConfig> InstanceLazy=new Lazy<JceConfig>(()=>new JceConfig());

        /// <summary>
        /// 获取配置类的单一实例
        /// </summary>
        public static JceConfig Instance
        {
            get
            {
                JceConfig config = InstanceLazy.Value;
                if (DataConfigReseter != null)
                {
                    config.DataConfig = DataConfigReseter.Reset(config.DataConfig);
                }
                if (LoggingConfigReseter != null)
                {
                    config.LoggingConfig = LoggingConfigReseter.Reset(config.LoggingConfig);
                }
                return config;
            }
        }
        /// <summary>
        /// 获取或设置 日志配置重置信息
        /// </summary>
        public static ILoggingConfigReseter LoggingConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 数据配置重置信息
        /// </summary>
        public static IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 日志配置信息
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DataConfig DataConfig { get; set; }

        /// <summary>
        /// 初始化一个<see cref="JceConfig"/>类型的实例
        /// </summary>
        private JceConfig()
        {
            JceFrameworkSection section = (JceFrameworkSection) ConfigurationManager.GetSection(JceSectionName);
            if (section == null)
            {
                DataConfig=new DataConfig();
                LoggingConfig=new LoggingConfig();
                return;
            }
            DataConfig=new DataConfig(section.Data);
            LoggingConfig=new LoggingConfig(section.Logging);
        }
    }
}
