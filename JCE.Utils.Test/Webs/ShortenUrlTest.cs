/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Test.Webs
 * 文件名：ShortenUrlTest
 * 版本号：v1.0.0.0
 * 唯一标识：86721313-a2af-4f19-a81c-f469591d17df
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/8/10 12:44:19
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/8/10 12:44:19
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
using JCE.Utils.Extensions;
using JCE.Utils.Webs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Webs
{
    /// <summary>
    /// 网址缩短测试
    /// </summary>
    [TestClass]
    public class ShortenUrlTest
    {
        /// <summary>
        /// 添加网址测试
        /// </summary>
        [TestMethod]
        public void AddUrlTest()
        {
            var result=ShortenUrl.AddUrl(new[] {"http://www.baidu.com"});
            result.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
            });
        }
    }
}
