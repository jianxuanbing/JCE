using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Webs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Webs
{
    [TestClass]
    public class WebUtilTest
    {

        [TestMethod]
        public void TestDownload()
        {
            string url =
                "http://26241.live-vod.cdn.aodianyun.com/m3u8/0x0/Test_xymn.stream.1506738651/Test_xymn.stream.1506738651.m3u8";
            string fileName = "测试.mp4";
            WebUtil.DownloadUrl(url,fileName);
        }
    }
}
