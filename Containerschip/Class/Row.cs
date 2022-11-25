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

        public List<Container> DevideOverRow(List<Container> containers)
        {
            List<Container> _normal = new List<Container>();
            List<Container> _valuable = new List<Container>();
            List<Container> _cooled = new List<Container>();
            List<Container> _valuableCooled = new List<Container>();

            (_normal, _valuable, _cooled, _valuableCooled) = SortContainers(containers);

            for (int i = 1; i < StackHight; i++)
            {
                foreach (Stack stack in _stacks.Where(x => x.Cooling == true))
                {
                    bool placeFilled = false;

                    if (i < StackHight)
                    {
                        (placeFilled, _cooled) = TryFillstack(stack, _cooled);

                        if (!placeFilled)
                        {
                            (placeFilled, _normal) = TryFillstack(stack, _normal);
                        }
                    }

                    else if (i == StackHight)
                    {
                        (placeFilled, _valuableCooled) = TryFillstack(stack, _valuableCooled);

                        if (!placeFilled)
                        {
                            (placeFilled, _cooled) = TryFillstack(stack, _cooled);
                        }

                        if (!placeFilled)
                        {
                            (placeFilled, _valuable) = TryFillstack(stack, _valuable);
                        }

                        if (!placeFilled)
                        {
                            (placeFilled, _normal) = TryFillstack(stack, _normal);
                        }
                    }

                }
            }

            containers.AddRange(_normal);
            containers.AddRange(_valuable);
            containers.AddRange(_cooled);
            containers.AddRange(_valuableCooled);

            return containers;
        }

        private (bool, List<Container>) TryFillstack(Stack stack, List<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (stack.TryFill(container))
                {
                    containers.Remove(container);
                    return (true, containers);
                }
            }
            return (false, containers);
        }
        private (List<Container>, List<Container>, List<Container>, List<Container>) SortContainers(List<Container> containers)
        {
            List<Container> normal = new List<Container>();
            List<Container> valuable = new List<Container>();
            List<Container> Cooled = new List<Container>();
            List<Container> ValuableCooled = new List<Container>();

            foreach(Container container in containers)
            {
                if (container.NeedElectricity)
                {
                    if (container.Type == ContainerType.Valuable)
                    {
                        ValuableCooled.Add(container);

                        break;
                    }

                    else
                    {
                        Cooled.Add(container);

                        break;
                    }
                }

                else if (container.Type == ContainerType.Valuable)
                {
                    valuable.Add(container);

                    break;
                }

                else
                {
                    normal.Add(container);

                    break;
                }
            }
            return (normal, valuable, Cooled, ValuableCooled);
        }
    }
}
