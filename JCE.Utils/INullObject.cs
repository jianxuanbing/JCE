/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：INullObject
 * 版本号：v1.0.0.0
 * 唯一标识：f543df6c-b766-49b4-89c1-88a969b2e492
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 21:20:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 21:20:58
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

namespace JCE.Utils
{
    /// <summary>
    /// 空对象
    /// </summary>
    public interface INullObject
    {
        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <returns></returns>
        bool IsNull();
    }

    /// <summary>
    /// 空对象
    /// </summary>
    public class NullObject:INullObject
    {
        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
        {
            return false;
        }
    }
}
