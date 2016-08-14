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

        public virtual IList<UsbRecord> UsbRecords { get; set; }

        private string _comments = "";
        public virtual string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
