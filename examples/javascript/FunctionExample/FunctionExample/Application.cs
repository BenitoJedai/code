using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FunctionExample.HTML.Pages;

namespace FunctionExample
{
    public sealed class AnanymousDataEntry
    {
        public string Text;
        public string Comment;
    }

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            page.SetVariable.onclick +=
                delegate
                {
                    var f = new IFunction("value", "this.foo = value;");

                    f.apply(null, page.VariableText.value);

                    page.ShowVariable.style.fontWeight = "bold";
                };

            page.ShowVariable.onclick +=
               delegate
               {
                   page.ShowVariable.style.fontWeight = "";

                   var f = new IFunction("alert(this.foo);");

                   f.apply(null);
               };

            page.GetVariableWithComment.onclick +=
               delegate
               {
                   var f = new IFunction("return { Text: this.foo, Comment: 'from javascript' };");

                   var r = (AnanymousDataEntry)f.apply(null);

                   Native.window.alert(
                       new
                       {
                           r.Text,
                           r.Comment
                       }.ToString()
                   );
               };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
