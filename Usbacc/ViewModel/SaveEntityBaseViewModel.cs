using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Usbacc.Core.Domain;
using Usbacc.Core.Import;
using Usbacc.Core.Repository;
using Usbacc.UI.ViewModel.Base;

namespace Usbacc.UI.ViewModel
{
    public class SaveEntityBaseViewModel : ViewModelBase
    {
        public SaveEntityBaseViewModel(EntityBase entity)
        {
            _repository = new Repository<EntityBase>();
            _entityList = new List<EntityBase>() { entity };
            
            SaveDbObjects = new RelayCommand(x => SaveMethod());
        }

        public SaveEntityBaseViewModel(IList<EntityBase> entityBases)
        {
            _repository = new Repository<EntityBase>();
            if (entityBases != null)
                _entityList = entityBases;

            SaveDbObjects = new RelayCommand(x => SaveMethod());
        }

        public int Minimum { get; private set; }
        public int Maximum { get; private set; }
        public int Value { get; private set; }
        public string Message { get; private set; }

        public ICommand SaveDbObjects { get; set; }
        public Action CloseAction { get; set; }

        private readonly Repository<EntityBase> _repository;
        private readonly IList<EntityBase> _entityList;

        private void SaveMethod()
        {
            var backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
            backgroundWorker.DoWork += (s, e) =>
            {
                var worker = s as BackgroundWorker;
                for (int i = 0; i < _entityList.Count; i++)
                {
                    _repository.Save(_entityList[i]);
                    
                    if (worker != null)
                        worker.ReportProgress((int)((i / (double)_entityList.Count) * 100));
                }
            };
            backgroundWorker.RunWorkerCompleted += (s, e) => CloseAction();
            backgroundWorker.ProgressChanged += (s, e) =>
            {
                Message = e.ProgressPercentage + "%";
            };

            backgroundWorker.RunWorkerAsync();
        }
    }
}
