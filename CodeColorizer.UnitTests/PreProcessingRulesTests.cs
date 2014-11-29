namespace CodeColorizer.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    using global::CodeColorizer.Language;
    using global::CodeColorizer.Language.PreProcessing;
    using global::CodeColorizer.TokenScopes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PreProcessingRulesTests
    {
        #region Getting Pre-Processed Rules

        [TestMethod]
        public void GetPreProcessedRules_ProvdingOneSimpleRule_PreProcessedRuleContainsProperSourcePiece()
        {
            // arrange
            var rule = new Rule { RegularExpression = "inlinetext", Scope = TokenScope.Keyword };
            const string Code = "sample inlinetext provided";
            var preProcessedRules = new PreProcessedRules(
                Code,
                new List<Rule> { rule });

            // act
            var result = preProcessedRules.GetPreProcessedRules().First();

            //asert
            Assert.AreEqual("inlinetext", result.SourcePiece);
        }

        [TestMethod]
        public void GetPreProcessedRules_ProvdingOneSimpleRule_PreProcessedRuleContainsProperStartIndex()
        {
            // arrange
            var rule = new Rule { RegularExpression = "inlinetext", Scope = TokenScope.Keyword };
            const string Code = "sample inlinetext provided";
            var preProcessedRules = new PreProcessedRules(
                Code,
                new List<Rule> { rule });

            // act
            var result = preProcessedRules.GetPreProcessedRules().First();

            //asert
            Assert.AreEqual(7, result.StartIndex);
        }

        [TestMethod]
        public void GetPreProcessedRules_ProvdingOneSimpleRule_ReturnsOnePreProcessedRule()
        {
            // arrange
            var rule = new Rule { RegularExpression = "inlinetext", Scope = TokenScope.Keyword };
            const string Code = "sample inlinetext provided";
            var preProcessedRules = new PreProcessedRules(
                Code,
                new List<Rule> { rule });

            // act
            var result = preProcessedRules.GetPreProcessedRules();

            //asert
            Assert.AreEqual(1, result.Count());
        } 

        #endregion
    }
}
