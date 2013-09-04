using AppletAsyncWhenReady;
using AppletAsyncWhenReady.Design;
using AppletAsyncWhenReady.HTML.Pages;
using java.applet;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Java.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AppletAsyncWhenReady
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationApplet applet = new ApplicationApplet();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            applet.WhenReady(
           delegate
           {

               Console.WriteLine("WhenReady");
           }
       );


            applet.With(
               async delegate
               {
                   Console.WriteLine("before await");

                   var e = new Stopwatch();

                   e.Start();

                   ILocalTasks loc = await applet;

                   Console.WriteLine("after await " + e.ElapsedMilliseconds);

                   //RewriteToAssembly error: System.ArgumentNullException: Value cannot be null.
                   //Parameter name: namedProperties[0]
                   //   at System.Reflection.Emit.CustomAttributeBuilder.InitCustomAttributeBuilder(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues, FieldInfo[] namedFields, Object[] fieldValues)
                   //   at System.Reflection.Emit.CustomAttributeBuilder..ctor(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues)
                   //   at jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins.IDLCompiler.<>c__DisplayClass52.<>c__DisplayClass60.<>c__DisplayClass69.<>c__DisplayClass6b.<Define>b__48(IDLParserToken )


                   loc.WithButton1(
                       async content_button1_click =>
                       {
                           // should only work once?
                           var x = await content_button1_click;


                           Console.WriteLine("at content_button1_click " + new { x });

                       }
                   );




               }
           );

            applet.AutoSizeAppletTo(page.ContentSize);
            applet.AttachAppletTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
