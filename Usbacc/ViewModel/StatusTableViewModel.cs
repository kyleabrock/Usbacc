using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class StatusTableViewModel : TableSearchViewModel<Status>
    {
        public StatusTableViewModel()
        {
            InitViewModel();
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public Action AddAction { get; set; }
        public Action<Status> EditAction { get; set; }
        public Func<string, bool> DeleteAction { get; set; }
        public Func<string[]> GetFileNames { get; set; }
        public Action<string> ShowInfoMessage { get; set; }
        public Action<string[]> SaveWithProgress { get; set; }
        
        private Repository<Status> _repository;

        private void InitViewModel()
        {
            _repository = new Repository<Status>();
            
            AddCommand = new RelayCommand(x => AddAction());
            EditCommand = new RelayCommand(x => EditMethod());
            DeleteCommand = new RelayCommand(x => DeleteMethod());
            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
        }

        private void EditMethod()
        {
            var item = SelectedItem as Status;
            if (item != null)
                EditAction(item);
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
            if (!(obj is Status))
                return false;

            var filterString = SearchString;
            var right = (Status) obj;

            if (StringContains(right.StatusName, filterString))
                return true;
            if (StringContains(right.Color, filterString))
                return true;
            return StringContains(right.Comments, filterString);
        }

        private void RefreshMethod()
        {
            var result = _repository.GetAll();
            TableItemList = result;
        }

        private void DeleteMethod()
        {
            var item = SelectedItem as Status;
            if (item != null)
            {
                const string text = "Вы действительно хотите удалить устройство?";
                if (DeleteAction(text))
                {
                    var repository = new StatusRepository();
                    var status = repository.GetById(item.Id, true);
                    if (status.DeviceAccounts.Count > 0)
                    {
                        string message = "Имеется связанных устройств: " + status.DeviceAccounts.Count + "\r\n";
                        message += "Смените статус устройств или удалите их для удаления статуса";
                        ShowInfoMessage(message);
                    }
                    else
                    {
                        try
                        {
                            repository.Delete(item);
                        }
                        catch (Exception ex)
                        {
                            ShowInfoMessage(ex.Message);
                        }
                    }

                    if (RefreshCommand != null) RefreshCommand.Execute(null);
                }
            }
        }
    }
}
