using ScooterRental.Interface;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentalCalculatorService : IRentalCalculatorService
    {
        private readonly decimal _maxPricePerDay = 20m;
        private readonly List<RentedScooter> _rentedScooters;

        public RentalCalculatorService(List<RentedScooter> rentedScooters)
        {
            _rentedScooters = rentedScooters;
        }

        public decimal CalculateRent(RentedScooter rentalRecord)
        {
            var rentStart = rentalRecord.RentStart;
            var rentEnd = rentalRecord.RentEnd ?? DateTime.Now;
            var pricePerMinute = rentalRecord.PricePerMinute;
            decimal totalCost = 0;

            if (rentStart > rentEnd)
            {
                throw new InvalidRentEndDateException();
            }

            var currentDay = rentStart.Date;

            while (currentDay <= rentEnd.Date)
            {
                var dayStartTime = currentDay == rentStart.Date ? rentStart.TimeOfDay : TimeSpan.Zero;
                var dayEndTime = currentDay == rentEnd.Date ? rentEnd.TimeOfDay : TimeSpan.FromHours(24);

                var minutes = (int)(dayEndTime - dayStartTime).TotalMinutes;

                decimal dailyCost = Math.Min(minutes * pricePerMinute, _maxPricePerDay);

                totalCost += dailyCost;

                currentDay = currentDay.AddDays(1);
            }

            return totalCost;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal totalIncome = 0;

            foreach (var rentedScooter in _rentedScooters)
            {
                if (includeNotCompletedRentals || rentedScooter.RentEnd.HasValue)
                {
                    if (!year.HasValue || rentedScooter.RentStart.Year == year)
                    {
                        totalIncome += CalculateRent(rentedScooter);
                    }
                }
            }

            return totalIncome;
        }
    }
}
