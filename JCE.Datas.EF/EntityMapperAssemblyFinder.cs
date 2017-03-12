/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：EntityMapperAssemblyFinder
 * 版本号：v1.0.0.0
 * 唯一标识：40995d9c-b01e-4c4a-b3df-3a157bd86513
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:33:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:33:08
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Reflection;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 实体映射程序集查找器
    /// </summary>
    public class EntityMapperAssemblyFinder: IEntityMapperAssemblyFinder
    {
        /// <summary>
        /// 获取或设置 所有程序集查找器
        /// </summary>
        public IAllAssemblyFinder AllAssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            Type baseType = typeof(IEntityMapper);
            Assembly[] assemblies =
                AllAssemblyFinder.Find(
                    assembly => assembly.GetTypes().Any(type => baseType.IsAssignableFrom(type) && !type.IsAbstract));
            return assemblies;
        }
    }
}
