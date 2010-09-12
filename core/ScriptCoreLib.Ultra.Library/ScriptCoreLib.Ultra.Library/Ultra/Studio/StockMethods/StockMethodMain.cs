using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
	public class StockMethodMain : SolutionProjectLanguageMethod
	{
		public StockMethodMain(SolutionProjectLanguageType ApplicationType)
		{
			// note: this method will run under javascript

			#region Parameters args
			var _args = new SolutionProjectLanguageArgument
			{
				Type = new SolutionProjectLanguageType
				{
                    ElementType = new KnownStockTypes.System.String()
				},

				Name = "args",
				Summary = "Commandline arguments"
			};

			#endregion

			this.Name = "Main";
            //this.Summary = "In debug build you can just hit F5 and debug the server side code.";
			this.IsStatic = true;

            var AsProgram_Launch =
                new PseudoCallExpression
                {

                    Method = new SolutionProjectLanguageMethod
                    {
                        DeclaringType = new SolutionProjectLanguageType
                        {
                            DeclaringType = new SolutionProjectLanguageType
                            {
                                Namespace = "jsc.meta.Commands.Rewrite.RewriteToUltraApplication",
                                Name = "RewriteToUltraApplication"
                            },
                            Name = "AsProgram"
                        },
                        IsStatic = true,
                        Name = "Launch"
                    },

                    ParameterExpressions = new[] {
						ApplicationType
					}
                };

			this.Code = new SolutionProjectLanguageCode
			{
				AsProgram_Launch
			};

			this.Parameters.Add(_args);
		}

	}
}
