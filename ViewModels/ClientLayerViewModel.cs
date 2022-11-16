using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels
{
    public class ClientLayerViewModel : Notifier
    {
        public Client? User { get; set; }

        public ClientLayerViewModel(Client client)
        {
            User = client;
        }
    }
}
