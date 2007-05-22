//using System.Linq;

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


            SpawnUserControl(u, new Point(400, 200));
            SpawnUserControl(u, new Point(420, 450));
        }

        static bool TypeEquals(object o, System.Type t)
        {
            // this seems to fault
            var a = o.GetType();

            var x = a.TypeHandle;
            var y = t.TypeHandle;

            return x.Value == y.Value;
        }

        private static void SpawnUserControl(UserControl1 u, Point pos)
        {


            var bg = new IHTMLDiv();

            bg.style.backgroundColor = "threedface";
            bg.style.SetSize(u.Size.Width, u.Size.Height);

            bg.attachToDocument();
            bg.SetCenteredLocation(pos.X, pos.Y);

            var ctrls = u.Controls;

            System.Console.WriteLine("start: " + System.DateTime.Now.Ticks);

            SpawnControls(bg, ctrls);

            System.Console.WriteLine("end: " + System.DateTime.Now.Ticks);
        }

        private static void SpawnControls(IHTMLElement bg, Control.ControlCollection ctrls)
        {
            foreach (Control v in ctrls)
            {
                IHTMLElement item = null;

                var IsTypeOf = Lambda.FixFirstParam<object, global::System.Type, bool>(TypeEquals, v);

                if (IsTypeOf(typeof(Button)))
                {
                    var btn = (Button)v;

                    item = new IHTMLButton(btn.Text);

                    //h.onclick += btn.Click;


                    System.Console.WriteLine("button: " + v.Name);
                }
                else if (IsTypeOf(typeof(ComboBox)))
                {
                    var cmb = (ComboBox)v;

                    item = new IHTMLSelect();


                }
                else if (IsTypeOf(typeof(TextBox)))
                {
                    var txt = (TextBox)v;

                    if (txt.Multiline)
                    {
                        item = new IHTMLTextArea(txt.Text);
                    }
                    else
                    {
                        item = new IHTMLInput(HTMLInputTypeEnum.text, txt.Text);
                    }

                    //h.onclick += btn.Click;

                    System.Console.WriteLine("textbox: " + v.Name);
                }
                else
                {
                    item = new IHTMLDiv();
                    item.style.border = "1px dotted gray";

                    if (v.Text != null)
                        item.innerText = v.Text;

                    System.Console.WriteLine("control: " + v.Name);
                }

                bg.style.display = IStyle.DisplayEnum.none;
                bg.appendChild(item);

                item.style.SetLocation(
                    v.Location.X, v.Location.Y, v.Size.Width, v.Size.Height
                    );

                System.Console.WriteLine("children: " + v.Controls.Count);

                if (v.Controls.Count > 0)
                    SpawnControls(item, v.Controls);

                bg.style.display = IStyle.DisplayEnum.block;
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
