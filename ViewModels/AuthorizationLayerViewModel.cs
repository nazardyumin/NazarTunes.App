using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels
{
    public class AuthorizationLayerViewModel : Notifier
    {
        private readonly CommonViewModel _commonViewModel;

        private readonly AuthorizationLayerDb _db;

        private string? _login;
        public string Login
        {
            get => _login!;
            set
            {
                value ??= string.Empty;
                SetField(ref _login, value);
                if (_login!.Length > 0) HelperText = string.Empty;
                RefreshCanPressEnterState();
                RefreshCanPressRegisterState();
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
                RefreshCanPressEnterState();
                RefreshCanPressRegisterState();
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
                RefreshCanPressRegisterState();
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
                RefreshCanPressRegisterState();
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
                RefreshCanPressRegisterState();
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

        private bool _canPressEnter;
        public bool CanPressEnter
        {
            get => _canPressEnter;
            set => SetField(ref _canPressEnter, value);    
        }

        private bool _canPressRegister;
        public bool CanPressRegister
        {
            get => _canPressRegister;
            set => SetField(ref _canPressRegister, value);
        }

        private bool _isRegistration;

        public AuthorizationLayerViewModel(ref CommonViewModel commonViewModel)
        {
            _commonViewModel = commonViewModel;

            _db = new AuthorizationLayerDb();

            Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;

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

            CanPressEnter = false;
            CommandEnter = new(_ =>
            {
                EnterFunction(Login, Password);
            }, _ => true);

            CanPressRegister = false;
            CommandRegister = new(_ =>
            {
                RegisterFunction();
            }, _ => true);

            _isRegistration = false;
        }

        private void EnterFunction(string login, string password)
        {
            var (correct_credentials, deleted_user, user) = _db.Authorization(login, password);
            if (!correct_credentials)
            {
                HelperText = "Invalid login or password!";
                Login = Password = string.Empty;
            }
            else if (deleted_user) HelperText = "This account is deleted! Please contact 8-800-000-00-00!";
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
            if (Password!=PasswordRepeat)
            {
                HelperText = "Passwords don't match!";
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
                    HelperText = "Unexpected error! Try again later!";
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
            RegistrationSectionVisibility = Visibility.Hidden;
            LoginSectionVisibility = Visibility.Visible;
            Login = Password = PasswordRepeat = FirstName = LastName = HelperText = string.Empty;
            _isRegistration = false;
        }

        private void RefreshCanPressEnterState()
        {
            if (Login == string.Empty || Password==string.Empty) CanPressEnter = false;
            else CanPressEnter = true;
        }

        private void RefreshCanPressRegisterState()
        {    
            if (_isRegistration)
            {
                if (_db.IfLoginExists(Login))
                {
                    CanPressRegister = false;
                    HelperText = "This login is occupied!";
                }
                else if (Login == string.Empty || Password == string.Empty || PasswordRepeat == string.Empty ||
                    FirstName == string.Empty || LastName == string.Empty) CanPressRegister = false;

                else CanPressRegister = true;
            }  
        }
    }
}
