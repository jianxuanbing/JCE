/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webform.Js
 * 文件名：MessageBox
 * 版本号：v1.0.0.0
 * 唯一标识：a0eccd6e-651e-4727-8dff-f068c69a7155
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:30:22
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:30:22
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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JCE.Utils.Webform.Js
{
    /// <summary>
    /// 消息框
    /// </summary>
    public class MessageBox
    {
        /// <summary>
        /// 初始化一个<see cref="MessageBox"/>类型的实例
        /// </summary>
        private MessageBox() { }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", $"<script language='javascript' defer>alert('{msg}');</script>");
        }

        /// <summary>
        /// 显示消息确认提示框，通过控件点击显示
        /// </summary>
        /// <param name="control">控件，当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(WebControl control, string msg)
        {
            control.Attributes.Add("onclick", $"return confirm('{msg}');");
        }

        /// <summary>
        /// 显示消息提示对话框，并跳转到指定地址
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标Url</param>
        /// <param name="top">是否父级跳转,true:是,false:否</param>
        public static void ShowAndRedirect(Page page, string msg, string url, bool top = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript' defer>");
            sb.Append($"alert('{msg}');");
            sb.Append(top ? $"top.location.href='{url}'" : $"location.href='{url}'");
            sb.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sb.ToString());
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                $"<script language='javascript' defer>{script}</script>");
        }
    }
}
