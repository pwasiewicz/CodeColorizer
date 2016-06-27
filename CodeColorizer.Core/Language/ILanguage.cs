namespace CodeColorizer.Core.Language
{
    using System.Collections.Generic;

    public interface ILanguage
    {
        IEnumerable<Rule> GetRules();
    }
}
