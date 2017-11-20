using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Modes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Modes
{
    [TestClass]
    public class TimestampIdTest
    {
        [TestMethod]
        public void Test_GetId()
        {
            var result=TimestampId.GetInstance().GetId();
            Console.WriteLine(result);
        }
    }
}
