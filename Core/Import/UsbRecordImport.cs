using System.Collections.Generic;
using System.IO;
using Usbacc.Core.Converter;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Import
{
    public class UsbRecordImport
    {
        public void Import(string filePath)
        {
            var reportConverter = new UsbDeviewConverter();
            var report = new Report
            {
                ReportName = Path.GetFileNameWithoutExtension(filePath),
                CreationDateTime = File.GetLastWriteTime(filePath)
            };

            var usbDevices = reportConverter.Convert(filePath);
            foreach (var usbDevice in usbDevices)
                usbDevice.Report = report;

            report.UsbRecords = usbDevices;

            var reportRepository = new Repository<Report>();
            reportRepository.Save(report);

            var usbDeviceRepository = new Repository<UsbRecord>();
            usbDeviceRepository.Save(usbDevices);
        }

        public void Import(IList<string> filesPath)
        {
            foreach (var filePath in filesPath)
                Import(filePath);
        }
    }
}
