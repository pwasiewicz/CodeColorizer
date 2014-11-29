CodeColorizer
=============

Sample regex-based library for coloring syntax.
Features
* Converts source code to highlighted html code
* Simple API to implement custom languages built-in languages (built-in is C# so far)

Sample usage:
```c#
var codeColorizer = Colorizer.Colorize(sourceCode)
                             .WithTheme(new ObsidianTheme())
                             .WithLanguage(new Csharp());
var html = codeColorizer.ToHtml();
```
