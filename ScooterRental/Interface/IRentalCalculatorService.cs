namespace ScooterRental.Interface
{
    public interface IRentalCalculatorService
    {
        decimal CalculateRent(RentedScooter rentalRecord);

        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}
