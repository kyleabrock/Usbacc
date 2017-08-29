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

        public virtual Status Status { get; protected set; }

        public virtual DeviceAccount Signature { get; protected set; }

        public virtual void RefreshStatus(IEnumerable<DeviceAccount> deviceAccounts)
        {
            foreach (var deviceAccount in deviceAccounts)
            {
                if (UsbDevice.Compare(deviceAccount.UsbDevice))
                {
                    Status = deviceAccount.Status;
                    Signature = deviceAccount;
                }
            }

            if (Status == null)
            {
                Status = Status.Untrusted;
                Signature = new DeviceAccount();
            }
        }
    }
}
