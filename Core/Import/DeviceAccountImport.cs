using System.Collections.Generic;
using System.IO;
using Usbacc.Core.Converter;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Import
{
    public class DeviceAccountImport
    {
        public void Import(string filePath)
        {
            var converter = new DeviceAccountConverter();
            var usbDevices = converter.Convert(filePath);

            var usbDeviceRepository = new Repository<DeviceAccount>();
            usbDeviceRepository.Save(usbDevices);
        }

        public void Import(IList<string> filesPath)
        {
            foreach (var filePath in filesPath)
                Import(filePath);
        }
    }
}
