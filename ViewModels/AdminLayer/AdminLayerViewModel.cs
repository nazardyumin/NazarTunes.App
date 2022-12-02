using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using NazarTunes.ViewModels.AdminLayer.Tab1;
using NazarTunes.ViewModels.AdminLayer.Tab2;
using NazarTunes.ViewModels.AdminLayer.Tab3;
using NazarTunes.ViewModels.AdminLayer.Tab4;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;

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

        private Database? _database;
        public Database? Database
        {
            get => _database;
            set => SetField(ref _database, value);
        }

        private TabNomenclatureDb? _tabNomenclatureDb;
        public TabNomenclatureDb? TabNomenclatureDb
        {
            get => _tabNomenclatureDb;
            set => SetField(ref _tabNomenclatureDb, value);
        }

        private TabEditNomenclature? _tabEditNomenclature;
        public TabEditNomenclature? TabEditNomenclature
        {
            get => _tabEditNomenclature;
            set => SetField(ref _tabEditNomenclature, value);
        }

        private TabNewNomenclature? _tabNewNomenclature;
        public TabNewNomenclature? TabNewNomenclature
        {
            get => _tabNewNomenclature;
            set => SetField(ref _tabNewNomenclature, value);
        }

        private TabSuppliers? _tabSuppliers;
        public TabSuppliers? TabSuppliers
        {
            get => _tabSuppliers;
            set => SetField(ref _tabSuppliers, value);
        }

        private TabProcurements? _tabProcurements;
        public TabProcurements? TabProcurements
        {
            get => _tabProcurements;
            set => SetField(ref _tabProcurements, value);
        }

        public int SelectedTab { get; set; }

        public AdminLayerViewModel(Admin admin, ref LanguagePack language)
        {
            _db = new();
            User = admin;
            Database = new(ref _db);

            SelectedTab = 0;

            TabNomenclatureDb = new(ref _db, ref _database!);
            TabEditNomenclature = new(ref _db, ref _database!, ref language);
            TabNewNomenclature = new(ref _db, ref _database!);
            TabSuppliers = new(ref _db, ref _database!);
            TabProcurements = new(ref _db, ref _database!);

        }

        public void RefreshLanguage(ref LanguagePack language)
        {
            TabEditNomenclature!.RefreshLanguage(ref language);
        }
    }
}
