/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Xmls.DynamicBuilder
 * 文件名：XmlBuilder
 * 版本号：v1.0.0.0
 * 唯一标识：5396bdbb-340d-4d3d-90bc-559beec1ccff
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:12:28
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:12:28
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

namespace JCE.Utils.Xmls.DynamicBuilder
{
    /// <summary>
    /// Xml动态生成器
    /// </summary>
    public static class XmlBuilder
    {
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <returns></returns>
        public static dynamic Create()
        {
            return new ChildXmlNodesBuilder();
        }
    }
}
