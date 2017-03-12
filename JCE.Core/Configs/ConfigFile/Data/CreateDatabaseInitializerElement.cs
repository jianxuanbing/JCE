/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：CreateDatabaseInitializerElement
 * 版本号：v1.0.0.0
 * 唯一标识：91413b64-76da-4eb0-b2be-adfe172f5141
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:49:15
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:49:15
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
    /// 数据库创建策略配置节点
    /// </summary>
    internal class CreateDatabaseInitializerElement : ConfigurationElement
    {
        private const string TypeKey = "type";

        /// <summary>
        /// 获取或设置 数据库创建策略类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string InitializerTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }
    }
}
