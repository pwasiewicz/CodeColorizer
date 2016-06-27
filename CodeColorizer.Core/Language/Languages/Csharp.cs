namespace CodeColorizer.Core.Language.Languages
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using global::CodeColorizer.Core.Extensions;
    using global::CodeColorizer.Core.TokenScopes;

    public class Csharp : ILanguage
    {
        public IEnumerable<Rule> GetRules()
        {
                return new[]
                    {
                        this.Keywords("abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|struct|sbyte|sealed|sizeof|stackalloc|short|static|string|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|volatile|void|while".Split('|')),
                        this.StringInQuotationMarks(),
                        this.NumericWithDot(),
                        new Rule
                            {
                                RegularExpression =
                                    @"'\\[a-zA-Z|\\]'",
                                Scope = TokenScope.String,
                                RegexOptions = RegexOptions.None
                            },
                        new Rule
                            {
                                RegularExpression =
                                    @"'\\[x|u|U][0-7ABCDEFabcdef]{1,8}'",
                                Scope = TokenScope.String,
                                RegexOptions = RegexOptions.None
                            },
                        new Rule
                            {
                                RegularExpression = @"//.*|/\*[\s\S]*\*/",
                                Scope = TokenScope.Comment,
                                RegexOptions = RegexOptions.None
                            },
                        new Rule
                            {
                                RegularExpression = @"^#.*$",
                                Scope = TokenScope.Keyword,
                                RegexOptions = RegexOptions.None
                            }
                    };
        }
    }
}
