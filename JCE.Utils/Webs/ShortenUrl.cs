/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Webs
 * 文件名：ShortenUrl
 * 版本号：v1.0.0.0
 * 唯一标识：5d76f835-4096-40cc-bd85-9fa8e34dce4b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/22 23:28:44
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/22 23:28:44
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// 网址缩短工具
    /// 参考地址:http://www.cnblogs.com/xmlnode/p/4544302.html
    /// </summary>
    public class ShortenUrl
    {
        /// <summary>
        /// 随机序列
        /// </summary>
        const string Seq = "s9LFkgy5RovixI1aOf8UhdY3r4DMplQZJXPqebE0WSjBn7wVzmN2Gc6THCAKut";

        /// <summary>
        /// 数据文件路径
        /// </summary>
        private static string DataFile
        {
            get { return Sys.GetPhysicalPath("/Url.db"); }
        }
        /// <summary>
        /// 索引文件路径
        /// </summary>
        private static string IndexFile
        {
            get { return Sys.GetPhysicalPath("/Url.idx"); }
        }

        /// <summary>
        /// 批量添加网址，按顺序返回Key。如果输入的一组网址中有不合法的元素，则返回数组的相同位置（下标）的元素将为空
        /// </summary>
        /// <param name="urls">网址</param>
        /// <returns></returns>
        public static string[] AddUrl(string[] urls)
        {
            FileStream index = new FileStream(IndexFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream data = new FileStream(DataFile, FileMode.Append, FileAccess.Write);
            data.Position = data.Length;
            DateTime now = DateTime.Now;
            byte[] dt = BitConverter.GetBytes(now.ToBinary());
            int _Hits = 0;
            byte[] hits = BitConverter.GetBytes(_Hits);
            string[] resultKey = new string[urls.Length];
            int seekSeek = unchecked((int)now.Ticks);
            Random seekRand = new Random(seekSeek);
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            byte[] status = BitConverter.GetBytes(true);
            //index: ID(8) + Begin(8) + Length(2) + Hits(4) + DateTime(8) = 30
            for (int i = 0; i < urls.Length && i < 1000; i++)
            {
                if (urls[i].ToLower().Contains(host) || urls[i].Length == 0 || urls[i].Length > 4096) continue;
                long begin = data.Position;
                byte[] urlData = Encoding.UTF8.GetBytes(urls[i]);
                data.Write(urlData, 0, urlData.Length);
                byte[] buff = new byte[8];
                long last;
                if (index.Length >= 30) //读取上一条记录的ID
                {
                    index.Position = index.Length - 30;
                    index.Read(buff, 0, 8);
                    index.Position += 22;
                    last = BitConverter.ToInt64(buff, 0);
                }
                else
                {
                    last = 1000000; //起步ID，如果太小，生成的短网址会太短。
                    index.Position = 0;
                }
                long randKey = last + (long)GetRnd(seekRand);
                byte[] beginData = BitConverter.GetBytes(begin);
                byte[] lengthData = BitConverter.GetBytes((Int16)(urlData.Length));
                byte[] randKeyData = BitConverter.GetBytes(randKey);

                index.Write(randKeyData, 0, 8);
                index.Write(beginData, 0, 8);
                index.Write(lengthData, 0, 2);
                index.Write(hits, 0, hits.Length);
                index.Write(dt, 0, dt.Length);
                resultKey[i] = Mixup(randKey);
            }
            data.Close();
            index.Close();
            return resultKey;
        }

        /// <summary>
        /// 批量解析Key，按顺序返回一组长网址。
        /// </summary>
        /// <param name="keys">需要解析的key</param>
        /// <returns></returns>
        public static string[] ParseUrl(string[] keys)
        {
            FileStream index = new FileStream(IndexFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream data = new FileStream(DataFile, FileMode.Open, FileAccess.Read);
            byte[] buff = new byte[8];
            long[] ids = keys.Select(n => UnMixup(n)).ToArray();
            string[] result = new string[ids.Length];
            long right = (long)(index.Length / 30) - 1;
            long Right = right;
            for (int j = 0; j < ids.Length; j++)
            {
                long id = ids[j];
                long left = 0;
                long middle = -1;
                while (left <= Right)
                {
                    middle = (long)(Math.Floor((double)((Right + left) / 2)));
                    if (middle < 0) break;
                    index.Position = middle * 30;
                    index.Read(buff, 0, 8);
                    long val = BitConverter.ToInt64(buff, 0);
                    if (val == id) break;
                    if (val < id)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        Right = middle - 1;
                    }
                }
                string url = null;
                if (middle != -1)
                {
                    index.Position = middle * 30 + 8; //跳过ID           
                    index.Read(buff, 0, buff.Length);
                    long begin = BitConverter.ToInt64(buff, 0);
                    index.Read(buff, 0, buff.Length);
                    Int16 length = BitConverter.ToInt16(buff, 0);
                    byte[] urlTxt = new byte[length];
                    data.Position = begin;
                    data.Read(urlTxt, 0, urlTxt.Length);
                    int hits = BitConverter.ToInt32(buff, 2);//跳过2字节的Length
                    byte[] newHits = BitConverter.GetBytes(hits + 1);//解析次数递增, 4字节
                    index.Position -= 6;//指针撤回到Length之后
                    index.Write(newHits, 0, newHits.Length);//覆盖老的Hits
                    url = Encoding.UTF8.GetString(urlTxt);
                }
                result[j] = url;
            }
            data.Close();
            index.Close();
            return result;
        }

        /// <summary>
        /// 混淆Id为字符串
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        private static string Mixup(long id)
        {
            string key = Convert(id);
            int s = 0;
            foreach (var c in key)
            {
                s += (int)c;
            }
            int len = key.Length;
            int x = (s % len);
            char[] arr = key.ToCharArray();
            char[] newarr = new char[arr.Length];
            Array.Copy(arr, x, newarr, 0, len - x);
            Array.Copy(arr, 0, newarr, len - x, x);
            string newKey = "";
            foreach (char c in newarr)
            {
                newKey += c;

            }
            return newKey;
        }

        /// <summary>
        /// 解析混淆字符串
        /// </summary>
        /// <param name="key">混淆字符串</param>
        /// <returns></returns>
        private static long UnMixup(string key)
        {
            int s = 0;
            foreach (char c in key)
            {
                s += (int)c;
            }
            int len = key.Length;
            int x = (s % len);
            x = len - x;
            char[] arr = key.ToCharArray();
            char[] newarr = new char[arr.Length];
            Array.Copy(arr, x, newarr, 0, len - x);
            Array.Copy(arr, 0, newarr, len - x, x);
            string newKey = "";
            foreach (char c in newarr)
            {
                newKey += c;

            }
            return Convert(newKey);
        }

        /// <summary>
        /// 将10进制转换为62进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string Convert(long num)
        {
            if (num < 62)
            {
                return Seq[(int)num].ToString();
            }
            int y = (int)(num % 62);
            long x = (long)(num / 62);
            return Convert(x) + Seq[y];
        }

        /// <summary>
        /// 将62进制转换为10进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static long Convert(string num)
        {
            long v = 0;
            int len = num.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                int t = Seq.IndexOf(num[i]);
                double s = (len - i) - 1;
                long m = (long)(Math.Pow(62, s) * t);
                v += m;
            }
            return v;
        }

        /// <summary>
        /// 生成随机的0-9a-zA-Z字符串
        /// </summary>
        /// <returns></returns>
        private static string GenerateKeys()
        {
            string[] chars =
                "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
                    .Split(',');
            int seek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seek);
            for (int i = 0; i < 100000; i++)
            {
                int r = seekRand.Next(1, chars.Length);
                string f = chars[0];
                chars[0] = chars[r - 1];
                chars[r - 1] = f;
            }
            return string.Join("", chars);
        }

        /// <summary>
        /// 返回随机递增步长
        /// </summary>
        /// <param name="seekRand">随机数</param>
        /// <returns></returns>
        private static int GetRnd(Random seekRand)
        {
            int step = seekRand.Next(1, 1);
            return step;
        }
    }
}
