using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
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
        public Action<string> ImportAction { get; set; }
        public Action<Report> EditAction { get; set; }
        public Func<string, bool> DeleteAction { get; set; } 
        public Action<string> ShowInfoMessage { get; set; }
        public Func<string[]> GetFileNames { get; set; }
        public Action<string[]> SaveWithProgress { get; set; }
        
        private ReportRepository _repository;

        private void InitViewModel()
        {
            _repository = new ReportRepository();
            
            ImportCommand = new RelayCommand(x => ImportMethod());
            EditCommand = new RelayCommand(x => EditMethod());
            DeleteCommand = new RelayCommand(x => DeleteMethod());
            RefreshCommand = new AsyncCommand(x => RefreshMethod());
            RefreshCommand.RunWorkerCompleted += RefreshCommand_RunWorkerCompleted;
        }

        private void ImportMethod()
        {
            var fileNames = GetFileNames();
            if (fileNames.Length == 0)
                return;
            if (fileNames.Length == 1)
                ImportAction(fileNames[0]);
            else
            {
                SaveWithProgress(fileNames);
                
                string message = "Импорт файлов завершен успешно!" + "\r\n";
                message += "Импортировано отчетов:" + fileNames.Length;
                ShowInfoMessage(message);
            }
            
            if (RefreshCommand != null) RefreshCommand.Execute(null);
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
            if (!(obj is Report))
                return false;

            var filterString = SearchString;
            var right = (Report)obj;

            if (StringContains(right.ReportName, filterString))
                return true;
            if (StringContains(right.CreationDateTime.ToShortDateString(), filterString))
                return true;
            return StringContains(right.Comments, filterString);
        }

        private void RefreshMethod()
        {
            var result = _repository.GetAll(false);
            TableItemList = result;
        }

        private void EditMethod()
        {
            var item = SelectedItem as Report;
            if (item != null)
                EditAction(item);
        }

        private void DeleteMethod()
        {
            var item = SelectedItem as Report;
            if (item != null)
            {
                const string text = "Вы действительно хотите удалить отчет?";
                if (DeleteAction(text))
                {
                    var usbRecordRepository = new Repository<UsbRecord>();
                    var reportRepository = new ReportRepository();
                    var report = reportRepository.GetById(item.Id, true);
                    try
                    {
                        usbRecordRepository.Delete(report.UsbRecords);
                        reportRepository.Delete(report);
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
