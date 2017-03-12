/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Repositories
 * 文件名：Pager
 * 版本号：v1.0.0.0
 * 唯一标识：79339f81-95f8-4eca-836f-e9d36bf3caa7
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:18:28
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:18:28
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
    /// 分页
    /// </summary>
    public class Pager : StateDescription, IPager
    {
        private int _pageIndex;

        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int Page
        {
            get
            {
                if (_pageIndex <= 0)
                {
                    _pageIndex = 1;
                }
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 初始化一个<see cref="Pager"/>类型的实例
        /// </summary>
        public Pager() : this(1)
        {

        }

        /// <summary>
        /// 初始化一个<see cref="Pager"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数，默认20</param>
        /// <param name="order">排序条件</param>
        public Pager(int page, int pageSize, string order) : this(page, pageSize, 0, order)
        {

        }
        /// <summary>
        /// 初始化一个<see cref="Pager"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数，默认20</param>
        /// <param name="totalCount">总行数</param>
        /// <param name="order">排序条件</param>
        public Pager(int page, int pageSize = 20, int totalCount = 0, string order = "")
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Order = order;
        }
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        public int GetPageCount()
        {
            if (TotalCount == 0)
            {
                return 0;
            }
            if ((TotalCount % PageSize) == 0)
            {
                return TotalCount / PageSize;
            }
            return (TotalCount / PageSize) + 1;
        }

        /// <summary>
        /// 获取跳过的行数
        /// </summary>
        /// <returns></returns>
        public int GetSkipCount()
        {
            if (Page > GetPageCount())
            {
                Page = GetPageCount();
            }
            return PageSize * (Page - 1);
        }

        /// <summary>
        /// 获取起始行数
        /// </summary>
        /// <returns></returns>
        public int GetStartNumber()
        {
            return (Page - 1) * PageSize + 1;
        }

        /// <summary>
        /// 获取结束行数
        /// </summary>
        /// <returns></returns>
        public int GetEndNumber()
        {
            return Page * PageSize;
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Page", Page);
            AddDescription("PageSize", PageSize);
            AddDescription("Order", Order);
        }
    }
}
