namespace CodeColorizer.Core.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using global::CodeColorizer.Core.Language.PreProcessing;
    using global::CodeColorizer.Core.Theme;

    internal class HtmlParser
    {
        private const string PreElementClass = "code-colorizer";

        private const string LineNumberClass = "code-colorizer-line";

        private const string LineNumberIndicatorClass =
            "code-colorizer-line-indicator";

        private readonly IPreProcessedLanguage preProcessedLanguage;

        private readonly ITheme theme;

        private readonly bool encodeHtml;

        private readonly bool lineNumbers;

        internal HtmlParser(IPreProcessedLanguage preProcessedLanguage, ITheme theme, bool encodeHtml, bool lineNumbers)
        {
            this.preProcessedLanguage = preProcessedLanguage;
            this.theme = theme;
            this.encodeHtml = encodeHtml;
            this.lineNumbers = lineNumbers;
        }

        public string Parse()
        {
            var builder =
                new StringBuilder(
                   this.PreprePreOpenTag(
                        this.theme.BaseHexColor,
                        this.theme.BackgroundHexColor));

            builder.Append(this.ParseSourceCode());

            builder.Append("</pre>");
            return builder.ToString();
        }

        private string ParseSourceCode()
        {
            var source = this.preProcessedLanguage.SourceCode;
            var processedText = new StringBuilder();
            var rules =
                this.preProcessedLanguage.GetPreProcessedRules().ToList();

            if (!rules.Any())
            {
                var output = this.encodeHtml ? WebUtility.HtmlEncode(source) : source;
                return this.WrapLinesWithSpan(output);
            }

            var textBeginning = GetTextBegin(source, rules);

            if (this.encodeHtml)
            {
                textBeginning = WebUtility.HtmlEncode(textBeginning);
            }

            processedText.Append(textBeginning);

            for (var i = 0; i < rules.Count(); i++)
            {
                var rule = rules[i];
                var style = this.theme.GetStyle(rule.Scope);

                this.FormatSourcePiece(processedText, rule.SourcePiece, style);

                string restOfSource;
                if (i + 1 < rules.Count())
                {
                    var nextRule = rules[i + 1];
                    var restStartIndex = rule.StartIndex
                                         + rule.SourcePiece.Length;

                    restOfSource =
                        source.Substring(
                           restStartIndex,
                            nextRule.StartIndex - restStartIndex);
                }
                else
                {
                    restOfSource =
                        source.Substring(
                            rule.StartIndex + rule.SourcePiece.Length);
                }

                if (this.encodeHtml)
                {
                    restOfSource = WebUtility.HtmlEncode(restOfSource);
                }

                processedText.Append(restOfSource);
            }

            var result = processedText.ToString();

            result = this.WrapLinesWithSpan(result);

            return result;
        }

        private string WrapLinesWithSpan(string result)
        {
            var splitString = this.encodeHtml ?  "&#10;" : "\n" ;

            var lines = result.Split(new []  {splitString}, StringSplitOptions.None);

            for (var i = 0; i < lines.Length; i++)
            {
                lines[i] = this.WrapLinesWithSpan(lines[i], i + 1, lines.Length);
            }

            return string.Join(splitString, lines);
        }

        private string WrapLinesWithSpan(string line, int lineNumber, int allLinesCount)
        {
            if (this.lineNumbers)
            {
                line = this.AddLineNumber(line, lineNumber, allLinesCount);
            }

            return string.Format(
                "<span class=\"{0}\">{1}</span>",
                LineNumberClass,
                line);
        }

        private string AddLineNumber(string line, int lineNumber, int allLinesCount)
        {
            var allDigits = IntLength(allLinesCount);
            var currrentDigits = IntLength(lineNumber);

            var missingDigis = allDigits - currrentDigits;

            var result =
                string.Format(
                    "<span style=\"color: {0}; background-color: {1};\" class=\"{5}\">{2}{3}.</span>{4}",
                    this.theme.LineNumberHexColor,
                    this.theme.LineNumberBackgroundHexColor,
                    new string('\u0020', missingDigis),
                    lineNumber,
                    line,
                    LineNumberIndicatorClass);

            return result;
        }

        private void FormatSourcePiece(StringBuilder builder, string source, Style style)
        {
            if (this.encodeHtml)
            {
                source = WebUtility.HtmlEncode(source);
            }

            if (style.Bold)
            {
                source = string.Format("<b>{0}</b>", source);
            }

            if (style.Italic)
            {
                source = string.Format("<i>{0}</i>", source);
            }

            builder.AppendFormat(
                "<span style=\"color: {0};\">{1}</span>",
                style.HexColor,
                source);
        }

        private static string GetTextBegin(
            string sourceCode,
            IEnumerable<PreProcessedRule> rules)
        {
            return sourceCode.Substring(0, rules.ElementAt(0).StartIndex);
        }

        private string PreprePreOpenTag(
            string hexColor,
            string hexBackgroundColor)
        {
            return
                string.Format(
                    "<pre style=\"color: {0}; background-color: {1};\" class=\"{2}\">\n",
                    hexColor,
                    hexBackgroundColor,
                    PreElementClass);
        }

        public static int IntLength(int i)
        {
            if (i <= 0) throw new ArgumentOutOfRangeException();

            return (int)Math.Floor(Math.Log10(i)) + 1;
        }
    }
}
