using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
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
    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {
            var t = new TextBlock().AttachTo(this);

            var redblockcontainer = new Canvas();

            redblockcontainer.Opacity = 0.8;
            redblockcontainer.Background = Brushes.Red;
            redblockcontainer.AttachTo(this);
            redblockcontainer.MoveTo(8, 8);
            this.SizeChanged += (s, e) => redblockcontainer.MoveTo(8, this.Height / 2).SizeTo(this.Width - 16.0, this.Height / 2 - 8);

            var redblockoverlay = new Canvas();

            redblockoverlay.Opacity = 0.1;
            redblockoverlay.Background = Brushes.Red;
            redblockoverlay.AttachTo(this);
            redblockoverlay.MoveTo(8, 8);
            this.SizeChanged += (s, e) => redblockoverlay.MoveTo(8, this.Height / 2).SizeTo(this.Width - 16.0, this.Height / 2 - 8);

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

                    // case 2
                    var a = ((object)e as __TouchEventArgs);
                    if (a != null)
                    {
                        t.Text = new
                        {
                            case2 = new
                            {
                                a.InternalValue.screenX,
                                a.InternalValue.screenY,
                                a.InternalValue.clientX,
                                a.InternalValue.clientY,
                                a.InternalValue.pageX,
                                a.InternalValue.pageY
                            }
                        }.ToString();

                    }



                    a_case2[e.TouchDevice.Id].MoveTo(p);


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

                    var a = ((object)e as __TouchEventArgs);
                    if (a != null)
                    {
                        t.Text = new
                        {
                            case2 = new
                            {
                                a.InternalValue.screenX,
                                a.InternalValue.screenY,
                                a.InternalValue.clientX,
                                a.InternalValue.clientY,
                                a.InternalValue.pageX,
                                a.InternalValue.pageY
                            }
                        }.ToString();

                    }


                    a_case3[e.TouchDevice.Id].MoveTo(p);

                    Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + p);
                };



        }

    }
}
