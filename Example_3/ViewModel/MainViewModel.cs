using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace Example_3.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        #region Buttons
        public RelayCommand StartGenerating { get; set; }
        public RelayCommand ToDetails { get; set; }
        public RelayCommand Clear { get; set; }
        #endregion

        #region collection
        public ObservableCollection<CargoVM> Cargo { get; set; }
        #endregion

        public MainViewModel()
        {
            Cargo = new ObservableCollection<CargoVM>();
            DemoData();

            StartGenerating = new RelayCommand(() => { });
            ToDetails = new RelayCommand(() => { });
            Clear = new RelayCommand(() => { });

            
        }

        private void DemoData()
        {
            CargoVM[] CargoArray = new CargoVM[5];
            CargoArray[0] = new CargoVM("Vienna", 5, new CargoItemVM(5,6,"Bricks"));
            CargoArray[0].AddItem(new CargoItemVM(8,9,"Timber"));
            CargoArray[0].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[0].AddItem(new CargoItemVM(8, 9, "Hammer"));

            CargoArray[1] = new CargoVM("Berlin", 5, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Hammer"));

            CargoArray[2] = new CargoVM("Cape Town", 5, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Hammer"));

            CargoArray[3] = new CargoVM("Tokyo", 5, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Hammer"));
                       
            CargoArray[4] = new CargoVM("Shanghai", 5, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Hammer"));
        }
    }
}