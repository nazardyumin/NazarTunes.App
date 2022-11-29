using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class TabNomenclatureDb : Notifier
    {
        private EditListPerformers? _editPerformers;
        public EditListPerformers? EditPerformers
        {
            get => _editPerformers;
            set => SetField(ref _editPerformers, value);
        }

        private EditListBands? _editBands;
        public EditListBands? EditBands
        {
            get => _editBands;
            set => SetField(ref _editBands, value);
        }

        private EditListGenres? _editGenres;
        public EditListGenres? EditGenres
        {
            get => _editGenres;
            set => SetField(ref _editGenres, value);
        }

        public MyCommand CommandOpenCloseEditPerformers { get; }
        public MyCommand CommandOpenCloseEditBands { get; }
        public MyCommand CommandOpenCloseEditGenres { get; }

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        public TabNomenclatureDb(ref AdminLayerDb db, ref Database database)
        {
            EditPerformers = new(ref db, ref database);
            EditBands = new(ref db, ref database);
            EditGenres = new(ref db, ref database);

            CommandOpenCloseEditPerformers = new(_ =>
            {
                OpenCloseEditPerformersFunction();
            }, _ => true);

            CommandOpenCloseEditBands = new(_ =>
            {
                OpenCloseEditBandsFunction();
            }, _ => true);

            CommandOpenCloseEditGenres = new(_ =>
            {
                OpenCloseEditGenresFunction();
            }, _ => true);

            ButtonPanelHeight = 47;
        }

        private void OpenCloseEditPerformersFunction()
        {
            EditGenres!.Hide();
            EditBands!.Hide();
            ButtonPanelHeight = EditPerformers!.OpenClose();
        }

        private void OpenCloseEditBandsFunction()
        {
            EditGenres!.Hide();
            EditPerformers!.Hide();
            ButtonPanelHeight = EditBands!.OpenClose();
        }

        private void OpenCloseEditGenresFunction()
        {
            EditPerformers!.Hide();
            EditBands!.Hide();
            ButtonPanelHeight = EditGenres!.OpenClose();
        }
    }
}
