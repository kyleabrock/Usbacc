using System.Windows;
using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для ReportEditView.xaml
    /// </summary>
    public partial class ReportEditView
    {
        public ReportEditView(Report report)
        {
            InitializeComponent();
            
            _viewModel = new ReportEditViewModel(report);
            DataContext = _viewModel;

            if (_viewModel.ShowInfoMessage == null)
                _viewModel.ShowInfoMessage = s => MessageBox.Show(s);
            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        private readonly ReportEditViewModel _viewModel;
    }
}
