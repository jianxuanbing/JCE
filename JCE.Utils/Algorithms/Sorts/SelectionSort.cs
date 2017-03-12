/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Algorithms.Sorts
 * 文件名：SelectionSort
 * 版本号：v1.0.0.0
 * 唯一标识：ba1eb0be-5f63-4851-a164-e10dbd59992a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 17:32:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 17:32:38
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
    /// 选择排序算法
    /// </summary>
    public class SelectionSort:ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序数组</param>
        /// <returns></returns>
        public T[] Sort<T>(T[] input)
        {             
            for (int i = 0; i < input.Length-1; i++)
            {
                var min = i;
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (((IComparable) input[j]).CompareTo(input[min]) < 0)
                    {
                        min = j;
                    }                    
                }
                T temp = input[min];
                input[min] = input[i];
                input[i] = temp;
            }
            return input;
        }
    }
}
