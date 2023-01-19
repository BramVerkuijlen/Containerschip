using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Containerschip
{
    public class Row
    {
        public int MaxLength { get; private set; }
        public int StackHight { get; private set; }

        private List<Stack> _stacks = new List<Stack>();

        public IReadOnlyCollection<Stack> Stacks
        {
            get
            {
                return _stacks.AsReadOnly();
            }
        }
        public Row(int rowLength, int stackHight, int amountOfCooledColumns)
        {
            MaxLength = rowLength;
            StackHight = stackHight;

            for (int i = 0; i < amountOfCooledColumns; i++)
            {
                _stacks.Add(new Stack(stackHight, true));
            }

            for (int i = 0; i < MaxLength - amountOfCooledColumns; i++)
            {
                _stacks.Add(new Stack(stackHight, false));
            }
        }

        public List<Container> DevideOverRow(List<Container> containers)
        {
            for (int currentHight = 0; currentHight < StackHight; currentHight++)
            {
                foreach (Stack stack in _stacks)
                {

                    if (stack.Cooling)
                    {
                        containers = TryFillCoolingColumn(stack, currentHight, containers);
                    }

                    else
                    {
                        containers = TryFillNormalColumn(stack, currentHight, containers);
                    }
                }
            }
            return containers;
        }

        private List<Container> TryFillCoolingColumn(Stack stack, int currentHight, List<Container> containers)
        {
            bool placeFilled;

            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            if (currentHight < StackHight - 1)
            {
                (placeFilled, _cooled) = TryFillSpace(stack, _cooled);

                if (!placeFilled)
                {
                    (_, _normal) = TryFillSpace(stack, _normal);
                }
            }

            else if (currentHight == StackHight - 1)
            {
                (placeFilled, _valuableCooled) = TryFillSpace(stack, _valuableCooled);

                if (!placeFilled)
                {
                    (placeFilled, _cooled) = TryFillSpace(stack, _cooled);
                }

                if (!placeFilled)
                {
                    (placeFilled, _valuable) = TryFillSpace(stack, _valuable);
                }

                if (!placeFilled)
                {
                    (_, _normal) = TryFillSpace(stack, _normal);
                }
            }

            return containers = SetRemainingContainers(_normal, _valuable, _cooled, _valuableCooled);
        }

        private List<Container> TryFillNormalColumn(Stack stack, int currentHight, List<Container> containers)
        {
            bool placeFilled;

            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            if (currentHight < StackHight - 1)
            {
                (_, _normal) = TryFillSpace(stack, _normal);
            }

            else if (currentHight == StackHight - 1)
            {
                (placeFilled, _valuable) = TryFillSpace(stack, _valuable);


                if (!placeFilled)
                {
                    (_, _normal) = TryFillSpace(stack, _normal);
                }
            }

            return containers = SetRemainingContainers(_normal, _valuable, _cooled, _valuableCooled);
        }
        private (List<Container>, List<Container>, List<Container>, List<Container>) SortContainers(List<Container> containers)
        {
            List<Container> normal = new List<Container>();
            List<Container> valuable = new List<Container>();
            List<Container> Cooled = new List<Container>();
            List<Container> ValuableCooled = new List<Container>();

            foreach (Container container in containers)
            {
                if (container.NeedElectricity)
                {
                    if (container.Type == ContainerType.Valuable)
                    {
                        ValuableCooled.Add(container);
                    }

                    else
                    {
                        Cooled.Add(container);
                    }
                }

                else if (container.Type == ContainerType.Valuable)
                {
                    valuable.Add(container);
                }

                else
                {
                    normal.Add(container);
                }
            }
            return (normal, valuable, Cooled, ValuableCooled);
        }

        private (bool, List<Container>) TryFillSpace(Stack stack, List<Container> containers)
        {
            bool spaceFilled = false;
            int filledContainerIndex = 0;

            for (int i = 0; i < containers.Count(); i++)
            {
                if (!spaceFilled)
                {
                    if (stack.TryAdd(containers[i]))
                    {
                        spaceFilled= true;
                        filledContainerIndex= i;
                    }
                }
            }

            if (spaceFilled)
            {
                containers.RemoveAt(filledContainerIndex);
                return (true, containers);
            }

            return (false, containers);
        }
        private List<Container> SetRemainingContainers(List<Container> _normal, List<Container> _valuable, List<Container> _cooled, List<Container> _valuableCooled)
        {
            List<Container> Remaining = new List<Container>();

            Remaining.AddRange(_normal);
            Remaining.AddRange(_valuable);
            Remaining.AddRange(_cooled);
            Remaining.AddRange(_valuableCooled);

            return Remaining;
        }
    }
}
