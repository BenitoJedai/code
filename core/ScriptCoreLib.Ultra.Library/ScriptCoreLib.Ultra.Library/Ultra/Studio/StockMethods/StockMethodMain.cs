using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

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
					ElementType = new SolutionProjectLanguageType
					{
						Name = "string"
					}
				},

				Name = "args",
				Summary = "Commandline arguments"
			};

			#endregion

			this.Name = "Main";
			this.Summary = "In debug build you can just hit F5 and debug the server side code.";
			this.IsStatic = true;
			this.Code = new SolutionProjectLanguageCode
			{
				"Prepare the yield value for",
				new PseudoCallExpression
				{

					Method = new SolutionProjectLanguageMethod {
						DeclaringType = new SolutionProjectLanguageType { 
							Namespace = "jsc.meta",
							Name = "AsProgram"
						},
						IsStatic = true,
						Name = "Launch" 
					},

					Parameters = new [] {
						ApplicationType
					}
				},

			
			};

			this.Parameters.Add(_args);
		}

	}
}
