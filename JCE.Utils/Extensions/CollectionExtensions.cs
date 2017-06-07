/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：CollectionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：a66ebb4a-7a57-4b9e-9221-7bd1ae7300e8
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:31:57
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:31:57
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 集合（IEnumerable、IQueryable、ICollection、IList）扩展
    /// </summary>
    public static class CollectionExtensions
    {
        #region IEnumerable扩展
        #region ExpandAndToString(集合展开并转为字符串)
        /// <summary>
        /// 将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">要处理的结合</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
        {            
            return collection.ExpandAndToString(t => t.ToString(), separator);
        }
        /// <summary>
        /// 将集合展开并转为字符串，循环集合每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">要处理的集合</param>
        /// <param name="itemFormatFunc">单个集合项的转换委托</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, Func<T, string> itemFormatFunc,
            string separator = ",")
        {
            collection = collection as IList<T> ?? collection.ToList();
            itemFormatFunc.CheckNotNull("itemFormatFunc");
            if (!collection.Any())
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            int count = collection.Count();
            foreach (T t in collection)
            {
                if (i == count - 1)
                {
                    sb.Append(itemFormatFunc(t));
                }
                else
                {
                    sb.Append(itemFormatFunc(t) + separator);
                }
                i++;
            }
            return sb.ToString();
        }
        #endregion
        #region IsEmpty(集合是否为空)
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 为空返回True，不为空返回False </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            collection = collection as IList<T> ?? collection.ToList();
            return !collection.Any();
        }
        #endregion
        #region WhereIf(是否执行条件查询)
        /// <summary>
        /// 是否执行指定条件的查询，根据第三方条件是否为真来决定
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            predicate.CheckNotNull("predicate");
            source = source as IList<T> ?? source.ToList();

            return condition ? source.Where(predicate) : source;
        }
        #endregion
        #region DistinctBy(根据指定条件返回集合中不重复的元素)

        /// <summary>
        /// 根据指定条件返回集合中不重复的元素
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <typeparam name="TKey">动态筛选条件类型</typeparam>
        /// <param name="source">要操作的源</param>
        /// <param name="keySelector">重复数据筛选条件</param>
        /// <returns>不重复元素的集合</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            keySelector.CheckNotNull("keySelector");
            source = source as IList<T> ?? source.ToList();
            return source.GroupBy(keySelector).Select(group => group.First());
        }
        #endregion        
        #region ForEach(对指定集合中的每个元素执行指定操作)
        /// <summary>
        /// 对指定集合中的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="values">集合</param>
        /// <param name="action">操作</param>
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }
        #endregion
        #region Join(将数组可以转换成指定格式的字符串)
        /// <summary>
        /// 扩展Linq的Join方法，使其传递的集合或者数组调用Join方法可以转换成按照规定格式转换的字符串
        /// </summary>
        /// <param name="source">需要转换字符串格式的集合信息</param>
        /// <param name="separator">以某种格式分隔集合的信息，不传递默认为,</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// string[] strJoin={"kencery","liuxiaoji"};
        /// strJoin.Join("需要分隔的格式，不传递默认按照，分隔")
        /// </code>
        /// </example>
        public static string Join(this IEnumerable<string> source, string separator = ",")
        {
            if (source == null)
            {
                throw new Exception("source is null,but source is not null");
            }
            return source.Aggregate((x, y) => x + separator + y);
        }
        #endregion
        #endregion

        #region IQueryable扩展
        #region WhereIf(是否执行条件查询)
        /// <summary>
        /// 是否执行指定条件的查询，根据第三方条件是否为真来决定
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");

            return condition ? source.Where(predicate) : source;
        }
        #endregion
        #region Between(筛选指定键范围的自数据集)
        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合中筛选指定键范围内的子数据集
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">筛选键类型</typeparam>
        /// <param name="source">要筛选的数据源</param>
        /// <param name="keySelector">筛选键的范围表达式</param>
        /// <param name="start">筛选范围起始值</param>
        /// <param name="end">筛选范围结束值</param>
        /// <param name="startEqual">是否等于起始值</param>
        /// <param name="endEqual">是否等于结束集</param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            TKey start,
            TKey end,
            bool startEqual = false,
            bool endEqual = false) where TKey : IComparable<TKey>
        {                        
            Expression[] paramters = keySelector.Parameters.Cast<Expression>().ToArray();
            Expression key = Expression.Invoke(keySelector, paramters);
            Expression startBound = startEqual
                ? Expression.GreaterThanOrEqual(key, Expression.Constant(start))
                : Expression.GreaterThan(key, Expression.Constant(start));
            Expression endBound = endEqual
                ? Expression.LessThanOrEqual(key, Expression.Constant(end))
                : Expression.LessThan(key, Expression.Constant(end));
            Expression and = Expression.AndAlso(startBound, endBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }
        #endregion
        #region OrderBy(根据属性和顺序进行排序)
        /// <summary>
        /// 扩展Linq的OrderBy方法，实现根据属性和顺序(倒序)进行排序，调用和Linq的方法一致
        /// </summary>
        /// <typeparam name="TEntity">需要排序的实体对象</typeparam>
        /// <param name="source">结果集信息</param>
        /// <param name="propertyStr">动态排序的属性名(从前台获取)</param>
        /// <param name="isDesc">排序方式，不传递表示顺序，默认true，false表示倒序</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyStr,
            bool isDesc = true) where TEntity : class
        {
            //以下四句用来建立c>c.propertyStr的Expression对象，实现Lambda表达式的状态
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "c");
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty(propertyStr);
            Expression expression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
            LambdaExpression lambdaExpression = Expression.Lambda(expression, parameterExpression);
            Type type = typeof(TEntity);

            //读取排序的顺序信息，如果传递的参数(isDesc)是true，则为顺序排序，否则为倒序排序
            string ascDesc = isDesc ? "OrderBy" : "OrderByDescending";

            //Expression.Call跟上面的信息一样，这里采用重载的形式，上面的GetCurrentMethod结果也是ascDesc
            //Expression.Call方法会利用typeof(Queryable),ascDesc,new Type[]{type,property,PropertyType}三个参数
            //合成跟MethodInfo等同的消息
            MethodCallExpression methodCallExpression = Expression.Call(typeof(Queryable), ascDesc,
                new Type[] { type, propertyInfo.PropertyType }, source.Expression, Expression.Quote(lambdaExpression));

            //返回成功
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(methodCallExpression);
        }
        #endregion
        #endregion

        #region ICollection扩展
        #region AddUnique(添加唯一值)
        /// <summary>
        /// 添加唯一值，向泛型集合添加值（唯一值）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            var alreadyHas = collection.Contains(value);
            if (!alreadyHas)
            {
                collection.Add(value);
            }
            return alreadyHas;
        }
        #endregion
        #region AddRangeUnique(批量添加唯一值)
        /// <summary>
        /// 批量添加唯一值，向泛型集合批量添加值（唯一值）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="values">值（批量）</param>
        /// <returns>返回添加成功数</returns>
        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (collection.AddUnique(value))
                    count++;
            }
            return count;
        }
        #endregion
        #region RemoveWhere(移除集合中的指定项)
        /// <summary>
        /// 移除集合中的指定项，根据查询条件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">查询集合</param>
        public static void RemoveWhere<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            var deleteList = collection.Where(child => predicate(child)).ToList();
            deleteList.ForEach(t => collection.Remove(t));
        }
        #endregion
        #region IsEmpty(集合是否为空)
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this ICollection collection)
        {
            collection.ExceptionIfNullOrEmpty("The collection cannot be null.", "collection");
            return collection.Count == 0;
        }
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>bool</returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            collection.ExceptionIfNullOrEmpty("The collection cannot be null.", "collection");
            return collection.Count == 0;
        }
        #endregion
        #endregion

        #region IList扩展
        #region IsEmpty(集合是否为空)
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this IList collection)
        {
            collection.ExceptionIfNullOrEmpty("The collection cannot be null.", "collection");
            return collection.Count == 0;
        }
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>bool</returns>
        public static bool IsEmpty<T>(this IList<T> collection)
        {
            collection.ExceptionIfNullOrEmpty("The collection cannot be null.", "collection");
            return collection.Count == 0;
        }
        #endregion
        #region InsertUnique(插入唯一值)
        /// <summary>
        /// 将唯一值插入到指定索引，向泛型列表添加值（唯一值）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index">索引</param>
        /// <param name="item">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (list.Contains(item) == false)
            {
                list.Insert(index, item);
                return true;
            }
            return false;
        }
        #endregion
        #region InsertRangeUnique(批量插入唯一值)
        /// <summary>
        /// 将值批量插入指定索引，向泛型列表批量插入值（唯一值）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="startIndex">索引</param>
        /// <param name="items">值（批量）</param>
        /// <returns>返回插入成功数</returns>
        public static int InsertRangeUnique<T>(this IList<T> list, int startIndex, IEnumerable<T> items)
        {
            var index = startIndex + items.Reverse().Count(item => list.InsertUnique(startIndex, item));
            return (index - startIndex);
        }
        #endregion
        #region IndexOf(获取第一个匹配项的索引)
        /// <summary>
        /// 获取第一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="comparison">条件</param>
        /// <returns>索引值</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                    return i;
            }
            return -1;
        }
        #endregion
        #region Join(将泛型列表合并为字符串)
        /// <summary>
        /// 将泛型列表合并为字符串，根据指定的字符进行分隔
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="joinChar">分隔符</param>
        /// <returns>字符串</returns>
        public static string Join<T>(this IList<T> list, char joinChar)
        {
            return list.Join(joinChar.ToString());
        }
        /// <summary>
        /// 将泛型列表合并为字符串，根据指定的字符串进行分隔
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="joinString">分隔符</param>
        /// <returns>字符串</returns>
        public static string Join<T>(this IList<T> list, string joinString)
        {
            if (list == null || !list.Any())
                return String.Empty;

            StringBuilder result = new StringBuilder();

            int listCount = list.Count;
            int listCountMinusOne = listCount - 1;

            if (listCount > 1)
            {
                for (var i = 0; i < listCount; i++)
                {
                    if (i != listCountMinusOne)
                    {
                        result.Append(list[i]);
                        result.Append(joinString);
                    }
                    else
                        result.Append(list[i]);
                }
            }
            else
                result.Append(list[0]);

            return result.ToString();
        }
        #endregion
        #region Match(获取匹配的指定项列表)
        /// <summary>
        /// 获取匹配的指定项列表，根据查询条件以及参数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据源</param>
        /// <param name="searchString">查询字符串</param>
        /// <param name="top">前几项</param>
        /// <param name="args">查询条件</param>
        /// <returns>List</returns>
        public static List<T> Match<T>(this IList<T> list, string searchString, int top, params Expression<Func<T, object>>[] args)
        {
            // Create a new list of results and matches;
            var results = new List<T>();
            var matches = new Dictionary<T, int>();
            var maxMatch = 0;
            // For each item in the source
            list.ForEach(s =>
            {
                // Generate the expression string from the argument.
                var regExp = string.Empty;
                if (args != null)
                {
                    // For each argument
                    Array.ForEach(args,
                        a =>
                        {
                            // Compile the expression
                            var property = a.Compile();
                            // Attach the new property to the expression string
                            regExp += (string.IsNullOrEmpty(regExp) ? "(?:" : "|(?:") + property(s) + ")+?";
                        });
                }
                // Get the matches
                var match = Regex.Matches(searchString, regExp, RegexOptions.IgnoreCase);
                // If there are more than one match
                if (match.Count > 0)
                {
                    // Add it to the match dictionary, including the match count.
                    matches.Add(s, match.Count);
                }
                // Get the highest max matching
                maxMatch = match.Count > maxMatch ? match.Count : maxMatch;
            });
            // Convert the match dictionary into a list
            var matchList = matches.ToList();

            // Sort the list by decending match counts
            // matchList.Sort((s1, s2) => s2.Value.CompareTo(s1.Value));

            // Remove all matches that is less than the best match.
            matchList.RemoveAll(s => s.Value < maxMatch);

            // If the top value is set and is less than the number of matches
            var getTop = top > 0 && top < matchList.Count ? top : matchList.Count;

            // Add the maches into the result list.
            for (var i = 0; i < getTop; i++)
                results.Add(matchList[i].Key);

            return results;
        }
        #endregion
        #region ToList(将IList转为指定类型List)
        /// <summary>
        /// 将IList转为指定类型List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(this IList source)
        {
            var list = new List<T>();            
            list.AddRange(source.OfType<T>());
            return list;
        }
        #endregion
        #region GetRandomItem(获取随机项)
        /// <summary>
        /// 获取随机项，根据随机数生成器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="random">随机数生成器</param>
        /// <returns>随机项</returns>
        public static T GetRandomItem<T>(this IList<T> source, Random random)
        {
            if (source.Count > 0)
            {
                return source[random.Next(0, source.Count)];
            }
            throw new InvalidOperationException("不能从空列表项目获取项");
        }
        /// <summary>
        /// 获取随机项，根据种子
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="seed">种子</param>
        /// <returns>随机项</returns>
        public static T GetRandomItem<T>(this IList<T> source, int seed)
        {
            var random = new Random(seed);
            return source.GetRandomItem(random);
        }
        /// <summary>
        /// 获取随机项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns>随机项</returns>
        public static T GetRandomItem<T>(this IList<T> source)
        {
            var random = new Random(DateTime.Now.Millisecond);
            return source.GetRandomItem(random);
        }
        #endregion
        #region Merge(合并列表)
        /// <summary>
        /// 合并列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="lists">列表</param>
        /// <returns>合并后的列表</returns>
        public static List<T> Merge<T>(params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list);
            return merged;
        }
        /// <summary>
        /// 合并列表，根据Lambda表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="match">Lambda表达式</param>
        /// <param name="lists">列表</param>
        /// <returns>合并后的列表</returns>
        public static List<T> Merge<T>(Expression<Func<T, object>> match, params List<T>[] lists)
        {
            var merged = new List<T>();
            foreach (var list in lists) merged.Merge(list, match);
            return merged;
        }
        /// <summary>
        /// 合并列表，根据Lambda表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <param name="match">Lambda表达式</param>
        /// <returns>合并后的列表</returns>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2, Expression<Func<T, object>> match)
        {
            if (list1 != null && list2 != null && match != null)
            {
                var matchFunc = match.Compile();
                foreach (var item in list2)
                {
                    var key = matchFunc(item);
                    if (!list1.Exists(i => matchFunc(i).Equals(key))) list1.Add(item);
                }
            }

            return list1;
        }
        /// <summary>
        /// 合并列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>合并后的列表 </returns>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2)
        {
            if (list1 != null && list2 != null) foreach (var item in list2.Where(item => !list1.Contains(item))) list1.Add(item);
            return list1;
        }
        #endregion

        #endregion

        #region List扩展
        #region ToDataTable(将List转换成数据表)
        /// <summary>
        /// 将List转换成数据表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">List集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> entities) where T : class
        {
            DataTable dt = new DataTable();
            var properties = typeof(T).GetProperties().ToList();
            properties.ForEach(item =>
            {
                Type colType = item.PropertyType;
                if ((colType.IsGenericType) && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                dt.Columns.Add(new DataColumn(item.Name) { DataType = colType });
            });
            entities.ToList().ForEach(item =>
            {
                var dr = dt.NewRow();
                properties.ForEach(property =>
                {
                    var value = property.GetValue(item, null);
                    dr[property.Name] = value ?? DBNull.Value;
                });
                dt.Rows.Add(dr);
            });
            return dt;
        }
        #endregion

        #endregion

        #region DataTable扩展
        #region ToArray(转换为数组对象)
        /// <summary>
        /// 将DataTable转换成T[]数组对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static T[] ToArray<T>(this DataTable table) where T : class, new()
        {
            List<T> list = ToList<T>(table) as List<T>;
            if (list != null)
            {
                return list.ToArray();
            }
            return null;
        }

        #endregion
        #region ToList(转换为List集合)
        /// <summary>
        /// 将DataTable转换为IList集合（反射实现）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable table) where T : class, new()
        {
            IList<T> list = new List<T>();
            if ((table != null) && (table.Rows.Count != 0))
            {
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (DataRow row in table.Rows)
                {
                    T local = Activator.CreateInstance<T>();
                    foreach (DataColumn column in table.Columns)
                    {
                        object obj = null;
                        if (row.RowState == DataRowState.Deleted)
                        {
                            obj = row[column, DataRowVersion.Original];
                        }
                        else
                        {
                            obj = row[column];
                        }

                        if (obj != DBNull.Value)
                        {
                            foreach (PropertyInfo info in properties)
                            {
                                if (column.ColumnName.Equals(info.Name, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    info.SetValue(local, obj, null);
                                }
                            }
                        }
                    }
                    list.Add(local);
                }
            }
            return list;
        }
        /// <summary>
        /// 将DataTable转换为IList集合（Lambda实现）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static IList<T> ToListByLambda<T>(this DataTable table) where T : class, new()
        {
            if (table == null || table.Rows.Count <= 0)
            {
                throw new ArgumentNullException("table", "当前对象为null，无法生成表达式树");
            }
            Func<DataRow, T> func = table.Rows[0].ToExpression<T>();
            List<T> collection = new List<T>(table.Rows.Count);
            foreach (DataRow dataRow in table.Rows)
            {
                collection.Add(func(dataRow));
            }
            return collection;
        }
        #endregion
        #endregion

        #region NameObjectCollectionBase扩展
        /// <summary>
        /// 获取序列中符合条件的第一个元素，如果序列中不包含指定条件的元素，则返回默认值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="collection">名称对象集合</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this NameObjectCollectionBase collection, Func<T, bool> filter)
        {            
            foreach (T item in collection)
            {
                if (filter(item))
                {
                    return item;
                }
            }
            return default(T);
        }
        #endregion

        #region HashSet扩展
        /// <summary>
        /// 在HashSet中添加值，并返回是否添加成功
        /// </summary>
        /// <typeparam name="T">HashSet的类型</typeparam>
        /// <param name="hashset">目标HashSet</param>
        /// <param name="obj">要添加的值</param>
        /// <returns>true:表示添加成功,false:表示已存在</returns>
        public static bool SafeAdd<T>(this HashSet<T> hashset, T obj)
        {
            if (hashset.Contains(obj))
            {
                return false;
            }
            hashset.Add(obj);
            return true;
        }
        #endregion
    }
}
