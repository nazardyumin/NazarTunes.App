using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using NazarTunes.ViewModels.AdminLayer.Tab1;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayer
{
    public class AdminLayerViewModel : Notifier
    {
        private readonly AdminLayerDb _db;

        private Admin? _user;
        public Admin? User
        {
            get => _user;
            set => SetField(ref _user, value);
        }

        private ObservableCollection<Nomenclature>? _nomenclatures;
        public ObservableCollection<Nomenclature>? Nomenclatures
        {
            get => _nomenclatures;
            set => SetField(ref _nomenclatures, value);
        }


        private TabNomenclatureDb? _tabNomenclatureDb;
        public TabNomenclatureDb? TabNomenclatureDb
        {
            get => _tabNomenclatureDb;
            set => SetField(ref _tabNomenclatureDb, value);
        }





        private NomenclatureConstructor? _selectedNomenclature;
        public NomenclatureConstructor? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        private SortedListsFromDb? _sortedLists;
        public SortedListsFromDb? SortedLists
        {
            get => _sortedLists;
            set => SetField(ref _sortedLists, value);
        }





        public int SelectedTab { get; set; }


        public MyCommand CommandFindNomenclature { get; }
        public MyCommand CommandSaveChanges { get; }
        public MyCommand CommandCancel { get; }







        public AdminLayerViewModel(Admin admin)
        {
            _db = new();
            User = admin;
            Nomenclatures = new ObservableCollection<Nomenclature>(_db.GetAllNomenclatures());


            SelectedTab = 0;

            SelectedNomenclature = new();
            SortedLists = new(ref _db);

            TabNomenclatureDb = new(ref _db, ref _nomenclatures!, ref _sortedLists!);

            CommandFindNomenclature = new(_ =>
            {
                FindNomenclatureFunction();
            }, _ => true);
            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => true);
            CommandCancel = new(_ =>
            {
                CancelFunction();
            }, _ => true);

        }

        private void FindNomenclatureFunction()
        {
            if (SelectedNomenclature!.SelectedIdIsNotNull())
            {
                if (SelectedNomenclature.SelectedIdContainsOnlyDigits())
                {
                    var i = SelectedNomenclature.GetSelectedIndex();
                    if (i <= Nomenclatures!.Count - 1 && i >= 0)
                    {
                        SelectedNomenclature.Set(Nomenclatures![i]);
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
            var result = SelectedNomenclature.CompareNewAndOldBands(Nomenclatures![i]);

            //TODO finish this function with all list fields!!!
            //TODO make editions for lists genres, performers and bands!





        }

        private void CancelFunction()
        {
            SelectedNomenclature!.Clear();
        }




    }
}
