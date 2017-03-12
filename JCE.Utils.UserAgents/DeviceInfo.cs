/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：DeviceInfo
 * 版本号：v1.0.0.0
 * 唯一标识：fd5435d9-55e2-4db8-9102-9421abcc7258
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:38:09
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:38:09
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

namespace JCE.Utils.UserAgents
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public sealed class DeviceInfo
    {
        public string Family { get; set; }
        public bool IsBot { get; set; }

        public DeviceInfo(string family, bool isBot)
        {
            this.Family = family;
            this.IsBot = isBot;
        }

        public override string ToString()
        {
            return this.Family;
        }
    }
}
