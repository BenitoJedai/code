using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using NatureBoy.js.Zak;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
{
    partial class Class6
    {
        static void AttachImageToDocument(IHTMLImage img)
        {
            img.AttachToDocument();
        }

        public static readonly Zak.WorldInfo DefaultData =
             new Zak.WorldInfo
             {
                 Sprites = new Zak.SpriteInfo[] {
                    "room 001, obj 005 (296,32)",
                    "room 001",
                    "room 002, obj 002 (0,24)",
                    "room 002"
                },
                 BackgroundColor = Color.FromRGB(0, 0, 0),
                 TextColor = Color.FromRGB(0, 0xff, 0),
                 ControlSize = new Zak.Point(320, 240),
                 Zoom = 1d.ToString(),
                 ClientRect = new Zak.Rect(0, 12, 320, 128),
                 ClientRectColor = Color.FromRGB(0, 0, 0xff)
             };


        private void Initialize()
        {
            var Images = new Dictionary<string, IHTMLImage>();
            var ImagesLoaded = default(Action);

            var Zoom = Data.Zoom.ToDouble();

            var ControlSize = new ZoomedPoint
            {
                Z = Zoom,
                X = Data.ControlSize.Xint,
                Y = Data.ControlSize.Yint
            };

            Control.style.SetSize(ControlSize.ZoomedXint, ControlSize.ZoomedYint);
            Control.style.backgroundColor = Data.BackgroundColor;
            Control.style.color = Data.TextColor;
            Control.AttachAsNextOrToDocument(AnchorElement);
            Control.style.position = IStyle.PositionEnum.relative;

            Action LoadImages =
                delegate
                {
                    Data.Sprites.
                        ForEach(i =>
                            new IHTMLImage(
                                Data.AssetsLocation.DefaultToEmptyString() + Assets.Images + "/" + i.Value + ".png"
                            ).InvokeOnComplete(
                                img =>
                                {
                                    Images[i.Value] = img;

                                    if (Images.Count == Data.Sprites.Length)
                                        ImagesLoaded();
                                }
                            )
                        );
                };

            ImagesLoaded =
                delegate
                {
                    var ClientRectPos = new ZoomedPoint
                    {
                        Z = Zoom,
                        X = Data.ClientRect.From.Xint,
                        Y = Data.ClientRect.From.Yint
                    };

                    var ClientRectSize = new ZoomedPoint
                    {
                        Z = Zoom,
                        X = Data.ClientRect.Size.Xint,
                        Y = Data.ClientRect.Size.Yint
                    };

                    var ClientRect = new IHTMLDiv();

                    ClientRect.style.backgroundColor = Data.ClientRectColor;
                    ClientRect.style.SetLocation(
                        ClientRectPos.ZoomedXint,
                        ClientRectPos.ZoomedYint,
                        ClientRectSize.ZoomedXint,
                        ClientRectSize.ZoomedYint
                        );
                    ClientRect.AttachTo(Control);


                    var img1 = (IHTMLImage)Images["room 001, obj 005 (296,32)"].cloneNode(false);
                    var img2 = (IHTMLImage)Images["room 002, obj 002 (0,24)"].cloneNode(false);
                    var img3 = (IHTMLImage)Images["room 002, obj 002 (0,24)"].cloneNode(false);

                    img1.style.SetLocation(4, 4);
                    img1.AttachTo(ClientRect);

                    img2.style.SetLocation(100, 4);
                    img2.AttachTo(ClientRect);
                    img2.onfocus += ev => img2.blur();

                    img3.style.SetLocation(200, 4);
                    img3.AttachTo(ClientRect);
                    img3.onfocus += ev => ev.PreventDefault();
                };

            LoadImages();
        }
    }
}
