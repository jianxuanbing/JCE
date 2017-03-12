/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：IDomainObject
 * 版本号：v1.0.0.0
 * 唯一标识：7aeefa90-e630-47ce-a22c-19ac6e182056
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:13:51
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:13:51
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

namespace JCE.Core.Domains
{
    /// <summary>
    /// 领域对象
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// 验证
        /// </summary>
        void Validate();
    }
}
