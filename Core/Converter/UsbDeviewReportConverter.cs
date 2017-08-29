using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Usbacc.Core.Domain;

namespace Usbacc.Core.Converter
{
    public class UsbDeviewReportConverter
    {
        public IList<UsbRecord> Convert(string filePath)
        {
            if (!File.Exists(filePath))
            {
                //TODO: Throw exception
                return new List<UsbRecord>();
            }
            try
            {
                var doc = XDocument.Parse(File.ReadAllText(filePath, System.Text.Encoding.GetEncoding("Windows-1251")));
                var root = doc.Root;
                if (root == null) return new List<UsbRecord>();

                var itemList = root.Descendants(XName.Get("item"));
                var resultList = itemList.Select(GetUsbDevice).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                var fileName = Path.GetFileName(filePath);
                MessageBox.Show("Ошибка при обработке файла: " + fileName + ".\r\nОписание:" + ex.Message);
            }
            
            return new List<UsbRecord>();
        }

        private UsbRecord GetUsbDevice(XElement element)
        {
            var item = new UsbRecord();
            var usbDevice = new UsbDevice
                {
                    DeviceName = (string) element.Element(XName.Get("device_name")) ?? string.Empty,
                    Description = (string) element.Element(XName.Get("description")) ?? string.Empty,
                    DeviceType = (string) element.Element(XName.Get("device_type")) ?? string.Empty,
                    SerialNumber = (string) element.Element(XName.Get("serial_number")) ?? string.Empty,
                    VendorId = (string) element.Element(XName.Get("vendorid")) ?? string.Empty,
                    ProductId = (string) element.Element(XName.Get("productid")) ?? string.Empty
                };

            item.UsbDevice = usbDevice;

            var createdDate = new DateTime();
            var createdDateElement = (string) element.Element(XName.Get("created_date")) ?? string.Empty;
            if (!string.IsNullOrEmpty(createdDateElement))
                DateTime.TryParse(createdDateElement, out createdDate);

            var lastPlug = new DateTime();
            var lastPlugElement = (string)element.Element(XName.Get("last_plug_unplug_date")) ?? string.Empty;
            if (!string.IsNullOrEmpty(lastPlugElement))
                DateTime.TryParse(lastPlugElement, out lastPlug);

            item.CreatedDateTime = createdDate;
            item.LastPlugDateTime = lastPlug;
            
            return item;
        }
    }
}
