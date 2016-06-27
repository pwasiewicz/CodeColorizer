namespace CodeColorizer.Core.Language.PreProcessing
{
    using global::CodeColorizer.Core.TokenScopes;

    internal class PreProcessedRule
    {
        public string SourcePiece { get; set; }

        public int StartIndex { get; set; }

        public TokenScope Scope { get; set; }
    }
}
