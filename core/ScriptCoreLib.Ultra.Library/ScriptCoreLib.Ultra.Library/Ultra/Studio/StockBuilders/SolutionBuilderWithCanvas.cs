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
            return InternalWithCanvas(sln);
        }

        public static SolutionBuilder WithCanvasAdobeFlash(this SolutionBuilder sln)
        {
            var content = default(SolutionProjectLanguageField);
            var sprite = default(SolutionProjectLanguageField);

            InternalWithCanvas(sln, value => content = value);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationSprite = new StockSpriteType(sln.Name, "ApplicationSprite", content);


                    ApplicationSprite.DependentUpon = content.FieldType;


                    content.DeclaringType = ApplicationSprite;

                    sprite = ApplicationSprite.ToInitializedField("sprite");

                    sprite.DeclaringType = sln.Interactive.ApplicationType;

                    AddType(ApplicationSprite);
                };


            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
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
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AutoSizeSpriteTo().ToCallExpression(
                            sprite,
                            page_get_ContentSize
                        )
                    );

                    AddCode(
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AttachSpriteTo().ToCallExpression(
                            sprite,
                            page_get_Content
                        )
                    );
                };

            return sln;
        }


        static SolutionBuilder InternalWithCanvas(SolutionBuilder sln,
            Action<SolutionProjectLanguageField> NotifyContent = null
            )
        {
            // should we make an Undo available?
            sln.ApplicationPage = StockPageDefault.CanvasDefaultPage;


            var content = default(SolutionProjectLanguageField);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {

                    #region ApplicationCanvas
                    var ApplicationCanvas = new StockCanvasType(sln.Name, "ApplicationCanvas");


                    // in Canvas applications we want to focus only the canvas
                    // to do that we hide other implementation detail classes

                    sln.Interactive.ApplicationWebServiceType.DependentUpon = ApplicationCanvas;
                    sln.Interactive.ApplicationType.DependentUpon = ApplicationCanvas;
                    sln.Interactive.ProgramType.DependentUpon = ApplicationCanvas;

                    AddType(ApplicationCanvas);

                    content = ApplicationCanvas.ToInitializedField("content");
                    content.DeclaringType = sln.Interactive.ApplicationType;

                    // we are adding a field. does it show up in the source code later?
                    // SolutionProjectLanguage.WriteType makes it happen!

                    if (NotifyContent != null)
                        NotifyContent(content);

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
                    #endregion

                };

            sln.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    // our content has been removed...
                    if (content.DeclaringType != sln.Interactive.ApplicationType)
                        return;

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
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.AttachToContainer().ToCallExpression(
                            content,
                            page_get_Content
                      )
                  );

                    AddCode(
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AvalonUltraExtensions.AutoSizeTo().ToCallExpression(
                            content,
                            page_get_ContentSize
                        )
                    );
                };

            return sln;
        }
    }
}
