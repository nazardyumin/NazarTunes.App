using MySqlX.XDevAPI.Common;
using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab6
{
    public class FreezeNomenclature : Notifier
    {
        protected readonly AdminLayerDb _refDb;
        protected readonly Database _refDatabase;

        private string? _clientId;
        public string? ClientId
        {
            get => _clientId;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _clientId, value);
                if (_clientId!.Length > 0)
                {
                    ClientsPhone = ClientsEmail = string.Empty;
                }
                CommandFindClient?.OnCanExecuteChanged();
                HelperTextSearchClient = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
            }
        }

        private string? _clientsPhone;
        public string? ClientsPhone
        {
            get => _clientsPhone;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _clientsPhone, value);
                if (_clientsPhone!.Length > 0)
                {
                    ClientId = ClientsEmail = string.Empty;
                }
                CommandFindClient?.OnCanExecuteChanged();
                HelperTextSearchClient = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
            }
        }

        private string? _clientsEmail;
        public string? ClientsEmail
        {
            get => _clientsEmail;
            set
            {
                SetField(ref _clientsEmail, value);
                if (_clientsEmail!.Length > 0)
                {
                    ClientId = ClientsPhone = string.Empty;
                }
                CommandFindClient?.OnCanExecuteChanged();
                HelperTextSearchClient = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
            }
        }

        private string? _clientsFullName;
        public string? ClientsFullName
        {
            get => _clientsFullName;
            set => SetField(ref _clientsFullName, value);
        }

        private int _actualClientId;

        private Nomenclature? _selectedNomenclature;
        public Nomenclature? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        private string? _amount;
        public string? Amount
        {
            get => _amount;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _amount, value);
            }
        }

        private string? _helperTextSearchClient;
        public string HelperTextSearchClient
        {
            get => _helperTextSearchClient!;
            set => SetField(ref _helperTextSearchClient, value);
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public MyCommand CommandFindClient { get; }

        public FreezeNomenclature(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            ClientId = ClientsPhone = ClientsEmail = string.Empty;

            CommandFindClient = new(_ =>
            {
                FindClientFunction();
            }, _ => RefreshCanFindState());

            IsVisible = Visibility.Collapsed;        
        }

        public int OpenClose()
        {
            int height;
            if (IsVisible == Visibility.Visible)
            {
                Hide();
                height = 47;
            }
            else
            {
                Show();
                height = 150;
            }
            return height;
        }

        private void Show()
        {
            IsVisible = Visibility.Visible;
        }

        private void Hide()
        {
            IsVisible = Visibility.Collapsed;
        }

        private void RemoveLettersOrSymbols(ref string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > 10) str = str.Remove(10); 
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i])) str = str.Remove(i, 1);
                }
            }
        }

        private bool RefreshCanFindState()
        {
            if (!string.IsNullOrEmpty(ClientId) || !string.IsNullOrEmpty(ClientsPhone) || !string.IsNullOrWhiteSpace(ClientsEmail)) return true;
            else return false;
        }

        private void FindClientFunction()
        {
            var id = ClientId == string.Empty ? 0 : int.Parse(ClientId!);
            if (_refDb.CheckIfClientExists(id, ClientsPhone!, ClientsEmail!))
            {
                var result = _refDb.GetClientsFullNameAndId(id, ClientsPhone!, ClientsEmail!);
                ClientsFullName = result.clientsFullName;
                _actualClientId = result.id; //reset after add new frozem item!!!
            }
            else
            {
                HelperTextSearchClient = LanguagePack.GetClientsNotFoundHelperText();
            }
        }
    }
}
