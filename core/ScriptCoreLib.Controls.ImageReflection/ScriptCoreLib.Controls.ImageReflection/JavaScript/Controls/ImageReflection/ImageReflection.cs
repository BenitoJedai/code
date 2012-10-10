using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls;
using System;


namespace ScriptCoreLib.JavaScript.Controls.ImageReflection
{

    [Script]
    public class ReflectionSetup
    {
        public int Left = -1;
        public int Top = -1;
        public int Right = -1;
        public int Bottom = -1;

        public IHTMLImage Image;

        public Point Size;
        public Point Position;
        public double ReflectionZoom;
        public bool Drag = true;

        public IHTMLDiv ConvertToImageReflection()
        {
            return ImageReflection.ConvertToImageReflection(this);
        }
    }

    [Script]
    public class ReflectionFrameInfo
    {
        public int From;
        public int To;
        public double Opacity;
        public int Size;
        public int Offset;
        public IHTMLElement Owner;
    }


    [Script]
    static class ImageReflection
    {
        public static IHTMLDiv ConvertToImageReflection(ReflectionSetup rs)
        {

            var Control = new IHTMLDiv();

            Control.style.SetLocation(rs.Position.X, rs.Position.Y, +rs.Size.X, +rs.Size.Y);

            rs.Image.style.SetLocation(0, 0, rs.Size.X, rs.Size.Y);
            rs.Image.AttachTo(Control);

            #region drag

            if (rs.Drag)
            {
                rs.Image.onmousedown += Native.DisabledEventHandler;
                rs.Image.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.move;

                var drag = new DragHelper(rs.Image);

                drag.Enabled = true;
                drag.Position = rs.Position;
                drag.DragMove +=
                    delegate
                    {
                        Control.style.SetLocation(drag.Position.X, drag.Position.Y);
                    };


            }

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
            Action<ReflectionFrameInfo> CopyLineY =
                i =>
                {
                    var clone = (IHTMLImage)rs.Image.cloneNode(true);

                    clone.style.SetLocation(0, i.Offset + i.To, rs.Size.X, i.Size);
                    clone.style.clip = GetRect(0, i.From, rs.Size.X, 1);
                    clone.style.Opacity = i.Opacity;
                    clone.AttachTo(i.Owner);
                };
            #endregion



            #region y
            var YMax = (rs.Size.Y * rs.ReflectionZoom).ToInt32();

            if (rs.Bottom >= 0)
            {
                var _bottom = new IHTMLDiv();
                _bottom.style.SetLocation(0, rs.Size.Y - 1 + rs.Bottom, rs.Size.X, YMax);
                _bottom.AttachTo(Control);
                _bottom.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

                for (int y = 0; y < YMax; y++)
                {
                    CopyLineY(
                        new ReflectionFrameInfo
                        {
                            From = y,
                            To = YMax - y * 2,
                            Opacity = y / YMax,
                            Size = YMax,
                            Offset = 0,
                            Owner = _bottom
                        }
                    );
                }
            }

            if (rs.Top >= 0)
            {
                var _top = new IHTMLDiv();
                _top.style.SetLocation(0, -YMax - rs.Top, rs.Size.X, YMax);
                _top.AttachTo(Control);
                _top.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;


                for (int y = 0; y < YMax; y++)
                {
                    CopyLineY(
                        new ReflectionFrameInfo
                        {
                            From = y,
                            To = YMax - y * 2,
                            Opacity = (1 - (y / (YMax))),
                            Size = YMax,
                            Offset = 0,
                            Owner = _top
                        }
                    );
                }
            }
            #endregion

            var XMax = (rs.Size.X * rs.ReflectionZoom).ToInt32();




            #region CopyLineX
            Action<ReflectionFrameInfo> CopyLineX =
                i =>
                {
                    var clone = (IHTMLImage)rs.Image.cloneNode(true);

                    clone.style.SetLocation(i.Offset + i.To, 0, i.Size, rs.Size.Y);
                    clone.style.clip = GetRect(i.From, 0, 1, rs.Size.Y);
                    clone.style.Opacity = i.Opacity;


                    clone.AttachTo(i.Owner);
                };
            #endregion

            #region x

            if (rs.Right >= 0)
            {
                var _right = new IHTMLDiv();
                _right.style.SetLocation(rs.Size.X - 1 + rs.Right, 0, XMax, rs.Size.Y);
                _right.AttachTo(Control);
                _right.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

                for (int x = 0; x < XMax; x++)
                {
                    CopyLineX(
                          new ReflectionFrameInfo
                          {
                              From = x,
                              To = XMax - x * 2,
                              Opacity = (x / (XMax)),
                              Size = XMax,
                              Offset = 0,
                              Owner = _right
                          }
                      );
                }
            }

            if (rs.Left >= 0)
            {
                var _left = new IHTMLDiv();
                _left.style.SetLocation(-XMax - rs.Left, 0, XMax, rs.Size.Y);
                _left.AttachTo(Control);
                _left.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

                for (int x = 0; x < XMax; x++)
                {
                    CopyLineX(
                          new ReflectionFrameInfo
                          {
                              From = x,
                              To = XMax - x * 2,
                              Opacity = (1 - (x / (XMax))),
                              Size = XMax,
                              Offset = 0,
                              Owner = _left
                          }
                      );
                }
            }
            #endregion

            return Control;
        }

    }
}
