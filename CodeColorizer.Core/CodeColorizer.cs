namespace CodeColorizer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::CodeColorizer.Core.Exceptions;
    using global::CodeColorizer.Core.Language;
    using global::CodeColorizer.Core.Language.PreProcessing;
    using global::CodeColorizer.Core.Parsers;
    using global::CodeColorizer.Core.Theme;
    using global::CodeColorizer.Core.TokenScopes;

    internal class CodeColorizer : ICodeColorizer
    {
        private readonly string sourceCode;

        private ILanguage language;

        private ITheme theme;

        internal CodeColorizer(string sourceCode)
        {
            if (sourceCode == null)
            {
                throw new ArgumentNullException(nameof(sourceCode));
            }

            this.sourceCode = sourceCode;
        }

        private ILanguage Language
        {
            get
            {
                if (this.language == null)
                {
                    throw new NoLanguageProvidedException();
                }

                return this.language;
            }

            set
            {
                this.language = value;
            }
        }

        private ITheme Theme
        {
            get
            {
                if (this.theme == null)
                {
                    throw new NoThemeProvidedException();
                }

                VerifyTheme(this.theme);
                return this.theme;
            }

            set
            {
                this.theme = value;
            }
        }

        private bool EncodeHtml { get; set; }

        private bool LineNumbers { get; set; }

        public ICodeColorizer WithLanguage(ILanguage language)
        {
            this.Language = language;
            return this;
        }

        public ICodeColorizer WithTheme(ITheme theme)
        {
            this.Theme = theme;
            return this;
        }


        public ICodeColorizer WithHtmlEncoding()
        {
            this.EncodeHtml = true;

            return this;
        }

        public ICodeColorizer AddLineNumbers()
        {
            this.LineNumbers = true;

            return this;
        }

        public string ToHtml()
        {
            var htmlParser = new HtmlParser(
                this.PreProcessLanguage(),
                this.Theme,
                this.EncodeHtml,
                this.LineNumbers);

            return htmlParser.Parse();
        }

        private IPreProcessedLanguage PreProcessLanguage()
        {
            return new PreProcessedLanguage(this.sourceCode, this.Language);
        }

        private static void VerifyTheme(ITheme theme)
        {
            var allTokens = GetValues<TokenScope>();
            foreach (var style in allTokens.Select(theme.GetStyle))
            {
                if (style == null)
                {
                    throw new NoThemeStyleProvidedException();
                }

                if (style.HexColor == null)
                {
                    throw new NoStyleColorProvidedException();
                }
            }

            if (theme.BaseHexColor == null
                || theme.BackgroundHexColor == null)
            {
                throw new NoStyleColorProvidedException();
            }
        }

        internal static IEnumerable<T> GetValues<T>()
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
                throw new InvalidOperationException("Type must be enumeration type.");

            return GetValuesImplicit<T>();
        }

        private static IEnumerable<T> GetValuesImplicit<T>()
        {
            return from field in typeof(T).GetTypeInfo().GetFields()
                   where field.IsLiteral && !string.IsNullOrEmpty(field.Name)
                   select (T)field.GetValue(null);
        }
    }
}
