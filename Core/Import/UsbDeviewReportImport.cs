using System.Collections.Generic;
using System.IO;
using UsbAcc.Core.Converter;
using UsbAcc.Core.Domain;
using UsbAcc.Core.Repository;

namespace UsbAcc.Core.Import
{
    public class UsbDeviewReportImport
    {
        public void Import(string filePath)
        {
            
        }

        public void Import(IList<string> filesPath)
        {
            var reportConverter = new UsbDeviewReport();
            foreach (var filePath in filesPath)
            {
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
        }
    }
}
