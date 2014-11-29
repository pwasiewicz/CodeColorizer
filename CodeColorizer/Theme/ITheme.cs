namespace CodeColorizer.Theme
{
    using global::CodeColorizer.TokenScopes;

    public interface ITheme
    {
        string BaseHexColor { get; }

        string BackgroundHexColor { get; }

        string LineNumberHexColor { get; }

        string LineNumberBackgroundHexColor { get; }

        Style GetStyle(TokenScope scope);
    }
}
