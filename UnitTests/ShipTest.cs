using Containerschip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void CalcTotalWeight_CalcWeight(int weight1, int weight2, int weight3, int expectedWeight)
        {
            //arrange

            Ship ship = new Ship(3, 3, 3, 1);

            List<Container> containers = new List<Container>
            {
                new Container(weight1, 1, ContainerType.Normal, false),
                new Container(weight2, 1, ContainerType.Normal, false),
                new Container(weight3, 1, ContainerType.Normal, false)
            };

            //act
            int expected = ship.CalcTotalWeight(containers);

            //assert
            Assert.AreEqual(expectedWeight, expected);
        }

        // not yet implemented \/

        [TestMethod]
        public void FillMiddelRow_FillRow(int weight1, int weight2, int weight3, int expectedWeight)
        {
            Ship ship = new Ship(3, 3, 3, 1);

            List<Container> containers = new List<Container>
            {
                new Container(weight1, 1, ContainerType.Normal, false),
                new Container(weight2, 1, ContainerType.Normal, false),
                new Container(weight3, 1, ContainerType.Normal, false)
            };

            ship.FillMiddelRow(containers);
        }

        [TestMethod]
        public void FillRowOnLeft_FillRow(int weight1, int weight2, int weight3, int expectedWeight)
        {
            Ship ship = new Ship(3, 3, 3, 1);

            List<Container> containers = new List<Container>
            {
                new Container(weight1, 1, ContainerType.Normal, false),
                new Container(weight2, 1, ContainerType.Normal, false),
                new Container(weight3, 1, ContainerType.Normal, false)
            };

            ship.FillRowOnLeft(containers, 0);
        }

        [TestMethod]
        public void FillRowOnRight_FillRow(int weight1, int weight2, int weight3, int expectedWeight)
        {
            Ship ship = new Ship(3, 3, 3, 1);

            List<Container> containers = new List<Container>
            {
                new Container(weight1, 1, ContainerType.Normal, false),
                new Container(weight2, 1, ContainerType.Normal, false),
                new Container(weight3, 1, ContainerType.Normal, false)
            };

            ship.FillRowOnRight(containers, 0);
        }
    }
}
