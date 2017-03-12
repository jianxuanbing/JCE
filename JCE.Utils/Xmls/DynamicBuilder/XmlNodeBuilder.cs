/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Xmls.DynamicBuilder
 * 文件名：XmlNodeBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：c2111899-6f03-4284-8822-608a2c8726be
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:13:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:13:07
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
    /// Xml节点动态生成器
    /// </summary>
    public class XmlNodeBuilder : DynamicObject
    {
        private ChildXmlNodesBuilder _children;//子节点生成器
        private XmlNodeBuilder parent;
        private string name;
        private string innerText;
        private IDictionary<string, string> attributesDic;
        /// <summary>
        /// 构造函数，初始化标签名、Xml父节点生成器、内容、属性字典
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <param name="parent">Xml父节点生成器</param>
        /// <param name="innerText">内容</param>
        /// <param name="attributes">属性字典</param>
        internal XmlNodeBuilder(string tagName, XmlNodeBuilder parent, string innerText = null,
            IDictionary<string, string> attributes = null)
        {
            this._children = new ChildXmlNodesBuilder();
            this.parent = parent;
            this.name = tagName;
            this.innerText = innerText;
            this.attributesDic = attributes;
        }

        /// <summary>
        /// Xml父节点生成器
        /// </summary>
        public XmlNodeBuilder End
        {
            get { return this.parent; }
        }

        /// <summary>
        /// Xml子节点生成器
        /// </summary>
        public ChildXmlNodesBuilder Begin
        {
            get
            {
                this._children = new ChildXmlNodesBuilder(this);
                return this._children;
            }
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">Xml节点生成器</param>
        internal void AddNode(XmlNodeBuilder node)
        {
            this._children.AddNode(node);
        }

        /// <summary>
        /// 生成Xml文档
        /// </summary>
        /// <param name="level">层级</param>
        /// <returns></returns>
        public string Build(int level = 0)
        {
            StringBuilder sb = new StringBuilder();
            var whiteSpace = new string(' ', level * 4);
            sb.Append(whiteSpace + "<" + this.name);
            if (this.attributesDic != null && this.attributesDic.Any())
            {
                StringBuilder attrAsString = new StringBuilder(" ");
                foreach (var attr in attributesDic)
                {
                    attrAsString.Append(attr.Key + "=\"" + attr.Value + "\" ");
                }
                sb.Append(attrAsString.ToString().TrimEnd());
            }
            sb.Append(">" + this.innerText);
            if (this._children.Any())
            {
                sb.AppendLine(whiteSpace).Append(this._children.Build(level + 1));
                sb.Append(whiteSpace + "</" + this.name + ">" + whiteSpace);
            }
            else
            {
                sb.Append("</" + this.name + ">");
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
            this.parent.AddNode(nextNode);
            result = nextNode;
            return true;
        }
        /// <summary>
        /// 尝试调用操作
        /// </summary>
        /// <param name="binder">调用操作</param>
        /// <param name="args">参数</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var text = (string)args[0];
            Dictionary<string, string> attr = null;
            if (args.Length > 1)
            {
                attr = (Dictionary<string, string>)args[1];
            }
            var nextNode = new XmlNodeBuilder(binder.Name, this.parent, text, attr);
            this.parent.AddNode(nextNode);
            result = nextNode;
            return true;
        }
    }
}
