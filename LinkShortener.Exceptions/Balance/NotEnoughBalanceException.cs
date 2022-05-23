namespace LinkShortener.Exceptions.Balance
{
    public class NotEnoughBalanceException : Exception
    {
        public NotEnoughBalanceException(string message) : base(message) { }
    }
}
