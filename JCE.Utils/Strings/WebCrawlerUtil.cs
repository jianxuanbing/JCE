/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Strings
 * 文件名：WebCrawlerUtil
 * 版本号：v1.0.0.0
 * 唯一标识：3355c1ea-cc95-449e-b3af-79578a911b67
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 13:28:32
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 13:28:32
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Strings
{
    /// <summary>
    /// 网页爬虫工具类
    /// </summary>
    public class WebCrawlerUtil
    {
        #region ClearTag(清除Html标签)
        /// <summary>
        /// 清除Html标签
        /// </summary>
        /// <param name="html">html内容</param>
        /// <returns></returns>
        public static string ClearTag(string html)
        {
            return ClearTag(html, @"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t");
        }
        /// <summary>
        /// 清除Hmtl标签
        /// </summary>
        /// <param name="html">html内容</param>
        /// <param name="reg">正则表达式</param>
        /// <returns></returns>
        public static string ClearTag(string html, string reg)
        {
            if (html.IsNullOrEmpty())
            {
                return Str.Empty;
            }
            Regex regex = new Regex(reg,
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return regex.Replace(html, "");
        }
        #endregion

        #region ConvertToJs(转换成js输出)
        /// <summary>
        /// 将html内容转换成js输出
        /// </summary>
        /// <param name="html">html内容</param>
        /// <returns></returns>
        public static string ConvertToJs(string html)
        {
            Str str=new Str();
            Regex regex=new Regex(@"\r\n",RegexOptions.IgnoreCase);
            string[] strArray = regex.Split(html);
            foreach (string strLine in strArray)
            {
                str.Append("document.writeln(\"{0}\");\r\n",strLine.Replace("\"","\\\""));
            }
            return str.ToString();
        }
        #endregion

        #region ReplaceSpace(替换空格)
        /// <summary>
        /// 替换空格
        /// </summary>
        /// <param name="html">html内容</param>
        /// <returns></returns>
        public static string ReplaceSpace(string html)
        {            
            if (html.Length > 0)
            {                
                html = html.Replace(" ", "");
                html = html.Replace("&nbsp;", "");                
            }
            return html;
        }
        #endregion

        #region StringToHtml(普通字符串转换成html字符串)
        /// <summary>
        /// 普通字符串转换成html字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {
            if (str.Length > 0)
            {
                char brChar = (char) 13;
                str = str.Replace(brChar.ToString(), "<br/>");
                str = str.Replace(" ", "&nbsp;");
                str = str.Replace("　", "&nbsp;nbsp;");
            }
            return str;
        }
        #endregion

        #region TruncateToHtml(截断长度并转换为Html)
        /// <summary>
        /// 截断长度并转换为Html
        /// </summary>
        /// <param name="content">字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string TruncateToHtml(string content, int length)
        {
            content = content.Truncate(length);
            content = StringToHtml(content);
            return content;
        }
        #endregion

        #region DelHtmlString(删除所有的Html标记)
        /// <summary>
        /// 删除所有的Html标记
        /// </summary>
        /// <param name="html">html内容</param>
        /// <returns></returns>
        public static string DelHtmlString(string html)
        {
            string[] regexs =
            {
                @"<script[^>]*?>.*?</script>",
                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                @"([\r\n])[\s]+",
                @"&(quot|#34);",
                @"&(amp|#38);",
                @"&(lt|#60);",
                @"&(gt|#62);",
                @"&(nbsp|#160);",
                @"&(iexcl|#161);",
                @"&(cent|#162);",
                @"&(pound|#163);",
                @"&(copy|#169);",
                @"&#(\d+);",
                @"-->",
                @"<!--.*\n"
            };
            string[] replaces =
            {
                "",
                "",
                "",
                "\"",
                "&",
                "<",
                ">",
                " ",
                "\xa1", //chr(161),
                "\xa2", //chr(162),
                "\xa3", //chr(163),
                "\xa9", //chr(169),
                "",
                "\r\n",
                ""
            };

            for (int i = 0; i < regexs.Length; i++)
            {
                html=new Regex(regexs[i],RegexOptions.Multiline|RegexOptions.IgnoreCase).Replace(html,replaces[i]);
            }
            html=html.Replace("<", "");
            html = html.Replace(">", "");
            html = html.Replace("\r\n", "");
            return html;
        }
        #endregion

        #region DelTag(删除指定Html标签)
        /// <summary>
        /// 删除指定Html标签
        /// </summary>
        /// <param name="html">html内容</param>
        /// <param name="tag">html标签</param>
        /// <param name="isContent">是否清除内容</param>
        /// <returns></returns>
        public static string DelTag(string html, string tag, bool isContent)
        {
            if (tag.IsNullOrEmpty())
            {
                return html;
            }
            return Regex.Replace(html,
                isContent
                    ? string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag)
                    : string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 删除指定Html标签数组
        /// </summary>
        /// <param name="html">html内容</param>
        /// <param name="tag">html标签数组,使用','号分割</param>
        /// <param name="isContent">是否清除内容</param>
        /// <returns></returns>
        public static string DelTagArray(string html, string tag, bool isContent)
        {
            string[] tagArray = tag.Split(',');
            foreach (string temp in tagArray)
            {
                html = DelTag(html, temp, isContent);
            }
            return html;
        }
        #endregion
    }
}
