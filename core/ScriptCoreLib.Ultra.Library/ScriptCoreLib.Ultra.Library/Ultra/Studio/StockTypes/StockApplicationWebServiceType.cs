﻿using System;
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

            // Cassini will not be able to support debugging if WebService is made internal?
            //this.IsInternal = true;

            this.Name = "ApplicationWebService";
            this.Summary = "Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.";

            this.UsingNamespaces.Add("System");
            this.UsingNamespaces.Add("System.Linq");
            this.UsingNamespaces.Add("System.Xml.Linq");
            this.UsingNamespaces.Add("System.Collections.Generic");
            this.UsingNamespaces.Add("System.Threading.Tasks");
            this.UsingNamespaces.Add("System.Text");
            this.UsingNamespaces.Add("System.Data");

            this.UsingNamespaces.Add("ScriptCoreLib");
            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            this.UsingNamespaces.Add("ScriptCoreLib.Delegates");

            WebMethod2 = new StockMethodWebMethod(Interactive);

            this.Methods.Add(WebMethod2);
        }

    }
}
