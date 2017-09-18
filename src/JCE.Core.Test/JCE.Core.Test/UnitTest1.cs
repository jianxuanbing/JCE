using System;
using Autofac.Extras.IocManager;
using JCE.Core.DependencyInjection;
using JCE.Core.Test.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        private static IContainer _container=new Container();
        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _container.Register(new IocConfig());
        }
        [TestMethod]
        public void TestMethod1()
        {
            var userManager=_container.Create<IUserManager>();
            userManager.WriteInfo();
        }
    }

    public class IocConfig : IConfig
    {
        public void Register(IIocBuilder builder)
        {
            builder.RegisterServices(r => r.Register<IUserManager, UserManager>());
            builder.RegisterServices(r => r.Register<IAccountManager, AccountManager>());
        }
    }
}
