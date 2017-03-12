/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Repositories
 * 文件名：IPager
 * 版本号：v1.0.0.0
 * 唯一标识：a37abfdf-afe3-46f5-83c5-0ab2f70523e0
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:17:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:17:36
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
    public interface IPager : IPagerBase
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        int GetPageCount();
        /// <summary>
        /// 获取跳过的行数
        /// </summary>
        /// <returns></returns>
        int GetSkipCount();

        /// <summary>
        /// 排序条件
        /// </summary>
        string Order { get; set; }

        /// <summary>
        /// 获取起始行数
        /// </summary>
        /// <returns></returns>
        int GetStartNumber();

        /// <summary>
        /// 获取结束行数
        /// </summary>
        /// <returns></returns>
        int GetEndNumber();

        /// <summary>
        /// 搜索关键字
        /// </summary>
        string Keyword { get; set; }
    }
}
