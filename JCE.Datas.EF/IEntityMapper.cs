/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：IEntityMapper
 * 版本号：v1.0.0.0
 * 唯一标识：9154f723-b14b-4330-80f9-8ca8e9556f86
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:23:45
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:23:45
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 实体映射接口
    /// </summary>
    public interface IEntityMapper
    {
        /// <summary>
        /// 获取 相关上下文类型，如果为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        Type DbContextType { get; }

        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        void RegistTo(ConfigurationRegistrar configurations);
    }
}
