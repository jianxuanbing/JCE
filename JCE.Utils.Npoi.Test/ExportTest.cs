using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Npoi.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Npoi.Test
{
    [TestClass]
    public class ExportTest
    {
        [TestMethod]
        public void ExportExcel()
        {
            List<ExportModel> entities=new List<ExportModel>();
            for (int i = 0; i < 1000; i++)
            {
                ExportModel entity=new ExportModel();
                entity.Id=Guid.NewGuid();
                entity.Name = "测试00" + i;
                entity.CreateTime=DateTime.Now.AddSeconds(i);
                entity.Sort = i;
                entity.Amount = i*20;
                entity.Sex = i%2 == 0 ? "男" : "女";
                entity.SexNum = i%2==0?1:0;
                entity.Power = i%3*10;

                entities.Add(entity);
            }

            entities.ToExcel("D:\\测试.xlsx");
        }
    }
}
