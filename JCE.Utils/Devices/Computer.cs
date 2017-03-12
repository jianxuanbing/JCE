/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Devices
 * 文件名：Computer
 * 版本号：v1.0.0.0
 * 唯一标识：b6f96338-e36d-47cc-8334-566e94f7d680
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:18:28
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:18:28
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Devices
{
    /// <summary>
    /// 电脑信息
    /// </summary>
    public class Computer
    {
        #region Proeprty(属性)
        /// <summary>
        /// Cpu代码
        /// </summary>
        public string CpuCode { get; }
        /// <summary>
        /// Cpu个数
        /// </summary>
        public int CpuCount { get; }
        /// <summary>
        /// Cpu频率，单位:HZ
        /// </summary>
        public string[] CpuMhz { get; }
        /// <summary>
        /// Mac地址
        /// </summary>
        public string Mac { get; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; }
        /// <summary>
        /// 操作系统登录用户名
        /// </summary>
        public string LoginUserName { get; }
        /// <summary>
        /// 计算机名
        /// </summary>
        public string ComputerName { get; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; }
        /// <summary>
        /// 总物理内存，单位：M
        /// </summary>
        public string TotalPhysicalMemory { get; }
        /// <summary>
        /// 硬盘信息
        /// </summary>
        public List<DiskInfo> DiskInfo { get; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化电脑信息
        /// </summary>
        public Computer()
        {
            CpuCode = GetCpuCode();
            CpuCount = 0;
            CpuMhz = GetCpuMhz();
            Mac = GetMacAddress();
            Ip = GetIpAddress();
            LoginUserName = GetUserName();
            ComputerName = GetComputerName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            DiskInfo = GetDiskInfos();
        }
        #endregion

        #region Instance(获取电脑信息实例)
        /// <summary>
        /// 实例
        /// </summary>
        private static Computer _instance;
        /// <summary>
        /// 获取电脑信息实例
        /// </summary>
        /// <returns></returns>
        public static Computer Instance()
        {
            if (_instance == null)
            {
                _instance = new Computer();
            }
            return _instance;
        }
        #endregion

        #region GetUserName(获取操作系统的登录用户名)
        /// <summary>
        /// 获取操作系统的登录用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            return Environment.UserName;
        }
        #endregion

        #region GetTotalPhysicalMemory(获取物理内存大小)
        /// <summary>
        /// 获取物理内存大小
        /// </summary>
        /// <returns></returns>
        public static string GetTotalPhysicalMemory()
        {
            string result = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_ComputerSystem"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    result = mo.Properties["TotalPhysicalMemory"].Value.ToString();
                }
            }
            return result;
        }
        #endregion

        #region GetSystemType(获取系统类型)
        /// <summary>
        /// 获取系统类型
        /// </summary>
        /// <returns></returns>
        public static string GetSystemType()
        {
            string result = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_ComputerSystem"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    result = mo.Properties["SystemType"].Value.ToString();
                }
            }
            return result;
        }
        #endregion

        #region GetComputerName(获取计算机名)
        /// <summary>
        /// 获取计算机名
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }
        #endregion

        #region GetMacAddress(获取网卡Mac地址)
        /// <summary>
        /// 获取网卡Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string mac = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo.Properties["IPEnabled"].Value)
                    {
                        mac = mo.Properties["MacAddress"].Value.ToString();
                        break;
                    }
                }
            }
            return mac;
        }
        #endregion

        #region GetIpAddress(获取网卡Ip地址)
        /// <summary>
        /// 获取网卡IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress()
        {
            string ip = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo.Properties["IPEnabled"].Value == true)
                    {
                        string[] array = (string[])mo.Properties["IpAddress"].Value;
                        ip = array.GetValue(0).ToString();
                        break;
                    }
                }
            }
            return ip;
        }
        #endregion

        #region GetCpuCode(获取CPU序列化代码)
        /// <summary>
        /// 获取CPU序列化代码
        /// </summary>
        /// <returns></returns>
        public static string GetCpuCode()
        {
            string result = string.Empty;
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    result = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return result;
        }
        #endregion

        #region GetCpuMhz(获取CPU频率)
        /// <summary>
        /// 获取CPU频率
        /// </summary>
        /// <returns></returns>
        public static string[] GetCpuMhz()
        {
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                string[] mHz = new string[moc.Count];
                int c = 0;
                using (ManagementObjectSearcher search = new ManagementObjectSearcher("select * from Win32_Processor"))
                {
                    foreach (ManagementObject mo in search.Get())
                    {
                        mHz[c] = mo.Properties["CurrentClockSpeed"].Value.ToString();
                        c++;
                    }
                }
                return mHz;
            }
        }
        #endregion

        #region GetDiskInfos(获取硬盘信息)
        /// <summary>
        /// 获取硬盘信息集合
        /// </summary>
        /// <returns></returns>
        public static List<DiskInfo> GetDiskInfos()
        {
            List<DiskInfo> list = new List<DiskInfo>();
            using (ManagementClass mc = new ManagementClass("Win32_DiskDrive"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    list.Add(new DiskInfo(mo.Properties["Model"].Value.ToString(),
                        mo.Properties["Size"].Value.ToString().ToLong()));
                }
            }
            return list;
        }
        #endregion

        #region GetCpuUsage(Cpu使用率类)
        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <returns></returns>
        public static int GetCpuUsage()
        {
            return CpuUsage.Create().Query();
        }
        #endregion
    }
}
