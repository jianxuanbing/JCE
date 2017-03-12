/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：BuildChildrenContext
 * 版本号：v1.0.0.0
 * 唯一标识：85f5fa54-2044-49dc-a82e-3890f1b7de6c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:31:54
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:31:54
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
    /// 构建子树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class BuildChildrenContext<T>
    {
        /// <summary>
        /// 初始化一个<see cref="BuildChildrenContext{T}"/>类型的实例
        /// </summary>
        /// <param name="tree">树节点</param>
        public BuildChildrenContext(TreeNode tree)
        {
            this.Tree = tree;
        }

        /// <summary>
        /// 当前树节点
        /// </summary>
        public TreeNode Tree { get; }

        /// <summary>
        /// 设置节点集合并返回子树节点上下文
        /// </summary>
        /// <typeparam name="V">实体类型</typeparam>
        /// <param name="itemSelector">节点集合选择器</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        public BuildChildrenContext<V> SetItems<V>(Func<T, IEnumerable<V>> itemSelector,
            Func<V, string> textSelect = null)
        {
            var leafNodes = Tree.GetLeafNodes().OfType<TreeNode<T>>();
            foreach (TreeNode<T> leafNode in leafNodes)
            {
                foreach (var child in itemSelector(leafNode.Value))
                {
                    var node = TreeBuilder.BuildNode(child, textSelect);
                    leafNode.Add(node);
                }
            }
            return new BuildChildrenContext<V>(Tree);
        }
        /// <summary>
        /// 递归设置节点集合并返回子树节点上下文
        /// </summary>
        /// <param name="itemSelector">节点集合选择器</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        public BuildChildrenContext<T> SetRecursiveItems(Func<T, IEnumerable<T>> itemSelector,
            Func<T, string> textSelect = null)
        {
            var context = this;
            while (Tree.GetLeafNodes().OfType<TreeNode<T>>().Any(n => itemSelector(n.Value).Any()))
            {
                context = context.SetItems<T>(itemSelector, textSelect);
            }
            return context;
        }
    }
}
