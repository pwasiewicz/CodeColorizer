namespace CodeColorizer.Core
{
    public static class Colorizer
    {
        public static ICodeColorizer Colorize(string sourceCode)
        {
            return new CodeColorizer(sourceCode);
        }
    }
}
