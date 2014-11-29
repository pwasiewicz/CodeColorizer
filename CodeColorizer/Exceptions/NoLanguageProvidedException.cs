namespace CodeColorizer.Exceptions
{
    using System;

    public class NoLanguageProvidedException : Exception
    {
        public NoLanguageProvidedException()
            : base("No language provided.")
        {
        }
    }
}
