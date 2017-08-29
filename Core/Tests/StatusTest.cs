using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class StatusTest
    {
        //[SetUp]
        //public void GenerateSchema()
        //{
        //    var cfg = new Configuration();
        //    cfg.Configure();
        //    cfg.AddAssembly(typeof(EntityBase).Assembly);

        //    new SchemaExport(cfg).Execute(false, true, false);
        //}

        [Test]
        public void ImportTest()
        {
            var statusList = new List<Status>
                {
                    new Status {StatusName = "Секретариат", Color = "#FFFFFF", Comments = "Секретариат"},
                    new Status {StatusName = "Отдел ИТ", Color = "#FFFFFF", Comments = "Отдел ИТ"},
                    new Status {StatusName = "Группа Ш", Color = "#FFFFFF", Comments = "Ш"},
                    new Status {StatusName = "Секретариат (У)", Color = "#FFFFFF", Comments = "Секретариат"},
                    new Status {StatusName = "Отдел ИТ (У)", Color = "#FFFFFF", Comments = "Отдел ИТ"},
                    new Status {StatusName = "Группа Ш (У)", Color = "#FFFFFF", Comments = "Ш"},
                    Status.Untrusted
                };

            var repository = new Repository<Status>();
            repository.Save(statusList);
            
            var items = repository.GetAll();
            Assert.Greater(items.Count, 0);
        }


    }
}
