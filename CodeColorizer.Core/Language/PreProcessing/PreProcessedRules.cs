namespace CodeColorizer.Core.Language.PreProcessing
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class PreProcessedRules : IPreProcessedRules
    {
        private IEnumerable<PreProcessedRule> preProcessedRules;

        internal PreProcessedRules(string sourceCode, IEnumerable<Rule> rules)
        {
            this.preProcessedRules = Enumerable.Empty<PreProcessedRule>();
            this.PreProcessRules(sourceCode, rules);
        }

        public IEnumerable<PreProcessedRule> GetPreProcessedRules()
        {
            return this.preProcessedRules;
        }

        public IEnumerator<PreProcessedRule> GetEnumerator()
        {
            return this.preProcessedRules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void PreProcessRules(string sourceCode, IEnumerable<Rule> rules)
        {
            var matches =
                rules.SelectMany(
                    rule =>
                        Regex.Matches(
                            sourceCode,
                            rule.RegularExpression,
                            RegexOptions.Multiline | rule.RegexOptions)
                             .Cast<Match>()
                             .Select(
                                 match =>
                                     new
                                         {
                                             match.Index,
                                             match.Value,
                                             rule.Scope
                                         }))
                     .OrderBy(match => match.Index)
                     .ThenBy(match => match.Scope)
                     .ToList();

            for (var i = 0; i < matches.Count; i++)
            {
                if (i + 1 >= matches.Count)
                {
                    continue;
                }

                var currentMatchIndexEnd = matches[i].Index
                                           + matches[i].Value.Length;
                if (currentMatchIndexEnd <= matches[i + 1].Index)
                {
                    continue;
                }

                matches.RemoveAt(i + 1);
                i--;
            }

            this.preProcessedRules =
                matches.Select(
                    match =>
                        new PreProcessedRule
                            {
                                Scope = match.Scope,
                                SourcePiece = match.Value,
                                StartIndex = match.Index
                            })
                       .ToList();
        }
    }
}
