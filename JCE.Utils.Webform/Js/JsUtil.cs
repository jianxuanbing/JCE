/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webform.Js
 * 文件名：JsUtil
 * 版本号：v1.0.0.0
 * 唯一标识：ebe034f4-6908-4dae-8b22-650bc70b8381
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:29:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:29:43
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JCE.Utils.Webform.Js
{
    /// <summary>
    /// 客户端脚本输出工具类
    /// </summary>
    public class JsUtil
    {
        /// <summary>
        /// 弹出信息，并跳转指定Url
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="toUrl">跳转Url</param>
        public static void AlertAndRedirect(string message, string toUrl)
        {
            HttpContext.Current.Response.Write(
                $@"<script language='javascript'>alert('{message}');window.location.replace('{toUrl}')</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息，并回退历史页面
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="value">回退历史页面数</param>
        public static void AlertAndGoHistory(string message, int value)
        {
            HttpContext.Current.Response.Write(
                $@"<script language='javaScript'>alert('{message}');history.go({value});</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 直接跳转到指定Url
        /// </summary>
        /// <param name="toUrl">跳转Url</param>
        public static void Redirect(string toUrl)
        {
            HttpContext.Current.Response.Write(
                $@"<script language='javascript'>window.location.replace('{toUrl}')</script>");
        }

        /// <summary>
        /// 弹出信息，并指定父窗口跳转指定Url
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="toUrl">跳转Url</param>
        public static void AlertAndParentUrl(string message, string toUrl)
        {
            HttpContext.Current.Response.Write(
                $@"<script language='javascript'>alert('{message}');window.top.location.replace('{toUrl}')</script>");
        }

        /// <summary>
        /// 指定父窗口直接跳转到指定Url
        /// </summary>
        /// <param name="toUrl">跳转Url</param>
        public static void ParentRedirect(string toUrl)
        {
            HttpContext.Current.Response.Write(
                $@"<script language='javascript'>window.top.location.replace('{toUrl}')</script>");
        }

        /// <summary>
        /// 回退历史页面
        /// </summary>
        /// <param name="value">回退历史页面数</param>
        public static void BackHistory(int value)
        {
            HttpContext.Current.Response.Write($@"<script language='javaScript'>history.go({value});</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息
        /// </summary>
        /// <param name="message">提示消息</param>
        public static void Alert(string message)
        {
            HttpContext.Current.Response.Write($@"<script language='javascript'>alert('{message}');</script>");
        }

        /// <summary>
        /// 注册Js脚本块
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void RegisterScriptBlock(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock",
                $"<script type='text/javascript'>{script}</script>");
        }

        /// <summary>
        /// 获取当前页对象实例
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }

        /// <summary>
        /// 写入Js脚本
        /// </summary>
        /// <param name="script">脚本内容</param>
        public static void WriteScript(string script)
        {
            Page page = GetCurrentPage();
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// 显示客户端确认窗口
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="eventName">事件名</param>
        /// <param name="title">标题</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="message">消息</param>
        public static void ShowClientConfirm(WebControl control, string eventName, string title, int width, int height,
            string message)
        {
            control.Attributes[eventName] =
                $"return showConfirm('{title}',{width},{height},'{message}','{control.ClientID}')";
        }

        /// <summary>
        /// 显示客户端确认窗口
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="eventName">事件名</param>
        /// <param name="message">消息</param>
        public static void ShowClientConfirm(WebControl control, string eventName, string message)
        {
            ShowClientConfirm(control, eventName, "系统提示", 210, 125, message);
        }
    }
}
