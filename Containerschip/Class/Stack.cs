using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class Stack
    {
        public int MaxHight { get; private set; }
        public bool Cooling { get; private set; }
        public bool Full
        {
            get { return (MaxHight <= _containers.Count); }
        }

        private List<Container> _containers = new List<Container>();
        public IReadOnlyCollection<Container> Containers
        {
            get
            {
                return _containers.AsReadOnly();
            }
        }
        public Stack(int stackHight, bool isCooling)
        {
            MaxHight = stackHight;
            Cooling = isCooling;
        }

        public bool TryFill(Container container)
        {
            return true;
        }
    }
}

