using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class UsbRecordFullTableViewModel : TableSearchViewModel<UsbRecord>
    {
        public UsbRecordFullTableViewModel()
        {
            _repository = new UsbRecordRepository();
            InitViewModel();
        }

        public ICommand AddDeviceAccountCommand { get; set; }
        public ICommand FindDeviceAccountCommand { get; set; }
        public Action ShowChanges { get; set; }
        public Action AddDeviceAccountAction { get; set; }
        public Action FindDeviceAccountAction { get; set; }

        private readonly UsbRecordRepository _repository;
        private IList<UsbRecord> _records;

        private void InitViewModel()
        {
            AddDeviceAccountCommand = new RelayCommand(x => AddDeviceAccountMethod());
            FindDeviceAccountCommand = new RelayCommand(x => FindDeviceAccountMethod());

            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
        }

        private void AddDeviceAccountMethod()
        {
            AddDeviceAccountAction();
            RefreshCommand.Execute(null);
        }

        private void FindDeviceAccountMethod()
        {
            FindDeviceAccountAction();
        }

        private void RefreshCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TableItemListView = CollectionViewSource.GetDefaultView(TableItemList);
            
            LoadTableSortOrder();
            ShowChanges();
        }

        private void RefreshMethod()
        {
            _records = !IsSearched ? _repository.GetAll() : _repository.GetBySearchString(SearchString);

            var deviceAccountsRepository = new DeviceAccountRepository();
            var deviceAccounts = deviceAccountsRepository.GetAll(true);

            foreach (var usbRecord in _records)
                usbRecord.RefreshStatus(deviceAccounts);

            TableItemList = _records;
        }

        private bool FilterItems(object obj)
        {
            if (!(obj is UsbRecord))
                return false;

            var filterString = SearchString;
            var right = (UsbRecord)obj;

            if (right.UsbDevice != null)
            {
                if (StringContains(right.UsbDevice.DeviceName, filterString))
                    return true;
                if (StringContains(right.UsbDevice.DeviceType, filterString))
                    return true;
                if (StringContains(right.UsbDevice.Description, filterString))
                    return true;
                if (StringContains(right.UsbDevice.VendorId, filterString))
                    return true;
                if (StringContains(right.UsbDevice.ProductId, filterString))
                    return true;
                if (StringContains(right.UsbDevice.SerialNumber, filterString))
                    return true;
            }
            if (StringContains(right.CreatedDateTime.ToShortDateString(), filterString))
                return true;
            if (StringContains(right.LastPlugDateTime.ToShortDateString(), filterString))
                return true;
            return (StringContains(right.Status.StatusName, filterString));
        }
    }
}