using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace Toolbar.JavaScript
{
    [Script]
    public class ToolbarDialog
    {
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

            t.Control.style.SetLocation(t.Drag.Position.X, t.Drag.Position.Y, toolbar_size.X, toolbar_size.Y);

            t.Control.SetDialogColor(toolbar_color);
            t.Drag.Enabled = true;
            t.Drag.DragMove += t.ApplyPosition;

            return t;
        }

        public IHTMLDiv Control;
        public DragHelper Drag;
        public Color Color;

        public Point Size;

        public readonly List<ToolbarButton> Buttons = new List<ToolbarButton>();

        public void Grow()
        {
            this.Size.X = (24 * (this.Buttons.Count) + 4);

            this.Control.style.SetSize(Size.X, Size.Y);
        }

        public Func<Color> ActivatedButtonColor;

        public void ApplyPosition()
        {
            // toolbar must remain visible all times
            var pos = this.Drag.Position;

            pos.X = pos.X.Max(0);
            pos.Y = pos.Y.Max(0);

            pos.X = pos.X.Min(Native.Window.Width - (Size.X + 2));
            pos.Y = pos.Y.Min(Native.Window.Height - (Size.Y + 2));

            this.Control.style.SetLocation(pos.X, pos.Y);
        }


        public ToolbarDialog()
        {
            ActivatedButtonColor = () => this.Color.AddLum(20);
        }
    }



}
