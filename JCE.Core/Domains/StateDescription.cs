/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：StateDescription
 * 版本号：v1.0.0.0
 * 唯一标识：916ea1a3-dc80-4827-bfae-0e24a81efc29
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:16:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:16:08
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
using JCE.Utils.Extensions;

namespace JCE.Core.Domains
{
    /// <summary>
    /// 状态描述
    /// </summary>
    public abstract class StateDescription
    {
        /// <summary>
        /// 描述
        /// </summary>
        private StringBuilder _description;

        /// <summary>
        /// 输出对象状态
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            _description = new StringBuilder();
            AddDescriptions();
            return _description.ToString().TrimEnd().TrimEnd(',');
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected virtual void AddDescriptions()
        {
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        /// <param name="description">描述</param>
        protected void AddDescription(string description)
        {
            _description.Append(description);
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        protected void AddDescription<T>(string name, T value)
        {
            if (value.ToStr().IsEmpty())
            {
                return;
            }
            _description.AppendFormat("{0}:{1},", name, value);
        }
    }
}
