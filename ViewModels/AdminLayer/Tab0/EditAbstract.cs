using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public abstract class EditAbstract : Notifier
    {
        protected readonly AdminLayerDb _refDb;
        protected readonly Database _refDatabase;

        private string? _textField1;
        public string? TextField1
        {
            get => _textField1;
            set
            {
                SetField(ref _textField1, value);
                CommandSaveChanges!.OnCanExecuteChanged();
            }
        }

        protected int _selectedIndex;

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public MyCommand? CommandSaveChanges { get; }

        protected EditAbstract(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            IsVisible = Visibility.Collapsed;

            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => RefreshCanSaveChangesState());
        }

        public int OpenClose()
        {
            int height;
            if (IsVisible == Visibility.Visible)
            {
                Hide();
                height = 47;
            }
            else
            {
                Show();
                height = 120;
            }
            return height;
        }

        protected void Show()
        {
            IsVisible = Visibility.Visible;
        }

        public abstract void Hide();

        protected virtual bool RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) || _selectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected abstract void SaveChangesFunction();
    }
}
