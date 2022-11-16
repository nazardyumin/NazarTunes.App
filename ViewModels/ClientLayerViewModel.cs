using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels
{
    public class ClientLayerViewModel : Notifier
    {
        private Client? _user;
        public Client? User
        {
            get => _user;
            set => SetField(ref _user, value);
        }

        public ClientLayerViewModel(Client client)
        {
            User = client;
        }
    }
}
