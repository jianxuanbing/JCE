/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：SocketExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：5ac0ff3b-4adc-4068-91a3-4c1b4dae41f0
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:53:55
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:53:55
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 套接字接口（Socket）扩展
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <param name="socket">套接字</param>
        /// <returns>bool</returns>
        public static bool IsConnected(this Socket socket)
        {
            var part1 = socket.Poll(1000, SelectMode.SelectRead);
            var part2 = (socket.Available == 0);
            return part1 & part2;
        }
    }
}
