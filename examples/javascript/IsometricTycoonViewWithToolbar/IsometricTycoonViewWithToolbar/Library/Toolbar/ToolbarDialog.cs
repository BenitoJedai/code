using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;
//using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using System.Windows.Forms;

namespace Toolbar.JavaScript
{
    public class ToolbarDialog
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/2013
        // lets make toolbar able to pop up

        public Form ControlForm = new Form
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Form.set_ShowIcon(System.Boolean)]

            ShowIcon = false,
            SizeGripStyle = SizeGripStyle.Hide,
            BackColor = System.Drawing.Color.Transparent
        };


        public static ToolbarDialog CreateToolbar(Point toolbar_pos, Point toolbar_size, Color toolbar_color)
        {
            var t = new ToolbarDialog
            {
                Color = toolbar_color,
                Size = toolbar_size
            };

            t.Control = new IHTMLDiv();
            t.Drag = new DragHelper(t.Control);
            t.Drag.Position = toolbar_pos;

            //t.ControlForm.BackColor = System.Drawing.Color.Green;
            t.ControlForm.MoveTo(t.Drag.Position.X, t.Drag.Position.Y);
            //t.ControlForm.SizeTo(toolbar_size.X + 16, toolbar_size.Y + 32);



            t.Control.style.SetLocation(t.Drag.Position.X, t.Drag.Position.Y, toolbar_size.X, toolbar_size.Y);

            t.Control.SetDialogColor(toolbar_color);
            t.Drag.Enabled = true;
            t.Drag.DragMove += t.ApplyPosition;

            t.Grow();

            return t;
        }

        public IHTMLDiv Control;
        public DragHelper Drag;
        public Color Color;

        public Point Size;

        public readonly List<ToolbarButton> Buttons = new List<ToolbarButton>();

        public void Grow()
        {
            this.Size.X = Math.Max(this.Size.X, (24 * (this.Buttons.Count) + 4));

            this.Control.style.SetSize(Size.X, Size.Y);
            this.ControlForm.SizeTo(Size.X + 4, Size.Y + 24);
        }

        public Func<Color> ActivatedButtonColor;

        public void ApplyPosition()
        {
            // toolbar must remain visible all times
            var pos = this.Drag.Position;

            pos.X = pos.X.Max(0);
            pos.Y = pos.Y.Max(0);

            pos.X = pos.X.Min(Native.window.Width - (Size.X + 2));
            pos.Y = pos.Y.Min(Native.window.Height - (Size.Y + 2));

            this.Control.style.SetLocation(pos.X, pos.Y);
        }


        public ToolbarDialog()
        {
            ActivatedButtonColor = () => this.Color.AddLum(20);
        }
    }



}
