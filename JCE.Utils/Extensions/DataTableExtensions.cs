/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DataTableExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b903e567-437f-47eb-803b-3ca68e35ef7a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 9:36:27
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 9:36:27
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Adapters;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 数据表（DataTable）扩展
    /// </summary>
    public static partial class DataTableExtensions
    {
        #region Sort(DataTable排序)
        /// <summary>
        /// DataTable排序
        /// </summary>
        /// <param name="dt">要排序的表</param>
        /// <param name="sort">要排序的字段（例如：ID DESC）</param>
        /// <returns></returns>
        public static DataTable Sort(this DataTable dt, string sort = "ID DESC")
        {
            var rows = dt.Select("", sort);
            var tmpDt = dt.Clone();

            foreach (var row in rows)
            {
                tmpDt.ImportRow(row);
            }
            return tmpDt;
        }
        #endregion

        #region Split(DataTable分页)
        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">源表</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static DataTable Split(this DataTable dt, int pageSize = 20, int pageIndex = 1)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            var dtNew = dt.Clone();
            int firstIndex;

            //计算开始索引
            if (pageIndex == 1)
            {
                firstIndex = 0;
            }
            else
            {
                firstIndex = pageSize*(pageIndex - 1);
                //索引超出记录总数时，返回空的表格
                if (firstIndex > dt.Rows.Count)
                {
                    return dtNew;
                }
            }
            //计算结束索引
            var endIndex = pageSize + firstIndex;
            if (endIndex > dt.Rows.Count)
            {
                endIndex = dt.Rows.Count;
            }

            for (var i = firstIndex; i < endIndex; i++)
            {
                dtNew.ImportRow(dt.Rows[i]);
            }
            return dtNew;
        }
        #endregion

        #region Reverse(DataTable倒序)
        /// <summary>
        /// DataTable倒序
        /// </summary>
        /// <param name="dt">源表</param>
        /// <returns></returns>
        public static DataTable Reverse(this DataTable dt)
        {
            var tmpDt = dt.Clone();
            for (var i = dt.Rows.Count - 1; i >= 0; i--)
            {
                tmpDt.ImportRow(dt.Rows[i]);
            }
            return tmpDt;
        }
        #endregion

        #region CloneData(DataTable深度复制)
        /// <summary>
        /// DataTable深度复制（包含数据）
        /// </summary>
        /// <param name="dt">源表</param>
        /// <returns></returns>
        public static DataTable CloneData(DataTable dt)
        {
            var newTable = dt.Clone();
            dt.Rows.ToRows().ForEach(newTable.ImportRow);
            return newTable;
        }
        #endregion

        #region ToRows(将DataRowCollection转换成List[DataRow])
        /// <summary>
        /// 将DataRowCollection转换成List[DataRow]
        /// </summary>
        /// <param name="drc">DataRowCollection</param>
        /// <returns></returns>
        public static List<DataRow> ToRows(this DataRowCollection drc)
        {
            var listRow=new List<DataRow>(drc.Count);
            listRow.AddRange(drc.Cast<DataRow>());
            return listRow;
        }
        #endregion
    }
}
