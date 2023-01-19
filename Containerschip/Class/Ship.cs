using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        // fill rows left, right, left right.
        public List<Container> DevideContainersOverShip(List<Container> containers)
        {
            if (_rows.Count() % 2 != 0)
            {
                FillMiddelRow(containers);
            }

            for (int i = 0; i < _rows.Count()/2; i++)
            {
                FillRowOnLeft(containers, i);
                FillRowOnRight(containers, i);
            }

            return containers;
        }

        private void FillMiddelRow(List<Container> containers)
        {
            _rows[_rows.Count() / 2].DevideOverRow(containers);
        }

        private void FillRowOnLeft(List<Container> containers, int NumFromLeft)
        {
            _rows[NumFromLeft].DevideOverRow(containers);
        }

        private void FillRowOnRight(List<Container> containers, int NumFromRight)
        {
            _rows[_rows.Count - 1 - NumFromRight].DevideOverRow(containers);
        }
    }
}
