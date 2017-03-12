/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Item
 * 版本号：v1.0.0.0
 * 唯一标识：98cee145-8715-4a48-87c4-f1f08c117fc1
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:08:54
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:08:54
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

namespace JCE.Utils
{
    /// <summary>
    /// 列表项
    /// </summary>
    public class Item : IComparable<Item>
    {
        #region Property(属性)
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化列表项
        /// </summary>
        public Item() { }
        /// <summary>
        /// 初始化列表项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public Item(string text, string value) : this(text, value, 0) { }
        /// <summary>
        /// 初始化列表项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public Item(string text, string value, int sortId)
        {
            Text = text;
            Value = value;
            SortId = sortId;
        }
        #endregion

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其他列表项</param>
        /// <returns></returns>
        public int CompareTo(Item other)
        {
            return string.Compare(Text, other.Text, StringComparison.CurrentCulture);
        }
    }
}
