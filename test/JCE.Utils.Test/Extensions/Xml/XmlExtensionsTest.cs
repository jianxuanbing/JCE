using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Test.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Extensions.Xml
{
    [TestClass]
    public class XmlExtensionsTest
    {

        [TestMethod]
        public void TestSerializeToXml()
        {
            var obj = new User
            {
                Id = Guid.NewGuid(),
                Name = "jian玄冰",
                Sex = "男",
                Address = "广州市天河区大观中路"
            };
            var result = obj.SerializeToXml();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestDeserializeXml()
        {
            var xml = "<?xml version=\"1.0\"?>" +
                      "<User xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                      "<Id>5add0519-74db-4493-a37a-e94743360295</Id>" +
                      "<Name>jian玄冰</Name>" +
                      "<Sex>男</Sex>" +
                      "<Address>广州市天河区大观中路</Address>" +
                      "</User>";
            var obj = xml.DeserializeXml<User>();
            Console.WriteLine(obj.Name);
        }
    }
}
