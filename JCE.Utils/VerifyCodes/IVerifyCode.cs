/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.VerifyCodes
 * 文件名：IVerifyCode
 * 版本号：v1.0.0.0
 * 唯一标识：384c5752-acff-4920-8394-fba8f3e9796c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/23 20:50:39
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/23 20:50:39
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JCE.Utils.VerifyCodes
{
    /// <summary>
    /// 验证码
    /// </summary>
    public interface IVerifyCode
    {
        /// <summary>
        /// 验证码长度
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// 验证码字符串
        /// </summary>
        string VerifyCodeText { get; }

        /// <summary>
        /// 是否包含小写字母
        /// </summary>
        bool HasLowerLetter { get; set; }

        /// <summary>
        /// 是否包含大写字母
        /// </summary>
        bool HasUpperLetter { get; set; }

        /// <summary>
        /// 是否包含汉字
        /// </summary>
        bool HasChineseCharacter { get; set; }

        /// <summary>
        /// 是否包含数字
        /// </summary>
        bool HasNumber { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        int FontSize { get; set; }
        
        /// <summary>
        /// 字体颜色
        /// </summary>
        Color FontColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        /// 字体类型
        /// </summary>
        string FontFamily { get; set; }

        /// <summary>
        /// 是否随机颜色
        /// </summary>
        bool IsRandomColor { get; set; }

        /// <summary>
        /// 前景噪点数量
        /// </summary>
        int ForeNoisePointCount { get; set; }

        /// <summary>
        /// 随机码的旋转角度
        /// </summary>
        int RandomAngle { get; set; }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <returns></returns>
        byte[] OutputImage();

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="response">Http响应实例</param>
        /// <returns>输出是否成功</returns>
        bool OutputImage(HttpResponse response);

        /// <summary>
        /// 获取问题
        /// </summary>
        /// <param name="questionList">默认数字加减验证</param>
        /// <returns></returns>
        KeyValuePair<string, string> GetQuestion(Dictionary<string, string> questionList = null);
    }
}
