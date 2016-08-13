namespace UsbAcc.Core.Domain
{
    public class DeviceAccount : EntityBase
    {
        public virtual UsbDevice UsbDevice { get; set; }

        private string _user = "";
        public virtual string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _department = "";
        public virtual string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        private string _registrator = "";
        public virtual string Registrator
        {
            get { return _registrator; }
            set { _registrator = value; }
        }

        private string _regNumber = "";
        public virtual string RegNumber
        {
            get { return _regNumber; }
            set { _regNumber = value; }
        }

        private string _comments = "";
        public virtual string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
