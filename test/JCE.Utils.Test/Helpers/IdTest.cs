using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Helpers
{
    [TestClass]
    public class IdTest
    {
        [TestMethod]
        public void TestCreate()
        {
            var result = Id.Create();
            Console.WriteLine(result);
        }
    }
}
