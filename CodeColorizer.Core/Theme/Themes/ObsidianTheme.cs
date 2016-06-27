namespace CodeColorizer.Core.Theme.Themes
{
    using global::CodeColorizer.Core.TokenScopes;

    public class ObsidianTheme : ITheme
    {
        public string BaseHexColor
        {
            get
            {
                return "#F1F2F3";
            }
        }

        public string BackgroundHexColor
        {
            get
            {
                return "#111";
            }
        }

        public string LineNumberHexColor
        {
            get
            {
                return this.BaseHexColor;
            }
        }

        public string LineNumberBackgroundHexColor
        {
            get
            {
                return "#333333";
            }
        }

        public Style GetStyle(TokenScope scope)
        {
            switch (scope)
            {
                case TokenScope.Comment:
                    return CommentStyle;
                case TokenScope.Keyword:
                    return KeywordStyle;
                case TokenScope.Preprocessor:
                    return PreprocessorStyle;
                case TokenScope.String:
                    return StringStyle;
                case TokenScope.Numeric:
                    return NumericStyle;
                default:
                    return KeywordStyle;
            }
        }

        private static Style KeywordStyle
        {
            get
            {
                return new Style
                           {
                               Bold = false,
                               HexColor = "#93C763",
                               Italic = false
                           };
            }
        }

        private static Style StringStyle
        {
            get
            {
                return new Style
                           {
                               Bold = false,
                               HexColor = "#EC7600",
                               Italic = false
                           };
            }
        }

        private static Style NumericStyle
        {
            get
            {
                return new Style
                           {
                               Bold = false,
                               HexColor = "#EC7600",
                               Italic = false
                           };
            }
        }

        private static Style PreprocessorStyle
        {
            get
            {
                return new Style
                           {
                               Bold = false,
                               HexColor = "#003399",
                               Italic = true
                           };
            }
        }

        private static Style CommentStyle
        {
            get
            {
                return new Style
                           {
                               Bold = false,
                               HexColor = "#888888",
                               Italic = true
                           };
            }
        }
    }
}
