namespace CodeColorizer.Core.Language.PreProcessing
{
    internal interface IPreProcessedLanguage : ILanguage
    {
        string SourceCode { get; }

        IPreProcessedRules GetPreProcessedRules();
    }
}
