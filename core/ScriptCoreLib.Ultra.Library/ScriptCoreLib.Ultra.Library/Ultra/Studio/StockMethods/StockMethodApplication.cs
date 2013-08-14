using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.StockMethods
{
    public class StockMethodApplication : SolutionProjectLanguageMethod
    {
        public StockMethodApplication(SolutionProjectLanguageType DeclaringType, SolutionBuilderInteractive Interactive /*, SolutionProjectLanguageField style*/)
        {
            // note: this method will run under javascript

            #region Parameters args
            var _page = new SolutionProjectLanguageArgument
            {
                Type = new SolutionProjectLanguageType
                {
                    Name = "IApp"
                },



                
                //Name = "document",
                Name = "page",
                Summary = "HTML document rendered by the web server which can now be enhanced."
            };

            #endregion

            this.Name = SolutionProjectLanguageMethod.ConstructorName;
            this.Summary = "This is a javascript application.";



            this.Code = new SolutionProjectLanguageCode();

            Interactive.RaiseGenerateApplicationExpressions(this.Code.Add);

            //#region style.Content.AttachToHead
            //var style_get_Content =
            //         new PseudoCallExpression
            //         {
            //             Object = style,

            //             Method =
            //                 new SolutionProjectLanguageMethod
            //                 {
            //                     IsProperty = true,
            //                     Name = "get_Content",
            //                     ReturnType = new KnownStockTypes.ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement()
            //                 }
            //         };

            //this.Code.Add(
            //    new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.INodeExtensions.AttachToHead().ToCallExpression(
            //        style_get_Content
            //    )
            //);
            //#endregion


            this.Code.Add(Interactive.ApplicationToDocumentTitle);

            // visual basic demands a local variable!
            this.Code.Add(Interactive.ApplicationCallWebMethod);

            this.DeclaringType = DeclaringType;
            this.Parameters.Add(_page);
        }

    }
}
