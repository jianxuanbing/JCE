/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Formatters
 * 文件名：JsonFormatter
 * 版本号：v1.0.0.0
 * 唯一标识：a1b78e1c-7a04-46b5-9f0c-e02883d6f007
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/8/11 0:41:33
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/8/11 0:41:33
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

namespace JCE.Utils.Formatters
{
    /// <summary>
    /// Json格式化
    /// </summary>
    public class JsonFormatter
    {
        public static string Indent = "    ";

        public static string PrettyPrint(string input)
        {
            var output=new StringBuilder(input.Length*2);
            char? quote = null;
            int depth = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                char ch = input[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        output.Append(ch);
                        if (!quote.HasValue)
                        {
                            output.AppendLine();
                            output.Append(Indent.Repeat(++depth));
                        }
                        break;
                    case '}':
                    case ']':
                        if (quote.HasValue)
                        {
                            output.Append(ch);
                        }
                        else
                        {
                            output.AppendLine();
                            output.Append(Indent.Repeat(--depth));
                            output.Append(ch);
                        }
                        break;
                    case '"':
                    case '\'':
                        output.Append(ch);
                        if (quote.HasValue)
                        {
                            if (!output.IsEscaped(i))
                            {
                                quote = null;
                            }
                        }
                        else
                        {
                            quote = ch;
                        }
                        break;
                    case ',':
                        output.Append(ch);
                        if (!quote.HasValue)
                        {
                            output.AppendLine();
                            output.Append(Indent.Repeat(depth));
                        }
                        break;
                    case ':':
                        if (quote.HasValue)
                        {
                            output.Append(ch);
                        }
                        else
                        {
                            output.Append(" : ");
                        }
                        break;
                    default:
                        if (quote.HasValue || !char.IsWhiteSpace(ch))
                        {
                            output.Append(ch);
                        }
                        break;
                }                
            }
            return output.ToString();
        }
    }

    static class Extensions
    {
        public static string Repeat(this string str, int count)
        {
            return new StringBuilder().Insert(0,str,count).ToString();
        }

        public static bool IsEscaped(this string str, int index)
        {
            bool escaped = false;
            while (index>0&&str[--index]=='\\')
            {
                escaped = !escaped;
            }
            return escaped;
        }

        public static bool IsEscaped(this StringBuilder str, int index)
        {
            return str.ToString().IsEscaped(index);
        }
    }
}
