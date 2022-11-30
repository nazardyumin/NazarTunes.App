using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace NazarTunes.ViewModels.AdminLayer
{
    public class Database : Notifier
    {
        private readonly AdminLayerDb _refDb;

        private List<Nomenclature>? _nomenclatures;
        public List<Nomenclature>? Nomenclatures
        {
            get => _nomenclatures;
            set => SetField(ref _nomenclatures, value);
        }

        private List<Band>? _band;
        public List<Band>? Bands
        {
            get => _band;
            set => SetField(ref _band, value);
        }

        private List<Genre>? _genre;
        public List<Genre>? Genres
        {
            get => _genre;
            set => SetField(ref _genre, value);
        }

        private List<Performer>? _performers;
        public List<Performer>? Performers
        {
            get => _performers;
            set => SetField(ref _performers, value);
        }

        private List<Supplier>? _suppliers;
        public List<Supplier>? Suppliers
        {
            get => _suppliers;
            set => SetField(ref _suppliers, value);
        }

        private List<Supplier>? _sortedSuppliers;
        public List<Supplier>? SortedSuppliers
        {
            get => _sortedSuppliers;
            set => SetField(ref _sortedSuppliers, value);
        }

        private List<Supplier>? _activeSuppliers;
        public List<Supplier>? ActiveSuppliers
        {
            get => _activeSuppliers;
            set => SetField(ref _activeSuppliers, value);
        }

        private List<Procurement>? _procurements;
        public List<Procurement>? Procurements
        {
            get => _procurements;
            set => SetField(ref _procurements, value);
        }

        public Database(ref AdminLayerDb db)
        {
            _refDb = db;
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
            Bands = new List<Band>(_refDb.GetAllBands());
            Genres = new List<Genre>(_refDb.GetAllGenres());
            Performers = new List<Performer>(_refDb.GetAllPerformers());
            Suppliers = new List<Supplier>(_refDb.GetAllSuppliers());           
            SortedSuppliers = new List<Supplier>(Suppliers!.OrderBy(b => b.SupplierName).ToList());
            ActiveSuppliers = SortedSuppliers!.Where(s => s.IsCooperating).ToList();
            Procurements = new List<Procurement>(_refDb.GetAllProcurements());
            AddRecordInfoToProcurementAndGetListRecords();       
        }

        public void RefreshNomenclaturesAndLists()
        {
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
            Bands = new List<Band>(_refDb.GetAllBands());
            Genres = new List<Genre>(_refDb.GetAllGenres());
            Performers = new List<Performer>(_refDb.GetAllPerformers());
        }
        public void RefreshNomenclaturesOnly()
        {
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
        }

        public void RefreshSuppliers()
        {
            Suppliers = new List<Supplier>(_refDb.GetAllSuppliers());
            SortedSuppliers = new List<Supplier>(Suppliers!.OrderBy(b => b.SupplierName).ToList());
            ActiveSuppliers = SortedSuppliers!.Where(s => s.IsCooperating).ToList();
        }

        private void AddRecordInfoToProcurementAndGetListRecords()
        {
            foreach (var proc in Procurements!)
            {
                proc.RecordInfo = Nomenclatures!.Find((n) => n.Record!.Id == proc.RecordId)!.Record!.ToString();
            }
        }
        public void RefreshProcurements()
        {
            Procurements = new List<Procurement>(_refDb.GetAllProcurements());
            AddRecordInfoToProcurementAndGetListRecords();
        }
    }
}
