/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Maths
 * 文件名：UnitConv
 * 版本号：v1.0.0.0
 * 唯一标识：1f3a084f-1b05-4b2f-b178-125c43e576c8
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/28 22:23:59
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/28 22:23:59
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

namespace JCE.Utils.Maths
{
    /// <summary>
    /// 单位转换
    /// </summary>
    public class UnitConv
    {
        /// <summary>
        /// 摄氏度转换为华氏度
        /// </summary>
        /// <param name="value">摄氏度</param>
        /// <returns></returns>
        public static decimal DegreesCelsiusToFahrenheit(decimal value)
        {
            return (decimal)1.8*value + 32;
        }

        /// <summary>
        /// 摄氏度转换为开氏度(热力学温度)
        /// </summary>
        /// <param name="value">摄氏度</param>
        /// <returns></returns>
        public static decimal DegreesCelsiusToThermodynamicTemperature(decimal value)
        {
            return value + (decimal) 273.16;
        }

        /// <summary>
        /// 华氏度转换为摄氏度
        /// </summary>
        /// <param name="value">华氏度</param>
        /// <returns></returns>
        public static decimal FahrenheitToDegreesCelsius(decimal value)
        {
            return (value - 32)/(decimal)1.8;
        }

        /// <summary>
        /// 华氏度转换为开氏度
        /// </summary>
        /// <param name="value">华氏度</param>
        /// <returns></returns>
        public static decimal FahrenheitToThermodynamicTemperature(decimal value)
        {
            return (value - 32)/ (decimal)1.8 + (decimal)273.16;
        }

        /// <summary>
        /// 开氏度转换为摄氏度
        /// </summary>
        /// <param name="value">开氏度</param>
        /// <returns></returns>
        public static decimal ThermodynamicTemperatureToDegreesCelsius(decimal value)
        {
            return value - (decimal) 273.16;
        }

        /// <summary>
        /// 开氏度转换为华氏度
        /// </summary>
        /// <param name="value">开氏度</param>
        /// <returns></returns>
        public static decimal ThermodynamicTemperatureToFahrenheit(decimal value)
        {
            return (value - (decimal)273.16)* (decimal)1.8 + 32;
        }
    }
}
