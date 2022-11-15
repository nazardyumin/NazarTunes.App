using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels
{
    public class MainViewModel : Notifier
    {
        private CommonViewModelData _viewModelData;
        public CommonViewModelData ViewModelData { get => _viewModelData; set =>SetField(ref _viewModelData, value); }

        public AuthorizationViewModel Authorization { get; set; }

        public MainViewModel()
        {
            ViewModelData = new();
            Authorization = new (ref _viewModelData!);
        }

    }
}
