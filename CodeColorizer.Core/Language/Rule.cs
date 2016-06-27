namespace CodeColorizer.Core.Language
{
    using System.Text.RegularExpressions;
    using global::CodeColorizer.Core.TokenScopes;

    public class Rule
    {
        public string RegularExpression { get; set; }

        public TokenScope Scope { get; set; } 

        public RegexOptions RegexOptions { get; set; }
    }
}
