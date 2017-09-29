using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;

namespace JCE.Logs.Exceptionless
{
    /// <summary>
    /// Exceptionless 配置
    /// </summary>
    public class ExceptionlessConfig
    {
        /// <summary>
        /// Exceptionless 配置实例
        /// </summary>
        public static ExceptionlessConfig Instance { get; set; }

        /// <summary>
        /// Api 密匙
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// 启用调试
        /// </summary>
        public bool EnabledDebug { get; set; }

        /// <summary>
        /// 启用跟踪
        /// </summary>
        public bool EnabledTrace { get; set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ExceptionlessConfig()
        {
            try
            {
                string json =
                    File.ReadAllText(Sys.GetPhysicalPath(ConfigUtil.GetAppSettings("ExceptionlessConfig")));
                Instance = json.ToObject<ExceptionlessConfig>();
            }
            catch (Exception ex)
            {
                Instance = null;
            }
        }
    }
}
