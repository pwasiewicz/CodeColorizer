namespace CodeColorizer.Exceptions
{
    using System;

    public class NoThemeProvidedException : Exception
    {
        public NoThemeProvidedException()
            : base("No theme provided.")
        {
        }
    }
}
