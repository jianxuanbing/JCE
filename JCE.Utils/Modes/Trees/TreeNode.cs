/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：TreeNode
 * 版本号：v1.0.0.0
 * 唯一标识：d8de09ce-28c4-4c9c-9f25-14e026059084
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:30:14
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:30:14
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
    /// 树节点
    /// 参考地址:http://www.cnblogs.com/ldp615/archive/2011/12/09/simple-create-a-complex-tree.html
    /// </summary>
    public class TreeNode
    {
        /**
         * 1、构建根节点，传入一个字符串作为根节点的文本
         * 2、指定树的第一级，必须传入一个集合
         * 3、指定树的第二级，传入的是条件
         * 4、调用Tree属性，返回根节点，也就是树
         * 有限级树
         * var tree=TreeBuilder.Build("产品")
         *      .SetItems(categories)
         *      .SetItems(category=>products.Where(p=>p.Category=category))
         *      .Tree;
         * 无限级树
         * var tree=TreeBuilder.Build("员工树")
         *      .SetItems(employess.Where(e=>e.ReportsTo==null))
         *      .SetRecursiveItems(e=>e.Subordinates)
         *      .Tree;
         */
        private readonly List<TreeNode> _children;

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; protected set; }
        /// <summary>
        /// 父树节点
        /// </summary>
        public TreeNode Parent { get; set; }
        /// <summary>
        /// 子树节点集合
        /// </summary>
        public IEnumerable<TreeNode> Children { get { return _children; } }

        /// <summary>
        /// 初始化一个<see cref="TreeNode"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public TreeNode(string text, object value = null)
        {
            this.Text = text;
            this.Value = value;
            _children = new List<TreeNode>();
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="childNode">子树节点</param>
        public void Add(TreeNode childNode)
        {
            _children.Add(childNode);
            childNode.Parent = this;
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="childNode">子树节点</param>
        public void Remove(TreeNode childNode)
        {
            _children.Remove(childNode);
        }

        /// <summary>
        /// 返回树节点为文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
