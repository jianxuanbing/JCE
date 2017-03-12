/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ExtensionMethodSetting
 * 版本号：v1.0.0.0
 * 唯一标识：242eb052-d907-429b-a4c2-35bd32382e03
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/19 0:32:59
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/19 0:32:59
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 扩展方法设置
    /// </summary>
    public class ExtensionMethodSetting
    {
        /// <summary>
        /// 初始化扩展方法设置类的静态实例
        /// </summary>
        static ExtensionMethodSetting()
        {
            DefaultEncoding = Encoding.UTF8;
            DefaultCulture = CultureInfo.CurrentCulture;
        }
        /// <summary>
        /// 默认编码
        /// </summary>
        public static Encoding DefaultEncoding { get; set; }
        /// <summary>
        /// 默认区域设置
        /// </summary>
        public static CultureInfo DefaultCulture { get; set; }
    }
}
