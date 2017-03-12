/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Xmls.DynamicBuilder
 * 文件名：ChildXmlNodesBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：0f94c682-72b1-4259-a9cb-f0585978c1ca
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:12:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:12:47
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Xmls.DynamicBuilder
{
    /// <summary>
    /// Xml子节点动态生成器
    /// </summary>
    public class ChildXmlNodesBuilder : DynamicObject
    {
        private IList<XmlNodeBuilder> children;//子节点集合
        private XmlNodeBuilder parent;
        /// <summary>
        /// 构造函数，初始化Xml节点动态生成器
        /// </summary>
        /// <param name="parent">Xml节点动态生成器</param>
        internal ChildXmlNodesBuilder(XmlNodeBuilder parent = null)
        {
            this.parent = parent;
            this.children = new List<XmlNodeBuilder>();
        }

        /// <summary>
        /// 结束节点
        /// </summary>
        public XmlNodeBuilder End
        {
            get { return parent; }
        }

        /// <summary>
        /// 添加节点生成器
        /// </summary>
        /// <param name="node">节点生成器</param>
        internal void AddNode(XmlNodeBuilder node)
        {
            this.children.Add(node);
        }
        /// <summary>
        /// 确认Xml子节点生成器是否包含任何元素
        /// </summary>
        /// <returns></returns>
        internal bool Any()
        {
            return this.children.Any();
        }
        /// <summary>
        /// 生成Xml文档
        /// </summary>
        /// <param name="level">层级</param>
        /// <returns></returns>
        public string Build(int level)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in this.children)
            {
                sb.AppendLine(child.Build(level));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 尝试获取成员值
        /// </summary>
        /// <param name="binder">获取成员值</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var nextNode = new XmlNodeBuilder(binder.Name, this.parent);
            this.AddNode(nextNode);
            result = nextNode;
            return true;
        }
        /// <summary>
        /// 尝试调用成员
        /// </summary>
        /// <param name="binder">调用操作</param>
        /// <param name="args">参数</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var innerText = (string)args[0];
            Dictionary<string, string> attr = null;
            if (args.Length > 1)
            {
                attr = args[1].GetType()
                    .GetProperties()
                    .ToDictionary(pr => pr.Name, pr => pr.GetValue(args[1]).ToString());
            }
            var nextNode = new XmlNodeBuilder(binder.Name, this.parent, innerText, attr);
            this.AddNode(nextNode);
            result = nextNode;
            return true;
        }
    }
}
