using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public abstract class AddAbstractPromo : Notifier
    {
        protected readonly AdminLayerDb _refDb;
        protected readonly Database _refDatabase;

        protected int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                CommandAdd!.OnCanExecuteChanged();
            }
        }

        private string? _discount;
        public string? Discount
        {
            get => _discount;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _discount, value);
                CommandAdd!.OnCanExecuteChanged();
            }
        }

        private bool _startPromo;
        public bool StartPromo
        {
            get => _startPromo;
            set => SetField(ref _startPromo, value);
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public MyCommand? CommandAdd { get; }

        protected AddAbstractPromo(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            IsVisible = Visibility.Collapsed;

            CommandAdd = new(_ =>
            {
                AddFunction();
            }, _ => RefreshCanAddState());

            StartPromo = false;
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

        protected virtual bool RefreshCanAddState()
        {
            if (string.IsNullOrWhiteSpace(Discount) || SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected abstract void AddFunction();

        private void RemoveLettersOrSymbols(ref string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1 && str[0] == '0') str = string.Empty;
                if (str.Length == 2 && str[0] == '0') str = str.Remove(0, 1);
                if (str.Length>2) str = str.Remove(2);
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i])) str = str.Remove(i, 1);
                }
            }
        }

        protected void Clear()
        {
            SelectedIndex = -1;
            Discount = string.Empty;
            StartPromo = false;
        }
    }
}
