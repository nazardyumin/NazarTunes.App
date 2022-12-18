using NazarTunes.Models.DataTemplates;
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

        private Nomenclature? _selectedNomenclature;
        public Nomenclature? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        public MyCommand CommandOpenCloseEditPerformers { get; }
        public MyCommand CommandOpenCloseEditBands { get; }
        public MyCommand CommandOpenCloseEditGenres { get; }
        public MyCommand CommandOpenNomenclatureInEditor { get; }

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        private readonly Action<int?> _switchSelectedTab;
        private readonly Action<int?> _openNomenclatureInEditor;

        public TabNomenclatureDb(ref AdminLayerDb db, ref Database database, Action<int?> switchSelectedTab, Action<int?> openNomenclatureInEditor)
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

            CommandOpenNomenclatureInEditor = new(_ =>
            {
                OpenNomenclatureInEditorFunction();
            }, _ => true);

            _switchSelectedTab = switchSelectedTab;
            _openNomenclatureInEditor = openNomenclatureInEditor;

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

        private void OpenNomenclatureInEditorFunction()
        {
            _switchSelectedTab.Invoke(1);
            _openNomenclatureInEditor.Invoke(SelectedNomenclature!.NomenclatureId);
            SelectedNomenclature = null;
        }
    }
}
