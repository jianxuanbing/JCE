/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Medias
 * 文件名：ImageLocationMode
 * 版本号：v1.0.0.0
 * 唯一标识：e2af57fe-4f45-4c1f-aea6-93b7df90fa89
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:20:42
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:20:42
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
    /// 图片位置枚举
    /// </summary>
    public enum ImageLocationMode
    {
        /// <summary>
        /// 左上角
        /// </summary>
        LeftTop,
        /// <summary>
        /// 靠上
        /// </summary>
        Top,
        /// <summary>
        /// 右上角
        /// </summary>
        RightTop,
        /// <summary>
        /// 左中
        /// </summary>
        LeftCenter,
        /// <summary>
        /// 居中
        /// </summary>
        Center,
        /// <summary>
        /// 右中
        /// </summary>
        RightCenter,
        /// <summary>
        /// 左下角
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 靠下
        /// </summary>
        Bottom,
        /// <summary>
        /// 右下角
        /// </summary>
        RightBottom
    }
}
