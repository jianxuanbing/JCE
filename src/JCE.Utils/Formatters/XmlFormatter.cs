using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JCE.Utils.Formatters
{
    /// <summary>
    /// Xml 格式化器
    /// </summary>
    public class XmlFormatter
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="input">输入值</param>
        /// <returns></returns>
        public static string Formate(string input)
        {
            XmlDocument xd=new XmlDocument();
            xd.LoadXml(input);
            StringBuilder sb=new StringBuilder();
            StringWriter writer=new StringWriter(sb);
            XmlTextWriter xmlTxtWriter = null;
            try
            {
                xmlTxtWriter = new XmlTextWriter(writer);
                xmlTxtWriter.Formatting = Formatting.Indented;
                xmlTxtWriter.Indentation = 1;
                xmlTxtWriter.IndentChar = '\t';
                xd.WriteTo(xmlTxtWriter);
            }
            finally
            {
                if (xmlTxtWriter != null)
                {
                    xmlTxtWriter.Close();
                }
            }
            return sb.ToString();
        }
    }
}
