using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_3.ViewModel
{
    public class CargoVM
    {

        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public ObservableCollection<CargoItemVM> CargoItem { get; set; }

        public CargoVM(string description, int deliveryTime, CargoItemVM item)
        {
            CargoItem = new ObservableCollection<CargoItemVM>();
            Description = description;
            DeliveryTime = deliveryTime;
            AddItem(item);
        }

        public void AddItem(CargoItemVM item)
        {
            CargoItem.Add(item);
        }
    }
}
