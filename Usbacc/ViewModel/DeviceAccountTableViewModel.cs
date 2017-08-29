using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class DeviceAccountTableViewModel : TableSearchViewModel<DeviceAccount>
    {
        public DeviceAccountTableViewModel()
        {
            InitViewModel();
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public Action AddAction { get; set; }
        public Action<DeviceAccount> EditAction { get; set; }
        public Func<string, bool> DeleteAction { get; set; }
        public Func<string[]> GetFileNames { get; set; }
        public Action<string> ShowInfoMessage { get; set; }
        public Action<string[]> SaveWithProgress { get; set; }

        private DeviceAccountRepository _repository;

        private void InitViewModel()
        {
            _repository = new DeviceAccountRepository();
            
            AddCommand = new RelayCommand(x => AddAction());
            EditCommand = new RelayCommand(x => EditMethod());
            DeleteCommand = new RelayCommand(x => DeleteMethod());
            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
        }

        private void RefreshCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!IsSearched)
                TableItemListView = CollectionViewSource.GetDefaultView(TableItemList);
            else
            {
                var itemList = CollectionViewSource.GetDefaultView(TableItemList);
                var filter = new Predicate<object>(FilterItems);
                itemList.Filter = filter;
                TableItemListView = itemList;
            }

            LoadTableSortOrder();
        }

        private bool FilterItems(object obj)
        {
            if (!(obj is DeviceAccount))
                return false;

            var filterString = SearchString;
            var right = (DeviceAccount) obj;

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
            if (StringContains(right.User, filterString))
                return true;
            if (StringContains(right.Department, filterString))
                return true;
            if (StringContains(right.RegNumber, filterString))
                return true;
            return StringContains(right.Comments, filterString);
        }

        private void EditMethod()
        {
            var item = SelectedItem as DeviceAccount;
            if (item != null)
                EditAction(item);
        }

        private void RefreshMethod()
        {
            var result = _repository.GetAll(true);
            TableItemList = result;
        }

        private void DeleteMethod()
        {
            var item = SelectedItem as DeviceAccount;
            if (item != null)
            {
                const string text = "Вы действительно хотите удалить устройство?";
                if (DeleteAction(text))
                {
                    var repository = new Repository<DeviceAccount>();
                    try
                    {
                        repository.Delete(item);
                    }
                    catch (Exception ex)
                    {
                        ShowInfoMessage(ex.Message);
                    }

                    if (RefreshCommand != null) RefreshCommand.Execute(null);
                }
            }
        }
    }
}
