using System;
using System.Collections.Generic;
using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для SaveEntityBaseView.xaml
    /// </summary>
    public partial class SaveEntityBaseView
    {
        public SaveEntityBaseView(EntityBase entity)
        {
            InitializeComponent();
            _viewModel = new SaveEntityBaseViewModel(entity);
            DataContext = _viewModel;

            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        public SaveEntityBaseView(IList<EntityBase> entity)
        {
            InitializeComponent();
            _viewModel = new SaveEntityBaseViewModel(entity);
            DataContext = _viewModel;

            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        private readonly SaveEntityBaseViewModel _viewModel;

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
            _viewModel.SaveDbObjects.Execute(null);
        }
    }
}
