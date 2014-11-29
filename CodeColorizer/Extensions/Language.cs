namespace CodeColorizer.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using global::CodeColorizer.Language;
    using global::CodeColorizer.TokenScopes;

    public static class Language
    {
        public static Rule Keywords(
            this ILanguage language,
            IEnumerable<string> keywords,
            RegexOptions regexOptions = RegexOptions.None)
        {
            return new Rule
                       {
                           RegularExpression =
                               string.Format(
                                   @"\b(?:{0})\b",
                                   string.Join(
                                       "|",
                                       keywords.ToArray())),
                           Scope = TokenScope.Keyword,
                           RegexOptions = regexOptions
                       };
        }

        public static Rule NumericWithDot(
            this ILanguage language,
            RegexOptions regexOptions = RegexOptions.None)
        {
            return new Rule
                       {
                           RegularExpression = @"\b\d+(\.d+)?\b",
                           Scope = TokenScope.Numeric,
                           RegexOptions = regexOptions
                       };
        }

        public static Rule StringInQuotationMarks(
            this ILanguage language,
            RegexOptions regexOptions = RegexOptions.None)
        {
            return new Rule
            {
                RegularExpression = "\"(?:[^\"\\\\]|\\\\.)*\"",
                Scope = TokenScope.String,
                RegexOptions = regexOptions
            };
        }
    }
}
