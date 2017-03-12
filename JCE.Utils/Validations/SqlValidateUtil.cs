/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Validations
 * 文件名：SqlValidateUtil
 * 版本号：v1.0.0.0
 * 唯一标识：8b729f02-069f-4eca-ae44-166a09c57da6
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:26:54
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:26:54
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
using System.Web;

namespace JCE.Utils.Validations
{
    /// <summary>
    /// Sql注入检测工具类
    /// </summary>
    public class SqlValidateUtil
    {
        #region 关键字验证常量
        /// <summary>
        /// sql关键字
        /// </summary>
        private const string StrKeyWord =
            @"select|insert|delete|from|where|count|drop|drop table|update|truncate|asc|mid|char|xp_cmdshell|exec|exec master|netlocalgroup administrators|net user|or|and";
        /// <summary>
        /// sql特殊字符
        /// </summary>
        private const string StrRegex = @"";
        #endregion

        /// <summary>
        /// 检查Url参数中是否带有sql注入关键字，返回true无注入信息（即验证成功）
        /// </summary>
        /// <param name="request">当前HttpRequest对象</param>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckRequestQuery(HttpRequest request)
        {
            if (request.QueryString.Count > 0)
            {
                //若URL中参数存在，逐个比较参数
                for (int i = 0; i < request.QueryString.Count; i++)
                {
                    if (!CheckKeyWord(request.QueryString[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 检查提交表单中是否存在sql注入关键字，返回true无注入信息（即验证成功）
        /// </summary>
        /// <param name="request">当前HttpRequest对象</param>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckRequetForm(HttpRequest request)
        {
            if (request.Form.Count > 0)
            {
                //获取提交的表单项不为0，逐个比较参数
                for (int i = 0; i < request.Form.Count; i++)
                {
                    //检查参数值是否合法
                    if (request.Form.AllKeys[i].Trim().ToUpper().Equals("__VIEWSTATE") ||
                        request.Form.AllKeys[i].Trim().ToUpper().Equals("__EVENTVALIDATION"))
                    {
                        continue;
                    }
                    else
                    {
                        if (!CheckKeyWord(request.Form[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 检查keyword是否包含sql关键字，返回true无注入信息（即验证成功）
        /// </summary>
        /// <param name="keyword">被检查的字符串</param>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckKeyWord(string keyword)
        {
            if (keyword != null && !keyword.Trim().Equals(string.Empty))
            {
                if (Regex.IsMatch(keyword, StrKeyWord, RegexOptions.IgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查keywords是否包含sql关键字，返回true无注入信息（即验证成功）
        /// </summary>
        /// <param name="keywords">被检查的字符串</param>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckKeyWordForParams(params string[] keywords)
        {
            if (keywords.Length > 0)
            {
                //若Url中参数存在，逐个比较参数
                for (int i = 0; i < keywords.Length; i++)
                {
                    if (!CheckKeyWord(keywords[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 检查字典是否包含sql关键字，返回true无注入信息（即验证成功）
        /// </summary>
        /// <param name="dic">被检查的字典</param>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckKeyWordForDictionary(IDictionary<string, object> dic)
        {
            if (dic != null)
            {
                foreach (object value in dic.Values)
                {
                    if (value != null)
                    {
                        if (!CheckKeyWord(value.ToString()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 检查HttpRequest是否存在sql注入，返回true无注入信息（即验证成功）
        /// </summary>
        /// <returns>true:不存在sql关键字,false:存在sql关键字</returns>
        public static bool CheckMessage()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (CheckRequestQuery(request) && CheckRequetForm(request))
            {
                return true;
            }
            return false;
        }
    }
}
