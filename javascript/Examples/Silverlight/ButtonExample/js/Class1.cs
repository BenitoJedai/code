using ScriptCoreLib;
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

        static int ClickCount = 1;

        [Script(NoDecoration = true)]
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

        [Script(NoDecoration = true)]
        public static void DynamicClick(UIElement sender, MouseEventArgs args)
        {
            Console.WriteLine("DynamicClick");
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
                    Console.WriteLine("Window onload");

                    var ag = (SilverlightControl)Native.Document.getElementById("wpfeControl1");

                    Action<SilverlightControl> ag_FullScreenChanged =
                        delegate
                        {
                            Console.WriteLine("FullScreenChanged A");
                        };

                    ag.FullScreenChanged +=
                        ag_FullScreenChanged;

                    ag.FullScreenChanged +=
                        delegate
                        {
                            Console.WriteLine("FullScreenChanged B");
                        };

                    ag.FullScreenChanged -=
                        ag_FullScreenChanged;

                    Console.WriteLine("InvokeOnComplete set");

                    ag.InvokeOnComplete(
                        delegate
                        {
                            Console.WriteLine("InvokeOnComplete called");


                            var c1 = (Canvas)ag.FindName("button");
                            var c2 = (Canvas)ag.FindName("button2");


                            c2.CanvasLeft = 100;
                            c2.CanvasTop = 200;
                            c2.Opacity = 0.4;

                            var s2 = (ScaleTransform)ag.FindName("button2_scale");

                            s2.ScaleX = 0.5;
                            s2.ScaleY = 0.5;

                            var r2 = (RotateTransform)ag.FindName("button2_rotate");

                            Timer.Interval((t) => r2.Angle++, 50);

                            var cm = (Canvas)ag.FindName("canvas_main");



                            //cm.Children.Add( ag.CreateFromXAML("<Image Source='star.png' />") );


                            var music = new MediaElement(ag);

                            music.BufferingProgressChanged +=
                                delegate
                                {
                                    Console.WriteLine("BufferingProgressChanged: " + music.BufferingProgress * 100 + "%");
                                };

                            music.CurrentStateChanged +=
                                delegate
                                {
                                    Console.WriteLine("CurrentStateChanged: " + music.CurrentState);
                                };

                            
                            music.Source = "music.mp3";

                            cm.Children.Add(music);


                            //c2.AddEventListener("MouseLeftButtonUp", "javascript:xxx$ccc");

                            int y = 1;


                            Action<UIElement, MouseEventArgs> c2_handler =
                                delegate
                                {
                                    y++;

                                    var img = new Image(ag)
                                      {
                                          Source = "star.png",
                                          CanvasLeft = 32,
                                          CanvasTop = 32 * y
                                      };

                                    cm.Children.Add(img);
                                };

                            c2.MouseLeftButtonUp += c2_handler;
                            c2.MouseEnter += delegate { c2.Opacity = .2; };
                            c2.MouseLeave += delegate { c2.Opacity = 1; };

                            //Action<UIElement, MouseEventArgs> ptr = DynamicClick;

                            //var f = IFunction.OfDelegate(ptr);

                            //c2.AddEventListener("MouseLeftButtonUp", f);

                            //c2.MouseLeftButtonUp +=
                            //    delegate
                            //    {
                            //        Native.Window.alert("wadap");
                            //    };

                        }
                    );
                };

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );
        }
    }
}
