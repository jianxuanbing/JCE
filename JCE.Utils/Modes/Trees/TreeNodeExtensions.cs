/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：TreeNodeExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：027b4a0a-dabd-4895-8b46-5ecbb38fd56e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:31:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:31:00
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
    /// 树节点扩展
    /// </summary>
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// 查找所有叶的子节点
        /// </summary>
        /// <param name="treeNode">当前树节点</param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> GetLeafNodes(this TreeNode treeNode)
        {
            foreach (TreeNode child in treeNode.Children)
            {
                if (child.Children.Any())
                {
                    foreach (var descendant in GetLeafNodes(child))
                    {
                        yield return descendant;
                    }
                }
                else
                {
                    yield return child;
                }
            }
        }
    }
}
