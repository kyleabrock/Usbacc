using System;
using System.Collections.Generic;
using System.IO;
using Usbacc.Core.Converter;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Import
{
    public class UsbDeviewReportImport
    {
        public Report GetReport(string filePath)
        {
            var reportConverter = new UsbDeviewReportConverter();
            var report = new Report
            {
                ReportName = Path.GetFileNameWithoutExtension(filePath),
                CreationDateTime = File.GetLastWriteTime(filePath)
            };

            var usbDevices = reportConverter.Convert(filePath);
            foreach (var usbDevice in usbDevices)
                usbDevice.Report = report;

            report.ReplaceUsbRecords(usbDevices);

            return report;
        }

        public Report GetReport(Report report, string filePath)
        {
            var reportConverter = new UsbDeviewReportConverter();
            var result = new Report
            {
                ReportName = report.ReportName,
                CreationDateTime = report.CreationDateTime,
                Comments = report.Comments
            };

            var usbDevices = reportConverter.Convert(filePath);
            report.ReplaceUsbRecords(usbDevices);
            
            return report;
        }


        public void Import(Report report, string filePath)
        {
            if (!report.IsNew)
                throw new Exception("Нельзя добавить данные в существующий отчет");

            var reportConverter = new UsbDeviewReportConverter();

            var usbDevices = reportConverter.Convert(filePath);
            foreach (var usbDevice in usbDevices)
                usbDevice.Report = report;

            report.ReplaceUsbRecords(usbDevices);

            var usbDeviceRepository = new Repository<UsbRecord>();
            usbDeviceRepository.Save(usbDevices);

            var reportRepository = new Repository<Report>();
            reportRepository.Save(report);
        }

        public void Import(string filePath)
        {
            var reportConverter = new UsbDeviewReportConverter();
            var report = new Report
            {
                ReportName = Path.GetFileNameWithoutExtension(filePath),
                CreationDateTime = File.GetLastWriteTime(filePath)
            };

            var usbDevices = reportConverter.Convert(filePath);
            foreach (var usbDevice in usbDevices)
                usbDevice.Report = report;

            report.ReplaceUsbRecords(usbDevices);

            var reportRepository = new Repository<Report>();
            reportRepository.Save(report);

            var usbDeviceRepository = new Repository<UsbRecord>();
            usbDeviceRepository.Save(usbDevices);
        }

        public void Import(IEnumerable<string> filesPath)
        {
            foreach (var filePath in filesPath)
                Import(filePath);
        }
    }
}
