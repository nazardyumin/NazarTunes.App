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
                RemoveLettersOrSymbols(ref value!, "cut digits");
                SetField(ref _clientId, value);
                if (_clientId!.Length > 0)
                {
                    ClientsPhone = ClientsEmail = string.Empty;
                }
                CommandFindClient?.OnCanExecuteChanged();
                HelperTextSearchClient = string.Empty;
                HelperTextAmountExceeds = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
                CommandFreezeNomenclature?.OnCanExecuteChanged();
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
                HelperTextAmountExceeds = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
                CommandFreezeNomenclature?.OnCanExecuteChanged();
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
                HelperTextAmountExceeds = string.Empty;
                ClientsFullName = string.Empty;
                _actualClientId = 0;
                CommandFreezeNomenclature?.OnCanExecuteChanged();
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
            set
            {
                SetField(ref _selectedNomenclature, value);
                CommandFreezeNomenclature?.OnCanExecuteChanged();
                HelperTextAmountExceeds = string.Empty;
            }
        }

        private string? _amount;
        public string? Amount
        {
            get => _amount;
            set
            {
                RemoveLettersOrSymbols(ref value!, "cut digits", "remove zero");
                SetField(ref _amount, value);
                CommandFreezeNomenclature?.OnCanExecuteChanged();
                HelperTextAmountExceeds = string.Empty;
            }
        }

        private string? _helperTextSearchClient;
        public string HelperTextSearchClient
        {
            get => _helperTextSearchClient!;
            set => SetField(ref _helperTextSearchClient, value);
        }

        private string? _helperTextAmountExceeds;
        public string HelperTextAmountExceeds
        {
            get => _helperTextAmountExceeds!;
            set => SetField(ref _helperTextAmountExceeds, value);
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public MyCommand CommandFindClient { get; }
        public MyCommand CommandFreezeNomenclature { get; }

        public FreezeNomenclature(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            ClientId = ClientsPhone = ClientsEmail = Amount = string.Empty;

            CommandFindClient = new(_ =>
            {
                FindClientFunction();
            }, _ => RefreshCanFindState());

            CommandFreezeNomenclature = new(_ =>
            {
                FreezeNomenclatureFunction();
            }, _ => RefreshFreezeNomenclatureState());

            IsVisible = Visibility.Collapsed;        
        }

        public int OpenClose()
        {
            int height;
            if (IsVisible == Visibility.Visible)
            {
                Hide();
                Clear();
                height = 47;
            }
            else
            {
                Show();
                height = 160;
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

        private void RemoveLettersOrSymbols(ref string str, string? key = null, string? removeZero = null)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (key is not null && str.Length > 10)
                {
                    str = str.Remove(10);
                }
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i])) str = str.Remove(i, 1);
                }
                if (removeZero is not null && str.Length == 1 && str[0]=='0')
                {
                    str = str.Remove(0);
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
                _actualClientId = result.id; //reset after adding new frozem item!!!
                CommandFreezeNomenclature?.OnCanExecuteChanged();
            }
            else
            {
                HelperTextSearchClient = LanguagePack.GetClientsNotFoundHelperText();
            }
        }

        private bool RefreshFreezeNomenclatureState()
        {
            if (_actualClientId!=0 && SelectedNomenclature is not null && !string.IsNullOrEmpty(Amount)) return true;
            else return false;
        }

        private void FreezeNomenclatureFunction()
        {
            if (!_refDb.CheckIfEnteredAmountExceedsActual(SelectedNomenclature!.NomenclatureId, int.Parse(Amount!)))
            {
                _refDb.AddNewFrozenItem(SelectedNomenclature!.NomenclatureId, _actualClientId, int.Parse(Amount!));
                _refDatabase.RefreshNomenclaturesOnly();
                Clear(); 
            }
            else
            {
                Amount = string.Empty;
                HelperTextAmountExceeds = LanguagePack.GetEnteredAmountHelperText();               
            }
            
        }

        private void Clear()
        {
            ClientId = ClientsPhone = ClientsEmail = ClientsFullName = HelperTextSearchClient = HelperTextAmountExceeds = Amount = string.Empty;
            SelectedNomenclature = null;
            _actualClientId = 0;
        }
    }
}
