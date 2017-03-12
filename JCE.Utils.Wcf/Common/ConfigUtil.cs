/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Common
 * 文件名：ConfigUtil
 * 版本号：v1.0.0.0
 * 唯一标识：e251382b-8f74-4fed-89fb-21a82b10272a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/10 23:44:06
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/10 23:44:06
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Common
{
    /// <summary>
    /// WCF客户端配置信息工具类
    /// </summary>
    public static partial class ConfigUtil
    {
        #region Fields(字段)
        /// <summary>
        /// 配置文件对象
        /// </summary>
        private static Configuration config = null;
        #endregion

        #region GetBinding(获取绑定信息)
        /// <summary>
        /// 获取绑定信息
        /// </summary>
        /// <param name="bindingName">绑定名</param>
        /// <param name="configName">配置名</param>
        /// <returns></returns>
        public static Binding GetBinding(string bindingName, string configName)
        {
            Binding binding = null;
            IBindingConfigurationElement bindingConfigurationElement = null;
            var bindingsSection = GetBindingsSection(configName);

            if (bindingsSection == null)
            {
                return null;
            }

            foreach (BindingCollectionElement element in bindingsSection.BindingCollections.Where(x => x.ConfiguredBindings.Count > 0))
            {
                bindingConfigurationElement = element.ConfiguredBindings.FirstOrDefault(x => x.Name == bindingName);
                if (bindingConfigurationElement != null)
                {
                    binding = Activator.CreateInstance(element.BindingType) as Binding;
                    binding.Name = bindingName;
                    bindingConfigurationElement.ApplyConfiguration(binding);
                    break;
                }
            }
            return binding;
        }
        #endregion

        #region GetEndpointAddress(获取终端地址)
        /// <summary>
        /// 获取终端地址
        /// </summary>
        /// <param name="endpointName">终端名</param>
        /// <param name="configName">配置名</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static EndpointAddress GetEndpointAddress(string endpointName, string configName, string url = "")
        {
            EndpointAddress endpointAddress = null;
            EndpointIdentity endpointIdentity = null;
            var clientSection = GetClientSection(configName);

            if (clientSection == null)
            {
                return null;
            }

            ChannelEndpointElement channelEndpoint =
                clientSection.Endpoints.OfType<ChannelEndpointElement>().FirstOrDefault(x => x.Name == endpointName);

            if (channelEndpoint != null && channelEndpoint.Identity != null)
            {
                var identity = channelEndpoint.Identity;

                if (!string.IsNullOrEmpty(identity.Dns.Value))
                {
                    endpointIdentity = EndpointIdentity.CreateDnsIdentity(identity.Dns.Value);
                }
                else if (!string.IsNullOrEmpty(identity.ServicePrincipalName.Value))
                {
                    endpointIdentity = EndpointIdentity.CreateSpnIdentity(identity.ServicePrincipalName.Value);
                }
                else if (!string.IsNullOrEmpty(identity.UserPrincipalName.Value))
                {
                    endpointIdentity = EndpointIdentity.CreateUpnIdentity(identity.UserPrincipalName.Value);
                }
                else if (!string.IsNullOrEmpty(identity.Rsa.Value))
                {
                    endpointIdentity = EndpointIdentity.CreateRsaIdentity(identity.Rsa.Value);
                }

                if (string.IsNullOrEmpty(url))
                {
                    endpointAddress = new EndpointAddress(channelEndpoint.Address, endpointIdentity);
                }
                else
                {
                    endpointAddress = new EndpointAddress(new Uri(url), endpointIdentity);
                }
            }
            return endpointAddress;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 获取客户端节
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns></returns>
        private static ClientSection GetClientSection(string configName)
        {
            ClientSection clientSection = null;
            SetConfigFile(configName);
            if (config != null)
            {
                clientSection = config.GetSection("system.serviceModel/client") as ClientSection;
            }
            return clientSection;
        }

        /// <summary>
        /// 获取绑定信息节点
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <returns></returns>
        private static BindingsSection GetBindingsSection(string configName)
        {
            BindingsSection bindingsSection = null;
            SetConfigFile(configName);
            if (config != null)
            {
                bindingsSection = config.GetSection("system.serviceModel/bindings") as BindingsSection;
            }
            return bindingsSection;
        }

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="configName">配置名</param>
        private static void SetConfigFile(string configName)
        {
            if (config == null)
            {
                var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, configName,
                    SearchOption.AllDirectories);
                if (files.Length == 0)
                {
                    return;
                }
                var configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = files[0]
                };
                config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }
        }
        #endregion

    }
}
