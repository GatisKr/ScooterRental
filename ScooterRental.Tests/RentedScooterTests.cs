using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterTests
    {
        private string _id = "1";
        private DateTime _rentStart = new DateTime(2024, 01, 28, 12, 00, 00);
        private decimal _pricePerMinute = 0.2m;

        [TestMethod]
        public void ConstructorParameters_OnNewInstance_CorrectValuesExpected()
        {
            //Arrange
            var rentedScooter = new RentedScooter(_id, _rentStart, _pricePerMinute);

            //Assert
            rentedScooter.ScooterId.Should().Be("1");
            rentedScooter.RentStart.Should().Be(new DateTime(2024, 01, 28, 12, 00, 00));
            rentedScooter.PricePerMinute.Should().Be(0.2m);
        }

        [TestMethod]
        public void RentEnd_OnRentEnd_CorrectPropertyValueExpected()
        {
            //Arrange
            var rentedScooter = new RentedScooter(_id, _rentStart, _pricePerMinute);

            //Act
            rentedScooter.RentEnd = new DateTime(2024, 01, 28, 13, 00, 00);

            //Assert
            rentedScooter.RentEnd.Should().Be(new DateTime(2024, 01, 28, 13, 00, 00));
        }
    }
}
