using System.Collections.Generic;
using System.ComponentModel;
using Usbacc.Core.Domain;

namespace Usbacc.UI.ViewModel.Base
{
    interface ITableBaseViewModel<T> where T : EntityBase
    {
        ICollectionView TableItemListView { get; set; }
        IEnumerable<T> TableItemList { get; set; }
        object SelectedItem { get; set; }

        AsyncCommand RefreshCommand { get; set; }
    }
}
