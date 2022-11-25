using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class Container
    {
        public int Weight { get; private set; }
        public int CarryCapacity { get; private set; }
        public ContainerType Type { get; private set; }
        public bool NeedElectricity { get; private set; }
        public Container(int weight, int carryCapacity, ContainerType type, bool needElectricity)
        {
            Weight = weight;
            CarryCapacity = carryCapacity;
            Type = type;
            NeedElectricity = needElectricity;
        }
    }
}
