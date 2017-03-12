/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：BuildRootContext
 * 版本号：v1.0.0.0
 * 唯一标识：4b62b5a2-480e-42a0-b955-d06c2fdc91fb
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:31:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:31:36
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
    /// 生成根树节点上下文
    /// </summary>
    public class BuildRootContext
    {
        /// <summary>
        /// 初始化一个<see cref="BuildRootContext"/>类型的实例
        /// </summary>
        /// <param name="tree">树节点</param>
        public BuildRootContext(TreeNode tree)
        {
            this.Tree = tree;
        }

        /// <summary>
        /// 当前树节点
        /// </summary>
        public TreeNode Tree { get; }

        /// <summary>
        /// 设置节点集合并返回子树内容
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="items">节点集合</param>
        /// <returns></returns>
        public BuildChildrenContext<T> SetItems<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var node = TreeBuilder.BuildNode(item);
                Tree.Add(node);
            }
            return new BuildChildrenContext<T>(Tree);
        }
    }
}
