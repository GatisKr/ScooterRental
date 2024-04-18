using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterTests
    {
        private string _id = "1";
        private decimal _pricePerMinute = 0.2m;

        [TestMethod]
        public void ConstructorParameters_OnNewInstance_CorrectValuesExpected()
        {
            //Arrange
            var scooter = new Scooter(_id, _pricePerMinute);

            //Assert
            scooter.Id.Should().Be("1");
            scooter.PricePerMinute.Should().Be(0.2m);
            scooter.IsRented.Should().BeFalse();
        }
    }
}
