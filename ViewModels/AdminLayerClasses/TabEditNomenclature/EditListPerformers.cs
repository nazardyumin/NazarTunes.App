using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayerClasses.TabEditNomenclature
{
    public class EditListPerformers : Notifier
    {
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set
            {
                SetField(ref _firstName, value);
                RefreshCanSaveChangesState();
            }

        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                SetField(ref _lastName, value);
                RefreshCanSaveChangesState();
            }
        }

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

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex>-1)
                {
                    FirstName = SortedLists!.Performers![_selectedIndex].FirstName;
                    LastName = SortedLists!.Performers![_selectedIndex].LastName;
                }
            }
        }

        public SortedListsFromDb? SortedLists { get; set; }

        public EditListPerformers(ref SortedListsFromDb sortedLists)
        {
            IsVisible = Visibility.Collapsed;
            SelectedIndex = -1;
            SortedLists = sortedLists;
        }

        private void RefreshCanSaveChangesState()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                CanSaveChanges = true;
            }
            else
            {
                CanSaveChanges = false;
            }
        }
        public void OpenClose()
        {
            if (IsVisible==Visibility.Visible)
            {
                Hide();
            }
            else
            {
                Show();
            } 
                
        }
        private void Show()
        {
            IsVisible = Visibility.Visible;
        }

        private void Hide()
        {
            FirstName = LastName = string.Empty;
            SelectedIndex = -1;
            IsVisible = Visibility.Collapsed;
        }

       
    }
}
