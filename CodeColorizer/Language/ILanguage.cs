namespace CodeColorizer.Language
{
    using System.Collections.Generic;

    public interface ILanguage
    {
        IEnumerable<Rule> GetRules();
    }
}
