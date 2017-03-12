/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Algorithms.Sorts
 * 文件名：BubbleSort
 * 版本号：v1.0.0.0
 * 唯一标识：1b9a7844-2702-40b8-8efa-be2f94d40e56
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 17:24:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 17:24:58
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

namespace JCE.Utils.Algorithms.Sorts
{
    /// <summary>
    /// 冒泡排序算法
    /// </summary>
    public class BubbleSort:ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序的数组</param>
        /// <returns></returns>
        public T[] Sort<T>(T[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var key = input[i];
                var j = i - 1;
                while (j >= 0 && ((IComparable) input[j]).CompareTo(key) > 0)
                {
                    input[j + 1] = input[j];
                    j = j - 1;
                }
                input[j + 1] = key;
            }
            return input;
        }
    }
}
