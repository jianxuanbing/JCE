/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Devices
 * 文件名：DiskInfo
 * 版本号：v1.0.0.0
 * 唯一标识：66979bb0-a187-4159-988c-585aa198c04c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:17:30
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:17:30
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

namespace JCE.Utils.Devices
{
    /// <summary>
    /// 硬盘信息
    /// </summary>
    public struct DiskInfo
    {
        /// <summary>
        /// 硬盘名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 容量大小，单位:字节
        /// </summary>
        public long Size { get; private set; }
        /// <summary>
        /// 初始化硬盘信息
        /// </summary>
        /// <param name="name">硬盘名</param>
        /// <param name="size">容量大小，单位:字节</param>
        public DiskInfo(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}
