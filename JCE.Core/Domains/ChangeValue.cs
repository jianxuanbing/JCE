/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：ChangeValue
 * 版本号：v1.0.0.0
 * 唯一标识：1a0de64b-f991-4702-8ecf-19c088f9f485
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:14:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:14:43
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
    /// 变更值
    /// </summary>
    public class ChangeValue
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desription { get; private set; }
        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue { get; private set; }
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; private set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="ChangeValue"/>类型的实例
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="description">描述</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="isAttention">是否关注</param>
        public ChangeValue(string propertyName, string description, string oldValue, string newValue, bool isAttention)
        {
            PropertyName = propertyName;
            Desription = description;
            OldValue = oldValue;
            NewValue = newValue;
            IsAttention = isAttention;
        }

        /// <summary>
        /// 输出变更信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}({1}),", PropertyName, Desription);
            result.AppendFormat("旧值:{0},新值:{1}", OldValue, NewValue);
            return result.ToString();
        }
    }
}
