using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_3.ViewModel
{
    public class CargoItemVM
    {

        public int Amount { get; set; }
        public float Weight { get; set; }
        public string Name { get; set; }
        public CargoItemVM(int amount, float weight, string name)
        {
            Amount = amount;
            Weight = weight;
            Name = name;
        }


    }
}
