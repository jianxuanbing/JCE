/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Entities
 * 文件名：Size
 * 版本号：v1.0.0.0
 * 唯一标识：db74d6ef-f77f-44af-8a47-9a06fd40e57f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:42:48
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:42:48
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

namespace JCE.Utils.Entities
{
    /// <summary>
    /// 尺寸
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// 初始化一个<see cref="Size"/>类型的实例
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
