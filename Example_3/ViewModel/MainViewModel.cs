using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Example_3.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region Buttons
        public RelayCommand StartGenerating { get; set; }
        public RelayCommand<CargoVM> ToDetails { get; set; }
        //public RelayCommand<CargoItemVM> ToDetails { get; set; }
        public RelayCommand Clear { get; set; }
        #endregion

        #region collection
        public ObservableCollection<CargoVM> Cargo { get; set; }
        public ObservableCollection<CargoVM> ReadyCollection { get; set; }
        public ObservableCollection<CargoItemVM> DetailsCollection { get; set; }
        //public ObservableCollection<CargoItemVM> DetailsCollection { get; set; }
        #endregion

        #region properties for waiting for
        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public ObservableCollection<CargoItemVM> CargoItem { get; set; }
        #endregion

        #region properties for details
        public string Name { get; set; }
        public string Weight { get; set; }
        public string Amount { get; set; }

        #endregion

        Thread TimerThread;
        Random random;
        Random randomDelivery;
        Timer ArrayGenerationTimer;


        public CargoVM SelectedItem { get; set; }

        public CargoVM[] CargoArray { get; set; }
        public string[] DescriptionArray { get; set; }

        public MainViewModel()
        {
            Cargo = new ObservableCollection<CargoVM>();

            DetailsCollection = new ObservableCollection<CargoItemVM>();
            ReadyCollection = new ObservableCollection<CargoVM>();
            StartGenerating = new RelayCommand(() => { StartTimer(); });

            ToDetails = new RelayCommand<CargoVM>((p) =>
            {
                foreach (var item in p.CargoItem)
                {
                    DetailsCollection.Add(item);
                }

            });
            Clear = new RelayCommand(() =>
            {
                if (DetailsCollection == null)
                {
                    DetailsCollection = new ObservableCollection<CargoItemVM>();
                }

                DetailsCollection.Clear();
            });

            //GenerateArrayData();
            //ArrayData();


        }

        private void ArrayData()
        {
            DescriptionArray = new string[5];

        }

        private void StartTimer()
        {
            TimerThread = new Thread(new ThreadStart(StartArrayTimer));
            TimerThread.IsBackground = true;
            TimerThread.Start();

        }

        private void StartArrayTimer()
        {
            ArrayGenerationTimer = new Timer(StartRandomArrayGeneration, null, 0, 5000);
        }

        private void StartRandomArrayGeneration(object state)
        {
            CargoVM cargoToAdd = new CargoVM();

            random = new Random();
            int randomArrayNumber = random.Next(0, 4);

            randomDelivery = new Random();
            int randomDeliveryNumber = randomDelivery.Next(1, 5);

            switch (randomArrayNumber)
            {
                case 0:
                    cargoToAdd = GenerateViennaData(randomDeliveryNumber);
                    break;
                case 1:
                    cargoToAdd = GenerateBerlinData(randomDeliveryNumber);
                    break;
                case 2:
                    cargoToAdd = GenerateCapeTownData(randomDeliveryNumber);
                    break;
                case 3:
                    cargoToAdd = GenerateTokyoData(randomDeliveryNumber);
                    break;
                case 4:
                    cargoToAdd = GenerateShanghaiData(randomDeliveryNumber);
                    break;
                default:
                    break;
            }



            App.Current.Dispatcher.Invoke(() =>
            {
                //ReadyCollection.Add(CargoArray[randomNumber]);
                if (!Cargo.Contains(cargoToAdd))
                {
                    Cargo.Add(cargoToAdd);
                }


            });

        }

        private CargoVM GenerateViennaData(int randomDeliveryNumber)
        {
            if (CargoArray == null)
            {
                CargoArray = new CargoVM[5];
            }

            CargoArray[0] = new CargoVM("Vienna", randomDeliveryNumber, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[0].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[0].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[0].AddItem(new CargoItemVM(8, 9, "Hammer"));

            return CargoArray[0];

        }

        private CargoVM GenerateBerlinData(int randomDeliveryNumber)
        {
            if (CargoArray == null)
            {
                CargoArray = new CargoVM[5];
            }

            CargoArray[1] = new CargoVM("Berlin", randomDeliveryNumber, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[1].AddItem(new CargoItemVM(8, 9, "Hammer"));

            return CargoArray[1];

        }


        private CargoVM GenerateCapeTownData(int randomDeliveryNumber)
        {
            if (CargoArray == null)
            {
                CargoArray = new CargoVM[5];
            }

            CargoArray[2] = new CargoVM("Cape Town", randomDeliveryNumber, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[2].AddItem(new CargoItemVM(8, 9, "Hammer"));

            return CargoArray[2];
        }

        private CargoVM GenerateTokyoData(int randomDeliveryNumber)
        {
            if (CargoArray == null)
            {
                CargoArray = new CargoVM[5];
            }

            CargoArray[3] = new CargoVM("Tokyo", randomDeliveryNumber, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[3].AddItem(new CargoItemVM(8, 9, "Hammer"));

            return CargoArray[3];
        }

        private CargoVM GenerateShanghaiData(int randomDeliveryNumber)
        {
            if (CargoArray == null)
            {
                CargoArray = new CargoVM[5];
            }

            CargoArray[4] = new CargoVM("Shanghai", randomDeliveryNumber, new CargoItemVM(5, 6, "Bricks"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Timber"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Nails"));
            CargoArray[4].AddItem(new CargoItemVM(8, 9, "Hammer"));

            return CargoArray[4];
        }







    }
}
