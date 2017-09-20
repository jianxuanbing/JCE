using System;
using Autofac.Extras.IocManager;
using JCE.Core.DependencyInjection;
using JCE.Core.Helpers;
using JCE.Core.Test.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Core.Test
{
    [TestClass]
    public class UnitTest1
    {        
        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Ioc.Register(new IocConfig());
        }
        [TestMethod]
        public void TestMethod1()
        {
            var userManager=Ioc.Create<IUserManager>();
            userManager.WriteInfo();
        }

        [TestMethod]
        public void TestCurrentRegister()
        {
            var container = Ioc.CreateContainer(new IocConfig());
            var userManager = container.Create<IUserManager>();
            userManager.WriteInfo();
        }
    }

    public class IocConfig : IConfig
    {
        public void Register(IIocBuilder builder)
        {            
            builder.RegisterServices(r => r.Register<IAccountManager, AccountManager>());
            builder.UseManager();
        }
    }

    public static class IocExtensionsTest
    {
        public static IIocBuilder UseManager(this IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IUserManager, UserManager>());
            return builder;
        }
    }
}
