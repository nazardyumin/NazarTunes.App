using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public abstract class EditAbstract : Notifier
    {
        protected AdminLayerDb _refDb { get; set; }
        protected ObservableCollection<Nomenclature> _refNomenclatures { get; set; }
        protected SortedListsFromDb? _refSortedLists { get; set; }


        private string? _textField1;
        public string? TextField1
        {
            get => _textField1;
            set
            {
                SetField(ref _textField1, value);
                RefreshCanSaveChangesState();
            }
        }
        
        protected int _selectedIndex;

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        private bool _canSaveChanges;
        public bool CanSaveChanges
        {
            get => _canSaveChanges;
            set => SetField(ref _canSaveChanges, value);
        }

        protected EditAbstract(ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures, ref SortedListsFromDb sortedLists)
        {
            _refDb = db;
            _refNomenclatures = nomenclatures;
            _refSortedLists = sortedLists;

            IsVisible = Visibility.Collapsed;
        }

        public void OpenClose()
        {
            if (IsVisible == Visibility.Visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
        protected void Show()
        {
            IsVisible = Visibility.Visible;
        }
        
        public void Hide()
        {
            TextField1 = string.Empty;
            IsVisible = Visibility.Collapsed;
        }

        protected void RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) || _selectedIndex == -1)
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
