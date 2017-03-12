/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：ICompareChange
 * 版本号：v1.0.0.0
 * 唯一标识：8b6cb8d4-e639-4fb1-ba2b-18342280077e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:14:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:14:12
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

namespace JCE.Core.Domains
{
    /// <summary>
    /// 通过对象比较获取变更属性集
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    public interface ICompareChange<in T> where T : IDomainObject
    {
        /// <summary>
        /// 获取变更属性
        /// </summary>
        /// <param name="newEntity">新对象</param>
        /// <returns></returns>
        ChangeValueCollection GetChanges(T newEntity);
    }
}
