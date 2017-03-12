/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes.Trees
 * 文件名：TreeBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：bd378318-4768-421a-8bba-59f51fd024db
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:31:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:31:17
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
    /// 树 生成器
    /// </summary>
    public class TreeBuilder
    {
        /// <summary>
        /// 生成根节点
        /// </summary>
        /// <param name="text">根节点名</param>
        /// <returns></returns>
        public static BuildRootContext Build(string text)
        {
            var root = new TreeNode(text);
            return new BuildRootContext(root);
        }

        /// <summary>
        /// 生成树节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        internal static TreeNode<T> BuildNode<T>(T t, Func<T, string> textSelect = null)
        {
            var text = textSelect != null ? textSelect(t) : Convert.ToString(t);
            var node = new TreeNode<T>(text, t);
            return node;
        }
    }
}
