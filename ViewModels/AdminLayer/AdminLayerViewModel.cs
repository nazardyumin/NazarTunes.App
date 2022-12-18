using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using NazarTunes.ViewModels.AdminLayer.Tab1;
using NazarTunes.ViewModels.AdminLayer.Tab2;
using NazarTunes.ViewModels.AdminLayer.Tab3;
using NazarTunes.ViewModels.AdminLayer.Tab4;
using NazarTunes.ViewModels.AdminLayer.Tab5;
using NazarTunes.ViewModels.AdminLayer.Tab6;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using Org.BouncyCastle.Security.Certificates;
using System;

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

        private TabPromotions? _tabPromotions;
        public TabPromotions? TabPromotions
        {
            get => _tabPromotions;
            set => SetField(ref _tabPromotions, value);
        }

        private TabFreezeNomenclature? _tabFreezeNomenclature;
        public TabFreezeNomenclature? TabFreezeNomenclature
        {
            get => _tabFreezeNomenclature;
            set => SetField(ref _tabFreezeNomenclature, value);
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set => SetField(ref _selectedTab, value);
        }

        public AdminLayerViewModel(Admin admin, ref LanguagePack language)
        {
            _db = new();
            User = admin;
            Database = new(ref _db);

            SelectedTab = 0;

            TabEditNomenclature = new(ref _db, ref _database!, ref language);
            TabNomenclatureDb = new(ref _db, ref _database!, SwitchSelectedTab, TabEditNomenclature.OpenNomenclatureFromDbInEditor);
            TabNewNomenclature = new(ref _db, ref _database!);
            TabSuppliers = new(ref _db, ref _database!);
            TabProcurements = new(ref _db, ref _database!);
            TabPromotions = new(ref _db, ref _database!);
            TabFreezeNomenclature = new(ref _db, ref _database!);

        }

        public void RefreshLanguage(ref LanguagePack language)
        {
            TabEditNomenclature!.RefreshLanguage(ref language);
            Database!.RefreshPromotions();
        }

        private void SwitchSelectedTab(int? index = null)
        {
            if (index is not null) SelectedTab = (int)index;
        }
    }
}
