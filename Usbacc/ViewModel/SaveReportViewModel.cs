using System;
using System.ComponentModel;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Import;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class SaveReportViewModel : ViewModelBase
    {
        public SaveReportViewModel(string[] files)
        {
            _files = files;
            Maximum = files.Length;
            Message = Minimum + "/" + Maximum;
            ProcessCommand = new RelayCommand(x => SaveMethod());
        }

        private int _minimum = 0;
        public int Minimum
        {
            get { return _minimum; }
            set { _minimum = value; OnPropertyChanged("Minimum"); }
        }

        private int _maximum = 100;
        public int Maximum
        {
            get { return _maximum; }
            set { _maximum = value; OnPropertyChanged("Maximum"); }
        }

        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        private string _message = "";
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }
        }

        public ICommand ProcessCommand { get; set; }
        public Action CloseAction { get; set; }

        private readonly string[] _files;
        private int _filesProcessed;

        private void SaveMethod()
        {
            var backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
            backgroundWorker.DoWork += (s, e) =>
            {
                var reportImport = new UsbDeviewReportImport();
                var reportRepository = new Repository<Report>();
                var usbRepository = new Repository<UsbRecord>();

                var worker = s as BackgroundWorker;
                for (int i = 0; i < _files.Length; i++)
                {
                    var report = reportImport.GetReport(_files[i]);
                    reportRepository.Save(report);
                    usbRepository.Save(report.UsbRecords);

                    _filesProcessed = i;
                    if (worker != null)
                        worker.ReportProgress(i);
                }
            };
            backgroundWorker.RunWorkerCompleted += (s, e) => CloseAction();
            backgroundWorker.ProgressChanged += (s, e) =>
            {
                Value = e.ProgressPercentage;
                Message = Value + "/" + Maximum;
            };

            backgroundWorker.RunWorkerAsync();
        }
    }
}
