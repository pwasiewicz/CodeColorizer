namespace CodeColorizer
{
    using global::CodeColorizer.Language;
    using global::CodeColorizer.Theme;

    public interface ICodeColorizer
    {
        ICodeColorizer WithLanguage(ILanguage language);

        ICodeColorizer WithTheme(ITheme theme);

        ICodeColorizer WithHtmlEncoding();

        ICodeColorizer AddLineNumbers();

        string ToHtml();
    }
}
