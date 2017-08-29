using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для ReportImportView.xaml
    /// </summary>
    public partial class ReportImportView
    {
        public ReportImportView(string filePath)
        {
            InitializeComponent();

            ViewModel = new ReportImportViewModel(filePath);
            DataContext = ViewModel;

            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }
    }
}
