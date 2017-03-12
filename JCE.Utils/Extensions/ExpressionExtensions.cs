/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ExpressionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：524c98ed-537a-41fa-9116-0cda76206724
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:47:31
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:47:31
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 表达式（Expression）扩展
    /// </summary>
    public static class ExpressionExtensions
    {
        #region Compose(以特定的条件运行组合两个Expression表达式)
        /// <summary>
        /// 以特定的条件运行组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <param name="merge">组合条件运算方式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            first.CheckNotNull("first");
            second.CheckNotNull("second");
            merge.CheckNotNull("merge");
            Dictionary<ParameterExpression, ParameterExpression> map =
                first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        #endregion

        #region And(与表达式)
        /// <summary>
        /// 以 Expression.AndAlso 组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            first.CheckNotNull("first");
            second.CheckNotNull("second");
            return first.Compose(second, Expression.AndAlso);
        }
        #endregion

        #region Or(或表达式)
        /// <summary>
        /// 以 Expression.OrElse 组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            first.CheckNotNull("first");
            second.CheckNotNull("second");
            return first.Compose(second, Expression.OrElse);
        }
        #endregion

        #region ParameterRebinder(内部类，参数重绑)
        /// <summary>
        /// 参数重绑
        /// </summary>
        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="map">字典</param>
            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            /// <summary>
            /// 替换参数
            /// </summary>
            /// <param name="map">字典</param>
            /// <param name="exp">表达式树</param>
            /// <returns></returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            /// <summary>
            /// 访问参数
            /// </summary>
            /// <param name="node">表达式</param>
            /// <returns>表达式树</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(node, out replacement))
                {
                    node = replacement;
                }
                return base.VisitParameter(node);
            }
        }
        #endregion
    }
}
