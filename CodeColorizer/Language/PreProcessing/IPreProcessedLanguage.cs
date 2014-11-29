namespace CodeColorizer.Language.PreProcessing
{
    internal interface IPreProcessedLanguage : ILanguage
    {
        string SourceCode { get; }

        IPreProcessedRules GetPreProcessedRules();
    }
}
