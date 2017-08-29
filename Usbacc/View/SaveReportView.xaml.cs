using System;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для SaveEntityBaseView.xaml
    /// </summary>
    public partial class SaveReportView
    {
        public SaveReportView(string[] files)
        {
            InitializeComponent();
            _viewModel = new SaveReportViewModel(files);
            DataContext = _viewModel;

            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        private readonly SaveReportViewModel _viewModel;

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
            _viewModel.ProcessCommand.Execute(null);
        }
    }
}
