using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockSpriteType : SolutionProjectLanguageType
	{
        public SolutionProjectLanguageMethod Constructor;

        public StockSpriteType(string Namespace, string Name, SolutionProjectLanguageField Content = null)
		{
			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.display.Sprite();

			this.IsSealed = true;
            this.IsInternal = true;

            this.Constructor = GetDefaultConstructorDefinition();
            this.Constructor.Code = new SolutionProjectLanguageCode
            {

            };

            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            this.UsingNamespaces.Add("ScriptCoreLib.ActionScript.Extensions");


            

            if (Content != null)
            {

                var AttachToContainer =
                      new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.AvalonExtensions.AttachToContainer().ToCallExpression(
                          Content,
                          new PseudoThisExpression()
                      );

                var get_stage = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.display.DisplayObject.get_stage().ToCallExpression(
                    new PseudoThisExpression()
                );

                var AutoSizeTo =
                     new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.ActionScriptAvalonExtensions.AutoSizeTo().ToCallExpression(
                        Content,
                        get_stage
                    );

                var handler = new SolutionProjectLanguageMethod
                {
                    Code = new SolutionProjectLanguageCode
                    {
                        AttachToContainer,
                        AutoSizeTo
                    }
                };

                this.Constructor.Code.Add(
                     new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.CommonExtensions.InvokeWhenStageIsReady().ToCallExpression(
                        new PseudoThisExpression(),
                        handler
                    )
                );
            }

            this.Methods.Add(this.Constructor);
		}

	
	}
}
