namespace CodeColorizer.Language
{
    using System.Text.RegularExpressions;

    using global::CodeColorizer.TokenScopes;

    public class Rule
    {
        public string RegularExpression { get; set; }

        public TokenScope Scope { get; set; } 

        public RegexOptions RegexOptions { get; set; }
    }
}
