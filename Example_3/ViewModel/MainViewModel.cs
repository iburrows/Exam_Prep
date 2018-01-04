using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Example_3.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand StartGenerating { get; set; }
        public RelayCommand ToDetails { get; set; }
        public RelayCommand Clear { get; set; }
        public MainViewModel()
        {
            StartGenerating = new RelayCommand(() => { });
            ToDetails = new RelayCommand(() => { });
            Clear = new RelayCommand(() => { });
        }
    }
}