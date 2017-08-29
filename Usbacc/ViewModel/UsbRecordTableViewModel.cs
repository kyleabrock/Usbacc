using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class UsbRecordTableViewModel : TableViewModel<UsbRecord>
    {
        public UsbRecordTableViewModel()
        {
            
        }

        public UsbRecordTableViewModel(Report report)
        {
            _repository = new ReportRepository();
            _report = _repository.GetById(report.Id, true);

            InitViewModel();
        }

        public ICommand AddDeviceAccountCommand { get; set; }
        public ICommand FindDeviceAccountCommand { get; set; }
        public Action ShowChanges { get; set; }
        public Action AddDeviceAccountAction { get; set; }
        public Action FindDeviceAccountAction { get; set; }

        private readonly ReportRepository _repository;
        private readonly Report _report;

        private void InitViewModel()
        {
            AddDeviceAccountCommand = new RelayCommand(x => AddDeviceAccountMethod());
            FindDeviceAccountCommand = new RelayCommand(x => FindDeviceAccountMethod());

            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
            RefreshCommand.Execute(null);
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
            var deviceAccountsRepository = new DeviceAccountRepository();
            var deviceAccounts = deviceAccountsRepository.GetAll(true);

            foreach (var usbRecord in _report.UsbRecords)
                usbRecord.RefreshStatus(deviceAccounts);

            TableItemList = _report.UsbRecords;
        }
    }
}