namespace CodeColorizer.Core
{
    using global::CodeColorizer.Core.Language;
    using global::CodeColorizer.Core.Theme;

    public interface ICodeColorizer
    {
        ICodeColorizer WithLanguage(ILanguage language);

        ICodeColorizer WithTheme(ITheme theme);

        ICodeColorizer WithHtmlEncoding();

        ICodeColorizer AddLineNumbers();

        string ToHtml();
    }
}
