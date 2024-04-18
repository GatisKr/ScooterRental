namespace ScooterRental.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() : base("provided price is nor valid")
        {

        }
    }
}
