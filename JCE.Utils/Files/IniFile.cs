/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Files
 * 文件名：IniFile
 * 版本号：v1.0.0.0
 * 唯一标识：36d27806-e1e0-4d74-80a0-855c0965c8e2
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:34:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:34:05
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Files
{
    /// <summary>
    /// INI文件读写类
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// Ini文件路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="IniFile"/>类型的实例
        /// </summary>
        /// <param name="path"></param>
        public IniFile(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// 将信息写入INI文件
        /// </summary>
        /// <param name="section">要写入的区域名。</param>
        /// <param name="key">key的名称。若传入null值，将移除指定的section。</param>
        /// <param name="val">设置key所对应的值。若传入null值，将移除指定的key。</param>
        /// <param name="filePath">INI文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 将信息从INI文件中读入程序中的变量
        /// </summary>
        /// <param name="section">section名</param>
        /// <param name="key">键名</param>
        /// <param name="defVal">默认值，如果INI文件没有前两个参数指定的字段名或键名，则将此值赋给变量</param>
        /// <param name="retVal">接收INI文件中的值，即目的缓存器</param>
        /// <param name="size">目的缓存器大小</param>
        /// <param name="filePath">INI文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 将信息从INI文件中读入程序中的变量
        /// </summary>
        /// <param name="section">要读区的区域名。若传入null值，第4个参数returnBuffer将会获得所有的section name。</param>
        /// <param name="key">key的名称。若传入null值，第4个参数returnBuffer将会获得所有的指定sectionName下的所有key name。</param>
        /// <param name="defVal">默认值，key没找到时的返回值。</param>
        /// <param name="retVal">接收INI文件中的值，即目的缓存器</param>
        /// <param name="size">目的缓存器大小</param>
        /// <param name="filePath">INI文件路径</param>
        /// <returns></returns>

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);

        /// <summary>
        /// 获取指定key的值
        /// </summary>
        /// <param name="sectionName">section名</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public string GetValue(string sectionName, string key)
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(sectionName, key, "发生错误", buffer, 999, this.Path);
            string rs = Encoding.Default.GetString(buffer, 0, length);
            return rs;
        }

        /// <summary>
        /// 获取Ini文件所有的section名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetSectionNames()
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(null, "", "", buffer, 999, this.Path);
            string[] rs = Const.DefaultEncoding.GetString(buffer, 0, length)
                .Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            return rs.ToList();
        }

        /// <summary>
        /// 获取指定section下的所有key名称
        /// </summary>
        /// <param name="sectionName">section名</param>
        /// <returns></returns>
        public List<string> GetKeys(string sectionName)
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(sectionName, null, "", buffer, 999, this.Path);
            string[] rs = Const.DefaultEncoding.GetString(buffer, 0, length)
                .Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            return rs.ToList();
        }

        /// <summary>
        /// 保存内容到ini文件
        /// <para>若存在相同的key，就覆盖，否则就增加</para>
        /// </summary>
        /// <param name="section">section名</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public bool SetValue(string section, string key, string value)
        {
            long rs = WritePrivateProfileString(section, key, value, this.Path);
            return rs > 0;
        }

        /// <summary>
        /// 移除指定的section
        /// </summary>
        /// <param name="section">section名</param>
        /// <returns></returns>
        public bool RemoveSection(string section)
        {
            return SetValue(section, null, null);
        }

        /// <summary>
        /// 移除指定的key
        /// </summary>
        /// <param name="section">section名</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public bool RemoveKey(string section, string key)
        {
            return SetValue(section, key, null);
        }

        /// <summary>
        /// 清空INI文件信息
        /// </summary>
        public bool Clear()
        {
            return SetValue(null, null, null);
        }

    }
}
