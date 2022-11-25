using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public abstract class EditAbstract : Notifier
    {
        protected readonly AdminLayerDb _refDb;
        public Database RefDatabase { get; set; }

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

        public MyCommand? CommandSaveChanges { get; }

        protected EditAbstract(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            RefDatabase = database;       

            IsVisible = Visibility.Collapsed;

            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => true);
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

        public abstract void Hide();

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

        protected abstract void SaveChangesFunction();
    }
}
