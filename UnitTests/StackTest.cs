using Containerschip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual(expected, expectedOutcome);
        }

        [DataRow(1, 1, ContainerType.Normal, false, 1, 1, ContainerType.Normal, false, true)]
        [TestMethod]
        public void CanPlaceOnTop(int containerWeight1, int containerCarryCapacity1, ContainerType containertype1, bool containercooling1, int containerWeight2, int containerCarryCapacity2, ContainerType containertype2, bool containercooling2, bool expecteOutcome)
        {
            // arrange
            Container container1 = new Container(containerWeight1, containerCarryCapacity1, containertype1, containercooling1);
            Container container2 = new Container(containerWeight2, containerCarryCapacity2, containertype2, containercooling2);

            Stack stack = new Stack(1000, true);

            // act
            if (!stack.TryAdd(container1)) // <- Moq container lijst?
            {
                Assert.Fail();
            }

            var expected = stack.CanPlaceOnTop(container2);

            // assert

            Assert.AreEqual(expected, expecteOutcome);
        }

        [TestMethod]
        public void WillColapseStack(int containerWeight, int containerCarryCapacity, ContainerType containertype, bool containercooling, bool expecteOutcome)
        {
            // arrange
            Container container = new Container(containerWeight, containerCarryCapacity, containertype, containercooling);

            Stack stack = new Stack(1000, true);

            // act
            var expected = stack.WillColapseStack(container); // <- Moq container lijst?

            // assert

            Assert.AreEqual(expected, expecteOutcome);
        }
    }
}
