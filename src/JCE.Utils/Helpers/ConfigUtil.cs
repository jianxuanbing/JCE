using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// 配置信息工具类
    /// </summary>
    public static partial class ConfigUtil
    {
        #region GetAppSettings(获取AppSettings)
        /// <summary>
        /// 获取AppSettings
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            //key.CheckNotNull("key");
            return ConfigurationManager.AppSettings[key];
        }
        #endregion
    }
}
