using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Test.Samples
{
    /// <summary>
    /// 帐户管理
    /// </summary>
    public class AccountManager:IAccountManager
    {
        public void WriteInfo()
        {
            Console.WriteLine("我就是不服，就是注入一下看看");
        }
    }
}
