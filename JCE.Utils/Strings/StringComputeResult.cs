/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Strings
 * 文件名：StringComputeResult
 * 版本号：v1.0.0.0
 * 唯一标识：593e6501-01f0-4012-ab89-b39b3c81e3ce
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 13:10:25
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 13:10:25
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

namespace JCE.Utils.Strings
{
    /// <summary>
    /// 字符串对比计算结果
    /// </summary>
    public struct StringComputeResult
    {
        /// <summary>
        /// 相似度
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 对比次数
        /// </summary>
        public string ComputeTimes { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public string UserTime { get; set; }
        /// <summary>
        /// 差异
        /// </summary>
        public int Difference { get; set; }
    }
}
