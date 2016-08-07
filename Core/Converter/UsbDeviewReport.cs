using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using UsbAcc.Core.Domain;

namespace UsbAcc.Core.Converter
{
    public class UsbDeviewReport
    {
        public IList<UsbDevice> Convert(string filePath)
        {
            if (!File.Exists(filePath))
            {
                //TODO: Throw exception
                return new List<UsbDevice>();
            }
            
            var doc = new XmlDocument();
            doc.Load(filePath);

            var rootNode = doc.DocumentElement;
            if (rootNode == null)
            {
                //TODO: return Error
                return new List<UsbDevice>();
            }

            var itemList = rootNode.SelectNodes("item");
            if (itemList == null)
                return new List<UsbDevice>();

            var resultList = new List<UsbDevice>();
            for (int i = 0; i < itemList.Count; i++)
            {
                var item = new UsbDevice();
                var deviceNameNode = itemList[i].SelectSingleNode("./device_name");
                item.DeviceName = deviceNameNode != null ? deviceNameNode.InnerText : "";

                var descriptionNode = itemList[i].SelectSingleNode("./description");
                item.Description = descriptionNode != null ? descriptionNode.InnerText : "";

                var deviceTypeNode = itemList[i].SelectSingleNode("./device_type");
                item.DeviceType = deviceTypeNode != null ? deviceTypeNode.InnerText : "";

                var serialNumberNode = itemList[i].SelectSingleNode("./serial_number");
                item.SerialNumber = serialNumberNode != null ? serialNumberNode.InnerText : "";

                var vendorIdNode = itemList[i].SelectSingleNode("./vendorid");
                item.VendorId = vendorIdNode != null ? vendorIdNode.InnerText : "";

                var productIdNode = itemList[i].SelectSingleNode("./productid");
                item.ProductId = productIdNode != null ? productIdNode.InnerText : "";

                var createdDateTimeNode = itemList[i].SelectSingleNode("./created_date");
                if (createdDateTimeNode != null)
                {
                    DateTime createdDate;
                    if (DateTime.TryParse(createdDateTimeNode.InnerText, out createdDate))
                        item.CreatedDateTime = createdDate;
                }

                var lastPlugDateTimeNode = itemList[i].SelectSingleNode("./last_plug_unplug_date");
                if (lastPlugDateTimeNode != null)
                {
                    DateTime lastPlugDate;
                    if (DateTime.TryParse(lastPlugDateTimeNode.InnerText, out lastPlugDate))
                        item.LastPlugDateTime = lastPlugDate;
                }

                resultList.Add(item);
            }

            return resultList;
        }
    }
}
