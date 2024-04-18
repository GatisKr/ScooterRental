using ScooterRental.Exceptions;
using ScooterRental.Interface;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;

        public ScooterService(List<Scooter> scooters)
        {
            _scooters = scooters;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException();
            }

            if (_scooters.Any(scooter => scooter.Id == id))
            {
                throw new DuplicateScooterException();
            }

            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(sc => sc.Id.Equals(scooterId));

            return scooter ?? throw new ScooterNotFoundException();
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.Count > 0 ? _scooters : throw new EmptyScooterListException();
        }

        public void RemoveScooter(string id)
        {
            var scooter = _scooters.SingleOrDefault(scooter => scooter.Id == id);

            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }

            _scooters.Remove(scooter);
        }
    }
}
