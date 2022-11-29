using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;

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

        public Database(ref AdminLayerDb db)
        {
            _refDb = db;
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
            Bands = new List<Band>(_refDb.GetAllBands());
            Genres = new List<Genre>(_refDb.GetAllGenres());
            Performers = new List<Performer>(_refDb.GetAllPerformers());
            Suppliers = new List<Supplier>(_refDb.GetAllSuppliers());
        }

        public void RefreshView()
        {
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
            Bands = new List<Band>(_refDb.GetAllBands());
            Genres = new List<Genre>(_refDb.GetAllGenres());
            Performers = new List<Performer>(_refDb.GetAllPerformers());
        }

        public void RefreshSuppliers()
        {
            Suppliers = new List<Supplier>(_refDb.GetAllSuppliers());
        }
    }
}
