using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
	public class StockMethodWebMethod : SolutionProjectLanguageMethod
	{
		public StockMethodWebMethod(SolutionBuilderInteractive Interactive)
		{
			// note: this method will run under javascript

            #region Parameters e y
            var _e = new SolutionProjectLanguageArgument
            {
                Type = new KnownStockTypes.System.String(),

                Name = "e",
                Summary = "A parameter from javascript. JSC supports string data type for all platforms."
            };

            var _y = new SolutionProjectLanguageArgument
            {
                Type = new KnownStockTypes.ScriptCoreLib.Delegates.StringAction(),

                Name = "y",
                Summary = "A callback to javascript. In the future all platforms will allow Action<XElementConvertable> delegates."
            };
            #endregion


			this.Name = "WebMethod2";
			this.Summary = "This Method is a javascript callable method.";

			this.Code = new SolutionProjectLanguageCode
			{
                //Interactive.WebMethod2,

				"Send it back to the caller.",
				new PseudoCallExpression
				{
					Object = _y.Name,

					Method =  new SolutionProjectLanguageMethod { Name = "Invoke" },

					ParameterExpressions = new [] {
						_e.Name
					}
				}
			};


			this.Parameters.Add(_e);
			this.Parameters.Add(_y);
		}

	}
}
