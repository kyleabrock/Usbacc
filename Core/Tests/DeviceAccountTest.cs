using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class DeviceAccountTest
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
        public void ConvertTest()
        {
            const string item = "C:\\Work\\TrustedUsb.xml";
            
            var converter = new Converter.DeviceAccountConverter();
            var result = converter.Convert(item);
            
            Assert.Greater(result.Count, 0);
        }

        [Test]
        public void ImportTest()
        {
            const string item = "C:\\Work\\TrustedUsb.xml";

            var converter = new Converter.DeviceAccountConverter();
            var result = converter.Convert(item);

            var repository = new Repository<DeviceAccount>();
            repository.Save(result);

            var fullList = repository.GetAll();

            Assert.Greater(fullList.Count, 0);
        }
    }
}
