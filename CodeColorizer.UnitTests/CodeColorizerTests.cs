namespace CodeColorizer.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::CodeColorizer.Exceptions;
    using global::CodeColorizer.Language;
    using global::CodeColorizer.Language.Languages;
    using global::CodeColorizer.Theme;
    using global::CodeColorizer.Theme.Themes;
    using global::CodeColorizer.TokenScopes;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSTestExtensions;

    using NSubstitute;

    [TestClass]
    public class CodeColorizerTests
    {
        #region WithTheme

        [TestMethod]
        public void WithTheme_ProvidingTheme_ReturnsSameCodeColorizerReference()
        {
            // arrange
            var sourceCode = string.Empty;
            var theme = Substitute.For<ITheme>();

            // act
            var codeColorizer = Colorizer.Colorize(sourceCode);
            var withThemeColorizer = codeColorizer.WithTheme(theme);

            // assert
            Assert.AreSame(codeColorizer, withThemeColorizer);
        }

        [TestMethod]
        public void WithLanguage_ProvidingLanguage_ReturnsSameCodeColorizerReference()
        {
            // arrange
            var sourceCode = string.Empty;
            var language = Substitute.For<ILanguage>();

            // act
            var codeColorizer = Colorizer.Colorize(sourceCode);
            var withLanguageColorizer = codeColorizer.WithLanguage(language);

            // assert
            Assert.AreSame(codeColorizer, withLanguageColorizer);
        } 

        #endregion

        #region ToHtml

        [TestMethod]
        public void ToHtml_NoLanguageProvided_WillThrowException()
        {
            // arrange
            var theme = Substitute.For<ITheme>();
            var colorizer = Colorizer.Colorize(string.Empty).WithTheme(theme);

            // act => assert
            ExceptionAssert.Throws<NoLanguageProvidedException>(
                () => colorizer.ToHtml());
        }

        [TestMethod]
        public void ToHtml_NoThemeProvided_ThrowsException()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var colorizer =
                Colorizer.Colorize(string.Empty).WithLanguage(language);

            // act => assert
            ExceptionAssert.Throws<NoThemeProvidedException>(
                () => colorizer.ToHtml());
        }

        [TestMethod]
        public void ToHtml_ColoringTextWithLangaugeThatHasNoRules_ReturnValueWillContainSourceText()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var theme = Substitute.For<ITheme>();

            language.GetRules().Returns(Enumerable.Empty<Rule>());
            
            theme.GetStyle(Arg.Any<TokenScope>())
                 .Returns(new Style { HexColor = string.Empty });
            theme.BackgroundHexColor.Returns(string.Empty);
            theme.BaseHexColor.Returns(string.Empty);

            const string SourceText = "sample text that could be anything";

            // act
            var coloredHtml =
                Colorizer.Colorize(SourceText)
                         .WithLanguage(language)
                         .WithTheme(theme)
                         .ToHtml();

            // assert
            Assert.IsTrue(coloredHtml.Contains(SourceText));
        }

        [TestMethod]
        public void ToHtml_ColoringTextWithLangaugeThatHasOneRules_ReturnValueWillContainSourceText()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var theme = Substitute.For<ITheme>();

            language.GetRules().Returns(
                new List<Rule>
                    {
                        new Rule
                            {
                                RegularExpression =
                                    "samplerule",
                                Scope =
                                    TokenScope.Keyword
                            }
                    });


            theme.GetStyle(Arg.Any<TokenScope>())
                 .Returns(new Style { HexColor = string.Empty });
            theme.BackgroundHexColor.Returns(string.Empty);
            theme.BaseHexColor.Returns(string.Empty);

            const string SourceText = "sample text that could be anything.";

            // act
            var coloredHtml =
                Colorizer.Colorize(SourceText)
                         .WithLanguage(language)
                         .WithTheme(theme)
                         .ToHtml();

            // assert
            Assert.IsTrue(coloredHtml.Contains(SourceText));
        }

        [TestMethod]
        public void ToHtml_ColorizeSampleCsharpCodeWithEmptyColorsTheme_WillReturnResult()
        {
            // arrange
            const string SampleCode = "public void Main()\n{\n\tvar a = new MyObject();\n}";
            var language = new Csharp();
            var theme = Substitute.For<ITheme>();

            theme.GetStyle(Arg.Any<TokenScope>())
                 .Returns(new Style { HexColor = string.Empty });
            theme.BackgroundHexColor.Returns(string.Empty);
            theme.BaseHexColor.Returns(string.Empty);

            // act
            var html =
                Colorizer.Colorize(SampleCode)
                         .WithLanguage(language)
                         .WithTheme(theme).ToHtml();

            // assert
            Assert.AreEqual(
                "<pre style=\"color: ; background-color: ;\" class=\"code-colorizer\">\n<span class=\"code-colorizer-line\"><span style=\"color: ;\">public</span> <span style=\"color: ;\">void</span> Main()</span>\n<span class=\"code-colorizer-line\">{</span>\n<span class=\"code-colorizer-line\">	<span style=\"color: ;\">var</span> a = <span style=\"color: ;\">new</span> MyObject();</span>\n<span class=\"code-colorizer-line\">}</span></pre>",
                html);
        }

        [TestMethod]
        public void ToHtmlColorizeSampleHtmlWithEncoding_WillReturnProperResult()
        {
            // arrange
            const string sampleCode =
                "<pre></pre>";
            var language = new Csharp();
            var theme = new ObsidianTheme();

            //act
            var html =
                Colorizer.Colorize(sampleCode)
                         .WithTheme(theme)
                         .WithLanguage(language)
                         .WithHtmlEncoding()
                         .ToHtml();

            // assert
            Assert.AreEqual("<pre style=\"color: #F1F2F3; background-color: #111;\" class=\"code-colorizer\">\n<span class=\"code-colorizer-line\">&lt;pre&gt;&lt;/pre&gt;</span></pre>", html);
        }

        [TestMethod]
        public void ToHtml_ProperLineNumbers_ReturnsProperData()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var theme = Substitute.For<ITheme>();
            const string SourceCode = "Line 1\nLine 2";

            language.GetRules().Returns(Enumerable.Empty<Rule>());

            theme.GetStyle(Arg.Any<TokenScope>())
                             .Returns(new Style { HexColor = string.Empty });
            theme.BackgroundHexColor.Returns(string.Empty);
            theme.BaseHexColor.Returns(string.Empty);

            // act
            var result = Colorizer.Colorize(SourceCode)
                                  .WithLanguage(language)
                                  .WithTheme(theme).AddLineNumbers().ToHtml();

            // assert
            Assert.AreEqual(
                "<pre style=\"color: ; background-color: ;\" class=\"code-colorizer\">\n<span class=\"code-colorizer-line\"><span style=\"color: ; background-color: ;\" class=\"code-colorizer-line-indicator\">1.</span>Line 1</span>\n<span class=\"code-colorizer-line\"><span style=\"color: ; background-color: ;\" class=\"code-colorizer-line-indicator\">2.</span>Line 2</span></pre>",
                result);
        }

        [TestMethod]
        public void ToHtml_ProvidingSimpleCSharpSourceCode_CallsGetStyleFromTheme()
        {
            // arrange
            var language = new Csharp();
            var theme = Substitute.For<ITheme>();
            const string SourceCode = "public void Main()\n{\n\tvar a = new MyObject();\n}";

            theme.GetStyle(Arg.Any<TokenScope>())
                             .Returns(new Style { HexColor = string.Empty });
            theme.BackgroundHexColor.Returns(string.Empty);
            theme.BaseHexColor.Returns(string.Empty);

            // act
            Colorizer.Colorize(SourceCode)
                     .WithLanguage(language)
                     .WithTheme(theme).ToHtml();

            // assert
            theme.Received().GetStyle(Arg.Any<TokenScope>());
        }

        [TestMethod]
        [ExpectedException(typeof(NoThemeStyleProvidedException))]
        public void ToHtml_ProvidingNullStylesInTheme_WillThrowException()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var theme = Substitute.For<ITheme>();

            theme.GetStyle(TokenScope.Keyword).Returns((Style)null);

            // act
            Colorizer.Colorize(string.Empty)
                     .WithLanguage(language)
                     .WithTheme(theme)
                     .ToHtml();
        }

        [TestMethod]
        public void ToHtml_ProvidingNullColorsInStylesInTheme_WillThrowException()
        {
            // arrange
            var language = Substitute.For<ILanguage>();
            var theme = Substitute.For<ITheme>();

            theme.GetStyle(Arg.Any<TokenScope>()).Returns(new Style());

            // act => assert
            ExceptionAssert.Throws<NoStyleColorProvidedException>(
                () => Colorizer.Colorize(string.Empty)
                               .WithLanguage(language)
                               .WithTheme(theme)
                               .ToHtml());
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void Constructor_ProvidingNullSourceCode_WillThrowArgumentNull()
        {
            // arrange => act => assert
            // ReSharper disable once ObjectCreationAsStatement
            ExceptionAssert.Throws<ArgumentNullException>(
                () => new CodeColorizer(null));
        } 

        #endregion
    }
}
