namespace CodeColorizer.Language.Languages
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using global::CodeColorizer.Extensions;
    using global::CodeColorizer.TokenScopes;

    public class Php : ILanguage
    {
        public IEnumerable<Rule> GetRules()
        {
            return new[]
            {
                this.Keywords(
                    "break|clone|die()|empty()|endswitch|final|global|include_once|list()|private|return|try|xor|abstract|callable|const|do|enddeclare|endwhile|finally|goto|instanceof|namespace|protected|static|unset()|yield|and|case|continue|echo|endfor|eval()|for|if|insteadof|new|public|switch|use|array()|catch|declare|else|endforeach|exit()|foreach|implements|or|require|throw|var|as|class|default|elseif|endif|extends|function|include|isset()|print|required_once|trait|while"
                        .Split('|')),
                this.StringInQuotationMarks(),
                this.NumericWithDot(),
                new Rule
                {
                    RegularExpression =
                        @"\$\w+",
                    Scope = TokenScope.Keyword,
                    RegexOptions = RegexOptions.None
                },
            };
        }
    }
}
