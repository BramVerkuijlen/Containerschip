using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containerschip
{
    public class Ship
    {
        private List<Row> _rows = new List<Row>();
        public IReadOnlyCollection<Row> Rows
        {
            get
            {
                return _rows.AsReadOnly();
            }
        }

        public Ship(int numRows, int rowLength, int stackHight, int amountOfCooledColumns)
        {
            for (int i = 0; i < numRows; i++)
            {
                _rows.Add(new Row(rowLength, stackHight, amountOfCooledColumns));
            }
        }

        public List<Container> DevideContainersOverShip(List<Container> containers)
        {
            if (_rows.Count() % 2 == 0)
            {
                containers = FillMiddelRow(containers);
            }

            for (int i = 0; i < _rows.Count()/2; i++)
            {
                FillRowOnLeft(containers, i);
                FillRowOnRight(containers, i);
            }

            return containers;
        }
        
        private int CalcTotalWeight(List<Container> containers)
        {
            return containers.Sum(e => e.Weight);
        }

        private List<Container> FillMiddelRow(List<Container> containers)
        {
            return _rows[_rows.Count() / 2 + 1].DevideOverRow(containers);
        }

        private List<Container> FillRowOnLeft(List<Container> containers, int NumFromLeft)
        {
            return _rows[NumFromLeft].DevideOverRow(containers);
        }

        private List<Container> FillRowOnRight(List<Container> containers, int NumFromRight)
        {
            return _rows[_rows.Count-1-NumFromRight].DevideOverRow(containers);
        }
    }
}
