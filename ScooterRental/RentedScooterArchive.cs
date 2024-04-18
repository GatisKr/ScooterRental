using ScooterRental.Interface;

namespace ScooterRental
{
    public class RentedScooterArchive : IRentedScooterArchive
    {
        private readonly List<RentedScooter> _rentedScooters;

        public RentedScooterArchive(List<RentedScooter> rentedScooters)
        {
            _rentedScooters = rentedScooters;
        }

        public void AddRentedScooter(RentedScooter scooter)
        {
            _rentedScooters.Add(scooter);
        }

        public RentedScooter EndRental(string scooterId, DateTime rentEnd)
        {
            var rentedScooter = _rentedScooters.SingleOrDefault(scooter => scooter.ScooterId == scooterId);
            rentedScooter.RentEnd = rentEnd;

            return rentedScooter;
        }

        public List<RentedScooter> GetRentedScooters()
        {
            return _rentedScooters;
        }
    }
}
 