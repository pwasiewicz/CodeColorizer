namespace CodeColorizer.Core.Language.PreProcessing
{
    using System.Collections.Generic;

    internal class PreProcessedLanguage : IPreProcessedLanguage
    {
        private readonly ILanguage language;

        private readonly IPreProcessedRules preProcessedRules;

        internal PreProcessedLanguage(string sourceCode, ILanguage language)
        {
            this.language = language;
            this.SourceCode = sourceCode;
            this.preProcessedRules = new PreProcessedRules(sourceCode, this.GetRules());
        }

        public IPreProcessedRules GetPreProcessedRules()
        {
            return this.preProcessedRules;
        }

        public IEnumerable<Rule> GetRules()
        {
            return this.language.GetRules();
        }

        public string SourceCode
        {
            get;
            set;
        }
    }
}
