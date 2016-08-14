using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для DeviceAccountAddView.xaml
    /// </summary>
    public partial class DeviceAccountAddView
    {
        public DeviceAccountAddView()
        {
            InitializeComponent();
            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }

        public DeviceAccountAddView(UsbRecord record)
        {
            InitializeComponent();
            ViewModel = new DeviceAccountAddViewModel(record);
            DataContext = ViewModel;

            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }
    }
}
