using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System;

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

        public TabNomenclatureDb(ref AdminLayerDb db, Action refreshDb)
        {
            EditPerformers = new(ref db, refreshDb);
            EditBands = new(ref db, refreshDb);
            EditGenres = new(ref db, refreshDb);

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
        }

        private void OpenCloseEditPerformersFunction()
        {
            EditGenres!.Hide();
            EditBands!.Hide();
            EditPerformers!.OpenClose();
        }

        private void OpenCloseEditBandsFunction()
        {
            EditGenres!.Hide();
            EditPerformers!.Hide();
            EditBands!.OpenClose();
        }

        private void OpenCloseEditGenresFunction()
        {
            EditPerformers!.Hide();
            EditBands!.Hide();
            EditGenres!.OpenClose();
        }
    }
}
