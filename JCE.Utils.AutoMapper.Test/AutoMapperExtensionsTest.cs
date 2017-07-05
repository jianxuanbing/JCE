/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.AutoMapper.Test
 * 文件名：AutoMapperExtensionsTest
 * 版本号：v1.0.0.0
 * 唯一标识：29003737-bed7-4ece-969e-5ac5e5ee9613
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/5 16:01:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/5 16:01:07
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
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JCE.Utils.AutoMapper;
using JCE.Utils.Develops;
using JCE.Utils.Extensions;

namespace JCE.Utils.AutoMapper.Test
{
    /// <summary>
    /// AutoMapper扩展 单元测试
    /// </summary>
    [TestClass]
    public class AutoMapperExtensionsTest
    {
        /// <summary>
        /// 测试对象映射
        /// </summary>
        [TestMethod]
        public void TestObjectMapper()
        {
            Source source=new Source();
            source.Code = "20170705";
            source.Name = "测试对象001";            
            var target = source.MapTo<Source, Target>();
            Console.WriteLine(target.ToJson());
        }

        /// <summary>
        /// 测试对象映射效率
        /// </summary>
        [TestMethod]
        public void TestObjectMapperSpeed()
        {
            Source source = new Source();
            source.Code = "20170705";
            source.Name = "测试对象001";
            Console.WriteLine("1000 万次用时");
            CodeTimer.CodeExecuteTime(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    source.MapTo<Source, Target>();
                }
            });                        
        }

        /// <summary>
        /// 测试原始方式映射效率
        /// </summary>
        [TestMethod]
        public void TestSourceObjectMapperSpeed()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Source, Target>();
            });
            Source source = new Source();
            source.Code = "20170705";
            source.Name = "测试对象001";
            Console.WriteLine("1000 万次用时");
            CodeTimer.CodeExecuteTime(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Mapper.Map<Target>(source);
                }
            });
        }

        /// <summary>
        /// 测试原始以及扩展方式映射效率
        /// </summary>
        [TestMethod]
        public void TestSourceAndExtensionsObjectMapperSpeed()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Source, Target>();
            });
            Source source = new Source();
            source.Code = "20170705";
            source.Name = "测试对象001";
            Console.WriteLine("100 万次用时");
            CodeTimer.CodeExecuteTime(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    source.MapTo<Source, Target>();
                }
            });
        }

        /// <summary>
        /// 测试列表对象映射
        /// </summary>
        [TestMethod]
        public void TestListMapper()
        {
            List<Source> sources=new List<Source>();
            sources.Add(new Source() {Code = "20170705001", Name = "测试对象001"});
            sources.Add(new Source() { Code = "20170705002", Name = "测试对象002" });
            sources.Add(new Source() { Code = "20170705003", Name = "测试对象003" });
            sources.Add(new Source() { Code = "20170705004", Name = "测试对象004" });
            sources.Add(new Source() { Code = "20170705005", Name = "测试对象005" });            
            var targets = sources.MapTo<List<Source>, List<Target>>();
            Console.WriteLine(targets.ToJson());
        }

        /// <summary>
        /// 测试列表对象映射效率
        /// </summary>
        [TestMethod]
        public void TestListMapperSpeed()
        {
            List<Source> sources = new List<Source>();
            for (int i = 0; i < 100; i++)
            {
                sources.Add(new Source() {Code = "20170705"+i.ToString("000"),Name = "测试对象"+i.ToString("000")});
            }            
            Console.WriteLine("1000 万次用时");
            CodeTimer.CodeExecuteTime(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    sources.MapTo<List<Source>, List<Target>>();
                }                
            });            
        }

        /// <summary>
        /// 测试原始方式列表对象映射效率
        /// </summary>
        [TestMethod]
        public void TestSourceListMapperSpeed()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Source, Target>();
            });
            List<Source> sources = new List<Source>();
            for (int i = 0; i < 100; i++)
            {
                sources.Add(new Source() { Code = "20170705" + i.ToString("000"), Name = "测试对象" + i.ToString("000") });
            }
            Console.WriteLine("1000 万次用时");
            CodeTimer.CodeExecuteTime(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Mapper.Map<List<Source>,List<Target>>(sources);
                }
            });
        }

        protected class Source
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        protected class Target
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
    }
}
