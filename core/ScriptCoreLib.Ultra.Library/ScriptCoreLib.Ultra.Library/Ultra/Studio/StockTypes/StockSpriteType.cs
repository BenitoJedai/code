using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockSpriteType : SolutionProjectLanguageType
	{
        public StockSpriteType(string Namespace, string Name, SolutionProjectLanguageField Content = null)
		{
			this.Namespace = Namespace;
			this.Name = Name;

			this.BaseType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.display.Sprite();

			this.IsSealed = true;
            this.IsInternal = true;

            var ctor = GetDefaultConstructorDefinition();

            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            
       
            ctor.Code = new SolutionProjectLanguageCode
            {
               
            };

            if (Content != null)
            {
                this.UsingNamespaces.Add("ScriptCoreLib.ActionScript.Extensions");

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

                ctor.Code.Add(
                     new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.CommonExtensions.InvokeWhenStageIsReady().ToCallExpression(
                        new PseudoThisExpression(),
                        handler
                    )
                );
            }

			this.Methods.Add(ctor);
		}

	
	}
}
