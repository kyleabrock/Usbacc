using System;
using System.Collections.Generic;

namespace Usbacc.Core.Domain
{
    public class Report : EntityBase
    {
        private string _reportName = "";
        public virtual string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        private DateTime _creationDateTime = DateTime.Now;
        public virtual DateTime CreationDateTime
        {
            get { return _creationDateTime; }
            set { _creationDateTime = value; }
        }

        public virtual IList<UsbRecord> UsbRecords { get; protected set; }

        public virtual void AddUsbRecord(UsbRecord record)
        {
            if (UsbRecords == null) UsbRecords = new List<UsbRecord>();

            record.Report = this;
            if (!UsbRecords.Contains(record))
                UsbRecords.Add(record);
        }

        public virtual void RemoveUsbRecord(UsbRecord record)
        {
            if (UsbRecords == null) return;

            record.Report = null;
            if (UsbRecords.Contains(record))
                UsbRecords.Remove(record);
        }

        public virtual void ReplaceUsbRecords(IList<UsbRecord> usbRecords)
        {
            foreach (var record in usbRecords)
                record.Report = this;
            UsbRecords = usbRecords;
        }

        private string _comments = "";
        public virtual string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
