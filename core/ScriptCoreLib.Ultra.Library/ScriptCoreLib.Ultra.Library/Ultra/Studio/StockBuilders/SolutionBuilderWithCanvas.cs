using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionBuilderWithCanvas
	{

        //class CreateMyApplet : PseudoCallExpression
        //{
        //    public CreateMyApplet(StockAppletType Type)
        //    {
        //        var page_Page1 =
        //                    new PseudoCallExpression
        //                    {
        //                        // Application(page)
        //                        Object = "page",

        //                        Method =
        //                            new SolutionProjectLanguageMethod
        //                            {
        //                                IsProperty = true,
        //                                Name = "get_Content",
        //                                ReturnType = new SolutionProjectLanguageType
        //                                {
        //                                    Name = "IHTMLElement"
        //                                }
        //                            }
        //                    };

        //        var new_Page1 =
        //            new PseudoCallExpression
        //            {
        //                // Application(page)

        //                Method =
        //                    new SolutionProjectLanguageMethod
        //                    {
        //                        Name = SolutionProjectLanguageMethod.ConstructorName,
        //                        DeclaringType = new SolutionProjectLanguageType
        //                        {
        //                            Namespace = Type.Namespace,
        //                            Name = Type.Name
        //                        }
        //                    }
        //            };

        //        this.Comment = "Initialize " + Type.Name;

        //        this.Method =
        //            new SolutionProjectLanguageMethod
        //            {
        //                IsStatic = true,
        //                IsExtensionMethod = true,
        //                Name = "AttachAppletTo",
        //                ReturnType = new SolutionProjectLanguageType
        //                {
        //                    Name = "AppletExtensions"
        //                }
        //            };

        //        this.ParameterExpressions = new[]
        //        {
        //            new_Page1,
        //            page_Page1
        //        };
        //    }
        //}

        public static SolutionBuilder WithCanvas(this SolutionBuilder that)
        {
            // should we make an Undo available?

            Func<StockCanvasType> GetType = () => new StockCanvasType(that.Name, "ApplicationCanvas");

            that.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationCanvas = GetType();

                    // in Canvas applications we want to focus only the canvas
                    // to do that we hide other implementation detail classes

                    that.Interactive.ApplicationWebServiceType.DependentUpon = ApplicationCanvas;
                    that.Interactive.ApplicationType.DependentUpon = ApplicationCanvas;
                    that.Interactive.ProgramType.DependentUpon = ApplicationCanvas;

                    AddType(ApplicationCanvas);
                };

            that.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    // new ApplicationCanvas().AttachToContainer(page.PageContainer);
                    // AddCode(new CreateMyApplet(GetType()));
                };

            return that;
        }
	}
}
