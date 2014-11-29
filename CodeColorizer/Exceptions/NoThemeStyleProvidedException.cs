namespace CodeColorizer.Exceptions
{
    using System;

    public class NoThemeStyleProvidedException : Exception
    {
        public NoThemeStyleProvidedException()
            : base("No theme style provided (style is null)")
        {
        }
    }
}
