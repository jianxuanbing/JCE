/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Windows
 * 文件名：SystemInfoHandler
 * 版本号：v1.0.0.0
 * 唯一标识：3f06f72e-a94f-480b-b0bc-969e9c6729cb
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:09:22
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:09:22
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

namespace JCE.Utils.Windows
{
    /// <summary>
    /// 获取系统硬件、软件信息
    /// </summary>
    public static class SystemInfoHandler
    {
        /// <summary>
        /// 获取指定WMI路径的信息集合
        /// </summary>
        /// <param name="wmi">WMI地址</param>
        /// <returns></returns>
        public static IEnumerable<Dictionary<string, object>> GetWmiInfos(WMIPath wmi)
        {
            ManagementClass mc = new ManagementClass(wmi.ToString());
            ManagementObjectCollection moc = mc.GetInstances();
            return (from ManagementObject mo in moc
                    select
                        mo.Properties.Cast<PropertyData>()
                            .ToDictionary(property => property.Name, property => property.Value)).ToList();
        }

        /// <summary>
        /// 获取硬件信息标识
        /// </summary>
        /// <returns></returns>
        public static string GetHardwareId()
        {
            var cpuId = new ManagementClass(WMIPath.Win32_Processor.ToString()).GetInstances()
                .Cast<ManagementObject>().Select(cpu => cpu.Properties["ProcessorId"].Value).First();
            var boardId = new ManagementClass(WMIPath.Win32_BaseBoard.ToString()).GetInstances()
                .Cast<ManagementObject>().Select(disk => disk.Properties["SerialNumber"].Value).First();
            var diskId = new ManagementClass(WMIPath.Win32_DiskDrive.ToString()).GetInstances()
                .Cast<ManagementObject>().Select(disk => disk.Properties["SerialNumber"].Value).First();
            string value = string.Format("{0}{1}{2}", cpuId, boardId, diskId);
            return GetHardwareId(value);
        }

        /// <summary>
        /// 由指定字符串生成硬件信息标识
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static string GetHardwareId(string value)
        {
            return "";
        }

        /// <summary>
        /// 获取操作系统名称
        /// </summary>
        public static string GetOperationSystemName()
        {
            return new ManagementClass(WMIPath.Win32_OperatingSystem.ToString()).GetInstances()
                .Cast<ManagementObject>().Select(op => op.Properties["Name"].Value).First().ToString();
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public static SystemInfo GetSystemInfo()
        {
            SystemInfo info = new SystemInfo();
            //CPU
            var objects = new ManagementClass(WMIPath.Win32_Processor.ToString()).GetInstances().Cast<ManagementObject>().ToArray();
            info.CpuName = objects.Select(m => (string)m.Properties["Name"].Value).FirstOrDefault();
            info.CpuId = objects.Select(m => (string)m.Properties["ProcessorId"].Value).FirstOrDefault();

            //主板
            objects = new ManagementClass(WMIPath.Win32_BaseBoard.ToString()).GetInstances().Cast<ManagementObject>().ToArray();
            info.BoardName = objects.Select(m => (string)m.Properties["Manufacturer"].Value + " " +
                                                 (string)m.Properties["Product"].Value + " " +
                                                 (string)m.Properties["Version"].Value).FirstOrDefault();
            info.BoardId = objects.Select(m => (string)m.Properties["SerialNumber"].Value).FirstOrDefault();

            //硬盘
            objects = new ManagementClass(WMIPath.Win32_DiskDrive.ToString()).GetInstances().Cast<ManagementObject>().ToArray();
            info.DiskName = objects.Select(m => (string)m.Properties["Model"].Value + " " +
                                                (Convert.ToDouble(m.Properties["Size"].Value) / (1024 * 1024 * 1024)) + " GB").FirstOrDefault();
            info.DiskId = objects.Select(m => (string)m.Properties["SerialNumber"].Value).FirstOrDefault();

            //操作系统
            objects = new ManagementClass(WMIPath.Win32_OperatingSystem.ToString()).GetInstances().Cast<ManagementObject>().ToArray();
            info.OSName = objects.Select(m => (string)m.Properties["Caption"].Value).FirstOrDefault();
            info.OSCsdVersion = objects.Select(m => (string)m.Properties["CSDVersion"].Value).FirstOrDefault();
            info.OSIs64Bit = objects.Select(m => (string)m.Properties["OSArchitecture"].Value == "64-bit").FirstOrDefault();
            info.OSVersion = objects.Select(m => (string)m.Properties["Version"].Value).FirstOrDefault();
            info.OSPath = objects.Select(m => (string)m.Properties["WindowsDirectory"].Value).FirstOrDefault();

            //内存
            info.PhysicalMemoryFree = objects.Select(m => Convert.ToDouble(m.Properties["FreePhysicalMemory"].Value) / (1024)).FirstOrDefault();
            info.PhysicalMemoryTotal = objects.Select(m => Convert.ToDouble(m.Properties["TotalVisibleMemorySize"].Value) / (1024)).FirstOrDefault();

            //屏幕
            objects = new ManagementClass(WMIPath.Win32_VideoController.ToString()).GetInstances().Cast<ManagementObject>().ToArray();
            info.ScreenWith = objects.Select(m => Convert.ToInt32(m.Properties["CurrentHorizontalResolution"].Value)).FirstOrDefault();
            info.ScreenHeight = objects.Select(m => Convert.ToInt32(m.Properties["CurrentVerticalResolution"].Value)).FirstOrDefault();
            info.ScreenColorDepth = objects.Select(m => Convert.ToInt32(m.Properties["CurrentBitsPerPixel"].Value)).FirstOrDefault();
            return info;
        }

        /// <summary>
        /// 获取当前系统运行的进程列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetProcessNames()
        {
            var objects =
                new ManagementClass(WMIPath.Win32_Process.ToString()).GetInstances()
                    .Cast<ManagementObject>()
                    .ToArray();
            return objects.Select(m => (string)m.Properties["Caption"].Value).OrderBy(m => m);
        }

        /// <summary>
        /// 获取当前系统正在运行的服务列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetStartedServiceNamesEnumerable()
        {
            var objects =
                new ManagementClass(WMIPath.Win32_Service.ToString()).GetInstances()
                    .Cast<ManagementObject>()
                    .ToArray();
            return
                objects.Where(m => (bool)m.Properties["Started"].Value)
                    .Select(m => (string)m.Properties["Caption"].Value)
                    .OrderBy(m => m);
        }

        /// <summary>
        /// 获取剩余空间最大的逻辑磁盘名称
        /// </summary>
        /// <returns></returns>
        public static string GetMaxFreeSizeLogicalDisk()
        {
            var objects =
                new ManagementClass(WMIPath.Win32_LogicalDisk.ToString()).GetInstances()
                    .Cast<ManagementObject>()
                    .ToArray();
            return
                objects.OrderByDescending(m => Convert.ToInt64(m.Properties["FreeSpace"].Value))
                    .Select(m => (string)m.Properties["Caption"].Value)
                    .FirstOrDefault();
        }
    }
}
