using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Http.Enums;
using JCE.Utils.Http.Extensions;
using JCE.Utils.Http.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Http.Test
{
    [TestClass]
    public class HttpRequestParameterTest
    {
        [TestMethod]
        public void TestGet()
        {
            HttpRequestParameter request=new HttpRequestParameter();
            request.Method=HttpMethod.Get;
            request.AddressUrl = "http://localhost:8094/api/Test/GetDefaultConfig";
            var result =request.SendReq();
            string content = result.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }
    }
}
