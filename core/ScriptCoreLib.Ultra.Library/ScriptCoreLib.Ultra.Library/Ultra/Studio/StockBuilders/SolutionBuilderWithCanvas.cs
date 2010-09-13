using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio
{
    public static class SolutionBuilderWithCanvas
    {
        public static SolutionBuilder WithCanvas(this SolutionBuilder sln)
        {
            // should we make an Undo available?
            sln.ApplicationPage = StockPageDefault.CanvasDefaultPage;

            Func<StockCanvasType> GetType = () => new StockCanvasType(sln.Name, "ApplicationCanvas");

            var content = default(SolutionProjectLanguageField);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {

                    var ApplicationCanvas = GetType();



                    // in Canvas applications we want to focus only the canvas
                    // to do that we hide other implementation detail classes

                    sln.Interactive.ApplicationWebServiceType.DependentUpon = ApplicationCanvas;
                    sln.Interactive.ApplicationType.DependentUpon = ApplicationCanvas;
                    sln.Interactive.ProgramType.DependentUpon = ApplicationCanvas;

                    AddType(ApplicationCanvas);

                    content = new SolutionProjectLanguageField
                    {
                        FieldType = ApplicationCanvas,
                        FieldConstructor = ApplicationCanvas.GetDefaultConstructor(),
                        Name = "content",
                        IsReadOnly = true
                    };

                    // we are adding a field. does it show up in the source code later?
                    // SolutionProjectLanguage.WriteType makes it happen!
                    sln.Interactive.ApplicationType.Fields.Add(content);

                    var Code = sln.Interactive.ProgramType_MainMethod.Code;


                    sln.Interactive.ProgramType_MainMethod.Code = new SolutionProjectLanguageCode
                    {
                        new PseudoIfExpression
                        {
                            IsConditionalCompilationDirective = true,
                            Expression = ("DEBUG"),
                            FalseCase = Code,
                            TrueCase = new SolutionProjectLanguageCode
                            {
                                new KnownStockTypes.ScriptCoreLib.Desktop.Extensions.DesktopAvalonExtensions.Launch().ToCallExpression(
                                    null,
                                    new SolutionProjectLanguageMethod
                                    {
                                        ReturnType = ApplicationCanvas,
                                        Code = new SolutionProjectLanguageCode 
                                        {
                                            ApplicationCanvas.GetDefaultConstructor()
                                        }
                                    }
                                )
                            }
                        }
                    };
                };

            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    // new ApplicationCanvas().AttachToContainer(page.PageContainer);

                    var page_get_Content =
                        new PseudoCallExpression
                        {
                            // Application(page)
                            Object = "page",

                            Method =
                                new SolutionProjectLanguageMethod
                                {
                                    IsProperty = true,
                                    Name = "get_Content",
                                    ReturnType = new KnownStockTypes.ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement()
                                }
                        };

                    var page_get_ContentSize =
                       new PseudoCallExpression
                       {
                           // Application(page)
                           Object = "page",

                           Method =
                               new SolutionProjectLanguageMethod
                               {
                                   IsProperty = true,
                                   Name = "get_ContentSize",
                                   ReturnType = new KnownStockTypes.ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement()
                               }
                       };

                    AddCode(
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AvalonUltraExtensions.AutoSizeTo().ToCallExpression(
                            null,
                            new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.AttachToContainer().ToCallExpression(
                                null,
                                content,
                                page_get_Content
                            ),
                            page_get_ContentSize
                        )

                    );
                };

            return sln;
        }
    }
}
