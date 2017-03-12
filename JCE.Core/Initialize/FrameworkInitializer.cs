/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Initialize
 * 文件名：FrameworkInitializer
 * 版本号：v1.0.0.0
 * 唯一标识：e8d2292b-010e-45a8-b647-8a10dbe85921
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 14:03:01
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 14:03:01
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
using JCE.Core.Configs;
using JCE.Core.Dependency;
using JCE.Utils.Extensions;

namespace JCE.Core.Initialize
{
    /// <summary>
    /// 框架初始化
    /// </summary>
    public class FrameworkInitializer:IFrameworkInitializer
    {
        //基础模块，只初始化一次
        private static bool _mapperInitialized;
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;
        private static bool _entityInfoInitialized;

        /// <summary>
        /// 开始执行框架初始化
        /// </summary>
        /// <param name="iocBuilder">依赖注入构建器</param>
        public void Initialize(IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            JceConfig config = JceConfig.Instance;

            //依赖注入初始化
            IServiceProvider provider = iocBuilder.Build();

            //对象映射功能初始化

            //日志功能初始化
            IBasicLoggingInitializer loggingInitializer = provider.GetService<IBasicLoggingInitializer>();
            if (!_basicLoggingInitialized && loggingInitializer != null)
            {
                loggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            //数据库初始化

            //实体信息初始化

            //功能信息初始化
        }
    }
}
