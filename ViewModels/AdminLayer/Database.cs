using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Linq;

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

        private List<Band>? _bands;
        public List<Band>? Bands
        {
            get => _bands;
            set => SetField(ref _bands, value);
        }

        private List<Genre>? _genres;
        public List<Genre>? Genres
        {
            get => _genres;
            set => SetField(ref _genres, value);
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

        private List<Promotion>? _promotions;
        public List<Promotion>? Promotions
        {
            get => _promotions;
            set => SetField(ref _promotions, value);
        }

        private List<Promotion>? _promotionsAwaitingToStart;
        public List<Promotion>? PromotionsAwaitingToStart
        {
            get => _promotionsAwaitingToStart;
            set => SetField(ref _promotionsAwaitingToStart, value);
        }

        private List<Promotion>? _promotionsAwaitingToFinish;
        public List<Promotion>? PromotionsAwaitingToFinish
        {
            get => _promotionsAwaitingToFinish;
            set => SetField(ref _promotionsAwaitingToFinish, value);
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
            AddRecordInfoToProcurement();
            Promotions = _refDb.GetPromotions();
            AddRecordInfoToPromotions();
            PromotionsAwaitingToStart = Promotions.Where(p => !p.IsStarted && !p.IsFinished).ToList();
            PromotionsAwaitingToFinish = Promotions.Where(p => p.IsStarted && !p.IsFinished).ToList();
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

        private void AddRecordInfoToProcurement()
        {
            foreach (var proc in Procurements!)
            {
                proc.RecordInfo = Nomenclatures!.Find((n) => n.Record!.Id == proc.RecordId)!.Record!.ToString();
            }
        }

        public void RefreshProcurements()
        {
            Procurements = new List<Procurement>(_refDb.GetAllProcurements());
            AddRecordInfoToProcurement();
        }

        public void RefreshPromotions()
        {
            Promotions = _refDb.GetPromotions();
            AddRecordInfoToPromotions();
            PromotionsAwaitingToStart = Promotions.Where(p => !p.IsStarted && !p.IsFinished).ToList();
            PromotionsAwaitingToFinish = Promotions.Where(p => p.IsStarted && !p.IsFinished).ToList();
        }

        private void AddRecordInfoToPromotions()
        {
            foreach (var promo in Promotions!)
            {
                if (promo.RecordId is not null)
                {
                    var nomenclature = Nomenclatures!.First(n => n.Record!.Id == promo.RecordId);
                    promo.RecordInfo = nomenclature.Record!.ToString();
                }
            }
        }
    }
}
