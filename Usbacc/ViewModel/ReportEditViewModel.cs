using System;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class ReportEditViewModel : ViewModelBase
    {
        public ReportEditViewModel(Report report)
        {
            Report = report;
            SaveCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        public Report Report { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action<string> ShowInfoMessage { get; set; }
        public Action CloseAction { get; set; }

        private void SaveMethod()
        {
            var repository = new Repository<Report>();
            try
            {
                repository.Save(Report);
                CloseAction();
            }
            catch (Exception ex)
            {
                ShowInfoMessage(ex.Message);
            }
        }
    }
}
