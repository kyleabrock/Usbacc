using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class DeviceAccountAddViewModel : ViewModelBase
    {
        public DeviceAccountAddViewModel()
        {
            InitViewModel();
        }

        public DeviceAccountAddViewModel(UsbRecord record)
        {
            InitViewModel();
            var usbDevice = new UsbDevice
                {
                    DeviceName = record.UsbDevice.DeviceName,
                    DeviceType = record.UsbDevice.DeviceType,
                    Description = record.UsbDevice.Description,
                    VendorId = record.UsbDevice.VendorId,
                    ProductId = record.UsbDevice.ProductId,
                    SerialNumber = record.UsbDevice.SerialNumber
                };

            DeviceAccount.UsbDevice = usbDevice;
        }

        public DeviceAccount DeviceAccount
        {
            get { return _deviceAccount; }
            set { _deviceAccount = value; OnPropertyChanged("DeviceAccount"); }
        }

        public ObservableCollection<Status> StatusList { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action CloseAction { get; set; }

        private DeviceAccount _deviceAccount = new DeviceAccount();

        private void InitViewModel()
        {
            var statusRepository = new Repository<Status>();
            var statusList = statusRepository.GetAll(x => x.StatusName);
            StatusList = new ObservableCollection<Status>(statusList);

            SaveCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CancelMethod());
        }

        private void SaveMethod()
        {
            var repository = new Repository<DeviceAccount>();
            try
            {
                repository.Save(DeviceAccount);
                CloseAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelMethod()
        {
            CloseAction();
        }
    }
}
