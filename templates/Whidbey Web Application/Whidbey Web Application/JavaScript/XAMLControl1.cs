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

namespace Whidbey_Web_Application.JavaScript
{
    [Script]
    public class XAMLControl1
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
            Func<string, TranslateTransform> FintTransform =
                delegate(string e) { return (TranslateTransform)sender.FindName(e); };

            Func<string, Rectangle> FintRectangle =
                delegate(string e) { return (Rectangle)sender.FindName(e); };


            Console.WriteLine("MouseOver: " + MouseOver);
            Console.WriteLine("MousePressed: " + MousePressed);

            if (MouseOver && MousePressed)
            {

                TranslateTransform transform = FintTransform("button_transform");
                transform.X = 2;
                transform.Y = 2;

                FintRectangle("button_rectangle").Fill = "sc#1, 0.548430264, 0.5354195, 0.5354195";

            }
            else
            {

                TranslateTransform transform = FintTransform("button_transform");
                transform.X = 0;
                transform.Y = 0;

                FintRectangle("button_rectangle").Fill = "sc#1, 0.8123474, 0.8123474, 0.8123474";

            }

            // highlight
            if (MouseOver || MousePressed)
            {
                Rectangle button_highlight = FintRectangle("button_highlight");

                button_highlight.Opacity = 1;
            }
            else
            {
                Rectangle button_highlight = FintRectangle("button_highlight");
                button_highlight.Opacity = 0;
            }
        }




        static XAMLControl1()
        {
            Native.Window.onload +=
                delegate
                {
                    Console.Log("onload called");

                    SilverlightControl ag = (SilverlightControl)Native.Document.getElementById("wpfeControl1");

                    if (ag == null)
                        return;


                    SilverlightControlExtensions.InvokeOnComplete(ag,
                        delegate
                        {
                            Console.Log("InvokeOnComplete");


                            Canvas c1 = (Canvas)ag.FindName("button");
                            Canvas c2 = (Canvas)ag.FindName("button2");


                            c2.CanvasLeft = 100;
                            c2.CanvasTop = 200;
                            c2.Opacity = 0.4;

                            ScaleTransform s2 = (ScaleTransform)ag.FindName("button2_scale");

                            s2.ScaleX = 0.5;
                            s2.ScaleY = 0.5;

                            RotateTransform r2 = (RotateTransform)ag.FindName("button2_rotate");

                            Timer.Interval(

                                delegate { r2.Angle++; } , 50);

                            Canvas cm = (Canvas)ag.FindName("canvas_main");



                            //cm.Children.Add( ag.CreateFromXAML("<Image Source='star.png' />") );


                            //MediaElement music = new MediaElement(ag);

                            //music.BufferingProgressChanged +=
                            //    delegate
                            //    {
                            //        Console.WriteLine("BufferingProgressChanged: " + music.BufferingProgress * 100 + "%");
                            //    };

                            //music.CurrentStateChanged +=
                            //    delegate
                            //    {
                            //        Console.WriteLine("CurrentStateChanged: " + music.CurrentState);
                            //    };


                            //music.Source = "music.mp3";

                            //cm.Children.Add(music);


                            //c2.AddEventListener("MouseLeftButtonUp", "javascript:xxx$ccc");

                            int y = 1;


                            Action<UIElement, MouseEventArgs> c2_handler =
                                delegate
                                {
                                    y++;

                                    Image img = new Image(ag);

                                    img.Source = "star.png";
                                    img.CanvasLeft = 32;
                                    img.CanvasTop = 32 * y;


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


        }
    }
}
