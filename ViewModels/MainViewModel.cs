using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;

namespace NazarTunes.ViewModels
{
    public class MainViewModel : Notifier
    {
        private CommonViewModel _commonViewModel;
        public CommonViewModel CommonViewModel
        {
            get => _commonViewModel;
            set => SetField(ref _commonViewModel, value);
        }

        private LanguagePack _language;
        public LanguagePack Language
        {
            get => _language;
            set => SetField(ref _language, value);
        }

        public AuthorizationLayerViewModel Authorization { get; set; }

        public List <string> ListLanguages { get; set; }

        private int _selectedLanguage;
        public int SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetField(ref _selectedLanguage, value);
                Language = LanguagePack.Load($"{ListLanguages[SelectedLanguage]}");
                LanguagePack.SaveIndex(_selectedLanguage);
                Authorization?.RefreshLanguage(ref _language);
            }
        }

        public MainViewModel()
        {
            ListLanguages = new List<string>() { "EN", "RU" };
            SelectedLanguage = LanguagePack.GetIndex();
            Language = LanguagePack.Load($"{ListLanguages[SelectedLanguage]}");

            CommonViewModel = new();
            Authorization = new(ref _commonViewModel!, ref _language!);

            
        }
    }
}
