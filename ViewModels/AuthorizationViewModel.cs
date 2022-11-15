using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels
{
    public class AuthorizationViewModel : Notifier
    {
        private readonly CommonViewModelData _data;
        private string? _login;
        public string Login
        {
            get => _login!;
            set
            {
                value ??= string.Empty;
                SetField(ref _login, value);
                if (_login!.Length > 0) EnterHelperText = string.Empty;
                RefreshCanPressEnterState();
            }
        }

        private string? _password;
        public string Password
        {
            get => _password!;
            set
            {
                value ??= string.Empty;
                SetField(ref _password, value);
                if (_password!.Length > 0) EnterHelperText = string.Empty;
                RefreshCanPressEnterState();
            }
        }
        private string? _enterHelperText;
        public string EnterHelperText
        {
            get => _enterHelperText!;
            set => SetField(ref _enterHelperText, value);
        }


        private Visibility _loginSectionVisibility;
        public Visibility LoginSectionVisibility
        {
            get => _loginSectionVisibility;
            set => SetField(ref _loginSectionVisibility, value);
        }

        private Visibility _registrationSectionVisibility;
        public Visibility RegistrationSectionVisibility
        {
            get => _registrationSectionVisibility;
            set => SetField(ref _registrationSectionVisibility, value);
        }

        public MyCommand CommandSwitchToRegistration { get; }
        public MyCommand CommandEnter { get; }

        private bool _canPressEnter;
        public bool CanPressEnter
        {
            get => _canPressEnter;
            set => SetField(ref _canPressEnter, value);    
        }

        public AuthorizationViewModel(ref CommonViewModelData data)
        {
            _data = data;

            Login = string.Empty;
            Password = string.Empty;
            EnterHelperText = string.Empty;

            LoginSectionVisibility = Visibility.Visible;
            RegistrationSectionVisibility = Visibility.Hidden;

            CommandSwitchToRegistration = new(_ =>
            {
                LoginSectionVisibility = Visibility.Hidden;
                RegistrationSectionVisibility = Visibility.Visible;
            }, _ => true);

            CanPressEnter = false;
            CommandEnter = new(_ =>
            {
                EnterFunction(Login, Password);
            }, _ => true);
            
        }

        private void EnterFunction(string login, string password)
        {
            var db = new AuthorizationSectionDb();
            var (correct_credentials, deleted_user, user) = db.Authorization(login, password);
            if (!correct_credentials)
            {
                EnterHelperText = "Invalid login or password!";
                Login = Password = string.Empty;
            }
            else if (deleted_user) EnterHelperText = "This account is deleted! Please contact 8-800-000-00-00!";
            else
            {
                _data.User = user!;
                _data.AuthorizationLayerVisibility = Visibility.Hidden;
                if (_data.User.GetType() == typeof(Admin))
                {
                    _data.AdminLayerVisibility = Visibility.Visible;
                }
                else
                {
                    _data.ClientLayerVisibility = Visibility.Visible;
                }
            }
        }

        private void RefreshCanPressEnterState()
        {
            if (Login == string.Empty || Password==string.Empty) CanPressEnter = false;
            else CanPressEnter = true;
        }
    }
}
