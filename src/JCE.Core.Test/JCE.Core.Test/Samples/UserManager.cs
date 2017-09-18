using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Test.Samples
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager:IUserManager
    {
        private IAccountManager _accountManager;
        public UserManager(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public void WriteInfo()
        {
            Console.WriteLine("注入成功！嘿嘿嘿");
            _accountManager.WriteInfo();
        }
    }
}
