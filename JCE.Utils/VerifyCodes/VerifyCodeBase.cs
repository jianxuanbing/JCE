/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.VerifyCodes
 * 文件名：VerifyCodeBase
 * 版本号：v1.0.0.0
 * 唯一标识：64d426c0-f1b8-4186-abb6-8571019dd09c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/23 21:58:45
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/23 21:58:45
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using JCE.Utils.Randoms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Extensions;

namespace JCE.Utils.VerifyCodes
{
    /// <summary>
    /// 验证码基类
    /// </summary>
    public class VerifyCodeBase:IVerifyCode
    {
        #region Field(字段)
        /// <summary>
        /// 随机数生成器
        /// </summary>
        protected RandomBuilder RandomBuilder=new RandomBuilder();

        /// <summary>
        /// 图片长度
        /// </summary>
        protected int ImgWidth
        {
            get { return this.VerifyCodeText.Length*FontSize; }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        protected int ImgHeight
        {
            get { return Conv.ToInt((60.0/100)*FontSize + FontSize); }
        }
        #endregion

        #region Property(属性)
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 验证码字符串
        /// </summary>
        public string VerifyCodeText { get; protected set; }
        /// <summary>
        /// 是否包含小写字母
        /// </summary>
        public bool HasLowerLetter { get; set; }
        /// <summary>
        /// 是否包含大写字母
        /// </summary>
        public bool HasUpperLetter { get; set; }
        /// <summary>
        /// 是否包含汉字
        /// </summary>
        public bool HasChineseCharacter { get; set; }
        /// <summary>
        /// 是否包含数字
        /// </summary>
        public bool HasNumber { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color FontColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// 字体类型
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// 是否随机颜色
        /// </summary>
        public bool IsRandomColor { get; set; }
        /// <summary>
        /// 前景噪点数量
        /// </summary>
        public int ForeNoisePointCount { get; set; }
        /// <summary>
        /// 随机码的旋转角度
        /// </summary>
        public int RandomAngle { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="VerifyCodeBase"/>类型的实例
        /// </summary>
        public VerifyCodeBase()
        {
            Length = 4;
            FontSize = 18;
            FontColor=Color.Blue;
            FontFamily = "Verdana";
            BackgroundColor=Color.AliceBlue;
            ForeNoisePointCount = 2;
            RandomAngle = 20;            
        }
        #endregion

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <returns></returns>
        public byte[] OutputImage()
        {
            using (Bitmap bitmap = this.GetVerifyCodeImage())
            {
                if (bitmap == null)
                {
                    return null;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }                
            }
        }
        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="response">Http响应实例</param>
        /// <returns>输出是否成功</returns>
        public bool OutputImage(HttpResponse response)
        {
            bool result = false;
            if (this.IsRandomColor)
            {
                this.FontColor = GetRandomColor();
            }
            byte[] bytes = this.OutputImage();
            if (bytes != null)
            {
                response.ClearContent();
                response.ContentType = "image/Jpeg";
                response.BinaryWrite(bytes);
                response.Flush();
                response.End();

                result = true;
            }
            return result;
        }

        /// <summary>
        /// 获取问题
        /// </summary>
        /// <param name="questionList">默认数字加减验证</param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetQuestion(Dictionary<string, string> questionList = null)
        {
            if (questionList == null)
            {
                questionList = GetQuestionDic(10);
            }
            return questionList.ToList()[this.RandomBuilder.GenerateInt(0, questionList.Count)];
        }        

        #region 内部方法
        /// <summary>
        /// 获取验证码字符串
        /// </summary>
        protected void GetVerifyCodeText()
        {
            if (this.VerifyCodeText.IsNullOrEmpty())
            {
                StringBuilder sb=new StringBuilder();
                if (this.HasNumber)
                {
                    sb.Append(Const.ArabicNumbers);
                }
                if (this.HasLowerLetter)
                {
                    sb.Append(Const.Lowercase.Replace("o",""));
                }
                if (this.HasUpperLetter)
                {
                    sb.Append(Const.Uppercase.Replace("O", ""));
                }
                if (this.HasChineseCharacter)
                {
                    sb.Append(Const.SimplifiedChinese);
                }
                this.VerifyCodeText = this.RandomBuilder.GenerateString(this.Length, sb.ToString());
            }
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        protected Bitmap GetVerifyCodeImage()
        {
            this.GetVerifyCodeText();
            Bitmap bitmap = new Bitmap(ImgWidth,ImgHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode=SmoothingMode.HighQuality;
                //清除整个绘图面并以指定背景色填充
                graphics.Clear(this.BackgroundColor);
                //创建画笔
                using (SolidBrush brush = new SolidBrush(this.IsRandomColor?GetRandomColor():this.FontColor))
                {
                    this.AddForeNoisePoint(bitmap);
                    this.AddBackgroundNoisePoint(bitmap,graphics);
                    //文字居中
                    StringFormat stringFormat=new StringFormat(StringFormatFlags.NoClip);
                    stringFormat.Alignment=StringAlignment.Center;
                    stringFormat.LineAlignment=StringAlignment.Center;

                    //字体样式
                    Font font=new Font(this.FontFamily,RandomBuilder.GenerateInt(this.FontSize-3,this.FontSize),FontStyle.Regular);

                    //验证码旋转，防止机器识别
                    char[] chars = this.VerifyCodeText.ToCharArray();

                    for (int i = 0; i < chars.Length; i++)
                    {
                        //转动的角度
                        float angle = RandomBuilder.GenerateInt(-this.RandomAngle, this.RandomAngle);

                        graphics.TranslateTransform(12,12);
                        graphics.RotateTransform(angle);
                        graphics.DrawString(chars[i].ToString(),font,brush,-2,2,stringFormat);
                        graphics.RotateTransform(-angle);
                        graphics.TranslateTransform(2,-12);
                    }
                }
            }
            return bitmap;
        }

        /// <summary>
        /// 添加前景噪点
        /// </summary>
        /// <param name="bitmap">位图</param>
        protected void AddForeNoisePoint(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width*this.ForeNoisePointCount; i++)
            {
                var x = RandomBuilder.GenerateInt(bitmap.Width-1);
                var y = RandomBuilder.GenerateInt(bitmap.Height-1);
                bitmap.SetPixel(x, y, this.FontColor);
            }
        }

        /// <summary>
        /// 添加背景噪点
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <param name="graphics">画图工具</param>
        protected void AddBackgroundNoisePoint(Bitmap bitmap, Graphics graphics)
        {
            using (Pen pen=new Pen(Color.Azure,0))
            {
                for (int i = 0; i < bitmap.Width*2; i++)
                {
                    graphics.DrawRectangle(pen,RandomBuilder.GenerateInt(bitmap.Width),RandomBuilder.GenerateInt(bitmap.Height),1,1);
                }
            }
        }

        /// <summary>
        /// 获取随机颜色
        /// </summary>
        /// <returns></returns>
        protected Color GetRandomColor()
        {            
            System.Random random=new System.Random((int)DateTime.Now.Ticks);
            Thread.Sleep(random.Next(50));
            System.Random random2=new System.Random((int)DateTime.Now.Ticks);
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = (red + green > 400) ? 0 : 400 - red - green;
            blue = (blue > 255) ? 255 : blue;
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// 获取问题字典
        /// </summary>
        /// <param name="maxLength">最大问题长度</param>
        /// <returns></returns>
        protected Dictionary<string, string> GetQuestionDic(int maxLength)
        {
            Dictionary<string,string> dictionary=new Dictionary<string, string>();
            string[] operArray = new[] {"+", "-", "*", "num"};
            for (int i = 0; i < maxLength; i++)
            {
                int left = this.RandomBuilder.GenerateInt(0, 100);
                int right = this.RandomBuilder.GenerateInt(0, 100);
                string oper = operArray[this.RandomBuilder.GenerateInt(0, operArray.Length)];
                string key = string.Empty;
                string value = string.Empty;
                switch (oper)
                {
                    case "+":
                        key=string.Format("{0}+{1}=?",left,right);
                        value = (left + right).ToString();
                        break;
                    case "-":
                        key = string.Format("{0}-{1}=?", left, right);
                        value = (left - right).ToString();
                        break;
                    case "*":
                        key = string.Format("{0}*{1}=?", left, right);
                        value = (left * right).ToString();
                        break;
                    default:
                        key = value = this.RandomBuilder.GenerateInt(1000, 9999).ToString();
                        break;
                }
                dictionary.Add(key, value);
            }
            return dictionary;
        }
        #endregion
    }
}