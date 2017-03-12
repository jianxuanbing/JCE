/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Str
 * 版本号：v1.0.0.0
 * 唯一标识：23b0ac53-407f-4ecd-a252-35e67235c124
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/23 0:10:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/23 0:10:35
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
    /// 字符串操作
    /// </summary>
    public sealed partial class Str
    {
        #region Field(字段)
        /// <summary>
        /// 字符串生成器
        /// </summary>
        private StringBuilder Builder { get; set; }
        #endregion

        #region Property(属性)
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int Length
        {
            get { return Builder.Length; }
        }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Str"/>类型的实例
        /// </summary>
        public Str()
        {
            Builder = new StringBuilder();
        }

        /// <summary>
        /// 初始化一个<see cref="Str"/>类型的实例
        /// </summary>
        /// <param name="length">起始大小</param>
        public Str(int length)
        {            
            Builder=new StringBuilder(length);
        }
        #endregion
        
        #region Append(追加内容)
        /// <summary>
        /// 追加内容
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">值</param>
        public void Append<T>(T value)
        {
            Builder.Append(value);
        }

        /// <summary>
        /// 追加内容
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void Append(string value, params object[] args)
        {
            if (args == null)
            {
                args = new object[] { string.Empty };
            }
            if (args.Length == 0)
            {
                Builder.Append(value);
            }
            else
            {
                Builder.AppendFormat(value, args);
            }
        }
        #endregion

        #region AppendLine(追加内容并换行)
        /// <summary>
        /// 追加内容并换行
        /// </summary>
        public void AppendLine()
        {
            Builder.AppendLine();
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">值</param>
        public void AppendLine<T>(T value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void AppendLine(string value, params object[] args)
        {
            Append(value, args);
            AppendLine();
        }
        #endregion

        #region Replace(替换内容)
        /// <summary>
        /// 替换内容
        /// </summary>
        /// <param name="value">值</param>
        public void Replace(string value)
        {
            Builder.Clear();
            Builder.Append(value);
        }
        #endregion

        #region RemoveEnd(移除末尾字符串)
        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="end">末尾字符串</param>
        public void RemoveEnd(string end)
        {
            string result = Builder.ToString();
            if (!result.EndsWith(end))
            {
                return;
            }
            int index = result.LastIndexOf(end, StringComparison.Ordinal);
            Builder = Builder.Remove(index, end.Length);
        }
        #endregion

        #region Clear(清空字符串)
        /// <summary>
        /// 清空字符串
        /// </summary>
        public void Clear()
        {
            Builder = Builder.Clear();
        }
        #endregion

        #region ToString(转换为字符串)
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Builder.ToString();
        }
        #endregion
        
    }
}
