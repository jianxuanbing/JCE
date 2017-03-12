/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：TextReaderExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：3649e3ef-e8e2-4cf1-a57b-3994658cac2d
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:54:49
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:54:49
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 文本读取器（TextReader）扩展
    /// </summary>
    public static class TextReaderExtensions
    {
        /// <summary>
        /// 通用文本迭代器
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <returns></returns>
        /// <example>
        /// 	<code>
        /// 		using(var reader = fileInfo.OpenText()) {
        /// 		foreach(var line in reader.IterateLines()) {
        /// 		// ...
        /// 		}
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> IterateLine(this TextReader reader)
        {
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
        /// <summary>
        /// 执行通用文本迭代器，（传递委托/Lambda表达式）
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <param name="action">委托/Lambda表达式</param>
        /// <example>
        /// 	<code>
        /// 		using(var reader = fileInfo.OpenText()) {
        /// 		reader.IterateLines(l => Console.WriteLine(l));
        /// 		}
        /// 	</code>
        /// </example>
        public static void IterateLines(this TextReader reader, Action<string> action)
        {
            foreach (var line in reader.IterateLine())
            {
                action(line);
            }
        }
    }
}
