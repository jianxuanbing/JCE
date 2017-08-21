using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Configs
{
    /// <summary>
    /// 提供NPOI配置
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public class FluentConfiguration<TEntity>:IFluentConfiguration where TEntity:class
    {
        /// <summary>
        /// 属性配置
        /// </summary>
        private IDictionary<PropertyInfo, PropertyConfiguration> _propertyConfigs;

        /// <summary>
        /// 统计信息配置
        /// </summary>
        private IList<StatisticsConfig> _statisticsConfigs;

        /// <summary>
        /// 过滤器配置
        /// </summary>
        private IList<FilterConfig> _filterConfigs;

        /// <summary>
        /// 冻结配置
        /// </summary>
        private IList<FreezeConfig> _freezeConfigs;

        /// <summary>
        /// 属性配置
        /// </summary>
        public IDictionary<PropertyInfo, PropertyConfiguration> PropertyConfigs
        {
            get { return _propertyConfigs; }
        }

        /// <summary>
        /// 统计信息配置
        /// </summary>
        public IList<StatisticsConfig> StatisticsConfigs
        {
            get { return _statisticsConfigs; }
        }

        /// <summary>
        /// 过滤器配置
        /// </summary>
        public IList<FilterConfig> FilterConfigs
        {
            get { return _filterConfigs; }
        }

        /// <summary>
        /// 冻结配置
        /// </summary>
        public IList<FreezeConfig> FreezeConfigs
        {
            get { return _freezeConfigs; }
        }

        /// <summary>
        /// 初始化一个<see cref="FluentConfiguration{TEntity}"/>类型的实例
        /// </summary>
        public FluentConfiguration()
        {
            _propertyConfigs=new Dictionary<PropertyInfo, PropertyConfiguration>();
            _statisticsConfigs=new List<StatisticsConfig>();
            _filterConfigs=new List<FilterConfig>();
            _freezeConfigs=new List<FreezeConfig>();
        }

        /// <summary>
        /// 通过指定的属性表达式获取指定<typeparamref name="TEntity"/>及其<typeparamref name="TProperty"/>的属性配置
        /// </summary>
        /// <typeparam name="TProperty">Excel属性配置<see cref="PropertyConfiguration"/></typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns></returns>
        public PropertyConfiguration Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            var pc=new PropertyConfiguration();
            var propertyInfo = GetPropertyInfo(propertyExpression);

            _propertyConfigs[propertyInfo] = pc;

            return pc;
        }

        /// <summary>
        /// 配置Excel统计信息，只适用于纵向，不适用于横向统计
        /// </summary>
        /// <param name="name">统计信息名称，默认名称位置为最后一行第一个单元格</param>
        /// <param name="formula">单元格公式，如SUM、AVERAGE等，适用于垂直统计</param>
        /// <param name="columnIndexes">列索引统计。如果<paramref name="formula"/>是SUM，而<paramref name="columnIndexes"/>是[1,3]，例如：列1和列3将是SUM第一行到最后一行。</param>
        /// <returns></returns>
        public FluentConfiguration<TEntity> HasStatistics(string name, string formula, params int[] columnIndexes)
        {
            var statistics=new StatisticsConfig()
            {
                Name = name,
                Formula = formula,
                Columns = columnIndexes
            };

            _statisticsConfigs.Add(statistics);
            return this;
        }

        /// <summary>
        /// 配置Excel过滤器
        /// </summary>
        /// <param name="firstColumn">第一列索引</param>
        /// <param name="lastColumn">最后一列索引</param>
        /// <param name="firstRow">第一行索引</param>
        /// <param name="lastRow">最后一行索引</param>
        /// <returns></returns>
        public FluentConfiguration<TEntity> HasFilter(int firstColumn, int lastColumn, int firstRow, int? lastRow = null)
        {
            var filter=new FilterConfig()
            {
                FirstCol = firstColumn,
                FirstRow = firstRow,
                LastCol = lastColumn,
                LastRow = lastRow
            };

            _filterConfigs.Add(filter);
            return this;
        }

        /// <summary>
        /// 配置Excel冻结信息
        /// </summary>
        /// <param name="columnSplit">要冻结单元格的列号</param>
        /// <param name="rowSplit">要冻结单元格的行号</param>
        /// <param name="leftMostColumn">最左边的列索引</param>
        /// <param name="topMostRow">最顶行的索引</param>
        /// <returns></returns>
        public FluentConfiguration<TEntity> HasFreeze(int columnSplit, int rowSplit, int leftMostColumn, int topMostRow)
        {
            var freeze=new FreezeConfig()
            {
                ColSplit = columnSplit,
                RowSplit = rowSplit,
                LeftMostColumn = leftMostColumn,
                TopRow = topMostRow,
            };

            _freezeConfigs.Add(freeze);
            return this;
        }

        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns></returns>
        private PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (propertyExpression.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException($"{nameof(propertyExpression)} 必须是 Lambda 表达式", nameof(propertyExpression));
            }

            var lambda = (LambdaExpression) propertyExpression;

            var memberExpression = ExtractMemberExpression(lambda.Body);
            if (memberExpression == null)
            {
                throw new ArgumentException($"{nameof(propertyExpression)} 必须是 Lambda 表达式", nameof(propertyExpression));
            }
            if (memberExpression.Member.DeclaringType == null)
            {
                throw new InvalidOperationException("属性没有声明类型");
            }
            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        /// <summary>
        /// 提取成员表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        private MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return (MemberExpression) expression;
            }
            if (expression.NodeType == ExpressionType.Convert)
            {
                var operand = ((UnaryExpression) expression).Operand;
                return ExtractMemberExpression(operand);
            }
            return null;
        }
    }
}
