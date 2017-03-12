/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：LoggingAdapterConfig
 * 版本号：v1.0.0.0
 * 唯一标识：92eaf0de-33f6-4b0b-b982-b4df440b3ee4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:01:27
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:01:27
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

namespace JCE.Core.Configs
{
    /// <summary>
    /// 日志适配器配置信息
    /// </summary>
    public class LoggingAdapterConfig
    {
        #region Property(属性)
        /// <summary>
        /// 获取或设置 适配器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否启用适配器
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置 适配器类型
        /// </summary>
        public Type AdapterType { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="LoggingAdapterConfig"/>类型的实例
        /// </summary>
        public LoggingAdapterConfig()
        {
            Name = Guid.NewGuid().ToString();
            Enabled = true;
        }

        /// <summary>
        /// 初始化一个<see cref="LoggingAdapterConfig"/>类型的实例
        /// </summary>
        /// <param name="element">适配器配置节点</param>
        internal LoggingAdapterConfig(LoggingAdapterElement element)
        {
            Name = element.Name;
            Enabled = element.Enabled;
            AdapterType = Type.GetType(element.AdapterTypeName);
        }
        #endregion

    }
}
