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

        static bool TypeEquals(object o, System.Type t)
        {
            // this seems to fault
            var a = o.GetType();

            var x = a.TypeHandle;
            var y = t.TypeHandle;

            return x.Value == y.Value;
        }

        private static void SpawnUserControl(UserControl u, Point pos)
        {
            var bg = u.GetHTMLTarget();

            if (bg == null)
                throw new System.Exception();


            var s = u.Size;


            bg.style.SetSize(s.Width, s.Height);

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


                    item = btn.GetHTMLTarget();

                    //h.onclick += btn.Click;


                    System.Console.WriteLine("button: " + v.Name);
                }
                else if (IsTypeOf(typeof(ComboBox)))
                {
                    var cmb = (ComboBox)v;

                    item = cmb.GetHTMLTarget();


                }
                else if (IsTypeOf(typeof(GroupBox)))
                {
                    item = v.GetHTMLTarget();
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
                        var ta = new IHTMLTextArea(txt.Text); ;

                        ta.rows = 1;
                        ta.style.overflow = IStyle.OverflowEnum.auto;

                        item = ta;
                        //item = new IHTMLInput(HTMLInputTypeEnum.text, txt.Text);
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
