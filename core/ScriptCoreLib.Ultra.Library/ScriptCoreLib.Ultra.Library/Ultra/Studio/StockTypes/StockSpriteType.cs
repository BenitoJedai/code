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

            var ctor = GetDefaultConstructorDefinition();

            var handler = new SolutionProjectLanguageMethod
            {
                Code = new SolutionProjectLanguageCode
                {

                }
            };

            ctor.Code = new SolutionProjectLanguageCode
            {
                new KnownStockTypes.ScriptCoreLib.ActionScript.Extensions.CommonExtensions.InvokeWhenStageIsReady().ToCallExpression(
                    new PseudoThisExpression(),
                    handler
                )
            };

			this.Methods.Add(ctor);
		}

	
	}
}
