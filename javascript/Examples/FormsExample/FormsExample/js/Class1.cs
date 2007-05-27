//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Windows.Forms;
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



            SpawnUserControl(new UserControl1(), new Point(400, 200));
            SpawnUserControl(new UserControl2(), new Point(420, 450));
            SpawnUserControl(new UserControl3(), new Point(820, 250));
        }



        private static void SpawnUserControl(UserControl u, Point pos)
        {
            var bg = u.GetHTMLTarget();

            if (bg == null)
                throw new System.Exception();


            //var s = u.Size;


            //bg.style.SetSize(s.Width, s.Height);

            bg.attachToDocument();
            bg.SetCenteredLocation(pos.X, pos.Y);

            var ctrls = u.Controls;

           
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
