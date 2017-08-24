using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Medias;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Medias
{
    [TestClass]
    public class ImageUtilTest
    {
        [TestMethod]
        public void TestImageWatermark()
        {
            string imgUrl = "D:\\20170623143033_5077.jpg";
            string waterUrl = "D:\\鸿荣水印图.png";
            string result=ImageUtil.ImageWatermark(imgUrl, waterUrl, ImageLocationMode.Center);
            Console.WriteLine(result);
        }
    }
}
