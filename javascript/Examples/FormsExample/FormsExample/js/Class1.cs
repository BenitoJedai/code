﻿//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Windows.Forms;



namespace FormsExample.js
{

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {


            if (DataElement != null)
            {
                new IHTMLSpan("hello world").attachToDocument();
            }

            var u = new UserControl1();

            //System.Diagnostics.Debugger.Break();



            foreach (Control v in u.Controls)
            {

                var t = v.GetType();

                var a = t.TypeHandle.Value ;
                var b = typeof(Button).TypeHandle.Value;

                IHTMLElement item = null;

                if (a == b)
                {
                    var btn = (Button)v;

                    item = new IHTMLButton(btn.Text);

                    //h.onclick += btn.Click;


                    System.Console.WriteLine("button: " + v.Name);
                }
                else
                {
                    item = new IHTMLDiv();
                    item.style.border = "1px solid gray";
                    item.innerText = v.Text;

                    System.Console.WriteLine("control: " + v.Name);
                }

                item.attachToDocument();

                item.style.SetLocation(
                    v.Location.X, v.Location.Y, v.Size.Width, v.Size.Height
                    );

                
            }
        }


        


        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
