using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazarTunes.ViewModels
{
    public class AuthorizationViewModel : Notifier
    {
        private string? _login;
        public string Login
        {
            get => _login!;
            set => SetField(ref _login, value);
        }

        public AuthorizationViewModel ()
        {
            Login = string.Empty;
        }
    }
}
