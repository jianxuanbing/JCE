/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Windows
 * 文件名：SystemInfo
 * 版本号：v1.0.0.0
 * 唯一标识：9c1908ee-0911-4c21-a902-c123642b5154
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:08:48
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:08:48
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

namespace JCE.Utils.Windows
{
    /// <summary>
    /// 系统信息类
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 获取或设置 CPU型号
        /// </summary>
        public string CpuName { get; set; }

        /// <summary>
        /// 获取或设置 CPU编号
        /// </summary>
        public string CpuId { get; set; }

        /// <summary>
        /// 获取或设置 主板型号
        /// </summary>
        public string BoardName { get; set; }

        /// <summary>
        /// 获取或设置 主板编号
        /// </summary>
        public string BoardId { get; set; }

        /// <summary>
        /// 获取或设置 硬盘型号
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// 获取或设置 硬盘编号
        /// </summary>
        public string DiskId { get; set; }

        /// <summary>
        /// 获取或设置 操作系统名称
        /// </summary>
        public string OSName { get; set; }

        /// <summary>
        /// 获取或设置 操作系统补丁版本
        /// </summary>
        public string OSCsdVersion { get; set; }

        /// <summary>
        /// 获取或设置 是否64位操作系统
        /// </summary>
        public bool OSIs64Bit { get; set; }

        /// <summary>
        /// 获取或设置 操作系统版本
        /// </summary>
        public string OSVersion { get; set; }

        /// <summary>
        /// 获取或设置 操作系统路径
        /// </summary>
        public string OSPath { get; set; }

        /// <summary>
        /// 获取或设置 可用物理内存，单位：MB
        /// </summary>
        public double PhysicalMemoryFree { get; set; }

        /// <summary>
        /// 获取或设置 总共物理内存，单位：MB
        /// </summary>
        public double PhysicalMemoryTotal { get; set; }

        /// <summary>
        /// 获取或设置 屏幕分辨率宽
        /// </summary>
        public int ScreenWith { get; set; }

        /// <summary>
        /// 获取或设置 屏幕分辨率高
        /// </summary>
        public int ScreenHeight { get; set; }

        /// <summary>
        /// 获取或设置 屏幕色深
        /// </summary>
        public int ScreenColorDepth { get; set; }
    }
}
