namespace ScooterRental.Interface
{
    public interface IRentedScooterArchive
    {
        void AddRentedScooter(RentedScooter scooter);

        RentedScooter EndRental(string scooterId, DateTime rentEnd);

        List<RentedScooter> GetRentedScooters();
    }
}
