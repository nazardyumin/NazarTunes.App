using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels
{
    public class MainViewModel : Notifier
    {
        private CommonViewModel _commonData;
        public CommonViewModel CommonData 
        { 
            get => _commonData; 
            set =>SetField(ref _commonData, value); 
        }

        public AuthorizationLayerViewModel Authorization { get; set; }

        public MainViewModel()
        {
            CommonData = new();
            Authorization = new (ref _commonData!);
        }
    }
}
