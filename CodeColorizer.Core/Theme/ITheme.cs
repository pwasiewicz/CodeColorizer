namespace CodeColorizer.Core.Theme
{
    using global::CodeColorizer.Core.TokenScopes;

    public interface ITheme
    {
        string BaseHexColor { get; }

        string BackgroundHexColor { get; }

        string LineNumberHexColor { get; }

        string LineNumberBackgroundHexColor { get; }

        Style GetStyle(TokenScope scope);
    }
}
