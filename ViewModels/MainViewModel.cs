using NazarTunes.ViewModels.Notifiers;

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

        public AuthorizationLayerViewModel Authorization { get; set; }

        public MainViewModel()
        {
            CommonViewModel = new();
            Authorization = new(ref _commonViewModel!);
        }
    }
}
