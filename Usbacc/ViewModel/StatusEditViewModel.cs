using System;
using System.Windows.Input;
using System.Windows.Media;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class StatusEditViewModel : ViewModelBase
    {
        public StatusEditViewModel()
        {
            SaveCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        public StatusEditViewModel(Status status)
        {
            Status = status;
            var convertFromString = ColorConverter.ConvertFromString(status.Color);
            if (convertFromString != null)
                Color = (Color)convertFromString;

            SaveCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        private Status _status = new Status();
        public Status Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged("Color"); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action<string> ShowInfoMessage { get; set; }
        public Action CloseAction { get; set; }

        private void SaveMethod()
        {
            var repository = new Repository<Status>();
            try
            {
                Status.Color = Color.ToString();
                repository.Save(Status);
                CloseAction();
            }
            catch (Exception ex)
            {
                ShowInfoMessage(ex.Message);
            }
        }
    }
}
