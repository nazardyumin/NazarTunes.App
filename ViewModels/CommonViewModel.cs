using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels
{
    public class CommonViewModel : Notifier
    {
        private Visibility _authorizationLayerVisibility;
        public Visibility AuthorizationLayerVisibility
        {
            get => _authorizationLayerVisibility;
            set => SetField(ref _authorizationLayerVisibility, value);
        }

        private Visibility _adminLayerVisibility;
        public Visibility AdminLayerVisibility
        {
            get => _adminLayerVisibility;
            set => SetField(ref _adminLayerVisibility, value);
        }

        private Visibility _clientLayerVisibility;
        public Visibility ClientLayerVisibility
        {
            get => _clientLayerVisibility;
            set => SetField(ref _clientLayerVisibility, value);
        }

        private AdminLayerViewModel? _admin;
        public AdminLayerViewModel? Admin 
        { 
            get => _admin; 
            set => SetField(ref _admin, value); 
        }

        private ClientLayerViewModel? _client;
        public ClientLayerViewModel? Client
        {
            get => _client;
            set => SetField(ref _client, value);
        }

        public CommonViewModel()
        {
            AuthorizationLayerVisibility = Visibility.Visible;
            AdminLayerVisibility = Visibility.Hidden;
            ClientLayerVisibility = Visibility.Hidden;
        }
    }
}
