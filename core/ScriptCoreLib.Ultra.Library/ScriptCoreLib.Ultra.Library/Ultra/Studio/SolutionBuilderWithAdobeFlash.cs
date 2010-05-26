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
	public static class SolutionBuilderWithAdobeFlash
	{
        class CreateMySprite : PseudoCallExpression
        {
            public CreateMySprite(StockSpriteType Type)
            {
                var page_Page1 =
                    new PseudoCallExpression
                    {
                        // Application(page)
                        Object = "page",

                        Method =
                            new SolutionProjectLanguageMethod
                            {
                                IsProperty = true,
                                Name = "get_Content",
                                ReturnType = new SolutionProjectLanguageType
                                {
                                    Name = "IHTMLElement"
                                }
                            }
                    };

                var new_Page1 =
                    new PseudoCallExpression
                    {
                        // Application(page)

                        Method =
                            new SolutionProjectLanguageMethod
                            {
                                Name = SolutionProjectLanguageMethod.ConstructorName,
                                DeclaringType = new SolutionProjectLanguageType
                                {
                                    Namespace = Type.Namespace,
                                    Name = Type.Name
                                }
                            }
                    };

                this.Comment = "Initialize " + Type.Name;

                this.Method =
                    new SolutionProjectLanguageMethod
                    {
                        IsStatic = true,
                        IsExtensionMethod = true,
                        Name = "AttachSpriteTo",
                        ReturnType = new SolutionProjectLanguageType
                        {
                            Name = "SpriteExtensions"
                        }
                    };

                this.ParameterExpressions = new[]
			    {
				    new_Page1,
				    page_Page1
			    };
            }
        }

        public static SolutionBuilder WithAdobeFlash(this SolutionBuilder that)
        {
            Func<StockSpriteType> GetType = () => new StockSpriteType(that.Name + ".Components", "MySprite1");

            that.Interactive.GenerateTypes +=
                AddType =>
                {
                    AddType(GetType());
                };

            that.Interactive.GenerateApplicationExpressions +=
                AddCode =>
                {
                    AddCode(new CreateMySprite(GetType()));
                };

            return that;
        }
	}
}
