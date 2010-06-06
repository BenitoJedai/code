extern alias examples;


// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ExampleGallery;
using ExampleGallery.Avalon.Images;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Carousel;
using ScriptCoreLib.Shared.Avalon.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextButton;
using ScriptCoreLib.Shared.Avalon.TiledImageButton;
using ScriptCoreLib.Shared.Lambda;

namespace ExampleGallery
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 720;

        public class OptionPosition
        {
            public int X;
            public int Y;

            public Action Clear;
        }

        public delegate void ViewSelected(string e);

        public event ViewSelected AtViewSelected;

        public ApplicationCanvas()
            : this(true, null)
        {
        }

        public ApplicationCanvas(bool EnableBackground, Func<string, OptionPosition> GetOptionPosition)
        {
            Width = DefaultWidth;
            Height = DefaultHeight;




            var navbar = new AeroNavigationBar();

            var Container = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Name = "AvalonExampleGalleryCanvas_Container"
            }.AttachTo(this);

            Container.ClipTo(0, 0, DefaultWidth, DefaultHeight);

            var Pages = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Name = "AvalonExampleGalleryCanvas_Pages"
            }.AttachTo(this);


            var CarouselPages = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Name = "AvalonExampleGalleryCanvas_CarouselPages"
            }.AttachTo(this);

            var Overlay = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Name = "AvalonExampleGalleryCanvas_Overlay"
            }.AttachTo(this);


            if (EnableBackground)
            {
                #region background
                var bg = new TiledBackgroundImage(
                    new Avalon.Images.bg().Source,
                    96,
                    96,
                    9,
                    8
                ).AttachContainerTo(Container);


                #endregion

            }


            var Toolbar = new Canvas
            {
                Width = DefaultWidth,
                Height = navbar.Height,
                Opacity = 0,
                Name = "Toolbar"
            }.AttachTo(this);

            1000.AtDelay(
                Toolbar.FadeIn
            );

            #region shadow
            Colors.Black.ToTransparentGradient(40).Select(
                (c, i) =>
                {
                    return new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = DefaultWidth,
                        Height = 1,
                        Opacity = c.A / 255.0
                    }.MoveTo(0, i).AttachTo(Toolbar);
                }
            ).ToArray();
            #endregion

            var cc = new SimpleCarouselControl(DefaultWidth, DefaultHeight);
            const string cc_Caption = "Click on a thumbnail!";

            cc.Caption.Text = cc_Caption;
            cc.Timer.Stop();

            var btnCarouselCanvas = new Canvas
            {
                Width = 128,
                Height = 32,
            }.AttachTo(this).MoveTo(DefaultWidth - 128, 4);


            #region Options
            var AllPages = new
            {
                Preview = default(Func<Image>),
                CanvasType = default(Func<Type>),
                Text = default(Func<string>)
            }.ToEmptyList(
                (Func<Image> Preview, Func<Type> CanvasType, Func<string> Text) => new { Preview, CanvasType, Text }
            );

            var x = new []
            {
                examples::InteractiveAffineCube.Example.Yield,
                examples::InteractiveAffineWall.Example.Yield
            }.WithEach(k => k(AllPages.Add));


            AllPages.ForEach(
                (k, i) =>
                {
                    var o = new OptionWithShadowAndType(k.Preview, k.CanvasType(), k.Text);


                    var ce = new SimpleCarouselControl.EntryInfo
                    {
                        Source = (k.Preview().Source),
                        Position = i * Math.PI * 2 / AllPages.Count,
                        MouseEnter =
                            delegate
                            {
                                cc.Caption.Text = o.Caption.Text;
                            },
                        MouseLeave =
                            delegate
                            {
                                cc.Caption.Text = cc_Caption;
                            },
                        Click =
                            delegate
                            {
                                o.InitializeHint();


                                navbar.History.Add(
                                    delegate
                                    {

                                        cc.Timer.Start();
                                        o.Target.Orphanize();
                                        CarouselPages.Show();
                                        Overlay.Show();
                                        btnCarouselCanvas.Show();
                                    },
                                    delegate
                                    {
                                        if (AtViewSelected != null)
                                            AtViewSelected(o.Caption.Text);


                                        btnCarouselCanvas.Hide();
                                        cc.Timer.Stop();
                                        CarouselPages.Hide();
                                        Overlay.Hide();
                                        o.Target.AttachTo(Container);
                                    }
                                );
                            }
                    };

                    cc.AddEntry(ce);


                    OptionPosition p = null;

                    if (GetOptionPosition != null)
                        p = GetOptionPosition(o.Caption.Text);

                    if (p == null)
                    {
                        o.MoveTo(
                            48 + (180) * (i % 4),
                            36 + Convert.ToInt32(i / 4) * 128
                        );
                    }
                    else
                    {
                        p.Clear();

                        o.MoveTo(
                            p.X,
                            p.Y
                        );
                    }

                    o.AttachContainerTo(Pages);
                    o.Overlay.AttachTo(Overlay);

                    o.TargetInitialized +=
                        delegate
                        {
                            o.Target.MoveTo(
                                (DefaultWidth - o.Target.Width) / 2,
                                (DefaultHeight - o.Target.Height) / 2
                            );

                            //o.Target.ClipTo(0, 0, Convert.ToInt32( o.Target.Width), Convert.ToInt32(o.Target.Height));
                        };

                    o.Click +=
                        delegate
                        {

                            navbar.History.Add(
                                delegate
                                {

                                    o.Target.Orphanize();
                                    Pages.Show();
                                    Overlay.Show();
                                    btnCarouselCanvas.Show();
                                },
                                delegate
                                {
                                    if (AtViewSelected != null)
                                        AtViewSelected(o.Caption.Text);

                                    btnCarouselCanvas.Hide();
                                    Pages.Hide();
                                    Overlay.Hide();
                                    o.Target.AttachTo(Container);
                                }
                            );
                        };
                }
            );
            #endregion

            #region btnCarousel


            var btnCarousel = new TextButtonControl
            {
                Width = btnCarouselCanvas.Width,
                Height = btnCarouselCanvas.Height,
                Text = "View as carousel...",
                Foreground = Brushes.White
            }.AttachContainerTo(btnCarouselCanvas);

            btnCarousel.MouseEnter +=
                delegate
                {
                    btnCarousel.Foreground = Brushes.Blue;
                };

            btnCarousel.MouseLeave +=
                delegate
                {
                    btnCarousel.Foreground = Brushes.White;
                };

            btnCarousel.Click +=
                delegate
                {
                    navbar.History.Add(
                        delegate
                        {
                            Pages.Show();
                            btnCarousel.Container.Show();
                            CarouselPages.Hide();

                            cc.Hide();
                            cc.Timer.Stop();
                        },
                        delegate
                        {
                            Pages.Hide();
                            CarouselPages.Show();
                            btnCarousel.Container.Hide();
                            cc.Show();
                            cc.Timer.Start();
                        }
                    );
                };
            #endregion


            cc.Hide();

            CarouselPages.Hide();
            cc.AttachContainerTo(CarouselPages);

            cc.Overlay.Name = "cc_Overlay";
            cc.Overlay.AttachTo(Overlay);



            #region logo
            var logo = new white_jsc
            {
                Width = 96,
                Height = 96
            }.MoveTo(DefaultWidth - 96, DefaultHeight - 96).AttachTo(Container);

            var logo_Overlay = new Rectangle
            {
                Width = 96,
                Height = 96,
                Fill = Brushes.Blue,
                Opacity = 0,
                Cursor = Cursors.Hand
            }.MoveTo(DefaultWidth - 96, DefaultHeight - 96).AttachTo(Overlay);

            logo_Overlay.MouseEnter +=
                delegate
                {
                    Pages.Opacity = 0.5;
                    CarouselPages.Opacity = 0.5;
                };

            logo_Overlay.MouseLeave +=
                delegate
                {
                    Pages.Opacity = 1;
                    CarouselPages.Opacity = 1;
                };

            logo_Overlay.MouseLeftButtonUp +=
                delegate
                {
                    new Uri("http://jsc.sourceforge.net").NavigateTo();
                };
            #endregion


            navbar.MoveContainerTo(4, 4).AttachContainerTo(Toolbar);


        }

    }
}
