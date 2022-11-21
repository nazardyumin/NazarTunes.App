using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListPerformers : EditAbstract
    {
        private string? _textField2;
        public string? TextField2
        {
            get => _textField2;
            set
            {
                SetField(ref _textField2, value);
                RefreshCanSaveChangesState();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = _refSortedLists!.Performers![_selectedIndex].FirstName;
                    TextField2 = _refSortedLists!.Performers![_selectedIndex].LastName;
                }
            }
        }

        public EditListPerformers(ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures, ref SortedListsFromDb sortedLists) : base(ref db, ref nomenclatures, ref sortedLists)
        {
            SelectedIndex = -1;
        }

        public new void Hide()
        {
            base.Hide();
            TextField2 = string.Empty;
            SelectedIndex = -1;
        }

        protected new void RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) && string.IsNullOrWhiteSpace(TextField2) || _selectedIndex == -1)
            {
                CanSaveChanges = false;
            }
            else
            {
                CanSaveChanges = true;
            }
        }
    }
}
