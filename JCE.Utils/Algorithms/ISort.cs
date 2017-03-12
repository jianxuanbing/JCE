/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Algorithms
 * 文件名：ISort
 * 版本号：v1.0.0.0
 * 唯一标识：8486a0e6-da4f-4d9f-b2e2-94d1ea0600fc
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 14:19:20
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 14:19:20
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

namespace JCE.Utils.Algorithms
{
    /// <summary>
    /// 排序
    /// </summary>
    public interface ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序数组</param>
        /// <returns></returns>
        T[] Sort<T>(T[] input);
    }
}
