using FluentAssertions;
using ScooterRental.Interface;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterArchiveTests
    {
        private List<RentedScooter> _rentedScooters;
        private IRentedScooterArchive _archive;

        [TestInitialize]
        public void Setup()
        {
            _rentedScooters =
            [
                new RentedScooter("1", new DateTime(2024,01,28,10,00,00),0.2m),
                new RentedScooter("2", new DateTime(2024, 01, 28, 11, 00, 00), 0.2m),
            ];
            _archive = new RentedScooterArchive(_rentedScooters);
        }

        [TestMethod]
        public void RentedScooters_ListShouldContainAddedItems_CorrectCountValueExpected()
        {
            //Assert
            _rentedScooters.Count.Should().Be(2);
        }

        [TestMethod]
        public void AddRentedScooter_ShouldAddRentedScooterToList_CorrectCountValueExpected()
        {
            //Arrange
            _archive.AddRentedScooter(new RentedScooter("3", new DateTime(2024, 01, 28, 12, 00, 00), 0.2m));

            //Assert
            _rentedScooters.Count.Should().Be(3);
        }

        [TestMethod]
        public void EndRental_OnEndRental_ShouldReturnCorrectIdAndDateTime()
        {
            //Arrange
            _archive.AddRentedScooter(new RentedScooter("3", new DateTime(2024, 01, 28, 12, 00, 00), 0.2m));

            //Act
            var result = _archive.EndRental("3", new DateTime(2024, 01, 28, 13, 00, 00));

            //Assert
            result.ScooterId.Should().Be("3");
            result.RentEnd.Should().Be(new DateTime(2024, 01, 28, 13, 00, 00));
        }

        [TestMethod]
        public void GetRentedScooters_ShouldReturnScootersList()
        {
            //Act
            var scooters = _archive.GetRentedScooters();

            //Assert
            scooters.Count.Should().Be(2);
        }
    }
}
