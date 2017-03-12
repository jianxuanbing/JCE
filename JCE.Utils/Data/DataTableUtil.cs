/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Data
 * 文件名：DataTableUtil
 * 版本号：v1.0.0.0
 * 唯一标识：5aa4710d-a5f7-4917-9e45-befe0effffbd
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:22:56
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:22:56
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Data
{
    /// <summary>
    /// 数据表(DataTable)工具类
    /// </summary>
    public class DataTableUtil
    {
        #region AddIdentityColumn(添加自增列)
        /// <summary>
        /// 给DataTable添加自增列，如果DataTable存在identityId字段，则直接返回DataTable，不做任何处理
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static DataTable AddIdentityColumn(DataTable table)
        {
            if (!table.Columns.Contains("identityId"))
            {
                table.Columns.Add("identityId");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i]["identityId"] = (i + 1).ToString();
                }
            }
            return table;
        }
        #endregion

        #region IsHaveRows(是否有数据行)
        /// <summary>
        /// 检查DataTable是否有数据行
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static bool IsHaveRows(DataTable table)
        {
            return table != null && table.Rows.Count > 0;
        }
        #endregion

        #region DataTableToList(DataTable转换成实体列表)
        /// <summary>
        /// DataTable转换成实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable table) where T : class
        {
            if (!IsHaveRows(table))
            {
                return new List<T>();
            }
            IList<T> list = new List<T>();
            T model = default(T);
            foreach (DataRow row in table.Rows)
            {
                model = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    object rowValue = row[column.ColumnName];
                    PropertyInfo info = model.GetType().GetProperty(column.ColumnName);
                    if (info != null && info.CanWrite && (rowValue != null && !Convert.IsDBNull(rowValue)))
                    {
                        info.SetValue(model, rowValue, null);
                    }
                }
                list.Add(model);
            }
            return list;
        }
        #endregion

        #region ListToDataTable(实体列表转换成DataTable)
        /// <summary>
        /// 实体列表转换成DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(IList<T> list) where T : class
        {
            if (list == null || list.Count < 0)
            {
                return null;
            }
            DataTable table = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int length = propertyInfos.Length;
            bool createColumn = true;
            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }
                row = table.NewRow();
                for (int i = 0; i < length; i++)
                {
                    PropertyInfo info = propertyInfos[i];
                    string name = info.Name;
                    if (createColumn)
                    {
                        column = new DataColumn(name, info.PropertyType);
                        table.Columns.Add(column);
                    }
                    row[name] = info.GetValue(t, null);
                }
                if (createColumn)
                {
                    createColumn = false;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        #endregion

        #region ToDataTable(将泛型集合转换成DataTable)
        /// <summary>
        /// 将泛型集合转换成DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable<T>(list, null);
        }
        /// <summary>
        /// 将泛型集合转换成DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">属性名，需要返回的列的列名</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
            {
                propertyNameList.AddRange(propertyName);
            }
            DataTable table = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertyInfos = list[0].GetType().GetProperties();
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (propertyNameList.Count == 0)
                    {
                        table.Columns.Add(info.Name, info.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(info.Name))
                        {
                            table.Columns.Add(info.Name, info.PropertyType);
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo info in propertyInfos)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = info.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(info.Name))
                            {
                                object obj = info.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    table.LoadDataRow(array, true);
                }
            }
            return table;
        }
        #endregion

        #region CreateTable(创建数据表)
        /// <summary>
        /// 创建数据表，根据列名数组创建表格
        /// </summary>
        /// <param name="columns">列名数组，包含字段信息的数组</param>
        /// <returns></returns>
        public static DataTable CreateTable(IList<string> columns)
        {
            if (columns.Count <= 0)
            {
                return null;
            }
            DataTable table = new DataTable();
            foreach (string columnName in columns)
            {
                table.Columns.Add(columnName, typeof(string));
            }
            return table;
        }
        /// <summary>
        /// 创建数据表，根据字符列表创建表字段，字段格式可以是：
        /// 1) a,b,c,d,e
        /// 2) a|int,b|string,c|bool,d|decimal
        /// </summary>
        /// <param name="columns">表字段列信息</param>
        /// <returns></returns>
        public static DataTable CreateTable(string columns)
        {
            string[] columnArray = columns.Split(new char[] { ',', ';' });
            DataTable table = new DataTable();
            foreach (string column in columnArray)
            {
                if (!string.IsNullOrEmpty(column))
                {
                    string[] subItems = column.Split('|');
                    if (subItems.Length == 2)
                    {
                        table.Columns.Add(subItems[0], ConvertType(subItems[1]));
                    }
                    else
                    {
                        table.Columns.Add(subItems[0]);
                    }
                }
            }
            return table;
        }
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <returns></returns>
        private static Type ConvertType(string typeName)
        {
            typeName = typeName.ToLower().Replace("system.", "");
            Type newType = typeof(string);
            switch (typeName)
            {
                case "boolean":
                case "bool":
                    newType = typeof(bool);
                    break;
                case "int16":
                case "short":
                    newType = typeof(short);
                    break;
                case "int32":
                case "int":
                    newType = typeof(int);
                    break;
                case "long":
                case "int64":
                    newType = typeof(long);
                    break;
                case "uint16":
                case "ushort":
                    newType = typeof(ushort);
                    break;
                case "uint32":
                case "uint":
                    newType = typeof(uint);
                    break;
                case "uint64":
                case "ulong":
                    newType = typeof(ulong);
                    break;
                case "single":
                case "float":
                    newType = typeof(float);
                    break;

                case "string":
                    newType = typeof(string);
                    break;
                case "guid":
                    newType = typeof(Guid);
                    break;
                case "decimal":
                    newType = typeof(decimal);
                    break;
                case "double":
                    newType = typeof(double);
                    break;
                case "datetime":
                    newType = typeof(DateTime);
                    break;
                case "byte":
                    newType = typeof(byte);
                    break;
                case "char":
                    newType = typeof(char);
                    break;
            }
            return newType;
        }
        #endregion

        #region GetDataRowArray(获取从DataRowCollection转换成的DataRow数组)
        /// <summary>
        /// 获取从DataRowCollection转换成的DataRow数组
        /// </summary>
        /// <param name="collection">数据行集合</param>
        /// <returns></returns>
        public static DataRow[] GetDataRowArray(DataRowCollection collection)
        {
            int count = collection.Count;
            DataRow[] rows = new DataRow[count];
            for (int i = 0; i < count; i++)
            {
                rows[i] = collection[i];
            }
            return rows;
        }
        #endregion

        #region GetDataTableFromRows(将DataRows转换成DataTable)
        /// <summary>
        /// 将DataRows转换成DataTable
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromRows(DataRow[] rows)
        {
            if (rows.Length <= 0)
            {
                return new DataTable();
            }
            DataTable table = rows[0].Table.Clone();
            table.DefaultView.Sort = rows[0].Table.DefaultView.Sort;
            for (int i = 0; i < rows.Length; i++)
            {
                table.LoadDataRow(rows[i].ItemArray, true);
            }
            return table;
        }
        #endregion

        #region SortedTable(排序表的视图)
        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="sorts">排序方式</param>
        /// <returns></returns>
        public static DataTable SortedTable(DataTable table, params string[] sorts)
        {
            if (table.Rows.Count > 0)
            {
                string temp = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    temp += sorts[i] + ",";
                }
                table.DefaultView.Sort = temp.TrimEnd(',');
            }
            return table;
        }
        #endregion

        #region FilterDataTable(过滤数据表的内容)
        /// <summary>
        /// 过滤数据表的内容
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable FilterDataTable(DataTable table, string condition)
        {
            if (condition.Trim() == "")
            {
                return table;
            }
            DataTable newTable = new DataTable();
            newTable = table.Clone();
            DataRow[] rows = table.Select(condition);
            for (int i = 0; i < rows.Length; i++)
            {
                newTable.ImportRow(rows[i]);
            }
            return newTable;
        }
        #endregion
    }
}
