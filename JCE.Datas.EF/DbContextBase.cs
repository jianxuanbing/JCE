/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：DbContextBase
 * 版本号：v1.0.0.0
 * 唯一标识：c04b5bac-f1e5-4fb5-acb5-99464d5469db
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:40:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:40:12
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Configs;
using JCE.Core.Domains.Uow;
using JCE.Utils.Logging;
using TransactionalBehavior = JCE.Core.Domains.TransactionalBehavior;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    public abstract class DbContextBase<TDbContext>:DbContext,IUnitOfWork where TDbContext:DbContext,IUnitOfWork,new()
    {
        /// <summary>
        /// 日志组件
        /// </summary>
        protected readonly ILogger Logger = LogManager.GetLogger(typeof(TDbContext));

        protected virtual bool DataLoggingEnabled
        {
            get
            {
                DbContextConfig contextConfig=null;
                if (contextConfig == null || !contextConfig.Enabled)
                {
                    return false;
                }
                return contextConfig.DataLoggingEnabled;
            }
        }

        public bool TransactionEnabled { get; set; }
        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
