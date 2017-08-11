using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Helpers
{
    /// <summary>
    /// Json测试
    /// </summary>
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void TestLoop()
        {
            List<Item> list=new List<Item>();
            list.Add(new Item("测试1","001") );
            list.Add(new Item("测试2", "002"));
            list.Add(new Item("测试3", "003"));
            var result = list.ToJson();
            Console.WriteLine(list.ToJson());
            Console.WriteLine(JsonFormatter.Format(result));
        }
    }
}
