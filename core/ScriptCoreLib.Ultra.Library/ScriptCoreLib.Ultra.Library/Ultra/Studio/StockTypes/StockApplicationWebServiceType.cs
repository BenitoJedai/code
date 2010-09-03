using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.StockMethods;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
	public class StockApplicationWebServiceType : SolutionProjectLanguageType
	{
		public StockMethodWebMethod WebMethod2 { get; private set; }

		public StockApplicationWebServiceType(SolutionBuilderInteractive Interactive)
		{
			this.IsSealed = true;
			this.Name = "ApplicationWebService";
			this.Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.";

			this.UsingNamespaces.Add("System");
			this.UsingNamespaces.Add("System.Linq");
			this.UsingNamespaces.Add("System.Xml.Linq");
			this.UsingNamespaces.Add("ScriptCoreLib");
            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            this.UsingNamespaces.Add("ScriptCoreLib.Delegates");

			WebMethod2 = new StockMethodWebMethod(Interactive);

			this.Methods.Add(WebMethod2);
		}
	}
}
