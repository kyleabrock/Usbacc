using System.Windows;
using Usbacc.Core.Domain;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для UsbRecordTableView.xaml
    /// </summary>
    public partial class UsbRecordFullTableView
    {
        public UsbRecordFullTableView()
        {
            InitializeComponent();
            
            if (ViewModel.AddDeviceAccountAction == null)
                ViewModel.AddDeviceAccountAction = AddDeviceAccount;
            if (ViewModel.FindDeviceAccountAction == null)
                ViewModel.FindDeviceAccountAction = FindDeviceAccount;
            if (ViewModel.ShowChanges == null)
                ViewModel.ShowChanges = MainDataGrid.Items.Refresh;
        }

        private void AddDeviceAccount()
        {
            var record = ViewModel.SelectedItem as UsbRecord;
            if (record != null)
            {
                var dialog = new DeviceAccountAddView(record) {Owner = Window.GetWindow(this)};
                dialog.Closed += (s, e) => ViewModel.RefreshCommand.Execute(null);
                dialog.ShowDialog();
            }
        }

        private void FindDeviceAccount()
        {
            var record = ViewModel.SelectedItem as UsbRecord;
            if (record != null)
            {
                var dialog = new UsbRecordInReportsView(record) { Owner = Window.GetWindow(this) };
                dialog.Closed += (s, e) => ViewModel.RefreshCommand.Execute(null);
                dialog.ShowDialog();
            }
        }
    }
}
