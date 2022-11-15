using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels
{
    public class CommonViewModelData : Notifier
    {
        private AbstractUser? _user;
        public AbstractUser? User 
        {
            get => _user;
            set => SetField(ref _user, value);
        } 

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

        public CommonViewModelData()
        {
            AuthorizationLayerVisibility = Visibility.Visible;
            AdminLayerVisibility = Visibility.Hidden;
            ClientLayerVisibility = Visibility.Hidden;
        }
    }
}
