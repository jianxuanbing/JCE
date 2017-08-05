/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Test.Common
 * 文件名：GuidUtilTest
 * 版本号：v1.0.0.0
 * 唯一标识：a3a67bda-ba38-4ce4-a1fb-ed40408ea185
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/8/5 13:01:09
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/8/5 13:01:09
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
using JCE.Utils.Common;
using JCE.Utils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Common
{
    [TestClass]
    public class GuidUtilTest
    {
        /// <summary>
        /// 生成有序的唯一ID 测试
        /// </summary>
        [TestMethod]
        public void GenerateSequentialGuidTest()
        {
            List<Guid> ids=new List<Guid>();
            for (int i = 0; i < 100; i++)
            {
                ids.Add(GuidUtil.GenerateSequentialGuid());
            }
            Console.WriteLine(ids.ToJson());
        }
    }
}
