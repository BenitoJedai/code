using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestExtensionsAdd;
using TestExtensionsAdd.Design;
using TestExtensionsAdd.HTML.Pages;

namespace TestExtensionsAdd
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // the code generated from a collection initializer will now happily 
            // call an extension method called Add.
            // It’s not much of a feature, but it’s occasionally useful, and it turned out implementing it in the new compiler amounted to removing a check that prevented it.

            // https://smellegantcode.wordpress.com/2014/04/24/adventures-in-roslyn-adding-crazily-powerful-operator-overloading-to-c-6/
            // http://blogs.msdn.com/b/kirillosenkov/archive/2013/01/07/collection-initializers-and-add-method-with-more-than-one-parameter.aspx
            // http://kirillosenkov.blogspot.com/2007/09/c-30-collection-initializers-duck.html
            // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/2743568-support-c-add-extension-methods-for-collection-
            // http://damieng.com/blog/2013/12/09/probable-c-6-0-features-illustrated

            new Foo { }.Add("not yet?").AttachToDocument();

            //new Foo {
            //    { "hello" },
            //    { "world" }
            //}.AttachToDocument();
        }

    }

    static class X
    {
        public static IFoo Add(this IFoo f, string pre)
        {

            new IHTMLPre { pre }.AttachTo(
                f.output
            );

            return f;
        }
    }
}
