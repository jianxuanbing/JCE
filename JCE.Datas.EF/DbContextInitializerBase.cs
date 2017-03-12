/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：DbContextInitializerBase
 * 版本号：v1.0.0.0
 * 唯一标识：23df97ee-2c83-4901-a9b3-0a86056f20f0
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 1:01:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 1:01:05
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Domains.Uow;

namespace JCE.Datas.EF
{    
    public abstract class DbContextInitializerBase<TDbContext>: DbContextInitializerBase
        where TDbContext:DbContext,IUnitOfWork,new()
    {
    }

    public abstract class DbContextInitializerBase
    {
        
    }
}
