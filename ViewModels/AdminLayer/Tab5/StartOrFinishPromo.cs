using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class StartOrFinishPromo : Notifier
    {
        private readonly AdminLayerDb _refDb;
        private readonly Database _refDatabase;

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                CommandStartOrFinish!.OnCanExecuteChanged();
            }
        }

        private Promotion? _selectedPromotion;
        public Promotion? SelectedPromotion
        {
            get => _selectedPromotion;
            set
            {
                SetField(ref _selectedPromotion, value);
            }
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public MyCommand? CommandStartOrFinish { get; }

        private readonly bool _key;

        public StartOrFinishPromo(ref AdminLayerDb db, ref Database database, bool key)
        {
            _refDb = db;
            _refDatabase = database;
            _key = key;

            IsVisible = Visibility.Collapsed;

            CommandStartOrFinish = new(_ =>
            {
                StartOrFinishFunction();
            }, _ => RefreshStartOrFinishState());

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

        private void Show()
        {
            IsVisible = Visibility.Visible;
        }

        public void Hide()
        {
            IsVisible = Visibility.Collapsed;
            SelectedIndex = -1;
            SelectedPromotion = null;
        }

        private bool RefreshStartOrFinishState()
        {
            if (SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void StartOrFinishFunction()
        {
            if (_key)  //if true start promo
            {
                
            }
            else // else finish promo
            {

            }

            SelectedIndex = -1;
            SelectedPromotion = null;
            _refDatabase.RefreshPromotions();
            _refDatabase.RefreshNomenclaturesOnly();
        }

    }
}
