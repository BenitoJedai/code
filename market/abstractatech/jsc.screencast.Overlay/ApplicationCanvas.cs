using ScriptCoreLib.Extensions;
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
using ScriptCoreLib.Avalon;
using System.Windows.Media.Effects;

namespace jsc.screencast.Overlay
{
    public class ApplicationCanvasX : ApplicationCanvasXTransparent
    {
        public ApplicationCanvasX()
        {
            this.Background = Brushes.Lime;
        }
    }

    public class ApplicationCanvasXTransparent : Canvas
    {
        public ApplicationCanvasXTransparent()
        {
            var Borders = Enumerable.Range(1, 12).Reverse().Select(
                Width =>
                {
                    var Left = new Rectangle { Fill = Brushes.Black, Opacity = 0.06 }.MoveTo(0, 0).AttachTo(this);
                    var Right = new Rectangle { Fill = Brushes.Black, Opacity = 0.06 }.MoveTo(0, 0).AttachTo(this);
                    var Bottom = new Rectangle { Fill = Brushes.Black, Opacity = 0.09 }.MoveTo(0, 0).AttachTo(this);
                    var Top = new Rectangle { Fill = Brushes.Black, Opacity = 0.11 }.MoveTo(0, 0).AttachTo(this);


                    return new
                    {
                        Width = Width * 1.5,
                        Left,
                        Right,
                        Bottom,
                        Top
                    };
                }
            ).ToArray();




            CreateLogoAnimation();

            #region CaptionTextShadow
            var CaptionTextShadow = new System.Windows.Controls.TextBox
            {
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.Black,
                Text = "jsc-solutions.net",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 16,
                TextAlignment = System.Windows.TextAlignment.Right
            }
            .AttachTo(this);
            #endregion

            #region CaptionText
            var CaptionText = new System.Windows.Controls.TextBox
            {
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.White,
                Text = "jsc-solutions.net",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 16,
                TextAlignment = System.Windows.TextAlignment.Right
            }
            .AttachTo(this);
            #endregion


            #region TopicTextShadow
            TopicTextShadow = new System.Windows.Controls.TextBox
            {
                //IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.Black,
                Text = "JSC C# Foo Bar",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 24,
                TextAlignment = System.Windows.TextAlignment.Right
            }
            .AttachTo(this);
            #endregion

            #region TopicText
            TopicText = new System.Windows.Controls.TextBox
            {
                //IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.White,
                Text = "JSC C# Foo Bar",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 24,
                TextAlignment = System.Windows.TextAlignment.Right
            }
            .AttachTo(this);
            #endregion

            Action SizeChanged =
              delegate
              {

                  var w = new { ActualWidth = this.Width, ActualHeight = this.Height };

                  Borders.WithEach(k => { k.Left.MoveTo(0, 0).SizeTo(k.Width, w.ActualHeight); });
                  Borders.WithEach(k => { k.Right.MoveTo(w.ActualWidth - k.Width, 0).SizeTo(k.Width, w.ActualHeight); });
                  Borders.WithEach(k => { k.Bottom.MoveTo(0, w.ActualHeight - k.Width).SizeTo(w.ActualWidth, k.Width); });
                  Borders.WithEach(k => { k.Top.MoveTo(0, 0).SizeTo(w.ActualWidth, k.Width); });

                  CaptionText.MoveTo(0, 6).SizeTo(this.Width - 6, 24);
                  CaptionTextShadow.MoveTo(0 + 1, 6 + 1).SizeTo(this.Width - 6, 24);

                  TopicText.MoveTo(0, this.Height - 38).SizeTo(this.Width - 6, 38);
                  TopicTextShadow.MoveTo(2, this.Height - 38 + 2).SizeTo(this.Width - 6, 38);
              };

            this.SizeChanged +=
                delegate
                {
                    SizeChanged();
                };

            this.SizeTo(200, 200);
        }

        System.Windows.Controls.TextBox TopicText;
        System.Windows.Controls.TextBox TopicTextShadow;

        public string Topic { get { return this.TopicText.Text; } set { this.TopicText.Text = value; this.TopicTextShadow.Text = value; } }

        private void CreateLogoAnimation()
        {
            c = new JSCSolutionsNETCarouselCanvas();

            c.CloseOnClick = false;
            c.AttachContainerTo(this);
            c.MoveContainerTo(-200 + 48, -200 + 48);

            if (AnimationCreated != null)
                AnimationCreated(c);

        }

        JSCSolutionsNETCarouselCanvas c;

        public event Action<JSCSolutionsNETCarouselCanvas> AnimationCreated;


        public void Splash(
            Action<AvalonPromotionBrandIntro.ApplicationCanvas> NotifySplash = null
            )
        {
            c.OrphanizeContainer();

            var s = new AvalonPromotionBrandIntro.ApplicationCanvas();
            s.Background = Brushes.Transparent;

            s.SizeTo(this.Width, this.Height);
            s.AttachTo(this);


            this.SizeChanged +=
               delegate
               {
                   s.SizeTo(this.Width, this.Height);
               };

            if (NotifySplash != null)
                NotifySplash(s);

            s.AnimationAllBlack +=
                delegate
                {
                    CreateLogoAnimation();
                };

            s.AnimationCompleted +=
                delegate
                {
                    s.Orphanize();
                };

            s.PrepareAnimation()();
        }
    }

}
