/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Repositories
 * 文件名：PagerList
 * 版本号：v1.0.0.0
 * 唯一标识：42806c59-743e-4d21-82a1-34f531c67a98
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:18:46
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:18:46
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

namespace JCE.Core.Domains.Repositories
{
    /// <summary>
    /// 分页集合
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class PagerList<T> : List<T>, IPagerBase
    {
        /// <summary>
        /// 页索引，即第几页，从开始
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Order { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="PagerList{T}"/>类型的实例
        /// </summary>
        /// <param name="pager">查询对象</param>
        public PagerList(IPager pager) : this(pager.Page, pager.PageSize, pager.TotalCount, pager.Order)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="PagerList{T}"/>类型的实例
        /// </summary>
        /// <param name="totalCount">总行数</param>
        public PagerList(int totalCount) : this(1, 20, totalCount)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="PagerList{T}"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数</param>
        /// <param name="totalCount">总行数</param>
        public PagerList(int page, int pageSize, int totalCount) : this(page, pageSize, totalCount, "")
        {
        }

        /// <summary>
        /// 初始化一个<see cref="PagerList{T}"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数</param>
        /// <param name="totalCount">总行数</param>
        /// <param name="order">排序条件</param>
        public PagerList(int page, int pageSize, int totalCount, string order)
        {
            var pager = new Pager(page, pageSize, totalCount);
            TotalCount = pager.TotalCount;
            PageCount = pager.GetPageCount();
            Page = pager.Page;
            PageSize = pager.PageSize;
            Order = order;
        }

        /// <summary>
        /// 转换分页集合的元素类型
        /// </summary>
        /// <typeparam name="TResult">目标元素类型</typeparam>
        /// <param name="converter">转换方法</param>
        /// <returns></returns>
        public PagerList<TResult> Convert<TResult>(Func<T, TResult> converter)
        {
            var result = new PagerList<TResult>(Page, PageSize, TotalCount, Order);
            result.AddRange(this.Select(converter));
            return result;
        }
    }
}
