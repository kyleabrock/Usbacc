using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для UsbRecordInReportsView.xaml
    /// </summary>
    public partial class UsbRecordInReportsView
    {
        public UsbRecordInReportsView(UsbRecord record)
        {
            InitializeComponent();
            
            ViewModel = new UsbRecordInReportViewModel(record);
            DataContext = ViewModel;
        }
    }
}
