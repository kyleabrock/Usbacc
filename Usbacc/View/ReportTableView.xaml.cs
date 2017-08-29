using System.Windows;
using System.Windows.Input;
using Microsoft.Windows.Controls;
using Usbacc.Core.Domain;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для ResourceFlowWindow.xaml
    /// </summary>
    public partial class ReportTableView
    {
        public ReportTableView()
        {
            InitializeComponent();

            if (ViewModel.ImportAction == null)
                ViewModel.ImportAction = ImportAction;
            if (ViewModel.GetFileNames == null)
                ViewModel.GetFileNames = GetFileNames;
            if (ViewModel.ShowInfoMessage == null)
                ViewModel.ShowInfoMessage = s => MessageBox.Show(s);
            if (ViewModel.SaveWithProgress == null)
                ViewModel.SaveWithProgress = SaveWithProgress;
            if (ViewModel.EditAction == null)
                ViewModel.EditAction = EditAction;
            if (ViewModel.DeleteAction == null)
                ViewModel.DeleteAction = s => (MessageBox.Show(s, "", MessageBoxButton.OKCancel) == MessageBoxResult.OK);
            
            RefreshButton.Command.Execute(null);
        }

        private void EditAction(Report report)
        {
            var dialog = new ReportEditView(report) {Owner = Window.GetWindow(this)};
            dialog.Closed += (s, e) => dialog.Owner.Focus();
            dialog.ShowDialog();
        }

        private void SaveWithProgress(string[] files)
        {
            var dialog = new SaveReportView(files) {Owner = Window.GetWindow(this)};
            dialog.Closed += (s, e) => dialog.Owner.Focus();
            dialog.ShowDialog();
        }

        private string[] GetFileNames()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "Файлы отчетов (*.xml)|*.xml|Все файлы (*.*)|*.*"
                };
            
            var result = dlg.ShowDialog();
            return result == true ? dlg.FileNames : new string[0];
        }

        private void ImportAction(string filePath)
        {
            var dialog = new ReportImportView(filePath) {Owner = Window.GetWindow(this)};
            dialog.Closed += (s, e) => dialog.Owner.Focus();
            dialog.ShowDialog();
        }

        private void MainDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.MouseDevice.DirectlyOver as FrameworkElement;
            if (element != null && element.Parent is DataGridCell)
            {
                var datagrid = sender as DataGrid;
                if (datagrid != null)
                {
                    var item = ViewModel.SelectedItem as Report;
                    if (item != null)
                    {
                        var dialog = new UsbRecordTableView(item) { Owner = Window.GetWindow(this) };
                        dialog.Closed += (s, j) => dialog.Owner.Focus();
                        dialog.Show();
                    }
                }
            }
        }
    }
}