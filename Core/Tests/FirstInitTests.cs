using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Usbacc.Core.Domain;
using Usbacc.Core.Import;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class FirstInitTests
    {
        [Test]
        public void StatusImportTest()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(EntityBase).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);

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

        [Test]
        public void DeviceAccountImportTest()
        {
            const string item = "C:\\Work\\TrustedUsb.xml";

            var converter = new Converter.DeviceAccountConverter();
            var result = converter.Convert(item);

            var repository = new Repository<DeviceAccount>();
            repository.Save(result);

            var fullList = repository.GetAll();

            Assert.Greater(fullList.Count, 0);
        }

        [Test]
        public void UsbImportTest()
        {
            var files = Directory.GetFiles("c:\\Work\\imp\\xml\\").ToList();
            var import = new UsbDeviewReportImport();
            import.Import(files);
        }

        [Test]
        public void TrustedUsbWithImportTest()
        {
            var repository = new UsbRecordRepository();
            var records = repository.GetAll();

            var deviceAccountsRepository = new DeviceAccountRepository();
            var deviceAccounts = deviceAccountsRepository.GetAll(true);

            foreach (var usbRecord in records)
            {
                usbRecord.RefreshStatus(deviceAccounts);
                if (usbRecord.Signature != null)
                {
                    var sign = usbRecord.Signature;
                    if (sign.UsbDevice.DeviceName == "*")
                    {
                        sign.UsbDevice = usbRecord.UsbDevice;
                        deviceAccountsRepository.Save(sign);
                    }
                }
            }
        }
    }
}
