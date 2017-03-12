/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Strings
 * 文件名：StringCompute
 * 版本号：v1.0.0.0
 * 唯一标识：1143dd57-84cb-44b2-89ea-8b48023d9c44
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 13:10:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 13:10:07
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

namespace JCE.Utils.Strings
{
    /// <summary>
    /// 字符串对比
    /// </summary>
    public class StringCompute
    {
        #region Field(字段)
        /// <summary>
        /// 字符串1
        /// </summary>
        private char[] _arrChar1;
        /// <summary>
        /// 字符串2
        /// </summary>
        private char[] _arrChar2;
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _beginTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime _endTime;
        /// <summary>
        /// 计算次数
        /// </summary>
        private int _computeTimes;
        /// <summary>
        /// 算法矩阵
        /// </summary>
        private int[,] _matrix;
        /// <summary>
        /// 矩阵列数
        /// </summary>
        private int _column;
        /// <summary>
        /// 矩阵行数
        /// </summary>
        private int _row;
        /// <summary>
        /// 对比结果
        /// </summary>
        private StringComputeResult _result;
        #endregion

        #region Property(属性)

        /// <summary>
        /// 对比结果
        /// </summary>
        public StringComputeResult ComputeResult
        {
            get { return _result; }
        }

        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="StringCompute"/>类型的实例
        /// </summary>
        public StringCompute() { }
        /// <summary>
        /// 初始化一个<see cref="StringCompute"/>类型的实例
        /// </summary>
        /// <param name="strOne">字符串1</param>
        /// <param name="strTwo">字符串2</param>
        public StringCompute(string strOne, string strTwo)
        {
            this.StringComputeInit(strOne, strTwo);
        }
        #endregion

        #region Compute(计算相似度)
        /// <summary>
        /// 计算相似度
        /// </summary>
        public void Compute()
        {
            //开始时间
            _beginTime = DateTime.Now;
            //初始化矩阵的第一行和第一列
            this.InitMatrix();
            int intCost = 0;
            for (int i = 1; i < _row; i++)
            {
                for (int j = 1; j < _column; j++)
                {
                    if (_arrChar1[i - 1] == _arrChar2[j - 1])
                    {
                        intCost = 0;
                    }
                    else
                    {
                        intCost = 1;
                    }
                    //关键步骤，计算当前位置值为左边+1、上面+1、左上角+intCost中的最小值 
                    //循环遍历到最后_Matrix[_Row - 1, _Column - 1]即为两个字符串的距离
                    _matrix[i, j] = this.Minimun(_matrix[i - 1, j] + 1, _matrix[i, j - 1] + 1,
                        _matrix[i - 1, j - 1] + intCost);
                    _computeTimes++;
                }
            }
            //结束时间
            _endTime = DateTime.Now;
            //相似率 移动次数小于最长的字符串长度的20%算同一题
            int intLength = _row > _column ? _row : _column;
            _result.Rate = (1 - (decimal)_matrix[_row - 1, _column - 1] / intLength);
            _result.ComputeTimes = _computeTimes.ToString();
            _result.UserTime = (_endTime - _beginTime).ToString();
            _result.Difference = _matrix[_row - 1, _column - 1];
        }
        /// <summary>
        /// 计算相似度
        /// </summary>
        /// <param name="strOne">字符串1</param>
        /// <param name="strTwo">字符串2</param>
        public void Compute(string strOne, string strTwo)
        {
            this.StringComputeInit(strOne, strTwo);
            this.Compute();
        }
        /// <summary>
        /// 计算相似度(不记录比较时间)
        /// </summary>
        public void SpeedyCompute()
        {
            //初始化矩阵的第一行和第一列
            this.InitMatrix();
            int intCost = 0;
            for (int i = 1; i < _row; i++)
            {
                for (int j = 1; j < _column; j++)
                {
                    if (_arrChar1[i - 1] == _arrChar2[j - 1])
                    {
                        intCost = 0;
                    }
                    else
                    {
                        intCost = 1;
                    }
                    //关键步骤，计算当前位置值为左边+1、上面+1、左上角+intCost中的最小值 
                    //循环遍历到最后_Matrix[_Row - 1, _Column - 1]即为两个字符串的距离
                    _matrix[i, j] = this.Minimun(_matrix[i - 1, j] + 1, _matrix[i, j - 1] + 1,
                        _matrix[i - 1, j - i] + intCost);
                    _computeTimes++;
                }
            }
            //相似率 移动次数小于最长的字符串长度的20%算同一题
            int intLength = _row > _column ? _row : _column;
            _result.Rate = (1 - (decimal)_matrix[_row - 1, _column - 1] / intLength);
            _result.ComputeTimes = _computeTimes.ToString();
            _result.Difference = _matrix[_row - 1, _column - 1];
        }
        /// <summary>
        /// 计算相似度(不记录比较时间)
        /// </summary>
        /// <param name="strOne">字符串1</param>
        /// <param name="strTwo">字符串2</param>
        public void SpeedyCompute(string strOne, string strTwo)
        {
            this.StringComputeInit(strOne, strTwo);
            this.SpeedyCompute();
        }
        #endregion

        #region Private Method(私有方法)
        /// <summary>
        /// 初始化算法基本信息
        /// </summary>
        /// <param name="strOne">字符串1</param>
        /// <param name="strTwo">字符串2</param>
        private void StringComputeInit(string strOne, string strTwo)
        {
            _arrChar1 = strOne.ToCharArray();
            _arrChar2 = strTwo.ToCharArray();
            _result = new StringComputeResult();
            _row = _arrChar1.Length + 1;
            _column = _arrChar2.Length + 1;
            _matrix = new int[_row, _column];

        }

        /// <summary>
        /// 初始化矩阵的第一行和第一列
        /// </summary>
        private void InitMatrix()
        {
            for (int i = 0; i < _column; i++)
            {
                _matrix[0, i] = i;
            }
            for (int i = 0; i < _row; i++)
            {
                _matrix[i, 0] = i;
            }

        }
        /// <summary>
        /// 获取三个数中的最小值
        /// </summary>
        /// <param name="first">值1</param>
        /// <param name="second">值2</param>
        /// <param name="third">值3</param>
        /// <returns></returns>
        private int Minimun(int first, int second, int third)
        {
            int intMin = first;
            if (second < first)
            {
                intMin = second;
            }
            if (third < intMin)
            {
                intMin = third;
            }
            return intMin;
        }
        #endregion
    }
}
