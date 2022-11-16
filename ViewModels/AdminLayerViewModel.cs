using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels
{
    public class AdminLayerViewModel : Notifier
    {
        private Admin? _user;
        public Admin? User 
        { 
            get => _user;
            set => SetField(ref _user, value);
        }

        public AdminLayerViewModel (Admin admin)
        {
            User = admin;
        }
    }
}
