using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.DependencyInjection;

namespace JCE.Core.Helpers
{
    /// <summary>
    /// 容器
    /// </summary>
    public static class Ioc
    {
        #region Property(属性)
        /// <summary>
        /// 默认容器
        /// </summary>
        private static readonly Container DefaultContainer=new Container();

        /// <summary>
        /// 需要跳过的程序集列表
        /// </summary>
        private const string AssemblySkipLoadingPattern =
            "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        #endregion

        #region Constructor(构造函数)
        #endregion

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return DefaultContainer.Create<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object Create(Type type)
        {
            return DefaultContainer.Create(type);
        }

        public static void Register(Assembly assembly, params IConfig[] configs)
        {
            
        }

        public static void Register(Assembly assembly, bool autoRegister, params IConfig[] configs)
        {
            
        }

        /// <summary>
        /// 释放容器
        /// </summary>
        public static void Dispose()
        {
            DefaultContainer.Dispose();
        }
    }
}
