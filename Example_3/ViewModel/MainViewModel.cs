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
        public ObservableCollection<CargoItemVM> ItemCollection { get; set; }
        //public ObservableCollection<CargoItemVM> DetailsCollection { get; set; }
        #endregion

        #region properties for waiting for
        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public ObservableCollection<CargoItemVM> CargoItem{ get; set; }
        #endregion

        #region properties for details
        public string Name { get; set; }
        public string Weight { get; set; }
        public string Amount { get; set; }

        #endregion

        #region arrays for data
        public string[] descriptionArray = { "Vienna", "Cape Town", "Berlin", "Tokyo", "Shanghai" };
        public string[] itemArray = { "Hammer", "Bricks", "Bottles", "Cement", "Wood" };
        #endregion

        Thread TimerThread;
        Thread CheckThread;
        public static Random random;
        Timer ArrayGenerationTimer;
        Timer CheckWaitingTimeTimer;

        public CargoVM SelectedItem { get; set; }

        public CargoVM[] CargoArray { get; set; }

        public MainViewModel()
        {
            Cargo = new ObservableCollection<CargoVM>();
            ItemCollection = new ObservableCollection<CargoItemVM>();
            DetailsCollection = new ObservableCollection<CargoItemVM>();
            ReadyCollection = new ObservableCollection<CargoVM>();
            StartGenerating = new RelayCommand(() => { StartTimer(); });

            GenerateArrayData();

            ToDetails = new RelayCommand<CargoVM>((p) => 
            {
                foreach (var item in p.CargoItems)
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
            

        }

        private void StartTimer()
        {
            TimerThread = new Thread(new ThreadStart(StartArrayTimer));
            TimerThread.IsBackground = true;
            TimerThread.Start();

            //CheckThread = new Thread(new ThreadStart(StartCheckTimer));
            //CheckThread.IsBackground = true;
            //CheckThread.Start();
        }

        private void StartWaitingListTimer()
        {
            CheckThread = new Thread(new ThreadStart(StartCheckTimer));
            CheckThread.IsBackground = true;
            CheckThread.Start();
        }

        private void StartCheckTimer()
        {
            CheckWaitingTimeTimer = new Timer(CheckWaitingCollection, null, 1000, 1000);
        }

        private void CheckWaitingCollection(object state)
        {
            int time = 0;
            CargoVM itemToRemove = new CargoVM();
            CargoVM itemToAdd = new CargoVM();
            //time++;
            //for (int i = 0; i < Cargo.Count; i++)
            //{
            //    if (Cargo[i].DeliveryTime >= time)
            //    {
            //        itemToAdd = Cargo[i];
            //        App.Current.Dispatcher.Invoke(() =>
            //        {
            //            Cargo.Remove(Cargo[i]);
            //        });
            //    }
            //}

            //App.Current.Dispatcher.Invoke(() => {
            //    if (itemToAdd.DeliveryTime != 0)
            //    {
            //        ReadyCollection.Add(itemToAdd);
            //        time = 0;
            //        CheckThread.Abort();
            //    }

            //});

            foreach (var item in Cargo)
            {
                if (item != null)
                {
                    if (item.DeliveryTime >= time)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            ReadyCollection.Add(item);
                            itemToRemove = item;
                        });

                    }
                }
            }
            time++;
            App.Current.Dispatcher.Invoke(() =>
            {
                Cargo.Remove(itemToRemove);
                time = 0;
            });

        }

        private void StartArrayTimer()
        {
            ArrayGenerationTimer = new Timer(StartRandomArrayGeneration, null, 0, 5000);
        }

        private void StartRandomArrayGeneration(object state)
        {
            //int randomArrayNumber = GetRandomNumber(0, 4);
            //int randomWeightNumber = GetRandomNumber(1, 100);
            //int randomAmountNumber = GetRandomNumber(1, 50);
            //int randomItemNumber = GetRandomNumber(1, 10);
            //int randomDelivery = GetRandomNumber(1, 5);
            //int randomDescriptionNumber = GetRandomNumber(0, 4);

            //GenerateArrayData(randomDelivery, randomArrayNumber, randomWeightNumber, randomAmountNumber, randomItemNumber, randomDescriptionNumber);

            //GenerateArrayData();

            App.Current.Dispatcher.Invoke(()=> {

                int ArrayNumberToAdd = GetRandomNumber(0, 4);

                ReadyCollection.Add(CargoArray[ArrayNumberToAdd]);
                Cargo.Add(CargoArray[ArrayNumberToAdd]);
            });
            StartWaitingListTimer();




        }

        //private void GenerateArrayData(int delivery, int arrayNumber, int weight, int amount, int item, int description)
        private void GenerateArrayData()
        {
            CargoArray = new CargoVM[5];
            int loopTimes = GetRandomNumber(1, 10);

            for (int i = 0; i < loopTimes; i++)
            {
                int amount = GetRandomNumber(1, 50);
                int weight = GetRandomNumber(1, 100);
                int itemInArray = GetRandomNumber(0, 4);

                //ItemCollection.Add(
                //    new CargoItemVM(GetRandomNumber(1,50), 
                //    GetRandomNumber(1,100), 
                //    itemArray[GetRandomNumber(0,4)]));


                ItemCollection.Add(
                    new CargoItemVM(amount,
                    weight,
                    itemArray[itemInArray]));
            }

            CargoArray[GetRandomNumber(0,4)] = new CargoVM(descriptionArray[GetRandomNumber(0,4)], GetRandomNumber(1,5), ItemCollection);

            //CargoArray[0] = new CargoVM("Vienna", 5, new CargoItemVM(5,6,"Bricks"));
            //CargoArray[0].AddItem(new CargoItemVM(8, 9,"Timber"));
            //CargoArray[0].AddItem(new CargoItemVM(8, 9, "Nails"));
            //CargoArray[0].AddItem(new CargoItemVM(8, 9, "Hammer"));

            //CargoArray[1] = new CargoVM("Berlin", 2, new CargoItemVM(5, 6, "Bricks"));
            //CargoArray[1].AddItem(new CargoItemVM(8, 9, "Timber"));
            //CargoArray[1].AddItem(new CargoItemVM(8, 9, "Nails"));
            //CargoArray[1].AddItem(new CargoItemVM(8, 9, "Hammer"));

            //CargoArray[2] = new CargoVM("Cape Town", 3, new CargoItemVM(5, 6, "Bricks"));
            //CargoArray[2].AddItem(new CargoItemVM(8, 9, "Timber"));
            //CargoArray[2].AddItem(new CargoItemVM(8, 9, "Nails"));
            //CargoArray[2].AddItem(new CargoItemVM(8, 9, "Hammer"));

            //CargoArray[3] = new CargoVM("Tokyo", 1, new CargoItemVM(5, 6, "Bricks"));
            //CargoArray[3].AddItem(new CargoItemVM(8, 9, "Timber"));
            //CargoArray[3].AddItem(new CargoItemVM(8, 9, "Nails"));
            //CargoArray[3].AddItem(new CargoItemVM(8, 9, "Hammer"));
                       
            //CargoArray[4] = new CargoVM("Shanghai", 4, new CargoItemVM(5, 6, "Bricks"));
            //CargoArray[4].AddItem(new CargoItemVM(8, 9, "Timber"));
            //CargoArray[4].AddItem(new CargoItemVM(8, 9, "Nails"));
            //CargoArray[4].AddItem(new CargoItemVM(8, 9, "Hammer"));

        }

        private static int GetRandomNumber(int min, int max)
        {
            random = new Random();
            return random.Next(min, max);
        }
    }
}