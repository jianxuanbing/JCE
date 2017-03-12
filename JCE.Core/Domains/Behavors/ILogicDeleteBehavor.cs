/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Behavors
 * 文件名：ILogicDeleteBehavor
 * 版本号：v1.0.0.0
 * 唯一标识：7f7929c0-106c-47eb-af42-71fe01d070f5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:12:14
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:12:14
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Domains.Behavors
{
    /// <summary>
    /// 实体－逻辑删除行为
    /// 具有逻辑删除的接口，实体需要实现这个接口，将IsDeleted实现
    /// 在仓储实现类中，delete方法判断实体是否实现了ILogicDeleteBehavor这个接口，然后再决定是否逻辑删除
    /// </summary>
    public interface ILogicDeleteBehavor
    {
        /// <summary>
        /// 是否已经删除，默认为false
        /// </summary>
        [DisplayName("是否删除")]
        bool IsDeleted { get; set; }
    }
}
