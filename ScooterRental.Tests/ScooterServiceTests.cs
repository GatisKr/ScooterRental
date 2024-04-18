using FluentAssertions;
using ScooterRental.Exceptions;
using ScooterRental.Interface;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;
        private List<Scooter> _scooters;
        private const string _defaultScooterId = "1";

        [TestInitialize]
        public void Setup()
        {
            _scooters = new List<Scooter>();
            _scooterService = new ScooterService(_scooters);
        }

        [TestMethod]
        public void AddScooter_ValidDataProvided_ScooterAdded()
        {
            //Act
            _scooterService.AddScooter(_defaultScooterId, 0.1m);

            //Assert
            _scooters.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddScooter_InvalidPriceProvided_InvalidPriceExceptionExpected()
        {
            //Act
            Action action = () => _scooterService.AddScooter(_defaultScooterId, 0.0m);

            //Assert
            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void AddScooter_InvalidIdProvided_InvalidIdExceptionExpected()
        {
            //Act
            Action action = () => _scooterService.AddScooter("", 0.1m);

            //Assert
            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void AddScooter_AddDuplicateScooter_DuplicateScooterExceptionExpected()
        {
            //Arrange
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));

            //Act
            Action action = () => _scooterService.AddScooter(_defaultScooterId, 0.1m);

            //Assert
            action.Should().Throw<DuplicateScooterException>();
        }

        [TestMethod]
        public void RemoveScooter_ExistingScooterIdProvided_ScooterRemoved()
        {
            //Arrange
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));

            //Act
            _scooterService.RemoveScooter(_defaultScooterId);

            //Assert
            _scooters.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveScooter_ExistingScooterIdProvided_ScooterNotFoundExceptionExpected()
        {
            //Act
            Action action = () => _scooterService.RemoveScooter(_defaultScooterId);

            //Assert
            action.Should().Throw<ScooterNotFoundException>();
        }

        [TestMethod]
        public void GetScooterById_ExistingScooterIdProvided_ScooterReturnExpected()
        {
            //Arrange
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));
            _scooters.Add(new Scooter("2", 0.5m));

            //Act
            var scooterChosen = _scooterService.GetScooterById(_defaultScooterId);

            //Assert
            scooterChosen.Id.Should().Be(_defaultScooterId);
        }

        [TestMethod]
        public void GetScooterById_NonExistingScooterIdProvided_ScooterNotFoundExceptionExpected()
        {
            //Act
            Action action = () => _scooterService.GetScooterById(_defaultScooterId);

            //Assert
            action.Should().Throw<ScooterNotFoundException>();
        }

        [TestMethod]
        public void GetScooters_ScooterListRequested_ScooterListReturnExpected()
        {
            //Arrange
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));
            _scooters.Add(new Scooter("2", 0.5m));

            //Act
            var scootersList = _scooterService.GetScooters();

            //Assert
            scootersList.Should().Contain(s => s.Id == _defaultScooterId);
            scootersList.Should().Contain(s => s.Id == "2");
        }

        [TestMethod]
        public void GetScooters_EmptyScooterListRequested_ThrowEmptyListRequestExpected()
        {
            //Act
            Action action = () => _scooterService.GetScooters();

            //Assert
            action.Should().Throw<EmptyScooterListException>();
        }
    }
}
