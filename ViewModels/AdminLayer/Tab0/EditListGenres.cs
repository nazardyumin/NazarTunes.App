using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListGenres : EditAbstract
    {
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = _refSortedLists!.Genres![_selectedIndex].GenreName;
                }
            }
        }

        public EditListGenres(ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures, ref SortedListsFromDb sortedLists) : base (ref db, ref nomenclatures, ref sortedLists)
        {
            SelectedIndex = -1;
        }

        public new void Hide()
        {
            base.Hide();
            SelectedIndex = -1;
        }
    }
}
