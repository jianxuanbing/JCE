using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Http.Model
{
    /// <summary>
    /// Form 表单参数
    /// </summary>
    public struct FormParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name;

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value;

        /// <summary>
        /// 初始化一个<see cref="FormParameter"/>类型的实例
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public FormParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 重写ToString()返回 name=value编码后的格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}
