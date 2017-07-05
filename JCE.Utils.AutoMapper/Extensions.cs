/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.AutoMapper
 * 文件名：Extensions
 * 版本号：v1.0.0.0
 * 唯一标识：07740c41-2ae7-41b7-a092-efa7f1237c3f
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/6/27 22:55:13
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/6/27 22:55:13
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
using AutoMapper;

namespace JCE.Utils.AutoMapper
{
    /// <summary>
    /// AutoMapper扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var sourceType = source.GetType();
            var destinationType = typeof(TDestination);
            try
            {
                var map = Mapper.Configuration.FindTypeMapFor(sourceType,destinationType);
                if (map != null)
                {
                    return Mapper.Map(source, destination);
                }
                var maps = Mapper.Configuration.GetAllTypeMaps();
                Mapper.Initialize(config =>
                {
                    foreach (var item in maps)
                    {
                        config.CreateMap(item.SourceType, item.DestinationType);
                    }
                    config.CreateMap(sourceType,destinationType);
                });

            }
            catch (InvalidOperationException)
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap(sourceType, destinationType);
                });
            }
            return Mapper.Map(source, destination);
        }
    }
}
