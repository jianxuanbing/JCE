/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：DataElement
 * 版本号：v1.0.0.0
 * 唯一标识：ba5b733e-ce51-43b5-8eef-b20a75f73bc1
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:49:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:49:35
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

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据配置节点
    /// </summary>
    internal class DataElement : ConfigurationElement
    {
        private const string ContextKey = "contexts";

        /// <summary>
        /// 数据上下文配置节点集合
        /// </summary>
        [ConfigurationProperty(ContextKey)]
        public virtual ContextCollection Contexts
        {
            get { return (ContextCollection)base[ContextKey]; }
        }
    }
}
