/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Medias
 * 文件名：ThumbnailMode
 * 版本号：v1.0.0.0
 * 唯一标识：faf43f69-2fdc-4f73-b3fb-90d15e325252
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:21:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:21:07
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

namespace JCE.Utils.Medias
{
    /// <summary>
    /// 缩略图枚举
    /// </summary>
    public enum ThumbnailMode
    {
        /// <summary>
        /// 指定高宽裁剪（不变形）
        /// </summary>
        Cut = 1,
        /// <summary>
        /// 指定宽度，高度自动
        /// </summary>
        FixedW = 2,
        /// <summary>
        /// 指定高度，宽度自动
        /// </summary>
        FixedH = 4,
        /// <summary>
        /// 宽度跟高度都制定，但是会变形
        /// </summary>
        FixedBoth = 8
    }
}
