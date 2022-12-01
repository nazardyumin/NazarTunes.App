using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels
{
    public class AuthorizationLayerViewModel : Notifier
    {
        private readonly CommonViewModel _commonViewModel;

        private readonly AuthorizationLayerDb _db;

        private readonly LanguagePack _language;

        private string? _login;
        public string Login
        {
            get => _login!;
            set
            {
                value ??= string.Empty;
                SetField(ref _login, value);
                if (_login!.Length > 0) HelperText = string.Empty;
                CommandEnter.OnCanExecuteChanged();
                CommandRegister.OnCanExecuteChanged();
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
                if (_password!.Length > 0) HelperText = string.Empty;
                CommandEnter.OnCanExecuteChanged();
                CommandRegister.OnCanExecuteChanged();
            }
        }

        private string? _passwordRepeat;
        public string PasswordRepeat
        {
            get => _passwordRepeat!;
            set
            {
                value ??= string.Empty;
                SetField(ref _passwordRepeat, value);
                if (_passwordRepeat!.Length > 0) HelperText = string.Empty;
                CommandRegister.OnCanExecuteChanged();
            }
        }

        private string? _firstName;
        public string FirstName
        {
            get => _firstName!;
            set
            {
                value ??= string.Empty;
                SetField(ref _firstName, value);
                if (_firstName!.Length > 0) HelperText = string.Empty;
                CommandRegister.OnCanExecuteChanged();
            }
        }

        private string? _lastName;
        public string LastName
        {
            get => _lastName!;
            set
            {
                value ??= string.Empty;
                SetField(ref _lastName, value);
                if (_lastName!.Length > 0) HelperText = string.Empty;
                CommandRegister.OnCanExecuteChanged();
            }
        }

        private string? _helperText;
        public string HelperText
        {
            get => _helperText!;
            set => SetField(ref _helperText, value);
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
        public MyCommand CommandSwitchToLogin { get; }
        public MyCommand CommandEnter { get; }
        public MyCommand CommandRegister { get; }

        private bool _isRegistration;

        public AuthorizationLayerViewModel(ref CommonViewModel commonViewModel, ref LanguagePack language)
        {
            _commonViewModel = commonViewModel;

            _db = new AuthorizationLayerDb();

            LoginSectionVisibility = Visibility.Visible;
            RegistrationSectionVisibility = Visibility.Hidden;

            CommandSwitchToRegistration = new(_ =>
            {
                SwitchToRegistrationFunction();
            }, _ => true);

            CommandSwitchToLogin = new(_ =>
            {
                SwitchToLoginFunction();
            }, _ => true);


            CommandEnter = new(_ =>
            {
                EnterFunction(Login, Password);
            }, _ => RefreshCanPressEnterState());


            CommandRegister = new(_ =>
            {
                RegisterFunction();
            }, _ => RefreshCanPressRegisterState());

            Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;

            _isRegistration = false;
            _language = language;
        }

        private void EnterFunction(string login, string password)
        {
            var (correct_credentials, deleted_user, user) = _db.Authorization(login, password);
            if (!correct_credentials)
            {
                HelperText = _language.Authorization.HelperTextInvalidCredentials;
                Login = Password = string.Empty;
            }
            else if (deleted_user) HelperText = _language.Authorization.HelperTextDeletedAccount;
            else
            {
                _commonViewModel.AuthorizationLayerVisibility = Visibility.Hidden;
                if (user!.GetType() == typeof(Admin))
                {
                    _commonViewModel.Admin = new AdminLayerViewModel((Admin)user);
                    _commonViewModel.AdminLayerVisibility = Visibility.Visible;
                }
                else
                {
                    _commonViewModel.Client = new ClientLayerViewModel((Client)user);
                    _commonViewModel.ClientLayerVisibility = Visibility.Visible;
                }
                Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;
            }
        }

        private void RegisterFunction()
        {
            if (Password != PasswordRepeat)
            {
                HelperText = _language.Authorization.HelperTextPasswordsDontMatch;
                Password = PasswordRepeat = string.Empty;
            }
            else
            {
                var (client, ifSucceed) = _db.CreateClient(Login, Password, FirstName, LastName);
                if (ifSucceed)
                {
                    _commonViewModel.Client = new ClientLayerViewModel(client!);
                    _commonViewModel.AuthorizationLayerVisibility = Visibility.Hidden;
                    _commonViewModel.ClientLayerVisibility = Visibility.Visible;
                    Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;
                    _isRegistration = false;
                }
                else
                {
                    HelperText = _language.Authorization.HelperTextUnexpectedError;
                }
            }
        }

        private void SwitchToRegistrationFunction()
        {
            LoginSectionVisibility = Visibility.Hidden;
            RegistrationSectionVisibility = Visibility.Visible;
            Login = Password = HelperText = string.Empty;
            _isRegistration = true;
        }

        private void SwitchToLoginFunction()
        {
            _isRegistration = false;
            RegistrationSectionVisibility = Visibility.Hidden;
            LoginSectionVisibility = Visibility.Visible;
            Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;
        }

        private bool RefreshCanPressEnterState()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)) return false;
            else return true;
        }

        private bool RefreshCanPressRegisterState()
        {
            if (_isRegistration)
            {
                if (_db.IfLoginExists(Login))
                {
                    HelperText = _language.Authorization.HelperTextOccupiedLogin;
                    return false;
                }
                else if (Login == string.Empty || Password == string.Empty || PasswordRepeat == string.Empty ||
                    FirstName == string.Empty || LastName == string.Empty) return false;

                else return true;
            }
            return false;
        }
    }
}
