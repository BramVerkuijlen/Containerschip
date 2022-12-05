using System;
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
            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            for (int i = 1; i < StackHight; i++)
            {
                foreach (Stack stack in _stacks)
                {
                    bool placeFilled = false;

                    if (stack.Cooling)
                    {
                        if (i < StackHight)
                        {
                            placeFilled = TryFillSpace(stack, _cooled);

                            if (!placeFilled)
                            {
                                placeFilled = TryFillSpace(stack, _normal);
                            }
                        }

                        else if (i == StackHight)
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
                    }

                    else
                    {
                        if (i < StackHight)
                        {
                            placeFilled = TryFillSpace(stack, _normal);
                        }

                        else if (i == StackHight)
                        {
                            placeFilled = TryFillSpace(stack, _valuable);


                            if (!placeFilled)
                            {
                                placeFilled = TryFillSpace(stack, _normal);
                            }
                        }
                    }
                }
            }

            List<Container> Remaining = new List<Container>();

            Remaining.AddRange(_normal);
            Remaining.AddRange(_valuable);
            Remaining.AddRange(_cooled);
            Remaining.AddRange(_valuableCooled);

            containers =  Remaining;
        }

        public void TryFillStacKCooling()
        {

        }

        private bool TryFillSpace(Stack stack, List<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (stack.TryFill(container))
                {
                    containers.Remove(container);
                    return true;
                }
            }
            return false;
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
    }
}
