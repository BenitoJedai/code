using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Shared.Avalon.Carousel;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;

namespace CarouselExample2.Shared
{
    public class CarouselExample2Canvas : Canvas
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 320;

        public CarouselExample2Canvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;


            Colors.Black.ToGradient(Colors.Red, DefaultHeight / 4).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = DefaultWidth,
                        Height = 4,
                    }.MoveTo(0, i * 4).AttachTo(this)
            ).ToArray();


            var cc = new SimpleCarouselControl(DefaultWidth, DefaultHeight);


            new AvalonCarouselControlExample.Avalon.Images.Preview().AttachTo(this).Orphanize();

            new Image[]
			{
                new AvalonCarouselControlExample.Avalon.Images.item1(),
                new AvalonCarouselControlExample.Avalon.Images.item2(),
                new AvalonCarouselControlExample.Avalon.Images.item3(),
                new AvalonCarouselControlExample.Avalon.Images.Preview(),
			}.WithEachIndex(
                (Image k, int index) =>
                {
                    cc.AddEntry(
                        new SimpleCarouselControl.EntryInfo
                            {
                                Click = cc.Timer.Toggle,
                                Source = k.Source,
                                Position = Math.PI / 2 * index,
                                MouseEnter =
                                    delegate
                                    {
                                        cc.Caption.Text = k.GetType().Name;
                                    },
                                MouseLeave =
                                    delegate
                                    {
                                        cc.Caption.Clear();
                                    }
                            }
                    );
                }
            );



            cc.AttachContainerTo(this);


            cc.Overlay.AttachTo(this);

        }
    }
}
