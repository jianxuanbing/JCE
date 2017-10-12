using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Http.Enums;
using JCE.Utils.Http.Extensions;
using JCE.Utils.Http.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpMethod = JCE.Utils.Http.Enums.HttpMethod;

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

        [TestMethod]
        public void TestPost()
        {
            HttpRequestParameter request=new HttpRequestParameter();
            request.Method = HttpMethod.Post;
            request.AddressUrl = "http://localhost:8094/oauth2/token";
            request.FormParameters.AddRange(new List<FormParameter>() {new FormParameter("username","zyl"),new FormParameter("password","123456"),new FormParameter("grant_type","password"),new FormParameter("scope","admin")});
            var result = request.SendReq();
            string content = result.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        [TestMethod]
        public void TestGetSellGoods()
        {
            HttpRequestParameter request = new HttpRequestParameter();
            request.Method = HttpMethod.Post;
            request.AddressUrl = "http://localhost:8094/api/sellGoods/getPageList";
            var obj = new
            {
                condition = new
                {
                    shopType=101,
                    name="",
                    deptId="",
                    sellType=-1,
                    sort = new
                    {
                        type=0,
                        desc=true,
                    }
                },
                pageSize=15,
                page=1
            };
            request.CustomBody = obj.ToJson();            
            var result = request.SendReq();
            string content = result.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        [TestMethod]
        public void TestLss()
        {
            HttpRequestParameter request = new HttpRequestParameter();
            request.Method = HttpMethod.Post;
            request.AddressUrl = "http://openapi.aodianyun.com/v2/LSS.GetAppStreamLiving";
            var obj = new
            {
                access_id= "",
                access_key= "",
                appid= ""
            };
            request.CustomBody = obj.ToJson();
            var result = request.SendReq();
            string content = result.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        [TestMethod]
        public void TestWms()
        {
            HttpRequestParameter request = new HttpRequestParameter();
            request.Method = HttpMethod.Post;
            request.AddressUrl = "http://127.0.0.1/EWMSService";
            var obj = new
            {
                sign = "c799e4d1fef21064cfc51418d009224a",
                content = new
                {
                    merchandises = new[]
                    {
                        new
                        {
                            price = 200.5,
                            name = "测试商品0001",
                            customerNo = "testgoods0001"
                        }
                    },
                    barcodes = new[]
                    {
                        new
                        {
                            customBC = "testSKU010001",
                            color = "颜色备注",
                            customerNo = "testgoods0001",
                        }
                    },
                },
                owner = "TGWMS",
                service = "synMerchandise",
                vertifyCode = "6B45B35B5F56915FE8B5297FE6EC8F13",
                version = "1.0"
            };
            request.CustomBody = obj.ToJson();
            var result = request.SendReq();
            string content = result.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        [TestMethod]
        public void TestWms2()
        {
            HttpRequestParameter request = new HttpRequestParameter();
            request.Method = HttpMethod.Post;
            request.AddressUrl = "http://127.0.0.1/EWMSService";
            var obj = new
            {
                sign = "c799e4d1fef21064cfc51418d009224a",
                content = new
                {
                    merchandises = new[]
                    {
                        new
                        {
                            price = 200.5,
                            name = "测试商品0001",
                            customerNo = "testgoods0001"
                        }
                    },
                    barcodes = new[]
                    {
                        new
                        {
                            customBC = "testSKU010001",
                            color = "颜色备注",
                            customerNo = "testgoods0001",
                        }
                    },
                },
                owner = "TGWMS",
                service = "synMerchandise",
                vertifyCode = "6B45B35B5F56915FE8B5297FE6EC8F13",
                version = "1.0"
            };
            request.SetCustomBody(obj);
            var result = request.SendReq().Get<object>();
            Console.WriteLine(result);
        }
    }
}
