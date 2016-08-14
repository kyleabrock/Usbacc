namespace Usbacc.Core.Domain
{
    public class Status : EntityBase
    {
        private string _statusName = "";
        public virtual string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        private string _color = "";
        public virtual string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private string _comments = "";
        public virtual string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public static Status Untrusted
        {
            get { return new Status { Color = "#DA4F49", StatusName = "Не доверенные" }; }
        }
    }
}
