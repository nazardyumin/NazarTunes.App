using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels
{
    public class ClientLayerViewModel : Notifier
    {
        private readonly ClientLayerDb _db;

        private Client? _user;
        public Client? User
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

        public ClientLayerViewModel(Client client, ref LanguagePack language)
        {
            _db = new();
            User = client;
            Nomenclatures = new ObservableCollection<Nomenclature>(_db.GetAllNomenclatures());
        }
    }
}
