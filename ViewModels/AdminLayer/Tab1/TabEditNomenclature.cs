using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayer.Tab1
{
    public class TabEditNomenclature : Notifier
    {
        protected AdminLayerDb _refDb { get; set; }
        protected ObservableCollection<Nomenclature> _refNomenclatures { get; set; }

        private NomenclatureConstructor? _selectedNomenclature;
        public NomenclatureConstructor? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        public MyCommand CommandFindNomenclature { get; }
        public MyCommand CommandSaveChanges { get; }
        public MyCommand CommandClear { get; }

        public TabEditNomenclature(ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures)
        {
            _refDb = db;
            _refNomenclatures = nomenclatures;

            SelectedNomenclature = new();

            CommandFindNomenclature = new(_ =>
            {
                FindNomenclatureFunction();
            }, _ => true);
            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => true);
            CommandClear = new(_ =>
            {
                ClearFunction();
            }, _ => true);
        }

        private void FindNomenclatureFunction()
        {
            if (SelectedNomenclature!.SelectedIdIsNotNullOrEmpty())
            {
                if (SelectedNomenclature.SelectedIdContainsOnlyDigits())
                {
                    var i = SelectedNomenclature.GetSelectedIndex();
                    if (i <= _refNomenclatures.Count - 1 && i >= 0)
                    {
                        SelectedNomenclature.Set(_refNomenclatures[i]);
                    }
                    else SelectedNomenclature.HelperText = "Invalid ID!";
                }
                else SelectedNomenclature.HelperText = "This field may contain only digits!";
            }
            else SelectedNomenclature.HelperText = "Enter ID!";
        }

        private void SaveChangesFunction()
        {
            var i = SelectedNomenclature!.GetSelectedIndex();
            var result = SelectedNomenclature.CompareNewAndOldBands(_refNomenclatures[i]);

            //TODO finish this function with all list fields!!!
            //TODO make editions for lists genres, performers and bands!





        }

        private void ClearFunction()
        {
            SelectedNomenclature!.Clear();
        }
    }
}
