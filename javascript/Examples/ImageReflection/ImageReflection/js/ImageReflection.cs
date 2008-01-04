using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using ScriptCoreLib.JavaScript.Controls;


namespace ImageReflection.js
{
    [Script]
    public delegate void Action<A, B, C, D, E>(A a, B b, C c, D d, E e);

    [Script]
    class Info
    {
        public int From;
        public int To;
        public double Opacity;
        public int Size;
        public int Offset;
    }

    [Script, ScriptApplicationEntryPoint(ScriptedLoading = true)]
    public class ImageReflection
    {
        public ImageReflection()
        {
            new IHTMLImage(Assets.Path + "/cal.png").InvokeOnComplete(
                img =>
                {
                    var Size = new Point(img.width, img.height);
                    var ReflectionZoom = 1.0;
                    var Position = new Point(
                        32 + (Size.X * ReflectionZoom).ToInt32(),
                        32 + (Size.Y * ReflectionZoom).ToInt32()
                        );


                    var Control = ConvertToImageReflection(img, Size, ReflectionZoom, Position);

                    Control.AttachToDocument();
                }
            );

            new IHTMLImage(Assets.Path + "/Preview.png").InvokeOnComplete(
                img =>
                {
                    var Size = new Point(img.width, img.height);
                    var ReflectionZoom = 1.0;
                    var Position = new Point(
                        Native.Window.Width - (Size.X + Size.X * ReflectionZoom * 2).ToInt32(),
                        Native.Window.Height - (Size.X + Size.Y * ReflectionZoom * 2).ToInt32()
                        );


                    var Control = ConvertToImageReflection(img, Size, ReflectionZoom, Position);

                    Control.AttachToDocument();
                }
            );
        }

        private static IHTMLDiv ConvertToImageReflection(IHTMLImage img, Point Size, double ReflectionZoom, Point Position)
        {
            var Control = new IHTMLDiv();

            Control.style.SetLocation(Position.X, Position.Y, +Size.X, +Size.Y);

            img.style.SetLocation(0, 0, Size.X, Size.Y);
            img.AttachTo(Control);

            #region drag

            img.onmousedown += Native.DisabledEventHandler;

            var drag = new DragHelper(img);

            drag.Enabled = true;
            drag.Position = Position;
            drag.DragMove +=
                delegate
                {
                    Control.style.SetLocation(drag.Position.X, drag.Position.Y);
                };

            #endregion

            #region GetRect
            Func<int, int, int, int, string> GetRect =
                (left, top, width, height) =>
                {
                    // rect (top, right, bottom, left)
                    var x = string.Format("rect({0}px, {1}px, {2}px, {3}px)", top, width + left, height + top, left);
                    return x;
                };
            #endregion


            #region CopyLineY
            Action<Info> CopyLineY =
                i =>
                {
                    var clone = (IHTMLImage)img.cloneNode(true);

                    clone.style.SetLocation(0, i.Offset + i.To, Size.X, i.Size);
                    clone.style.clip = GetRect(0, i.From, Size.X, 1);
                    clone.style.Opacity = i.Opacity;
                    clone.AttachTo(Control);
                };
            #endregion



            #region y
            var YMax = (Size.Y * ReflectionZoom).ToInt32();

            for (int y = 0; y < YMax; y++)
            {
                CopyLineY(
                    new Info
                    {
                        From = y,
                        To = YMax - y * 2,
                        Opacity = y / YMax,
                        Size = YMax,
                        Offset = (Size.Y) - 1
                    }
                );
            }


            for (int y = 0; y < YMax; y++)
            {
                CopyLineY(
                    new Info
                    {
                        From = y,
                        To = YMax - y * 2,
                        Opacity = (1 - (y / (YMax))),
                        Size = YMax,
                        Offset = -(YMax)
                    }
                );
            }
            #endregion


            #region CopyLineX
            Action<Info> CopyLineX =
                i =>
                {
                    var clone = (IHTMLImage)img.cloneNode(true);

                    clone.style.SetLocation(i.Offset + i.To, 0, i.Size, Size.Y);
                    clone.style.clip = GetRect(i.From, 0, 1, Size.Y);
                    clone.style.Opacity = i.Opacity;

                    clone.AttachTo(Control);
                };
            #endregion

            #region x
            var XMax = (Size.X * ReflectionZoom).ToInt32();

            for (int x = 0; x < XMax; x++)
            {
                CopyLineX(
                      new Info
                      {
                          From = x,
                          To = XMax - x * 2,
                          Opacity = (x / (XMax)),
                          Size = XMax,
                          Offset = (Size.X - 1)
                      }
                  );
            }

            for (int x = 0; x < XMax; x++)
            {
                CopyLineX(
                      new Info
                      {
                          From = x,
                          To = XMax - x * 2,
                          Opacity = (1 - (x / (XMax))),
                          Size = XMax,
                          Offset = -(XMax)
                      }
                  );
            }
            #endregion
            return Control;
        }

        static ImageReflection()
        {
            typeof(ImageReflection).SpawnTo(i => new ImageReflection());
        }

    }

}
