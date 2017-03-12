/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：DataLoggingConfig
 * 版本号：v1.0.0.0
 * 唯一标识：0bc88c19-6c74-41bf-88aa-237741e7c77a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 16:01:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 16:01:17
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Configs.ConfigFile;
using JCE.Utils.Logging;

namespace JCE.Core.Configs
{
    /// <summary>
    /// 数据日志配置信息
    /// </summary>
    public class DataLoggingConfig
    {
        private const string DefaultAdapterTypeName =
            "JCE.Core.Data.Entity.Logging.DatabaseLoggerAdapter,JCE.Core.Data.Entity";

        /// <summary>
        /// 获取或设置 是否启用数据日志
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置 日志输出级别
        /// </summary>
        public LogLevel OutLevel { get; set; }

        /// <summary>
        /// 获取或设置 适配器类型
        /// </summary>
        public Type AdapterType { get; set; }

        /// <summary>
        /// 初始化一个<see cref="DataLoggingConfig"/>类型的实例
        /// </summary>
        public DataLoggingConfig()
        {
            Enabled = true;
            OutLevel=LogLevel.All;
            AdapterType = Type.GetType(DefaultAdapterTypeName);
        }

        /// <summary>
        /// 初始化一个<see cref="DataLoggingConfig"/>类型的实例
        /// </summary>
        /// <param name="element">数据日志配置节点</param>
        internal DataLoggingConfig(DataLoggingElement element)
        {
            Enabled = element.Enabled;
            OutLevel = element.OutLogLevel;
            AdapterType = Type.GetType(element.AdapterTypeName);
        }
    }
}
