﻿using ScriptCoreLib;
using ScriptCoreLib.Shared;



using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.Silverlight;
using ScriptCoreLib.JavaScript.Silverlight.Input;
using ScriptCoreLib.JavaScript.Silverlight.Media;
using ScriptCoreLib.JavaScript.Silverlight.Shapes;
using ScriptCoreLib.JavaScript.Silverlight.Controls;

using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;


using global::System.Collections.Generic;

namespace ButtonExample.js
{
    using Color = ScriptCoreLib.Shared.Drawing.Color;

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        static bool MouseOver;
        static bool MousePressed;

        [Script(NoDecoration = true)]
        public static void button_MouseLeftButtonDown(UIElement sender, MouseEventArgs args)
        {
            Console.WriteLine("button_MouseDown");

            MouseOver = true;
            MousePressed = true;

            sender.CaptureMouse();

            UpdateVisuals(sender);
        }

        static int ClickCount = 0;

        [Script(NoDecoration=true)]
        public static void button_MouseLeftButtonUp(UIElement sender, MouseEventArgs args)
        {
            Console.WriteLine("button_MouseUp");
            MousePressed = false;

            sender.ReleaseMouseCapture();

            Console.WriteLine(args.Position.ToString());

            UpdateVisuals(sender);

            if (args.Ctrl)
            {
                ClickCount++;
                sender.GetHost().FullScreen = ClickCount % 2 == 0;
            }
        }

        [Script(NoDecoration = true)]
        public static void button_MouseEnter(UIElement sender, MouseEventArgs args)
        {
            Console.WriteLine("button_MouseEnter");
            MouseOver = true;

            UpdateVisuals(sender);
        }

        [Script(NoDecoration = true)]
        public static void button_MouseLeave(UIElement sender, MouseEventArgs args)
        {
            Console.WriteLine("button_MouseLeave");
            MouseOver = false;

            UpdateVisuals(sender);
        }

        static void UpdateVisuals(UIElement sender)
        {
            Func<string, TranslateTransform> FintTransform = e => (TranslateTransform)sender.FindName(e);
            Func<string, Rectangle> FintRectangle = e => (Rectangle)sender.FindName(e);

            Console.WriteLine("MouseOver: " + MouseOver);
            Console.WriteLine("MousePressed: " + MousePressed);

            if (MouseOver && MousePressed)
            {

                var transform = FintTransform("button_transform");
                transform.X = 2;
                transform.Y = 2;

                FintRectangle("button_rectangle").Fill = "sc#1, 0.548430264, 0.5354195, 0.5354195";

            }
            else
            {

                var transform = FintTransform("button_transform");
                transform.X = 0;
                transform.Y = 0;

                FintRectangle("button_rectangle").Fill = "sc#1, 0.8123474, 0.8123474, 0.8123474";

            }

            // highlight
            if (MouseOver || MousePressed)
            {
                var button_highlight = FintRectangle("button_highlight");

                button_highlight.Opacity = 1;
            }
            else
            {
                var button_highlight = FintRectangle("button_highlight");
                button_highlight.Opacity = 0;
            }
        }

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
       
            // do nothing

        }

        static Class1()
        {
            Native.Window.onload +=
                delegate
                {
                    IHTMLButton.Create("spawn",
                        delegate
                        {
                            var ag = (SilverlightControl)Native.Document.getElementById("wpfeControl1");

                            var c2 = (Canvas)ag.FindName("button2");


                            c2.CanvasLeft = 100;
                            c2.CanvasTop = 200;
                            c2.Opacity = 0.4;

                            var s2 = (ScaleTransform)ag.FindName("button2_scale");

                            s2.ScaleX = 0.5;
                            s2.ScaleY = 0.5;

                            var r2 = (RotateTransform)ag.FindName("button2_rotate");

                            Timer.Interval((t) => r2.Angle++, 50);


                        }
                    ).attachToDocument();
                };

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );
        }
    }
}
