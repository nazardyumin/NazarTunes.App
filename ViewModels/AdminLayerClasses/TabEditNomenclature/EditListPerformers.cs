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
            set => SetField(ref _selectedIndex, value);
        }

        public EditListPerformers()
        {
            IsVisible = Visibility.Collapsed;
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

        public void Show()
        {
            IsVisible = Visibility.Visible;
        }

        public void Hide()
        {
            IsVisible = Visibility.Collapsed;
        }
    }
}
