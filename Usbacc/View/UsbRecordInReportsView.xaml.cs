using System.Windows.Input;
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

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                //TODO: Replace ViewModel.SelectedItem with proper MVVM
                var report = ViewModel.SelectedItem as Report;
                if (report != null)
                {
                    var dialog = new UsbRecordTableView(report) {Owner = GetWindow(this)};
                    dialog.Closed += (s, j) => dialog.Owner.Focus();
                    dialog.Show();
                }
            }
            
        }
    }
}
