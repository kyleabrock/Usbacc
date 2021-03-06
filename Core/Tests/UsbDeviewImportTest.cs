﻿using System.IO;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Usbacc.Core.Domain;
using Usbacc.Core.Import;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class UsbDeviewImportTest
    {
        [SetUp]
        public void GenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(EntityBase).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }

        [Test]
        public void UsbImportTest()
        {
            var files = Directory.GetFiles("c:\\Work\\Import\\").ToList();
            var import = new UsbDeviewReportImport();
            import.Import(files);
        }
    }
}
