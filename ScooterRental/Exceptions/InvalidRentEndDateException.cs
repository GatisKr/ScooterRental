namespace ScooterRental.Exceptions
{
    public class InvalidRentEndDateException : Exception
    {
        public InvalidRentEndDateException() : base("RentEndDate should be set to a value later than RentStartDate")
        {

        }
    }
}