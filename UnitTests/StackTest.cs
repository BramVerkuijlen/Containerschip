using Containerschip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class StackTest
    {
        [DataRow(1, 1, ContainerType.Normal, false, 5, true, true)]
        [TestMethod]
        public void TryAdd_AddContainer(int containerWeight, int containerCarryCapacity, ContainerType containertype, bool containercooling, int stackHight, bool stackCooling, bool expectedOutcome)
        {
            //arrange 
            Container container = new Container(containerWeight, containerCarryCapacity, containertype, containercooling);

            Stack stack = new Stack(stackHight, stackCooling);

            // act
            var expected = stack.TryAdd(container); 

            //assert
            Assert.AreEqual(expectedOutcome, expected);
        }

        [DataRow(1, 5, ContainerType.Normal, false, 1, 5, ContainerType.Normal, false, 10, 1, ContainerType.Normal, false, 5, true, true, false)]
        [TestMethod]
        public void TryAdd_AddContainer(int containerWeight, int containerCarryCapacity, ContainerType containertype, bool containercooling, int containerWeight2, int containerCarryCapacity2, ContainerType containertype2, bool containercooling2, int containerWeight3, int containerCarryCapacity3, ContainerType containertype3, bool containercooling3, int stackHight, bool stackCooling, bool expectedOutcome, bool expectedOutcome2, bool expectedOutcome3)
        {
            //arrange 
            Container container = new Container(containerWeight, containerCarryCapacity, containertype, containercooling);
            Container container2 = new Container(containerWeight2, containerCarryCapacity2, containertype2, containercooling2);
            Container container3 = new Container(containerWeight3, containerCarryCapacity3, containertype3, containercooling3);

            Stack stack = new Stack(stackHight, stackCooling);

            // act
            var expected = stack.TryAdd(container);
            var expected2 = stack.TryAdd(container2);
            var expected3 = stack.TryAdd(container3);

            //assert
            Assert.AreEqual(expectedOutcome, expected);
            Assert.AreEqual(expectedOutcome, expected2);
            Assert.AreEqual(expectedOutcome, expected3);
        }
    }
}
