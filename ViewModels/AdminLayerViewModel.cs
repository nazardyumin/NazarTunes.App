using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels
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





        public AdminLayerViewModel (Admin admin)
        {
            _db = new ();
            User = admin;
            Nomenclatures = new ObservableCollection<Nomenclature>(_db.GetAllNomenclatures());
        }



    }
}
