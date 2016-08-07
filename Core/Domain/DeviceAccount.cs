namespace UsbAcc.Core.Domain
{
    public class DeviceAccount : EntityBase
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

        private string _user = "";
        public virtual string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _comments = "";
        public virtual string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
