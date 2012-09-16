using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TestAvalonMultitouch
{
    class GetPositionData
    {
        public IHTMLElement Element;

        public int X;
        public int Y;

        public static List<GetPositionData> Of(IHTMLElement e)
        {
            var a = new List<GetPositionData>();

            var x = 0;
            var y = 0;

            while (ShouldVisitParent(e))
            {
                x += e.offsetLeft;
                y += e.offsetTop;

                a.Add(
                    new GetPositionData
                    {
                        Element = e,
                        X = x,
                        Y = y
                    }
                );

                e = (IHTMLElement)e.parentNode;
            }

            return a;
        }

        static bool ShouldVisitParent(IHTMLElement e)
        {
            if (e.parentNode == null)
                return false;

            return e.parentNode != Native.Document;
        }
    }

    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {
            var c = new CheckBox
            {
                Content = new TextBlock { Text = "Print to Console  " }
            }.MoveTo(8, 96);


            var t = new TextBlock { Text = "?" }.AttachTo(this);

            var redblockcontainer = new Canvas();

            redblockcontainer.Opacity = 0.8;
            redblockcontainer.Background = Brushes.Red;
            redblockcontainer.AttachTo(this);
            redblockcontainer.MoveTo(8, 8);
            this.SizeChanged += (s, e) => redblockcontainer.MoveTo(64 - 16, this.Height / 3 - 16).SizeTo(this.Width - 96, this.Height / 3 - 8);

            var redblockoverlay = new Canvas();

            redblockoverlay.Opacity = 0.1;
            redblockoverlay.Background = Brushes.Red;
            redblockoverlay.AttachTo(this);
            redblockoverlay.MoveTo(8, 8);
            this.SizeChanged += (s, e) => redblockoverlay.MoveTo(64 + 64, this.Height / 3).SizeTo(this.Width - 96 - 64, this.Height / 3 - 8);

            var yellowblock = new Canvas();
            yellowblock.Opacity = 0.7;
            yellowblock.Background = Brushes.Yellow;
            yellowblock.AttachTo(this);
            yellowblock.SizeTo(400, 200);


            var a_case1 = Enumerable.Range(0, 64).Select(
              i =>
              {
                  var rr = new Rectangle();
                  rr.Fill = Brushes.Blue;
                  rr.AttachTo(yellowblock);
                  rr.SizeTo(32, 32);
                  rr.MoveTo(-32, -32);
                  return rr;
              }
          ).ToArray();


            var a_case2 = Enumerable.Range(0, 64).Select(
              i =>
              {
                  var rr = new Rectangle();
                  rr.Fill = Brushes.Green;
                  rr.AttachTo(this);
                  rr.SizeTo(32, 32);
                  rr.MoveTo(-32, -32);
                  return rr;
              }
          ).ToArray();


            var greenblock = new Canvas();
            greenblock.Opacity = 0.5;
            greenblock.Background = Brushes.Green;
            greenblock.AttachTo(this);
            greenblock.SizeTo(400, 200);
            greenblock.MoveTo(200 - 12, 12);

            var a_case3 = Enumerable.Range(0, 64).Select(
              i =>
              {
                  var rr = new Rectangle();
                  rr.Fill = Brushes.Black;
                  rr.AttachTo(redblockcontainer);
                  rr.SizeTo(32, 32);
                  rr.MoveTo(-32, -32);
                  return rr;
              }
          ).ToArray();



            var case1 = yellowblock;
            var case2 = greenblock;
            var case3 = redblockoverlay;

            #region case1
            case1.TouchDown +=
                (s, e) =>
                {
                    e.Handled = true;
                    // is called implicitly on Android Chrome
                    e.TouchDevice.Capture(case1);

                    Console.WriteLine("TouchDown");
                };

            case1.MouseMove +=
            (s, e) =>
            {
                // case 1
                var p = e.GetPosition(case1);

                a_case1.Last().MoveTo(p);

                if ((bool)c.IsChecked)
                    Console.WriteLine("MouseMove " + p);
            };

            case1.TouchUp +=
          (s, e) =>
          {
              Console.WriteLine("TouchUp");
          };
            #endregion

            case1.TouchMove +=
                (s, e) =>
                {
                    // case 1
                    var p = e.GetTouchPoint(case1).Position;

                    a_case1[e.TouchDevice.Id].MoveTo(p);

                    if ((bool)c.IsChecked)
                        Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + p);
                };




            #region case2
            case2.TouchDown +=
            (s, e) =>
            {
                e.Handled = true;
                // is called implicitly on Android Chrome
                e.TouchDevice.Capture(case2);

                Console.WriteLine("TouchDown");
            };

            case2.MouseMove +=
             (s, e) =>
             {
                 // case 1
                 var p = e.GetPosition(this);

                 a_case2.Last().MoveTo(p);

                 if ((bool)c.IsChecked)
                     Console.WriteLine("MouseMove " + p);
             };
            case2.TouchUp +=
          (s, e) =>
          {
              Console.WriteLine("TouchUp");
          };
            #endregion
            case2.TouchMove +=
                (s, e) =>
                {
                    var p = e.GetTouchPoint(this).Position;

                    t.Text = new { case2 = p }.ToString();

                    //// case 2
                    //var a = ((object)e as __TouchEventArgs);
                    //if (a != null)
                    //{



                    //    var pp = new Point(
                    //       a.InternalValue.pageX,
                    //       a.InternalValue.pageY
                    //    );


                    //    var b = GetPositionData.Of(a.InternalElement);

                    //    var item = b.Last();

                    //    pp.X -= item.X;
                    //    pp.Y -= item.Y;


                    //    t.Text = new
                    //    {
                    //        case2 = new
                    //        {
                    //            pp,

                    //            a.InternalValue.screenX,
                    //            a.InternalValue.screenY,
                    //            a.InternalValue.clientX,
                    //            a.InternalValue.clientY,
                    //            a.InternalValue.pageX,
                    //            a.InternalValue.pageY
                    //        }
                    //    }.ToString();
                    //}



                    a_case2[e.TouchDevice.Id].MoveTo(p);


                    if ((bool)c.IsChecked)
                        Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + p);
                };



            #region case3
            case3.TouchDown +=
            (s, e) =>
            {
                e.Handled = true;
                // is called implicitly on Android Chrome
                e.TouchDevice.Capture(case3);

                Console.WriteLine("TouchDown");
            };

            case3.MouseMove +=
                (s, e) =>
                {
                    // case 1
                    var p = e.GetPosition(redblockcontainer);

                    a_case3.Last().MoveTo(p);

                    if ((bool)c.IsChecked)
                        Console.WriteLine("MouseMove " + p);
                };
            case3.TouchUp +=
                  (s, e) =>
                  {
                      Console.WriteLine("TouchUp");
                  };
            #endregion
            case3.TouchMove +=
                (s, e) =>
                {
                    // case 3
                    var p = e.GetTouchPoint(redblockcontainer).Position;

                    t.Text = new { case3 = p }.ToString();

                    //var args = ((object)e as __TouchEventArgs);
                    //if (args != null)
                    //{

                    //    var pp = new Point(
                    //       args.InternalValue.pageX,
                    //       args.InternalValue.pageY
                    //    );


                    //    var a = GetPositionData.Of(((object)redblockcontainer as __Canvas).InternalContent);
                    //    var b = GetPositionData.Of(args.InternalElement);


                    //    if (b.Count > 0)
                    //    {
                    //        var item = b.Last();

                    //        pp.X -= item.X;
                    //        pp.Y -= item.Y;
                    //    }


                    //    // top elements might be the same so we remove them
                    //    var loop = true;

                    //    while (loop)
                    //    {
                    //        loop = false;

                    //        if (a.Count > 0)
                    //            if (b.Count > 0)
                    //                if (a[a.Count - 1].Element == b[b.Count - 1].Element)
                    //                {
                    //                    a.RemoveAt(a.Count - 1);
                    //                    b.RemoveAt(b.Count - 1);

                    //                    loop = true;
                    //                }
                    //    }

                    //    if (a.Count > 0)
                    //    {
                    //        var itembb = a.Last();

                    //        pp.X -= itembb.X;
                    //        pp.Y -= itembb.Y;
                    //    }

                    //    if (b.Count > 0)
                    //    {
                    //        var item = b.Last();

                    //        pp.X += item.X;
                    //        pp.Y += item.Y;
                    //    }

                    //    t.Text = new
                    //    {
                    //        case2 = new
                    //        {
                    //            p,
                    //            pp,

                    //            //a.InternalValue.screenX,
                    //            //a.InternalValue.screenY,
                    //            //a.InternalValue.clientX,
                    //            //a.InternalValue.clientY,
                    //            args.InternalValue.pageX,
                    //            args.InternalValue.pageY
                    //        }
                    //    }.ToString();

                    //    a_case3[e.TouchDevice.Id].MoveTo(pp);

                    //    Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + pp);
                    //    return;
                    //}


                    a_case3[e.TouchDevice.Id].MoveTo(p);

                    if ((bool)c.IsChecked)
                        Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + p);
                };

            c.AttachTo(this);

        }

    }
}
