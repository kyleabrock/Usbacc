using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Converter
{
    public class DeviceAccountConverter
    {
        public IList<DeviceAccount> Convert(string filePath)
        {
            if (!File.Exists(filePath))
            {
                //TODO: Throw exception
                return new List<DeviceAccount>();
            }
            
            var doc = XDocument.Parse(File.ReadAllText(filePath, System.Text.Encoding.UTF8));
            var root = doc.Root;
            if (root == null) return new List<DeviceAccount>();

            var itemList = root.Descendants(XName.Get("item"));
            _repository = new Repository<Status>();
            var resultList = itemList.Select(GetUsbDevice).ToList();

            return resultList;
        }

        private Repository<Status> _repository;

        private DeviceAccount GetUsbDevice(XElement element)
        {
            var item = new DeviceAccount();
            var usbDevice = new UsbDevice
                {
                    DeviceName = (string) element.Element(XName.Get("device_name")) ?? "*",
                    Description = (string) element.Element(XName.Get("description")) ?? "*",
                    DeviceType = (string) element.Element(XName.Get("device_type")) ?? "*",
                    SerialNumber = (string) element.Element(XName.Get("serial_number")) ?? "*",
                    VendorId = (string) element.Element(XName.Get("vendorid")) ?? "*",
                    ProductId = (string) element.Element(XName.Get("productid")) ?? "*"
                };

            item.UsbDevice = usbDevice;

            item.Department = (string) element.Element(XName.Get("department")) ?? string.Empty;
            item.User = (string) element.Element(XName.Get("user")) ?? string.Empty;
            item.RegNumber = (string) element.Element(XName.Get("reg_number")) ?? string.Empty;
            item.Comments = (string) element.Element(XName.Get("comments")) ?? string.Empty;

            var status = int.Parse((string) element.Element(XName.Get("status")) ?? "0");
            item.Status = _repository.GetById(status);

            return item;
        }
    }
}
