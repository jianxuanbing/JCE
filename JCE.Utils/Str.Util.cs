/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Str
 * 版本号：v1.0.0.0
 * 唯一标识：441c83c9-c0f9-4d32-851f-41afc2d4737f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 9:15:40
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 9:15:40
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JCE.Utils.Encrypts;
using JCE.Utils.Extensions;
using Microsoft.VisualBasic;

namespace JCE.Utils
{
    /// <summary>
    /// 字符串操作 - 工具方法
    /// </summary>
    public sealed partial class Str
    {
        #region Empty(空字符串)
        /// <summary>
        /// 空字符串
        /// </summary>
        public static string Empty
        {
            get { return string.Empty; }
        }
        #endregion

        #region GenerateCode(创建流水号)
        /// <summary>
        /// 创建一个32位流水号
        /// </summary>
        /// <returns></returns>
        public static string GenerateCode()
        {
            return MD5Crypt.EncryptBy32(Guid.NewGuid().ToString()).ToLower();
        }

        /// <summary>
        /// 创建一个16位流水号
        /// </summary>
        /// <returns></returns>
        public static string GenerateCodeBy16()
        {
            return MD5Crypt.EncryptBy16(Guid.NewGuid().ToString()).ToLower();
        }
        #endregion

        #region GetHideMobile(获取隐藏中间几位后的手机号码)
        /// <summary>
        /// 获取隐藏中间几位后的手机号码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public static string GetHideMobile(string mobile)
        {
            if (!mobile.IsNullOrEmpty())
            {
                string show = mobile.Substring(0, 3) + "******" + mobile.Substring(mobile.Length - 3);
                return show;
            }
            return mobile;
        }
        #endregion

        #region GetStringLength(获取字符串的字节数)
        /// <summary>
        /// 获取字符串的字节数
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static int GetStringLength(string source)
        {
            if (source.IsNullOrEmpty())
            {
                return 0;
            }
            int strLength = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(source);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)
                {
                    strLength++;
                }
                strLength++;
            }

            return strLength;
        }
        #endregion

        #region CutString(把指定字符串截取成指定长度的子串)
        /// <summary>
        /// 把指定字符串截取成指定长度的子串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static string CutString(string source, int length)
        {
            return CutString(source, length, "");
        }
        /// <summary>
        /// 把指定字符串截取成指定长度的子串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="endStr">字符串尾部</param>
        /// <returns></returns>
        public static string CutString(string source, int length, string endStr)
        {
            if (Encoding.Default.GetBytes(source).Length <= length)
            {
                return source;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            length -= Encoding.Default.GetBytes(endStr).Length;
            int num = 0;
            StringBuilder sb = new StringBuilder();
            byte[] bytes = encoding.GetBytes(source);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > length)
                {
                    break;
                }
                sb.Append(source.Substring(i, 1));
            }

            if (!endStr.IsNullOrEmpty())
            {
                sb.Append(endStr);
            }

            return sb.ToString();
        }
        #endregion

        #region FilterInputText(过滤特殊字符与标签)
        /// <summary>
        /// 过滤特殊字符与标签（'-.\\;:\%《》* @ >大于 小于）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterInputText(string source)
        {
            source = FilterAllHtmlTags(source);
            source =
                source.Replace("'", "")
                    .Replace("-", "")
                    .Replace(".", "")
                    .Replace("\\", "")
                    .Replace(";", "")
                    .Replace(":", "")
                    .Replace("\"", "")
                    .Replace("%", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("《", "")
                    .Replace("》", "")
                    .Replace(" ", "")
                    .Replace("*", "")
                    .Replace("@", "")
                    .Trim();
            return source;
        }
        #endregion

        #region FilterUrl(过滤Url地址特殊字符)
        /// <summary>
        /// 过滤Url地址特殊字符（'-\\;\>小于 大于《》 ）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterInputUrl(string source)
        {
            return
                source.Replace("'", "")
                    .Replace("-", "")
                    .Replace("\\", "")
                    .Replace(";", "")
                    .Replace("\"", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("《", "")
                    .Replace("》", "")
                    .Replace(" ", "")
                    .Trim();
        }
        #endregion

        #region FilterSqlSpecialChar(Sql特殊字符的过滤)
        /// <summary>
        /// Sql语句特殊字符过滤，防Sql注入
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterSqlSpecialChar(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                string pattern = "exec|insert|select|delete|'|update|chr|mid|master|truncate|char|declare|and|--";
                if (Regex.IsMatch(source.ToLower(), pattern, RegexOptions.IgnoreCase))
                {
                    source = Regex.Replace(source.ToLower(), pattern, " ", RegexOptions.IgnoreCase);
                }
            }
            return source;
        }
        #endregion

        #region ReplaceSqlSpecialChar(替换Sql语句的特殊字符)
        /// <summary>
        /// Sql语句特殊字符(%,-,')替换处理，防Sql注入
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string ReplaceSqlSpecialChar(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = source.Replace("'", "''").Replace("%", "[%]").Replace("-", "[-]");
            }
            return source;
        }
        #endregion

        #region ReplaceXmlSpecialChar(替换Xml的特殊字符)
        /// <summary>
        /// 替换Xml的特殊字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string ReplaceXmlSpecialChar(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = source.Replace("&", "&amp;");
                source = source.Replace("<", "&lt;");
                source = source.Replace(">", "&gt;");
                source = source.Replace("'", "&apos;");
                source = source.Replace("\"", "&quot;");
            }
            return source;
        }
        #endregion

        #region ReplaceJsSpecialChar(替换Js的特殊字符)
        /// <summary>
        /// 替换Js的特殊字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string ReplaceJsSpecialChar(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = source.Replace(@"\", @"\\");
                source = source.Replace("\n", @"\n");
                source = source.Replace("\r", @"\r");
                source = source.Replace("\"", "\\\"");
            }
            return source;
        }
        #endregion

        #region Filter(过滤标签)
        /// <summary>
        /// 过滤对象中string类型的参数的Html标签
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static T Filter<T>(T t) where T : class, new()
        {
            Type type = typeof(T);
            if (t != null)
            {
                //反射该类所有属性
                var properties =
                    type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => x.PropertyType == typeof(string));
                foreach (var property in properties)
                {
                    string val = property.GetValue(t, null) == null ? "" : property.GetValue(t, null).ToString();
                    val = FilterAllHtmlTags(val);
                    property.SetValue(t, val, null);
                }
            }
            return t;
        }
        /// <summary>
        /// 过滤a标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterA(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<.?a(.|\n)*?>", "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤div标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterDiv(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<.?div(.|\n)*?>", "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤font标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterFont(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<.?font(.|\n)*?>", "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤img标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterImg(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<img(.|\n)*?>", "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤span标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterSpan(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<.?span(.|\n)*?>", "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤object标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterObject(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                const string pattern = @"<object((?:.|\n)*?)</object>";
                Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(source);
                if (match.Success)
                {
                    var objStr = match.Value;
                    source = source.Replace(objStr, "");
                }
            }
            return source;
        }

        /// <summary>
        /// 过滤JavaScript标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterScript(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                const string pattern = @"<script((?:.|\n)*?)</script>";
                Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(source);
                if (match.Success)
                {
                    var objStr = match.Value;
                    source = source.Replace(objStr, "");
                }
            }
            return source;
        }

        /// <summary>
        /// 过滤IFrame标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterIFrame(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                const string pattern = @"<iframe((?:.|\n)*?)</iframe>";
                Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(source);
                if (match.Success)
                {
                    var objStr = match.Value;
                    source = source.Replace(objStr, "");
                }
            }
            return source;
        }

        /// <summary>
        /// 过滤style样式标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterStyle(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                const string pattern = @"<style((?:.|\n)*?)</style>";
                Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(source);
                if (match.Success)
                {
                    var objStr = match.Value;
                    source = source.Replace(objStr, "");
                }
            }
            return source;
        }

        /// <summary>
        /// 过滤table、tr、td标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterTableProperty(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source =
                    Regex.Replace(
                        Regex.Replace(Regex.Replace(source, "<.?table(.|\n)*?>", "", RegexOptions.IgnoreCase),
                            "<.?tr(.|\n)*?>", "", RegexOptions.IgnoreCase), "<.?td(.|\n)*?>", "",
                        RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 字符串根据传入的正则表达式进行过滤
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string SuperiorHtml(string source, string pattern)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, pattern, "", RegexOptions.IgnoreCase);
            }
            return source;
        }

        /// <summary>
        /// 过滤转义字符(..)Html标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterEscapeChar(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                foreach (Match match in Regex.Matches(source, "&.+?;"))
                {
                    source = source.Replace(match.Value, "");
                }
            }
            return source;
        }
        /// <summary>
        /// 过滤所有Html标签，忽略尖括号
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterHtmlTags(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                source = Regex.Replace(source, "<([^<]|\n)+?>", "");
            }
            return source;
        }
        /// <summary>
        /// 过滤Html的所有标签，带有尖括号均过滤
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string FilterAllHtmlTags(string source)
        {
            if (!source.IsNullOrEmpty())
            {
                foreach (Match match in Regex.Matches(source, "<(.|\n)+?>"))
                {
                    source = source.Replace(match.Value, "");
                }
            }
            return source;
        }
        #endregion

        #region NormalFilter(一般过滤)
        /// <summary>
        /// 一般过滤，过滤Sql的危险，同时过滤xml、script
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string NormalFilter(string source)
        {
            string result = FilterSqlSpecialChar(ReplaceSqlSpecialChar(source));//过滤Sql
            result = ReplaceXmlSpecialChar(result);//Xml过滤
            result = FilterScript(FilterIFrame(FilterObject(result)));
            return result;
        }
        #endregion

        #region HeightFilter(高度过滤)
        /// <summary>
        /// 高度过滤，过滤所有的危险，如果带有富文本文档则不适用
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string HeightFilter(string source)
        {
            string result = FilterSqlSpecialChar(ReplaceSqlSpecialChar(source));//过滤Sql
            result = ReplaceXmlSpecialChar(result);//过滤Xml
            result = FilterHtmlTags(FilterAllHtmlTags(result));
            return result;
        }
        #endregion

        #region BubbleSort(冒泡排序)
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="source">源数组</param>
        /// <returns></returns>
        public static string[] BubbleSort(string[] source)
        {
            for (var i = 0; i < source.Length; i++)//最多做source.Length-1躺排序
            {
                var exchange = false;
                for (var j = source.Length - 2; j >= i; j--)
                {
                    if (String.CompareOrdinal(source[j + 1], source[j]) < 0)
                    {
                        var temp = source[j + 1];//临时变量
                        source[j + 1] = source[j];
                        source[j] = temp;

                        exchange = true;//发生了交换，故将交换标识设置为真
                    }
                }
                if (!exchange)//本躺排序未发生交换，提前终止算法
                {
                    break;
                }
            }
            return source;
        }
        #endregion

        #region MockInt(模拟虚拟数据)
        /// <summary>
        /// 模拟虚拟数据，可以到小时返回一个模拟后的结果整数
        /// </summary>
        /// <param name="realInt">原始值</param>
        /// <param name="beginTime">开始模拟的时间</param>
        /// <param name="endTime">截止日期</param>
        /// <param name="key">定义k值，2~20</param>
        /// <param name="seed">种子数，可以是一个对象的ID</param>
        /// <returns></returns>
        public static int MockInt(int realInt, DateTime beginTime, DateTime endTime, int key, int seed)
        {
            //固定随机数
            double randK = 0.314;
            int rand = (int)Math.Floor(((seed * randK) - Math.Floor(seed * randK)) * 10);
            rand = rand < 0 ? 3 : rand;

            TimeSpan tsDiffer = endTime.Date - beginTime.Date;
            int days = tsDiffer.Days < 0 ? 0 : tsDiffer.Days;

            //计算每天需要模拟的数
            int mockInt = rand * key;
            double seedK = 8.6;

            //今天的附加K值
            int todayK = (int)Math.Floor(endTime.Day * seedK);

            //计算总数的附加值
            int totalK = 0;
            DateTime totalDate = beginTime.AddDays(-1);
            while (totalDate.AddDays(1) <= endTime)
            {
                totalK += (int)Math.Floor(totalDate.Day * seedK);
                totalDate = totalDate.AddDays(1);
            }

            //计算当前阶段模拟展现的数
            int currentTimeInt = (int)Math.Floor((double)((mockInt + todayK) / 24) * DateTime.Now.Hour);
            int mockTodayInt = mockInt + todayK + realInt - currentTimeInt;

            //计算总模拟数
            int mockTotalInt = mockInt * days + realInt + totalK - mockTodayInt;

            return mockTotalInt;
        }
        #endregion

        #region HtmlToText(Html转换成Text)
        /// <summary>
        /// Html转换成Text
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string HtmlToText(string source)
        {
            string[] arrayRegex =
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
            string strOutput = source;
            for (int i = 0; i < arrayRegex.Length; i++)
            {
                Regex regex = new Regex(arrayRegex[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }
            strOutput.Replace("<", "").Replace(">", "").Replace("\r\n", "");
            return strOutput;
        }
        #endregion

        #region PinYin(获取拼音简码)
        /// <summary>
        /// 获取汉字的拼音简码，即首字母缩写，范例：中国，返回zg
        /// </summary>
        /// <param name="chineseText">汉字文本,范例： 中国</param>
        /// <returns>首字母缩写</returns>
        public static string PinYin(string chineseText)
        {
            if (string.IsNullOrWhiteSpace(chineseText))
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (char text in chineseText)
            {
                sb.AppendFormat("{0}", ResolvePinYin(text));
            }
            return sb.ToString().ToLower();
        }

        /// <summary>
        /// 解析单个汉字的拼音简码
        /// </summary>
        /// <param name="text">单个汉字</param>
        /// <returns>拼音简码</returns>
        private static string ResolvePinYin(char text)
        {
            byte[] charBytes = Encoding.Default.GetBytes(text.ToString());
            if (charBytes[0] < 127)
            {
                return text.ToString();
            }
            var unicode = (ushort)(charBytes[0] * 256 + charBytes[1]);
            string pinYin = ResolvePinYinByCode(unicode);
            if (!string.IsNullOrWhiteSpace(pinYin))
            {
                return pinYin;
            }
            return ResolvePinYinByFile(text.ToString());
        }

        /// <summary>
        /// 使用字符编码方式获取拼音简码
        /// </summary>
        /// <param name="unicode">字符编码</param>
        /// <returns>拼音简码</returns>
        private static string ResolvePinYinByCode(ushort unicode)
        {
            if (unicode >= '\uB0A1' && unicode <= '\uB0C4')
            {
                return "A";
            }
            if (unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464)
            {
                return "B";
            }
            if (unicode >= '\uB2C1' && unicode <= '\uB4ED')
            {
                return "C";
            }
            if (unicode >= '\uB4EE' && unicode <= '\uB6E9')
            {
                return "D";
            }
            if (unicode >= '\uB6EA' && unicode <= '\uB7A1')
            {
                return "E";
            }
            if (unicode >= '\uB7A2' && unicode <= '\uB8C0')
            {
                return "F";
            }
            if (unicode >= '\uB8C1' && unicode <= '\uB9FD')
            {
                return "G";
            }
            if (unicode >= '\uB9FE' && unicode <= '\uBBF6')
            {
                return "H";
            }
            if (unicode >= '\uBBF7' && unicode <= '\uBFA5')
            {
                return "J";
            }
            if (unicode >= '\uBFA6' && unicode <= '\uC0AB')
            {
                return "K";
            }
            if (unicode >= '\uC0AC' && unicode <= '\uC2E7')
            {
                return "L";
            }
            if (unicode >= '\uC2E8' && unicode <= '\uC4C2')
            {
                return "M";
            }
            if (unicode >= '\uC4C3' && unicode <= '\uC5B5')
            {
                return "N";
            }
            if (unicode >= '\uC5B6' && unicode <= '\uC5BD')
            {
                return "O";
            }
            if (unicode >= '\uC5BE' && unicode <= '\uC6D9')
            {
                return "P";
            }
            if (unicode >= '\uC6DA' && unicode <= '\uC8BA')
            {
                return "Q";
            }
            if (unicode >= '\uC8BB' && unicode <= '\uC8F5')
            {
                return "R";
            }
            if (unicode >= '\uC8F6' && unicode <= '\uCBF9')
            {
                return "S";
            }
            if (unicode >= '\uCBFA' && unicode <= '\uCDD9')
            {
                return "T";
            }
            if (unicode >= '\uCDDA' && unicode <= '\uCEF3')
            {
                return "W";
            }
            if (unicode >= '\uCEF4' && unicode <= '\uD188')
            {
                return "X";
            }
            if (unicode >= '\uD1B9' && unicode <= '\uD4D0')
            {
                return "Y";
            }
            if (unicode >= '\uD4D1' && unicode <= '\uD7F9')
            {
                return "Z";
            }
            return string.Empty;
        }

        /// <summary>
        /// 从拼音简码文件获取
        /// </summary>
        /// <param name="text">单个汉字</param>
        /// <returns>汉字首字母</returns>
        private static string ResolvePinYinByFile(string text)
        {
            int index = Const.ChinesePinYin.IndexOf(text, StringComparison.Ordinal);
            if (index < 0)
            {
                return string.Empty;
            }
            return Const.ChinesePinYin.Substring(index + 1, 1);
        }
        #endregion

        #region ConvertPinYin(获取汉字的全拼)
        /// <summary>
        /// 获取汉字的全拼，范例：中国，返回zhongguo
        /// </summary>
        /// <param name="text">汉字文本，范例：中国</param>
        /// <returns></returns>
        public static string ConvertPinYin(string text)
        {
            byte[] arr = new byte[2];
            string pyStr = "";
            int asc = 0, M1 = 0, M2 = 0;
            char[] mChar = text.ToCharArray();//获取汉字对应的字符数组
            for (int i = 0; i < mChar.Length; i++)
            {
                //如果输入的是汉字
                if (Valid.IsContainsChinese(mChar[i].ToString()))
                {
                    arr = Encoding.Default.GetBytes(mChar[i].ToString());
                    M1 = (short)(arr[0]);
                    M2 = (short)(arr[1]);
                    asc = M1 * 256 + M2 - 65536;
                    if (asc > 0 && asc < 160)
                    {
                        pyStr += mChar[i];
                    }
                    else
                    {
                        switch (asc)
                        {
                            case -9254:
                                pyStr += "Zhen"; break;
                            case -8985:
                                pyStr += "Qian"; break;
                            case -5463:
                                pyStr += "Jia"; break;
                            case -8274:
                                pyStr += "Ge"; break;
                            case -5448:
                                pyStr += "Ga"; break;
                            case -5447:
                                pyStr += "La"; break;
                            case -4649:
                                pyStr += "Chen"; break;
                            case -5436:
                                pyStr += "Mao"; break;
                            case -5213:
                                pyStr += "Mao"; break;
                            case -3597:
                                pyStr += "Die"; break;
                            case -5659:
                                pyStr += "Tian"; break;
                            default:
                                for (int j = (Const.SpellCode.Length - 1); j >= 0; j--)
                                {
                                    if (Const.SpellCode[j] <= asc)
                                    {
                                        //判断汉字的拼音区编码是否在指定范围内
                                        pyStr += Const.SpellLetter[j];//如果不超出范围则获取对应的拼音
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                }
                else
                {
                    //如果不是汉字则返回
                    pyStr += mChar[i].ToString();
                }
            }
            return pyStr;
        }
        #endregion

        #region Distinct(去除重复)
        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
        /// <returns></returns>
        public static string Distinct(string value)
        {
            char[] array = value.ToCharArray();
            return new string(array.Distinct().ToArray());
        }
        #endregion

        #region Truncate(截断字符串)
        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="length">返回长度</param>
        /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        /// <returns></returns>
        public static string Truncate(string text, int length, int endCharCount = 0, string endChar = ".")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            if (text.Length < length)
            {
                return text;
            }
            return text.Substring(0, length) + GetEndString(endCharCount, endChar);
        }

        /// <summary>
        /// 获取结束字符串
        /// </summary>
        /// <param name="endCharCount">结束符号的个数</param>
        /// <param name="endChar">结束符号</param>
        /// <returns></returns>
        private static string GetEndString(int endCharCount, string endChar)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < endCharCount; i++)
            {
                sb.Append(endChar);
            }
            return sb.ToString();
        }
        #endregion

        #region ToSimplifiedChinese(转换为简体中文)
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        /// <param name="text">繁体中文</param>
        /// <returns></returns>
        public static string ToSimplifiedChinese(string text)
        {
            return Microsoft.VisualBasic.Strings.StrConv(text, VbStrConv.SimplifiedChinese);
        }
        #endregion

        #region ToTraditionalChinese(转换为繁体中文)
        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        /// <param name="text">简体中文</param>
        /// <returns></returns>
        public static string ToTraditionalChinese(string text)
        {
            return Microsoft.VisualBasic.Strings.StrConv(text, VbStrConv.TraditionalChinese);
        }
        #endregion

        #region GetLastProperty(获取最后一个属性)
        /// <summary>
        /// 获取最后一个属性
        /// </summary>
        /// <param name="propertyName">属性名，范例，A.B.C,返回"C"</param>
        public static string GetLastProperty(string propertyName)
        {
            if (propertyName.IsEmpty())
                return string.Empty;
            var lastIndex = propertyName.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return propertyName.Substring(lastIndex);
        }
        #endregion

    }
}
