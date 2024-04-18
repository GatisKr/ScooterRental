using FluentAssertions;
using ScooterRental.Interface;
using ScooterRental.Exceptions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCalculatorServiceTests
    {
        private IRentalCalculatorService _calculatorService;
        private List<RentedScooter> _rentedScooters;

        [TestInitialize]
        public void Setup()
        {
            _rentedScooters = new List<RentedScooter>
            {
                new RentedScooter("1", new DateTime(2023, 12, 28, 10, 00, 00), 0.2m)
                {
                    RentEnd = new DateTime(2023, 12, 29, 01, 00, 00)
                },
                new RentedScooter("1", new DateTime(2024, 01, 28, 10, 00, 00), 0.2m)
                {
                    RentEnd = new DateTime(2024, 01, 28, 12, 00, 00)
                },
                new RentedScooter("2", new DateTime(2024, 01, 28, 11, 00, 00), 0.2m),
            };

            _calculatorService = new RentalCalculatorService(_rentedScooters);
        }

        [TestMethod]
        public void CalculateRent_SingleDayRent_CorrectCostExpected()
        {
            // Arrange
            var rentedScooter = new RentedScooter("3", new DateTime(2024, 01, 28, 12, 00, 00), 0.2m)
            {
                RentEnd = new DateTime(2024, 01, 28, 12, 30, 00)
            };

            // Act
            var result = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            result.Should().Be(6);
        }

        [TestMethod]
        public void CalculateRent_TwoDaysRent_CorrectCostExpected()
        {
            // Arrange
            var rentedScooter = new RentedScooter("3", new DateTime(2024, 01, 28, 10, 00, 00), 0.2m)
            {
                RentEnd = new DateTime(2024, 01, 29, 13, 00, 00)
            };

            // Act
            var result = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            result.Should().Be(40);
        }

        [TestMethod]
        public void CalculateRent_FiveDaysRent_CorrectCostExpected()
        {
            // Arrange
            var rentedScooter = new RentedScooter("3", new DateTime(2024, 01, 20, 10, 00, 00), 0.2m)
            {
                RentEnd = new DateTime(2024, 01, 25, 01, 00, 00)
            };

            // Act
            var result = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            result.Should().Be(112);
        }

        [TestMethod]
        public void CalculateRent_InvalidRentEndDate_ErrorMessageExpected()
        {
            // Arrange
            var rentedScooter = new RentedScooter("3", new DateTime(2024, 01, 20, 10, 00, 00), 0.2m)
            {
                RentEnd = new DateTime(2024, 01, 19, 01, 00, 00)
            };

            // Act
            Action action = () => _calculatorService.CalculateRent(rentedScooter);

            //Assert
            action.Should().Throw<InvalidRentEndDateException>();
        }

        [TestMethod]
        public void CalculateIncome_NotIncludeActiveRentals_CorrectTotalIncomeExpected()
        {
            // Arrange
            var specificYear = 2024;

            // Act
            var result = _calculatorService.CalculateIncome(specificYear, includeNotCompletedRentals: false);

            // Assert
            result.Should().Be(20);
        }

        [TestMethod]
        public void CalculateIncome_IncludeActiveRentals_CorrectTotalIncomeExpected()
        {
            // Arrange
            var specificYear = 2024;

            // Act
            var result = _calculatorService.CalculateIncome(specificYear, includeNotCompletedRentals: false);

            // Assert
            result.Should().Be(100);
        }

        [TestMethod]
        public void CalculateIncome_NotIncludeYear_CorrectTotalIncomeExpected()
        {
            // Arrange
            int? specificYear = null;

            // Act
            var result = _calculatorService.CalculateIncome(specificYear, includeNotCompletedRentals: false);

            // Assert
            result.Should().Be(52);
        }
    }
}
