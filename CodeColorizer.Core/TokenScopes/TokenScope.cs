namespace CodeColorizer.Core.TokenScopes
{
    public enum TokenScope
    {
        String = 0x1,
        Comment = 0x2,
        Preprocessor = 0x4,
        Numeric = 0x8,
        Keyword = 0x16
    }
}
