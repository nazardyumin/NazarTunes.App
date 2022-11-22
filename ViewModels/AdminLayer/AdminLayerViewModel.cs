using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using NazarTunes.ViewModels.AdminLayer.Tab1;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;

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

        private List<Nomenclature>? _nomenclatures;
        public List<Nomenclature>? Nomenclatures
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

        private TabEditNomenclature? _tabEditNomenclature;
        public TabEditNomenclature? TabEditNomenclature
        {
            get => _tabEditNomenclature;
            set => SetField(ref _tabEditNomenclature, value);
        }

        public int SelectedTab { get; set; }

        public AdminLayerViewModel(Admin admin)
        {
            _db = new();
            User = admin;
            Nomenclatures = new List<Nomenclature>(_db.GetAllNomenclatures());

            SelectedTab = 0;
 
            TabNomenclatureDb = new(ref _db, RefreshDbView);
        }

        private void RefreshDbView()
        {
            Nomenclatures = new List<Nomenclature>(_db.GetAllNomenclatures());
        }
    }
}
