/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：WcfUtil
 * 版本号：v1.0.0.0
 * 唯一标识：3af423e9-657d-44ad-bf9b-df49d4d6676c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 22:30:52
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 22:30:52
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils
{
    /// <summary>
    /// Wcf工具类
    /// </summary>
    public static class WcfUtil
    {
        #region CreateProxy(创建客户端代理对象)
        /// <summary>
        /// 创建客户端代理对象
        /// </summary>
        /// <typeparam name="T">操作契约类型,范例: IService</typeparam>
        /// <param name="endpointConfigName">配置文件中客户端终结点的名称</param>
        /// <returns></returns>
        public static T CreateProxy<T>(string endpointConfigName) where T : class
        {
            return CreateProxy<T>(endpointConfigName, string.Empty, string.Empty);
        }
        /// <summary>
        /// 创建客户端代理对象
        /// </summary>
        /// <typeparam name="T">操作契约类型,范例: IService</typeparam>
        /// <param name="endpointConfigName">配置文件中客户端终结点的名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static T CreateProxy<T>(string endpointConfigName, string userName, string password) where T : class
        {
            var factory = new ChannelFactory<T>(endpointConfigName);
            if (factory.Credentials == null || string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return factory.CreateChannel();
            }
            factory.Credentials.UserName.UserName = userName;
            factory.Credentials.UserName.Password = password;
            return factory.CreateChannel();
        }
        #endregion

        #region CloseProxy(关闭客户端代理对象)
        /// <summary>
        /// 关闭客户端代理对象
        /// </summary>
        /// <param name="proxy">客户端代理对象</param>
        public static void CloseProxy(object proxy)
        {
            var communicationObject = proxy as ICommunicationObject;
            if (communicationObject == null)
            {
                return;
            }
            try
            {
                if (communicationObject.State == CommunicationState.Faulted)
                {
                    return;
                }
                communicationObject.Close();
            }
            catch
            {
                communicationObject.Abort();
                throw;
            }
        }
        #endregion
    }
}
