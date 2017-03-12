/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Data
 * 文件名：CsvUtil
 * 版本号：v1.0.0.0
 * 唯一标识：f38a9995-01a8-4241-9c7e-2710a183c038
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:19:44
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:19:44
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Files;

namespace JCE.Utils.Data
{
    /// <summary>
    /// Csv工具类
    /// </summary>
    public class CsvUtil
    {
        #region Field(字段)
        /// <summary>
        /// 逗号分隔符
        /// </summary>
        private const string Seperator = ",";
        /// <summary>
        /// 引号
        /// </summary>
        private const string Bracer = "\"";
        /// <summary>
        /// 换行符
        /// </summary>
        private const string NewLine = "\n";
        /// <summary>
        /// 特殊字符正则
        /// </summary>
        private const string RegexSpecial = ".*[,\n\"].*";
        #endregion

        #region DataSet
        #region DataSetToCsvData(DataSet集合转换为CSV数据)
        /// <summary>
        /// DataSet集合转换成为CSV数据
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns></returns>
        public static string DataSetToCsvData(DataSet ds)
        {
            List<List<string>> list = new List<List<string>>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                DataTable table = ds.Tables[0];
                List<string> columnNameList = new List<string>();
                //添加列
                if (table.Columns.Count > 0)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        columnNameList.Add(column.ColumnName);
                    }
                    list.Add(columnNameList);
                }
                //迭代添加行记录
                foreach (DataRow row in table.Rows)
                {
                    List<string> rowData = new List<string>();
                    foreach (string columnName in columnNameList)
                    {
                        rowData.Add(Convert.ToString(row[columnName]));
                    }
                    list.Add(rowData);
                }
            }
            return ListToCsvData(list);
        }
        #endregion
        #endregion

        #region Csv
        #region ToDataTable(CSV文件转换成DataTable)
        /// <summary>
        /// CSV文件转换成DataTable(OleDb数据库访问方式)
        /// </summary>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns></returns>
        public static DataTable ToDataTableByOledb(string csvPath)
        {
            DataTable table = new DataTable("csv");
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException("csv文件路径不存在!");
            }

            FileInfo fileInfo = new FileInfo(csvPath);
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileInfo.DirectoryName + ";Extended Properties='Text;'"))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + fileInfo.Name + "]", conn);
                adapter.Fill(table);
            }
            return table;
        }
        /// <summary>
        /// CSV文件转换成DataTable(文件流方式)
        /// </summary>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns></returns>
        public static DataTable ToDataTableByStreamReader(string csvPath)
        {
            DataTable table = new DataTable("csv");
            int intColCount = 0;
            bool blnFlag = true;
            DataColumn column;
            DataRow row;
            string strLine = null;
            string[] aryLine;

            using (StreamReader reader = new StreamReader(csvPath, FileUtil.GetEncoding(csvPath)))
            {
                while (!string.IsNullOrEmpty((strLine = reader.ReadLine())))
                {
                    aryLine = strLine.Split(new char[] { ',' });
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryLine.Length;
                        for (int i = 0; i < aryLine.Length; i++)
                        {
                            column = new DataColumn(aryLine[i]);
                            table.Columns.Add(column);
                        }
                        continue;
                    }

                    row = table.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        row[i] = aryLine[i];
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        #endregion

        #endregion

        #region List
        #region ListToCsvData(List集合转换为CSV数据)
        /// <summary>
        /// List集合转换为CSV数据
        /// </summary>
        /// <param name="list">数据集合，行、列</param>
        /// <returns></returns>
        public static string ListToCsvData(List<List<string>> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (List<string> row in list)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    var temp = row[i];
                    temp = temp ?? "";
                    if (i != 0)
                    {
                        sb.Append(Seperator);
                    }
                    if (temp.Contains(Seperator) || temp.Contains(Bracer) || temp.Contains(NewLine))
                    {
                        if (temp.Contains(Bracer))
                        {
                            temp = temp.Replace(Bracer, Bracer + Bracer);
                        }
                        if (temp.Contains(NewLine))
                        {
                            temp = temp.Replace(NewLine, "<br/>");
                        }
                        sb.Append(Bracer + temp + Bracer);
                    }
                    else
                    {
                        sb.Append(temp);
                    }
                    if (i == row.Count - 1)
                    {
                        sb.Append(NewLine);
                    }
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region DataTable
        #region DataTableToCsv(DataTable生成CSV文件)
        /// <summary>
        /// DataTable生成CSV文件
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="csvPath">csv文件路径</param>
        public static bool DataTableToCsv(DataTable table, string csvPath)
        {
            if (null == table)
            {
                return false;
            }
            try
            {
                StringBuilder csvText = new StringBuilder();
                StringBuilder csvRowText = new StringBuilder();
                foreach (DataColumn column in table.Columns)
                {
                    csvRowText.Append(",");
                    csvRowText.Append(column.ColumnName);
                }
                csvText.AppendLine(csvRowText.ToString().Substring(1));

                foreach (DataRow row in table.Rows)
                {
                    csvRowText = new StringBuilder();
                    foreach (DataColumn column in table.Columns)
                    {
                        csvRowText.Append(",");
                        csvRowText.Append(row[column.ColumnName].ToString().Replace(',', ' '));
                    }
                    csvText.AppendLine(csvRowText.ToString().Substring(1));
                }
                File.WriteAllText(csvPath, csvText.ToString(), Encoding.Default);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// DataTable生成CSV文件
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="csvPath">csv文件路径</param>
        /// <param name="tableHeader">表头</param>
        /// <param name="columnName">字段标题，逗号分隔</param>
        public static bool DataTableToCsv(DataTable table, string csvPath, string tableHeader, string columnName)
        {
            if (null == table)
            {
                return false;
            }
            try
            {
                string strBufferLine = "";
                StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8);
                sw.WriteLine(tableHeader);
                sw.WriteLine(columnName);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            strBufferLine += ",";
                        }
                        strBufferLine += table.Rows[i][j].ToString();
                    }
                    sw.WriteLine(strBufferLine);
                }
                sw.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
        #endregion
    }
}
