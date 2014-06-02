using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
    public class StockApplicationWebServiceType : SolutionProjectLanguageType
    {
        public StockMethodWebMethod WebMethod2 { get; private set; }

        public StockApplicationWebServiceType(SolutionBuilderInteractive Interactive)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140602

            // we can now use it as a base type for our client side apps
            //this.IsSealed = true;

            // Cassini will not be able to support debugging if WebService is made internal?
            //this.IsInternal = true;

            // will this help us?
            //this.IsPartial = true;

            this.Name = "ApplicationWebService";
            this.Summary = "Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.";

            this.UsingNamespaces.Add("System");
            this.UsingNamespaces.Add("System.Linq");
            this.UsingNamespaces.Add("System.Xml.Linq");
            this.UsingNamespaces.Add("System.Collections.Generic");
            this.UsingNamespaces.Add("System.Threading.Tasks");
            this.UsingNamespaces.Add("System.Text");
            this.UsingNamespaces.Add("System.Data");
            this.UsingNamespaces.Add("System.Diagnostics");
            this.UsingNamespaces.Add("System.Runtime.CompilerServices");

            this.UsingNamespaces.Add("ScriptCoreLib");
            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            this.UsingNamespaces.Add("ScriptCoreLib.Delegates");


            //  <h1 id='Header'>JSC - The .NET crosscompiler for web platforms.</h1>
            var HeaderField = new SolutionProjectLanguageField { Name = "Header", FieldType = new StockTypes.KnownStockTypes.System.Xml.Linq.XElement() };

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140602
            HeaderField.Summary = "The static content defined in the HTML file will be update to the dynamic content once application is running.";
            HeaderField.FieldConstructor =
                new PseudoCallExpression
                   {

                       Method = new StockTypes.KnownStockTypes.System.Xml.Linq.XElement().GetDefaultConstructorDefinition(),
                       ParameterExpressions = new object[] {
                        new PseudoStringConstantExpression { Value = "h1"},
                        new PseudoStringConstantExpression { Value = "JSC - The .NET crosscompiler for web platforms. ready."}
                       }
                   };

            this.Fields.Add(HeaderField);

            this.WebMethod2 = new StockMethodWebMethod(Interactive);

            this.Methods.Add(WebMethod2);
        }

    }
}
