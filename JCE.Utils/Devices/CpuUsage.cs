/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Devices
 * 文件名：CpuUsage
 * 版本号：v1.0.0.0
 * 唯一标识：0b78818f-8829-4a5e-b6d6-3549b71a4670
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:17:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:17:47
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace JCE.Utils.Devices
{
    /// <summary>
    /// Cpu使用率，定义一个实现CPU使用计数器的抽象基类
    /// </summary>
    public abstract class CpuUsage
    {
        /// <summary>
        /// Cpu使用率实例
        /// </summary>
        private static CpuUsage _cpuUsage = null;

        /// <summary>
        /// 创建并返回一个Cpu使用率实例，可用于查询操作系统上的CPU时间
        /// </summary>
        /// <returns></returns>
        public static CpuUsage Create()
        {
            if (_cpuUsage == null)
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    _cpuUsage = new CpuUsageNt();
                }
                else if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
                {
                    _cpuUsage = new CpuUsage9x();
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            return _cpuUsage;
        }
        /// <summary>
        /// 查询当前的平均CPU负载(CPU使用率)
        /// </summary>
        /// <returns>CPU负载百分比</returns>
        /// <exception cref="NotSupportedException">系统调用失败，CPU时间不能获得</exception>
        public abstract int Query();
    }
    /// <summary>
    /// Inherits the CPUUsage class and implements the Query method for Windows 9x systems.
    /// </summary>
    /// <remarks>
    /// <p>This class works on Windows 98 and Windows Millenium Edition.</p>
    /// <p>You should not use this class directly in your code. Use the CPUUsage.Create() method to instantiate a CPUUsage object.</p>
    /// </remarks>
    internal sealed class CpuUsage9x : CpuUsage
    {
        /// <summary>
        /// Initializes a new CPUUsage9x instance.
        /// </summary>
        /// <exception cref="NotSupportedException">One of the system calls fails.</exception>
        public CpuUsage9x()
        {
            try
            {
                // start the counter by reading the value of the 'StartStat' key
                RegistryKey startKey = Registry.DynData.OpenSubKey(@"PerfStats\StartStat", false);
                if (startKey == null)
                    throw new NotSupportedException();
                startKey.GetValue(@"KERNEL\CPUUsage");
                startKey.Close();
                // open the counter's value key
                m_StatData = Registry.DynData.OpenSubKey(@"PerfStats\StatData", false);
                if (m_StatData == null)
                    throw new NotSupportedException();
            }
            catch (NotSupportedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new NotSupportedException("Error while querying the system information.", e);
            }
        }

        /// <summary>
        /// Determines the current average CPU load.
        /// </summary>
        /// <returns>An integer that holds the CPU load percentage.</returns>
        /// <exception cref="NotSupportedException">One of the system calls fails. The CPU time can not be obtained.</exception>
        public override int Query()
        {
            try
            {
                return (int)m_StatData.GetValue(@"KERNEL\CPUUsage");
            }
            catch (Exception e)
            {
                throw new NotSupportedException("Error while querying the system information.", e);
            }
        }

        /// <summary>
        /// Closes the allocated resources.
        /// </summary>
        ~CpuUsage9x()
        {
            try
            {
                m_StatData.Close();
            }
            catch { }
            // stopping the counter
            try
            {
                RegistryKey stopKey = Registry.DynData.OpenSubKey(@"PerfStats\StopStat", false);
                stopKey.GetValue(@"KERNEL\CPUUsage", false);
                stopKey.Close();
            }
            catch { }
        }

        /// <summary>Holds the registry key that's used to read the CPU load.</summary>
        private RegistryKey m_StatData;
    }
    /// <summary>
    /// 实现Window NT系统的查询方法
    /// </summary>
    /// <remarks>
    /// <p>This class works on Windows NT4, Windows 2000, Windows XP, Windows .NET Server and higher.</p>
    /// <p>You should not use this class directly in your code. Use the CPUUsage.Create() method to instantiate a CPUUsage object.</p>
    /// </remarks>
    internal sealed class CpuUsageNt : CpuUsage
    {
        /// <summary>
        /// Initializes a new CpuUsageNt instance.
        /// </summary>
        /// <exception cref="NotSupportedException">One of the system calls fails.</exception>
        public CpuUsageNt()
        {
            byte[] timeInfo = new byte[32];         // SYSTEM_TIME_INFORMATION structure
            byte[] perfInfo = new byte[312];        // SYSTEM_PERFORMANCE_INFORMATION structure
            byte[] baseInfo = new byte[44];         // SYSTEM_BASIC_INFORMATION structure
            int ret;
            // get new system time
            ret = NtQuerySystemInformation(SYSTEM_TIMEINFORMATION, timeInfo, timeInfo.Length, IntPtr.Zero);
            if (ret != NO_ERROR)
                throw new NotSupportedException();
            // get new CPU's idle time
            ret = NtQuerySystemInformation(SYSTEM_PERFORMANCEINFORMATION, perfInfo, perfInfo.Length, IntPtr.Zero);
            if (ret != NO_ERROR)
                throw new NotSupportedException();
            // get number of processors in the system
            ret = NtQuerySystemInformation(SYSTEM_BASICINFORMATION, baseInfo, baseInfo.Length, IntPtr.Zero);
            if (ret != NO_ERROR)
                throw new NotSupportedException();
            // store new CPU's idle and system time and number of processors
            oldIdleTime = BitConverter.ToInt64(perfInfo, 0); // SYSTEM_PERFORMANCE_INFORMATION.liIdleTime
            oldSystemTime = BitConverter.ToInt64(timeInfo, 8); // SYSTEM_TIME_INFORMATION.liKeSystemTime
            processorCount = baseInfo[40];
        }
        /// <summary>
        /// Determines the current average CPU load.
        /// </summary>
        /// <returns>An integer that holds the CPU load percentage.</returns>
        /// <exception cref="NotSupportedException">One of the system calls fails. The CPU time can not be obtained.</exception>
        public override int Query()
        {
            byte[] timeInfo = new byte[32];         // SYSTEM_TIME_INFORMATION structure
            byte[] perfInfo = new byte[312];        // SYSTEM_PERFORMANCE_INFORMATION structure
            double dbIdleTime, dbSystemTime;
            int ret;
            // get new system time
            ret = NtQuerySystemInformation(SYSTEM_TIMEINFORMATION, timeInfo, timeInfo.Length, IntPtr.Zero);
            if (ret != NO_ERROR)
                throw new NotSupportedException();
            // get new CPU's idle time
            ret = NtQuerySystemInformation(SYSTEM_PERFORMANCEINFORMATION, perfInfo, perfInfo.Length, IntPtr.Zero);
            if (ret != NO_ERROR)
                throw new NotSupportedException();
            // CurrentValue = NewValue - OldValue
            dbIdleTime = BitConverter.ToInt64(perfInfo, 0) - oldIdleTime;
            dbSystemTime = BitConverter.ToInt64(timeInfo, 8) - oldSystemTime;
            // CurrentCpuIdle = IdleTime / SystemTime
            if (dbSystemTime != 0)
                dbIdleTime = dbIdleTime / dbSystemTime;
            // CurrentCpuUsage% = 100 - (CurrentCpuIdle * 100) / NumberOfProcessors
            dbIdleTime = 100.0 - dbIdleTime * 100.0 / processorCount + 0.5;
            // store new CPU's idle and system time
            oldIdleTime = BitConverter.ToInt64(perfInfo, 0); // SYSTEM_PERFORMANCE_INFORMATION.liIdleTime
            oldSystemTime = BitConverter.ToInt64(timeInfo, 8); // SYSTEM_TIME_INFORMATION.liKeSystemTime
            return (int)dbIdleTime;
        }
        /// <summary>
        /// NtQuerySystemInformation is an internal Windows function that retrieves various kinds of system information.
        /// </summary>
        /// <param name="dwInfoType">One of the values enumerated in SYSTEM_INFORMATION_CLASS, indicating the kind of system information to be retrieved.</param>
        /// <param name="lpStructure">Points to a buffer where the requested information is to be returned. The size and structure of this information varies depending on the value of the SystemInformationClass parameter.</param>
        /// <param name="dwSize">Length of the buffer pointed to by the SystemInformation parameter.</param>
        /// <param name="returnLength">Optional pointer to a location where the function writes the actual size of the information requested.</param>
        /// <returns>Returns a success NTSTATUS if successful, and an NTSTATUS error code otherwise.</returns>
        [DllImport("ntdll", EntryPoint = "NtQuerySystemInformation")]
        private static extern int NtQuerySystemInformation(int dwInfoType, byte[] lpStructure, int dwSize, IntPtr returnLength);

        /// <summary>Returns the number of processors in the system in a SYSTEM_BASIC_INFORMATION structure.</summary>
        private const int SYSTEM_BASICINFORMATION = 0;

        /// <summary>Returns an opaque SYSTEM_PERFORMANCE_INFORMATION structure.</summary>
        private const int SYSTEM_PERFORMANCEINFORMATION = 2;

        /// <summary>Returns an opaque SYSTEM_TIMEOFDAY_INFORMATION structure.</summary>
        private const int SYSTEM_TIMEINFORMATION = 3;

        /// <summary>The value returned by NtQuerySystemInformation is no error occurred.</summary>
        private const int NO_ERROR = 0;

        /// <summary>Holds the old idle time.</summary>
        private long oldIdleTime;

        /// <summary>Holds the old system time.</summary>
        private long oldSystemTime;

        /// <summary>Holds the number of processors in the system.</summary>
        private double processorCount;
    }
}
