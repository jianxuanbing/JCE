using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using Autofac;
using Autofac.Core;
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
            //Ioc.Register(new IocConfig());
            Thread.CurrentPrincipal = new ClaimsPrincipal(GetIdentity(Guid.NewGuid().ToString(), "jian玄冰"));
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var executionAssembly = Assembly.GetExecutingAssembly();
            Ioc.Register(Assembly.GetExecutingAssembly(),new IocConfig());
            var userManager = Ioc.Create<IUserManager>();
            userManager.WriteInfo();
        }

        [TestMethod]
        public void TestCurrentPrincipal()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var username = identity.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            Console.WriteLine(username);

        }

        private ClaimsIdentity GetIdentity(string userid, string userName)
        {
            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim("userid", userid));
            identity.AddClaim(new Claim("username", userName));

            return identity;
        }
    }

    public class IocConfig : ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.AddScoped<IUserManager, UserManager>();
            builder.AddScoped<IAccountManager, AccountManager>();
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
