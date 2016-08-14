using System;
using System.Windows.Input;

namespace Usbacc.UI.ViewModel.Base
{
    interface ITableNavigationViewModel
    {
        ICommand AddCommand { get; set; }
        ICommand EditCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        Action AddAction { get; set; }
        Action EditAction { get; set; }
    }
}
