/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：ChangeValueCollection
 * 版本号：v1.0.0.0
 * 唯一标识：8fd327b5-56dd-438d-ae74-21223085c99e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:15:01
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:15:01
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

namespace JCE.Core.Domains
{
    /// <summary>
    /// 变更值集合
    /// </summary>
    public class ChangeValueCollection : List<ChangeValue>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="desription">描述</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="isAttention">是否关注</param>
        public void Add(string propertyName, string desription, string oldValue, string newValue,
            bool isAttention = false)
        {
            Add(new ChangeValue(propertyName, desription, oldValue, newValue, isAttention));
        }

        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention
        {
            get { return this.Any(t => t.IsAttention); }
        }

        /// <summary>
        /// 获取关注的变更集
        /// </summary>
        /// <returns></returns>
        public List<ChangeValue> GetAttentionValues()
        {
            return this.Where(t => t.IsAttention).ToList();
        }

        /// <summary>
        /// 输出变更信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in this)
            {
                result.AppendLine(item.ToString());
            }
            return result.ToString();
        }
    }
}
