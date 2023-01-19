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
        public void DevideContainersOverShip_DevideContainersOverShip_SameWeigtSameCarryC()
        {
            //arrange
            Ship ship = new Ship(3, 3, 3, 1);

            List<Container> containers = new List<Container>
            {
                // normal
                new Container(10, 40, ContainerType.Normal, false),
                new Container(10, 40, ContainerType.Normal, false),
                new Container(10, 40, ContainerType.Normal, false),
                new Container(10, 40, ContainerType.Normal, false),

                // cooled
                new Container(10, 40, ContainerType.Normal, true),
                new Container(10, 40, ContainerType.Normal, true),
                new Container(10, 40, ContainerType.Normal, true),
                new Container(10, 40, ContainerType.Normal, true),

                // valuable
                new Container(10, 40, ContainerType.Valuable, false),
                new Container(10, 40, ContainerType.Valuable, false),
                new Container(10, 40, ContainerType.Valuable, false),
                new Container(10, 40, ContainerType.Valuable, false),

                // valuable cooled
                new Container(10, 40, ContainerType.Valuable, true),
                new Container(10, 40, ContainerType.Valuable, true),
                new Container(10, 40, ContainerType.Valuable, true),
                new Container(10, 40, ContainerType.Valuable, true),
            };

            //act
            var expectedAccess = ship.DevideContainersOverShip(containers);

            //assert
            Assert.AreEqual(1, expectedAccess.Count());
        }
    }
    
}
