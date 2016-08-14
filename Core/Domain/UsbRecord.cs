using System;
using System.Collections.Generic;

namespace Usbacc.Core.Domain
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

        private Status _status;
        public virtual Status Status
        {
            get
            {
                if (_status != null)
                    return _status;
                
                return Status.Untrusted;
            }

            set { _status = value; }
        }

        public virtual DeviceAccount Signature { get; set; }

        public virtual void RefreshStatus(IEnumerable<DeviceAccount> deviceAccounts)
        {
            foreach (var deviceAccount in deviceAccounts)
            {
                if (UsbDevice.Compare(deviceAccount.UsbDevice))
                {
                    _status = deviceAccount.Status;
                    Signature = deviceAccount;
                }
            }

            if (_status == null)
            {
                _status = Status.Untrusted;
                Signature = new DeviceAccount();
            }
        }
    }
}
