using System.Windows.Input;
using Usbacc.Core.Filter;

namespace Usbacc.UI.ViewModel.Base
{
    interface ITableComplexFilterViewModel
    {
        IFilterBase ComplexFilter { get; set; }
        bool ComplexFilterStatus { get; set; }

        ICommand FilterCommand { get; set; }
        ICommand ClearFilterCommand { get; set; }
    }
}
