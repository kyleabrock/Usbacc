using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для UsbRecordTableView.xaml
    /// </summary>
    public partial class UsbRecordTableView
    {
        public UsbRecordTableView(Report report)
        {
            InitializeComponent();
            ViewModel = new UsbRecordTableViewModel(report);
            DataContext = ViewModel;

            if (ViewModel.AddDeviceAccountAction == null)
                ViewModel.AddDeviceAccountAction = AddDeviceAccount;
            if (ViewModel.FindDeviceAccountAction == null)
                ViewModel.FindDeviceAccountAction = FindDeviceAccount;
            if (ViewModel.ShowChanges == null)
                ViewModel.ShowChanges = MainDataGrid.Items.Refresh;

            Title = "Отчет: " + report.ReportName;
        }

        private void AddDeviceAccount()
        {
            var record = ViewModel.SelectedItem as UsbRecord;
            if (record != null)
            {
                var dialog = new DeviceAccountAddView(record) {Owner = GetWindow(this)};
                dialog.Closed += (s, e) => ViewModel.RefreshCommand.Execute(null);
                dialog.ShowDialog();
            }
        }

        private void FindDeviceAccount()
        {
            var record = ViewModel.SelectedItem as UsbRecord;
            if (record != null)
            {
                var dialog = new UsbRecordInReportsView(record) { Owner = GetWindow(this) };
                dialog.Closed += (s, e) => ViewModel.RefreshCommand.Execute(null);
                dialog.ShowDialog();
            }
        }
    }
}
