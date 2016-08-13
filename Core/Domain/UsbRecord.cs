using System;

namespace UsbAcc.Core.Domain
{
    public class UsbRecord : EntityBase
    {
        public virtual Report Report { get; set; }

        public virtual UsbDevice UsbDevice { get; set; }

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
