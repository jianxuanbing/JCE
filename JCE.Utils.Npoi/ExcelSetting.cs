using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Npoi.Configs;

namespace JCE.Utils.Npoi
{
    /// <summary>
    /// Excel设置，表示从Excel加载所有保存的设置
    /// </summary>
    public class ExcelSetting
    {
        /// <summary>
        /// 获取或设置 公司名称
        /// </summary>
        public string Compnay { get; set; } = "jce";

        /// <summary>
        /// 获取或设置 作者
        /// </summary>
        public string Author { get; set; } = "jce";

        /// <summary>
        /// 获取或设置 主题
        /// </summary>
        public string Subject { get; set; } = "JCE.Utils.Npoi 扩展封装，提供IEnumerable<T>导出数据";

        /// <summary>
        /// 获取或设置 指示是否使用创建信息（Excel公司或作者信息）
        /// </summary>
        public bool UseCreateInfo { get; set; } = false;

        /// <summary>
        /// 获取或设置 指示是否使用*.xlsx文件扩展名
        /// </summary>
        public bool UserXlsx { get; set; } = true;

        /// <summary>
        /// 获取指定实体配置入口点
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public FluentConfiguration<TEntity> For<TEntity>() where TEntity : class
        {
            var fc=new FluentConfiguration<TEntity>();

            FluentConfigs[typeof(TEntity)] = fc;

            return fc;
        }

        /// <summary>
        /// 获取 实体配置字典
        /// </summary>
        internal IDictionary<Type,IFluentConfiguration> FluentConfigs { get; }=new Dictionary<Type, IFluentConfiguration>();
    }
}
