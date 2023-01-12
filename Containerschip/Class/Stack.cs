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

        public bool TryAdd(Container newContainer)
        {
            if (Full)
            {
                return false;
            }

            if (!CanPlaceOnTop(newContainer))
            {
                return false;
                }

            if (WillColapseStack(newContainer))
            {
                return false;
            }

            _containers.Add(newContainer);
            return true;
        }
        public bool CanPlaceOnTop(Container newContainer)
        {
            if (_containers.Count() != 0)
            {
                return (_containers.Last().Type != ContainerType.Valuable);
            }
            return true;
        }

        public bool WillColapseStack(Container newContainer)
        {
            for (int currentContainer = 0; currentContainer < _containers.Count(); currentContainer++)
            {
                int WeightOnTop = 0;

                for (int i = 1 + currentContainer; i < Containers.Count(); i++)
                {
                    WeightOnTop += _containers[i].Weight;
                }

                if (_containers[currentContainer].CarryCapacity < WeightOnTop + newContainer.Weight)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

