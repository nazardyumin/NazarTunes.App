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

        public Database(ref AdminLayerDb db)
        {
            _refDb = db;
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
        }

        public void RefreshView()
        {
            Nomenclatures = new List<Nomenclature>(_refDb.GetAllNomenclatures());
        }
    }
}
