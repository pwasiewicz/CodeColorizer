namespace CodeColorizer.Language.PreProcessing
{
    using System.Collections.Generic;

    internal interface IPreProcessedRules : IEnumerable<PreProcessedRule>
    {
        IEnumerable<PreProcessedRule> GetPreProcessedRules();
    }
}
