/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ConstructorExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：87346536-ac27-4fe6-80ad-82366da0c066
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:40:37
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:40:37
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 构造函数（T）扩展
    /// </summary>
    public static class ConstructorExtensions
    {
        #region AutoInitialize(自动初始化源对象)
        /// <summary>
        /// 自动初始化源对象，对象数据为绑定标识指定的所有属性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">源对象，源对象对其属性初始化</param>
        /// <param name="data">数据对象，用于初始化源对象的数据对象</param>
        /// <param name="flags">用于属性的绑定标识</param>
        /// <returns></returns>
        public static T AutoInitialize<T>(this T source, object data, BindingFlags flags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty)
        {
            //检查数据是否为空，检查数据是否相同类型
            if (data == null || data.GetType() != source.GetType())
            {
                return source;
            }
            //获取实例中所有包含（getter和setter）的公共属性
            PropertyInfo[] properties = source.GetType().GetProperties(flags);
            //遍历属性赋值
            foreach (PropertyInfo property in properties)
            {
                //确定当前属性的类型
                Type propertyType = property.PropertyType;
                try
                {
                    //检索给定属性名称的值
                    object objectValue = property.GetValue(data, null);
                    if (objectValue != null)
                    {
                        //如果对象的值已经是属性类型
                        if (objectValue.GetType() == propertyType)
                        {
                            //将对象值设为源
                            property.SetValue(source, objectValue, null);
                        }
                        else
                        {
                            //否则将使用属性转换器，转换对象值
                            TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
                            {
                                //转换对象值
                                object convertedData = converter.ConvertFrom(objectValue);
                                //检查转换后的数据的属性是否相同
                                if (convertedData != null && convertedData.GetType() == propertyType)
                                {
                                    //如果是，将转换后的数据设置为源对象
                                    property.SetValue(source, convertedData, null);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return source;
        }

        /// <summary>
        /// 自动初始化源对象，数据行的为绑定标识指定的所有属性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">源对象，源对象对其属性初始化</param>
        /// <param name="row">数据行，初始化包含数据的数据行</param>
        ///<param name="flags">用于属性的绑定标识</param>
        public static void AutoInitialize<T>(this T source, DataRow row,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty)
        {
            if (row == null)
            {
                return;
            }
            //获取实例中所有的包含（setter）的公共属性
            PropertyInfo[] properties = source.GetType().GetProperties(flags);
            foreach (PropertyInfo property in properties)
            {
                //从列种获取列名或使用属性名称
                string columnName = property.Name;
                //获取属性类型
                Type propertyType = property.PropertyType;
                try
                {
                    //检索给定列名称的行值
                    object rowValue = row[columnName];
                    //确定行的值不是null（DBNulll）
                    if (rowValue != Convert.DBNull)
                    {
                        //获取当前属性的转换器
                        TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
                        {
                            //将行值转换为属性类型
                            object data = converter.ConvertFrom(rowValue);
                            //检查转换后的数据的属性是否相同，将转换后的数据设置为源对象
                            if (data != null && data.GetType() == propertyType)
                            {
                                property.SetValue(source, data, null);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 自动初始化源对象，数据行的绑定标识指定的所有属性
        /// </summary>
        ///  <typeparam name="T">泛型</typeparam>
        /// <param name="source">源对象，源对象对其属性初始化</param>
        /// <param name="row">数据行，初始化包含数据的数据行</param>
        ///<param name="flags">用于属性的绑定标识</param>
        /// <param name="columns">列，表达式</param>
        public static void AutoInitialize<T>(this T source, DataRow row,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
            params Expression<Func<T, object>>[] columns)
        {
            if (row == null || columns == null)
            {
                return;
            }
            Type sourceType = source.GetType();
            foreach (Expression<Func<T, object>> t in columns)
            {
                //从列种获取列名称或使用属性名称
                string columnName = GetProperty(t).Name;
                //获取给定列名称的属性
                PropertyInfo property = sourceType.GetProperty(columnName, flags);
                //如果属性为空
                if (property != null)
                {
                    //获取属性类型
                    Type propertyType = property.PropertyType;
                    //检索给定列名称的行的值
                    object rowValue = row[columnName];
                    //确定行的值是否为空
                    if (rowValue != Convert.DBNull)
                    {
                        //获取属性转换器
                        TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
                        {
                            object data = converter.ConvertFrom(rowValue);
                            if (data != null && data.GetType() == propertyType)
                            {
                                property.SetValue(source, data, null);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region GetProperty(从属性表达式获取属性信息)
        /// <summary>
        /// 从属性表达式获取属性信息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo GetProperty<T>(Expression<Func<T, object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            var expression = lambda.Body as UnaryExpression;
            if (expression != null)
            {
                var unaryExpression = expression;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }
            return memberExpression.Member as PropertyInfo;
        }
        #endregion

    }
}
