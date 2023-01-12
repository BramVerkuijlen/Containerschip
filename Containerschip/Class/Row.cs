using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void DevideOverRow(List<Container> containers)
        {
            for (int currentHight = 1; currentHight < StackHight; currentHight++)
            {
                foreach (Stack stack in _stacks)
                {

                    if (stack.Cooling)
                    {
                        TryFillCoolingColumn(stack, currentHight, containers);
                    }

                    else
                    {
                        TryFillNormalColumn(stack, currentHight, containers);
                    }
                }
            }
        }

        private void TryFillCoolingColumn(Stack stack, int currentHight, List<Container> containers)
        {
            bool placeFilled;

            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            if (currentHight < StackHight)
            {
                placeFilled = TryFillSpace(stack, _cooled);

                if (!placeFilled)
                {
                    placeFilled = TryFillSpace(stack, _normal);
                }
            }

            else if (currentHight == StackHight)
            {
                placeFilled = TryFillSpace(stack, _valuableCooled);

                if (!placeFilled)
                {
                    placeFilled = TryFillSpace(stack, _cooled);
                }

                if (!placeFilled)
                {
                    placeFilled = TryFillSpace(stack, _valuable);
                }

                if (!placeFilled)
                {
                    placeFilled = TryFillSpace(stack, _normal);
                }
            }

            SetRemainingContainers(_normal, _valuable, _cooled, _valuableCooled, containers);
        }

        private void TryFillNormalColumn(Stack stack, int currentHight, List<Container> containers)
        {
            bool placeFilled;

            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            if (currentHight < StackHight)
            {
                placeFilled = TryFillSpace(stack, _normal);
            }

            else if (currentHight == StackHight)
            {
                placeFilled = TryFillSpace(stack, _valuable);


                if (!placeFilled)
                {
                    placeFilled = TryFillSpace(stack, _normal);
                }
            }

            SetRemainingContainers(_normal, _valuable, _cooled, _valuableCooled, containers);
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

        private bool TryFillSpace(Stack stack, List<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (stack.TryAdd(container))
                {
                    containers.Remove(container);
                    return true;
                }
            }
            return false;
        }
        private void SetRemainingContainers(List<Container> _normal, List<Container> _valuable, List<Container> _cooled, List<Container> _valuableCooled, List<Container> containers)
        {
            List<Container> Remaining = new List<Container>();

            Remaining.AddRange(_normal);
            Remaining.AddRange(_valuable);
            Remaining.AddRange(_cooled);
            Remaining.AddRange(_valuableCooled);

            containers = Remaining;
        }
    }
}
