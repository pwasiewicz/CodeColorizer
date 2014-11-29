namespace CodeColorizer.Exceptions
{
    using System;

    public class NoStyleColorProvidedException : Exception
    {
        public NoStyleColorProvidedException()
            : base("No color provided within theme style.")
        {
        }
    }
}
