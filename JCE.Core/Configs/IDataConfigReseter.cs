/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：IDataConfigReseter
 * 版本号：v1.0.0.0
 * 唯一标识：d2007a86-fd2b-433b-9b9f-37ea38965c3a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:54:16
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:54:16
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

namespace JCE.Core.Configs
{
    /// <summary>
    /// 数据配置信息重置类
    /// </summary>
    public interface IDataConfigReseter
    {
        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        DataConfig Reset(DataConfig config);
    }
}
