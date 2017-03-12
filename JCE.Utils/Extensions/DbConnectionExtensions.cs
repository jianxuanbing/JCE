/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DbConnectionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：df6016c4-6515-4fad-9f4c-49b1caaf9a1a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:43:50
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:43:50
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 数据库连接（DbConnection）扩展
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// 判断当前数据库连接是否在指定状态范围内，在则返回true
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="states">数据库连接状态</param>
        /// <returns>bool</returns>
        public static bool StateIsWithin(this IDbConnection connection, params ConnectionState[] states)
        {
            return (connection != null && (states != null && states.Length > 0) &&
                    (states.Where(x => (connection.State & x) == x).Count() > 0));
        }
        /// <summary>
        /// 判断当前数据库连接是否处于指定状态，处于则返回true
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="state">数据库连接状态</param>
        /// <returns>bool</returns>
        public static bool IsInState(this IDbConnection connection, ConnectionState state)
        {
            return (connection != null && (connection.State & state) == state);
        }
        /// <summary>
        /// 打开数据库连接，如果尚未打开
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public static void OpenIfNot(this IDbConnection connection)
        {
            if (!connection.IsInState(ConnectionState.Open))
            {
                connection.Open();
            }
        }
    }
}
