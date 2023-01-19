using Containerschip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class RowTest
    {
        [TestMethod]
        public void DevideOverRow_TryToFillRow_LightWeakContainers()
        {
            // arrange
            Row row = new Row(3, 3, 1);

            List<Container> containers = new List<Container>()
            {
                new Container(1, 1, ContainerType.Normal, false),
                new Container(1, 1, ContainerType.Normal, false),
                new Container(1, 1, ContainerType.Normal, false),
                new Container(1, 1, ContainerType.Normal, false),
                new Container(1, 1, ContainerType.Normal, false)
            };

            // act
            row.DevideOverRow(containers);

            // assert
            Assert.AreEqual(2, row.Stacks.ElementAt(0).Containers.Count);
        }

        [TestMethod]
        public void DevideOverRow_TryToFillRow_LightStrongContainers()
        {
            // arrange
            Row row = new Row(3, 3, 1);

            List<Container> containers = new List<Container>()
            {
                new Container(1, 10, ContainerType.Normal, false),
                new Container(1, 10, ContainerType.Normal, false),
                new Container(1, 10, ContainerType.Normal, false),
                new Container(1, 10, ContainerType.Normal, false),
                new Container(1, 10, ContainerType.Normal, false)
            };

            // act
            row.DevideOverRow(containers);

            // assert
            Assert.AreEqual(3, row.Stacks.ElementAt(0).Containers.Count);
        }
    }
}
