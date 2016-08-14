using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class ReportTableViewModel : TableSearchViewModel<Report>
    {
        public ReportTableViewModel()
        {
            InitViewModel();
        }

        public ICommand ImportCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private Repository<Report> _repository;

        private void InitViewModel()
        {
            _repository = new Repository<Report>();
            
            ImportCommand = new RelayCommand(x => ImportMethod());
            //AddCommand = new RelayCommand(x => AddMethod());
            //EditCommand = new RelayCommand(x => EditMethod());
            //DeleteCommand = new RelayCommand(x => DeleteMethod());
            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
        }

        private void ImportMethod()
        {
            var dlg = new OpenFileDialog
                {
                    DefaultExt = "*.xml", 
                    Filter = "Файлы XML (*.xml)|*.xml|Все файлы (*.*)|*.*"
                };

            var result = dlg.ShowDialog();
            if (result != true) return;
            
            string filename = dlg.FileName;
        }

        private void RefreshCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!IsSearched)
                TableItemListView = CollectionViewSource.GetDefaultView(TableItemList);
            else
            {
                //Get all units
                var itemList = CollectionViewSource.GetDefaultView(TableItemList);
                //var filter = new Predicate<object>(FilterItems);
                //itemList.Filter = filter;
                TableItemListView = itemList;
            }

            LoadTableSortOrder();
        }

        private void RefreshMethod()
        {
            TableItemList = _repository.GetAll();
        }

        private void AddMethod()
        {
            //AddAction();
        }

        private void EditMethod()
        {
            //EditAction();
        }

        private void DeleteMethod()
        {
            //var item = SelectedItem as Card;
            //if (item != null)
            //{
            //    const string caption = "Удаление";
            //    const string text = "Вы действительно хотите удалить эту запись?\r\n" +
            //                        "Все устройства будут удалены.";
            //    const MessageBoxButton buttons = MessageBoxButton.OKCancel;

            //    if (MessageBox.Show(text, caption, buttons) == MessageBoxResult.OK)
            //    {
            //        DeleteCard(item);
            //        if (RefreshCommand != null)
            //            RefreshCommand.Execute(null);
            //    }
            //}
        }

        private void DeleteCard(Report item)
        {
            //var repository = new CardRepository();
            //var card = repository.GetById(item.Id);
            //var defaultCard = repository.GetDefaultCard();
            //if (card.StockUnitList != null)
            //{
            //    var stockUnitRepository = new StockUnitRepository();
            //    foreach (var stockUnit in card.StockUnitList)
            //    {
            //        stockUnit.Card = defaultCard;
            //        stockUnitRepository.Update(stockUnit);
            //    }
            //}

            //repository.Delete(item);
        }
    }
}
