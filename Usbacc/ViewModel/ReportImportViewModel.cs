using System;
using System.IO;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Import;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class ReportImportViewModel : ViewModelBase
    {
        public ReportImportViewModel()
        {
            FilePath = "";
            InitViewModel();
        }

        public ReportImportViewModel(string filePath)
        {
            FilePath = filePath;
            InitViewModel();
        }

        private Report _report = new Report();
        public Report Report
        {
            get { return _report; }
            set { _report = value; OnPropertyChanged("Report"); }
        }

        private bool _getPropertiesFromFile = false;
        public bool GetPropertiesFromFile
        {
            get { return _getPropertiesFromFile; }
            set
            {
                _getPropertiesFromFile = value;
                
                if (value)
                {
                    _reportName = Report.ReportName;
                    _reportCreationDate = Report.CreationDateTime;
                    Report.ReportName = Path.GetFileNameWithoutExtension(FilePath);
                    Report.CreationDateTime = File.GetLastWriteTime(FilePath);
                }
                else
                {
                    Report.ReportName = _reportName;
                    Report.CreationDateTime = _reportCreationDate;
                }

                OnPropertyChanged("GetPropertiesFromFile");
                OnPropertyChanged("Report");
            }
        }

        public string FilePath { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action CloseAction { get; set; }

        private string _reportName;
        private DateTime _reportCreationDate;

        private void InitViewModel()
        {
            _reportName = string.Empty;
            _reportCreationDate = DateTime.Now;

            SaveCommand = new AsyncCommand(x => SaveMethod());
            ((AsyncCommand) SaveCommand).RunWorkerCompleted += (s, e) => CloseAction();
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        private void SaveMethod()
        {
            var reportImport = new UsbDeviewReportImport();
            var reportRepository = new Repository<Report>();
            var usbRepository = new Repository<UsbRecord>();
            var report = reportImport.GetReport(Report, FilePath);
            
            reportRepository.Save(report);
            usbRepository.Save(report.UsbRecords);
        }
    }
}
