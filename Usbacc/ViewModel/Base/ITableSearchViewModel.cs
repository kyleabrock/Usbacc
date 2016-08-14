using System.Windows.Input;

namespace Usbacc.UI.ViewModel.Base
{
    interface ITableSearchViewModel
    {
        string SearchString { get; set; }
        int SearchStringMininumLength { get; set; }

        bool IsSearched { get; set; }
        ICommand SearchCommand { get; set; }
    }
}
