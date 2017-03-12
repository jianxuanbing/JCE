/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：OrderByAttribute
 * 版本号：v1.0.0.0
 * 唯一标识：29b95346-bd35-4151-b22d-46d6105c753b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:16:20
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:16:20
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

namespace JCE.Utils.Common
{
    /// <summary>
    /// 排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OrderByAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="OrderByAttribute"/>类型的实例
        /// </summary>
        /// <param name="sortId">排序号</param>
        public OrderByAttribute(int sortId)
        {
            SortId = sortId;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
    }
}
