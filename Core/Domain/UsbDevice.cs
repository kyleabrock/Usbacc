using System;

namespace UsbAcc.Core.Domain
{
    public class UsbDevice : EntityBase
    {
        public virtual Report Report { get; set; }

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

        private DateTime _createdDateTime = DateTime.MinValue;
        public virtual DateTime CreatedDateTime
        {
            get { return _createdDateTime; }
            set { _createdDateTime = value; }
        }

        private DateTime _lastPlugDateTime = DateTime.MinValue;
        public virtual DateTime LastPlugDateTime
        {
            get { return _lastPlugDateTime; }
            set { _lastPlugDateTime = value; }
        }
    }
}
