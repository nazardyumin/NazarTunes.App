using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Windows.Documents;

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

        public List <string> Languages { get; set; }

        private int _selectedLanguage;
        public int SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetField(ref _selectedLanguage, value);
                Language = LanguagePack.Load($"Language\\{Languages[_selectedLanguage]}.lang");
                LanguagePack.SaveIndex(_selectedLanguage);
            }
        }

        public MainViewModel()
        {
            CommonViewModel = new();
            Authorization = new(ref _commonViewModel!, ref _language!);

            Languages = new List<string>() { "EN", "RU"};
            var index = LanguagePack.GetIndex();
            SelectedLanguage = index;

            Language = LanguagePack.Load($"Language\\{Languages[index]}.lang");
        }
    }
}
