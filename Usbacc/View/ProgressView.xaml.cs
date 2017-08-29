using System.ComponentModel;
using Usbacc.UI.Properties;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для ProgressView.xaml
    /// </summary>
    public partial class ProgressView : INotifyPropertyChanged
    {
        public ProgressView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _message = "0%";
        public string Message
        {
            get { return _message; } 
            set { _message = value; OnPropertyChanged("Message"); }
        }

        public int Minimum { get; set; }
        public int Maximum { get; set; }
        
        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value; 
                OnPropertyChanged("Value");

                if (_value == Maximum) Close(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
