/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：DataConfig
 * 版本号：v1.0.0.0
 * 唯一标识：dc533336-adec-41db-8d23-f10205d03403
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:52:54
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:52:54
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
    /// 数据配置信息
    /// </summary>
    public class DataConfig
    {
        /// <summary>
        /// 初始化一个<see cref="DataConfig"/>类型的新实例
        /// </summary>
        public DataConfig()
        {
            ContextConfigs = new List<DbContextConfig>();
        }

        /// <summary>
        /// 初始化一个<see cref="DataConfig"/>类型的新实例
        /// </summary>
        internal DataConfig(DataElement element)
        {
            ContextConfigs = element.Contexts.OfType<ContextElement>()
                .Select(context => new DbContextConfig(context)).ToList();
        }

        /// <summary>
        /// 获取或设置 上下文配置信息集合
        /// </summary>
        public ICollection<DbContextConfig> ContextConfigs { get; set; }
    }
}
