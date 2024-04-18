using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ScooterRental;
using ScooterRental.Interface;

namespace ScooterRentalMoq.Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private AutoMocker _mocker;
        private RentalCompany _company;
        private Mock<IScooterService> _scooterServiceMock;
        private Mock<IRentedScooterArchive> _rentedScooterArchiveMock;
        private Mock<IRentalCalculatorService> _rentalCalculatorMock;
        private const string _defaultCompanyName = "tests";

        [TestInitialize]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _scooterServiceMock = _mocker.GetMock<IScooterService>();
            _rentedScooterArchiveMock = _mocker.GetMock<IRentedScooterArchive>();
            _rentalCalculatorMock = _mocker.GetMock<IRentalCalculatorService>();

            _company = new RentalCompany(
                _defaultCompanyName,
                _scooterServiceMock.Object,
                _rentedScooterArchiveMock.Object,
                _rentalCalculatorMock.Object);
        }

        [TestMethod]
        public void Name_NewRentalCompanyInstanceCreated_DefaultCompanyNameExpected()
        {
            _company.Name.Should().Be("tests");
        }

        [TestMethod]
        public void StartRent_Rent_ExistingScooter_ScooterIsRented()
        {
            //Arrange
            var scooter = new Scooter("1", 0.2m);
            _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);
            
            //Act
            _company.StartRent("1");

            //Assert
            scooter.IsRented.Should().BeTrue();
        }

        [TestMethod]
        public void EndRent_StopRenting_ExistingScooter_ScooterRentStopped()
        {
            //Arrange
            var scooter = new Scooter("1", 0.2m){IsRented = true};
            var now = DateTime.Now;

            var rentalRecord = new RentedScooter(scooter.Id, now.AddMinutes(-20), scooter.PricePerMinute){RentEnd = now};

            _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);

            _rentedScooterArchiveMock.Setup(archive => archive.EndRental(scooter.Id, It.IsAny<DateTime>()))
                .Returns(rentalRecord);

            _rentalCalculatorMock.Setup(calculator => calculator.CalculateRent(rentalRecord)).Returns(5);

            //Act
            var result = _company.EndRent("1");

            //Assert
            scooter.IsRented.Should().BeFalse();
        }

        [TestMethod]
        public void CalculateIncome_CorrectReturnValueExpected()
        {
            // Arrange
            int? specificYear = null;
            _rentalCalculatorMock.Setup(calculator => calculator.CalculateIncome(specificYear, false)).Returns(30);

            // Act
            var result = _company.CalculateIncome(specificYear, includeNotCompletedRentals: false);

            // Assert
            result.Should().Be(30);
        }
    }
}
