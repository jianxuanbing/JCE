using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using Autofac;
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
        internal static readonly Container DefaultContainer=new Container();

        /// <summary>
        /// 需要跳过的程序集列表
        /// </summary>
        private const string AssemblySkipLoadingPattern =
            "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        #endregion

        #region Constructor(构造函数)
        #endregion

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="configs">依赖配置</param>
        /// <returns></returns>
        public static JCE.Core.DependencyInjection.IContainer CreateContainer(params IConfig[] configs)
        {
            var container=new Container();
            container.Register(null,builder=>builder.EnableAspectScoped(),configs);
            return container;
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static List<T> CreateList<T>(string name = null)
        {
            return DefaultContainer.CreatList<T>(name);
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static List<TResult> CreateList<TResult>(Type type, string name = null)
        {
            return ((IEnumerable<TResult>) DefaultContainer.CreatList(type, name)).ToList();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static T Create<T>(string name=null)
        {
            return DefaultContainer.Create<T>(name);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static TResult Create<TResult>(Type type,string name=null)
        {
            return (TResult)DefaultContainer.Create(type);
        }

        /// <summary>
        /// 作用域开始
        /// </summary>
        /// <returns></returns>
        public static IScope BeginScope()
        {
            return DefaultContainer.BeginScope();
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="assembly">项目所在的程序集</param>
        /// <param name="configs">依赖配置</param>
        public static void Register(Assembly assembly, params IConfig[] configs)
        {
            DefaultContainer.Register(assembly,configs);
        }

        public static void Init(Assembly[] assemblies, params IConfig[] configs)
        {
            DefaultContainer.Init(builder=>RegisterTypes(assemblies,builder),configs);
        }

        public static void Init(params IConfig[] configs)
        {
            DefaultContainer.Init(null,configs);
        }

        private static void RegisterTypes(IEnumerable<Assembly> assemblies, ContainerBuilder builder)
        {
            //builder.RegisterAssemblyModules(FilterSystemAssembly(assemblies));
            //builder.RegisterAssemblyTypes(FilterSystemAssembly(assemblies))
            //    .Where(t => !t.IsAbstract)
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired()
            //    .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 过滤系统程序集
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <returns></returns>
        private static Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return
                assemblies.Where(
                    assembly =>
                        !Regex.IsMatch(assembly.FullName, AssemblySkipLoadingPattern,
                            RegexOptions.IgnoreCase | RegexOptions.Compiled)).ToArray();
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
