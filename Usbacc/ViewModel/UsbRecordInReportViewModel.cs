using System.Collections.ObjectModel;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.UI.ViewModel
{
    public class UsbRecordInReportViewModel : Base.ViewModelBase
    {
        public UsbRecordInReportViewModel()
        {
            
        }

        public UsbRecordInReportViewModel(UsbRecord record)
        {
            _record = record;
            var repository = new ReportRepository();
            var result = repository.GetByUsbRecord(_record);
            ReportsList = new ObservableCollection<Report>(result);
        }

        public ObservableCollection<Report> ReportsList { get; private set; }
        public object SelectedItem { get; set; }

        private readonly UsbRecord _record;
    }
}
