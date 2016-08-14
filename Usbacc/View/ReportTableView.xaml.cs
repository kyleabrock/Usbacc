using System;
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
            SetActions();
        }

        public void Refresh()
        {
            if (RefreshButton.Command.CanExecute(null))
                RefreshButton.Command.Execute(null);
        }

        private void SetActions()
        {
            //if (ViewModel.AddAction == null)
            //    ViewModel.AddAction = DisplayAddDialog;
            //if (ViewModel.EditAction == null)
            //    ViewModel.EditAction = DisplayEditDialog;
        }

        private void DisplayAddDialog()
        {
            //var dialog = new CardAddView { Owner = Window.GetWindow(this) };
            //dialog.Closed += (s, e) => dialog.Owner.Focus();
            //dialog.Show();
        }

        private void DisplayEditDialog()
        {
            var item = ViewModel.SelectedItem as Report;
            if (item != null)
            {
                var dialog = new UsbRecordTableView(item) { Owner = Window.GetWindow(this) };
                dialog.Closed += (s, e) => dialog.Owner.Focus();
                dialog.Show();
            }
        }

        private void MainDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.MouseDevice.DirectlyOver as FrameworkElement;
            if (element != null && element.Parent is DataGridCell)
            {
                var datagrid = sender as DataGrid;
                if (datagrid != null)
                    DisplayEditDialog();
            }
        }

        private void FilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FilterGridColumn.Width.IsAbsolute && Math.Abs(FilterGridColumn.Width.Value) <= 0.0)
                FilterGridColumn.Width = new GridLength(1, GridUnitType.Auto);
            else
            {
                FilterGridColumn.Width = new GridLength(0, GridUnitType.Pixel);
            }
        }
    }
}
