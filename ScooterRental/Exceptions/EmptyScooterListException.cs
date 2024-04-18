namespace ScooterRental.Exceptions
{
    public class EmptyScooterListException : Exception
    {
        public EmptyScooterListException() : base("Scooter list is empty.")
        {

        }
    }
}
