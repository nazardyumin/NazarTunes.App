using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayerClasses.TabNomenclatureDb
{
    public class TabNomenclatureDb : Notifier
    {
        private readonly AdminLayerDb _db;
        public ObservableCollection<Nomenclature>? Nomenclatures { get; set; }




        public TabNomenclatureDb (ref AdminLayerDb db, ref ObservableCollection<Nomenclature> nomenclatures)
        {
            _db = db;
            Nomenclatures = nomenclatures;
        }
    }
}
