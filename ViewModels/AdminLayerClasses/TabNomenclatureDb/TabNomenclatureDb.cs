using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayerClasses.TabEditNomenclature;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayerClasses.TabNomenclatureDb
{
    public class TabNomenclatureDb : Notifier
    {
        private readonly AdminLayerDb _db;
        public ObservableCollection<Nomenclature>? Nomenclatures { get; set; }
        public SortedListsFromDb? SortedLists { get; set; }



        private EditListPerformers? _editPerformers;
        public EditListPerformers? EditPerformers
        {
            get => _editPerformers;
            set => SetField(ref _editPerformers, value);
        }


        public MyCommand CommandOpenCloseEditPerformers { get; }
        public MyCommand CommandCloseEditPerformers { get; }

        public TabNomenclatureDb (ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures, ref SortedListsFromDb sortedLists)
        {
            _db = db;
            Nomenclatures = nomenclatures;
            SortedLists = sortedLists;

            EditPerformers = new(ref sortedLists);

            CommandOpenCloseEditPerformers = new(_ =>
            {
                OpenCloseEditPerformersFunction();
            }, _ => true);

        }

        private void OpenCloseEditPerformersFunction()
        {

            EditPerformers!.OpenClose();
        }

    }
}
