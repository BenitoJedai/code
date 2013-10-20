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
    public static class SolutionBuilderWithForms
    {
        public static SolutionBuilder WithForms(this SolutionBuilder sln)
        {
            return Internal(sln,
                IsApplet: false,
                 ApplcationWebServiceAsComponent: true
            );
        }

        public static SolutionBuilder WithFormsApplet(this SolutionBuilder sln)
        {
            var content = default(SolutionProjectLanguageField);
            var sprite = default(SolutionProjectLanguageField);

            Internal(sln,
                IsApplet: true,
                ApplcationWebServiceAsComponent: false,
                NotifyContent: value => content = value);

            sln.Interactive.GenerateTypes +=
                AddType =>
                {
                    var ApplicationSprite = new StockAppletType(sln.Name, "ApplicationApplet", content);


                    ApplicationSprite.DependentUpon = content.FieldType;


                    content.DeclaringType = ApplicationSprite;

                    sprite = ApplicationSprite.ToInitializedField("applet");

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
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AppletExtensions.AutoSizeAppletTo().ToCallExpression(
                            sprite,
                            page_get_ContentSize
                        )
                    );

                    AddCode(
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.AppletExtensions.AttachAppletTo().ToCallExpression(
                            sprite,
                            page_get_Content
                        )
                    );
                };

            return sln;
        }

        static SolutionBuilder Internal(SolutionBuilder sln,

            bool ApplcationWebServiceAsComponent = false,

            bool IsApplet = false,
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
                    var ApplicationCanvas = new StockUserControlType(sln.Name, "ApplicationControl");


                    // in Canvas applications we want to focus only the canvas
                    // to do that we hide other implementation detail classes

                    //sln.Interactive.ApplicationWebServiceType.DependentUpon = ApplicationCanvas;

                    if (ApplcationWebServiceAsComponent)
                    {
                        sln.Interactive.ApplicationWebServiceType.With(
                            ApplicationWebServiceType =>
                            {
                                var ApplicationWebServiceDesignerType =
                                     new SolutionProjectLanguagePartialType
                                     {
                                         Name = ApplicationWebServiceType.Name + ".Designer",
                                     };


                                ApplicationWebServiceDesignerType.Type.Name = ApplicationWebServiceType.Name;

                                ApplicationWebServiceType.NamespaceChanged +=
                                    delegate
                                    {
                                        ApplicationWebServiceDesignerType.Type.Namespace = ApplicationWebServiceType.Namespace;
                                    };

                                #region components
                                var components =
                                    new SolutionProjectLanguageField
                                    {
                                        IsPrivate = true,
                                        Name = "components",
                                        Summary = "Required designer variable.",
                                        FieldType = new KnownStockTypes.System.ComponentModel.IContainer()
                                    };

                                ApplicationWebServiceDesignerType.Type.Fields.Add(components);
                                #endregion
                                #region InitializeComponent
                                var InitializeComponent =
                                    new SolutionProjectLanguageMethod
                                    {
                                        Summary = @"Required method for Designer support - do not modify
the contents of this method with the code editor.",

                                        IsPrivate = true,
                                        DeclaringType = ApplicationWebServiceDesignerType.Type,
                                        Name = "InitializeComponent",
                                        Code = new SolutionProjectLanguageCode
                                        {
                                            //set_Name,
                                            //set_Size
                                        }
                                    };

                                ApplicationWebServiceDesignerType.Type.Methods.Add(InitializeComponent);

                                #endregion


                                #region ApplicationWebServiceDesignerTypeConstructor
                                var ApplicationWebServiceDesignerTypeConstructor =
                                    new SolutionProjectLanguageMethod
                                    {
                                        DeclaringType = ApplicationWebServiceDesignerType.Type,
                                        Name = SolutionProjectLanguageMethod.ConstructorName,
                                        Code = new SolutionProjectLanguageCode
					                    {
						                    new PseudoCallExpression
						                    {
							                    Object = new PseudoThisExpression(),
							                    Method = InitializeComponent
						                    }
					                    }
                                    };
                                ApplicationWebServiceDesignerType.Type.Methods.Add(ApplicationWebServiceDesignerTypeConstructor);
                                #endregion


                                ApplicationWebServiceType.BaseType = new KnownStockTypes.System.ComponentModel.Component();

                                ApplicationWebServiceType.DependentPartialTypes = new[]
				                {
					                ApplicationWebServiceDesignerType
				                };

                                ApplicationWebServiceDesignerType.Type.UsingNamespaces.Add("System.ComponentModel");

                            }
                        );

                    }

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
                                new KnownStockTypes.ScriptCoreLib.Desktop.Forms.Extensions.DesktopFormsExtensions.Launch().ToCallExpression(
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

                    if (!IsApplet)
                    {
                        AddCode(
                            new KnownStockTypes.ScriptCoreLib.JavaScript.FormExtensions.AttachControlToDocument().ToCallExpression(
                                content
                            )
                        );

                        return;
                    }

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
                         new KnownStockTypes.ScriptCoreLib.JavaScript.Windows.Forms.WindowsFormsExtensions.AttachControlTo().ToCallExpression(
                             content,
                             page_get_Content
                         )
                     );

                    AddCode(
                        new KnownStockTypes.ScriptCoreLib.JavaScript.Windows.Forms.WindowsFormsExtensions.AutoSizeControlTo().ToCallExpression(
                            content,
                            page_get_ContentSize
                        )
                    );
                };

            return sln;
        }
    }
}
