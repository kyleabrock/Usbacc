using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Usbacc.Core.Domain;
using Usbacc.UI.ViewModel;

namespace Usbacc.UI.View
{
    /// <summary>
    /// Логика взаимодействия для StatusEditView.xaml
    /// </summary>
    public partial class StatusEditView : Window
    {
        public StatusEditView()
        {
            InitializeComponent();

            _viewModel = new StatusEditViewModel();
            DataContext = _viewModel;

            if (_viewModel.ShowInfoMessage == null)
                _viewModel.ShowInfoMessage = s => MessageBox.Show(s);
            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        public StatusEditView(Status status)
        {
            InitializeComponent();

            _viewModel = new StatusEditViewModel(status);
            DataContext = _viewModel;

            if (_viewModel.ShowInfoMessage == null)
                _viewModel.ShowInfoMessage = s => MessageBox.Show(s);
            if (_viewModel.CloseAction == null)
                _viewModel.CloseAction = Close;
        }

        private readonly StatusEditViewModel _viewModel;
    }
}
