/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：TreeNode_
 * 版本号：v1.0.0.0
 * 唯一标识：30d12e1b-3869-41d2-b85b-fe43d764249c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:30:37
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:30:37
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

namespace JCE.Utils.Modes.Trees
{
    /// <summary>
    /// 泛型树节点
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class TreeNode<T> : TreeNode
    {
        /// <summary>
        /// 初始化一个<see cref="TreeNode{T}"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public TreeNode(string text, object value = null) : base(text, value)
        {
        }

        /// <summary>
        /// 值
        /// </summary>
        public new T Value
        {
            get { return (T)base.Value; }
        }
    }
}
