namespace Usbacc.Core.Domain
{
    public class UsbDevice
    {
        private string _deviceName = "";
        public virtual string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _deviceType = "";
        public virtual string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }

        private string _description = "";
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _vendorId = "";
        public virtual string VendorId
        {
            get { return _vendorId; }
            set { _vendorId = value; }
        }

        private string _productId = "";
        public virtual string ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private string _serialNumber = "";
        public virtual string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public virtual bool Compare(UsbDevice device)
        {
            bool vendorId = string.IsNullOrEmpty(device.VendorId) && device.VendorId != "*";
            bool productId = string.IsNullOrEmpty(device.ProductId) && device.ProductId != "*";
            bool serialNumber = string.IsNullOrEmpty(device.SerialNumber) && device.SerialNumber != "*";

            return SerialNumber.ToLower() == device.SerialNumber.ToLower();
            //if (serialNumber)
            //    return CompareMainParams(device);
            //if (vendorId && productId && serialNumber)
            //    return CompareMainParams(device);
            //return CompareAll(device);
        }

        private bool CompareAll(UsbDevice device)
        {
            var deviceName = (device.DeviceName == "*") || DeviceName.ToLower() == device.DeviceName.ToLower();
            var deviceType = (device.DeviceType == "*") || DeviceType.ToLower() == device.DeviceType.ToLower();
            var description = (device.Description == "*") || Description.ToLower() == device.Description.ToLower();
            var vendorId = (device.VendorId == "*") || VendorId.ToLower() == device.VendorId.ToLower();
            var productId = (device.ProductId == "*") || ProductId.ToLower() == device.ProductId.ToLower();
            var serialNumber = (device.SerialNumber == "*") || SerialNumber.ToLower() == device.SerialNumber.ToLower();

            var result = deviceName && deviceType && description &&
                vendorId && productId && serialNumber;

            return result;
        }

        private bool CompareMainParams(UsbDevice device)
        {
            var vendorId = (device.VendorId == "*") || VendorId.ToLower() == device.VendorId.ToLower();
            var productId = (device.ProductId == "*") || ProductId.ToLower() == device.ProductId.ToLower();
            var serialNumber = (device.SerialNumber == "*") || SerialNumber.ToLower() == device.SerialNumber.ToLower();

            var result = serialNumber;
            //var result = vendorId && productId && serialNumber;

            return result;
        }
    }
}
