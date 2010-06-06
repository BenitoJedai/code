using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace ExampleGallery
{
    public class OptionWithShadow : ISupportsContainer
    {
        public Canvas Container { get; set; }

        public Rectangle Overlay;

        public OptionWithShadow MoveTo(int x, int y)
        {
            this.Container.MoveTo(x, y);
            this.Overlay.MoveTo(x + 9, y + 9);

            return this;
        }

        public TextBox Caption { get; set; }

        public OptionWithShadow(Func<Image> i)
        {
            this.Overlay = new Rectangle
            {
                Fill = Brushes.Black,
                Width = 120,
                Height = 90 + 18 + 4 + 20,
                Opacity = 0,
                Cursor = Cursors.Hand
            };

            this.Container = new Canvas
            {
                //Background = Brushes.Red,
                Width = 166 + 9,
                Height = 90 + 18 + 4 + 20
            }.MoveTo(48, 48);

            //new Rectangle
            //{
            //    Fill = Brushes.Yellow,
            //    Width = 196,
            //    Height = 90,
            //}.AttachTo(this.Container).MoveTo(0, 0); 

            new Avalon.Images.PreviewShadow
            {
                // no stretch by default
                //Stretch = Stretch.Fill,

                Width = 166,
                Height = 90
            }.AttachTo(this.Container).MoveTo(9, 9);

            var PreviewSelection = new Avalon.Images.PreviewSelection
            {
                Width = 138,
                Height = 108,
                Visibility = Visibility.Hidden
            }.AttachTo(this.Container);

            this.Caption = new TextBox
            {
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                Text = "title",
                Width = 120 + 9 * 2,
                Height = 20,
                IsReadOnly = true,
                TextAlignment = TextAlignment.Center
            }.AttachTo(this.Container).MoveTo(0, 104);

            this.Overlay.MouseEnter +=
                delegate
                {
                    Caption.Foreground = Brushes.Blue;
                    PreviewSelection.Show();
                };

            this.Overlay.MouseLeave +=
                delegate
                {
                    Caption.Foreground = Brushes.White;
                    PreviewSelection.Hide();
                };

            this.Overlay.MouseLeftButtonUp +=
                delegate
                {
                    InitializeHint();

                    if (this.Click != null)
                        this.Click();

                };

            var ii = i();
            ii.Width = 120;
            ii.Height = 90;
            ii.MoveTo(9, 9).AttachTo(this.Container);
        }

        public Canvas Target;

        public void InitializeHint()
        {
            if (this.Target == null)
                if (this.Initialize != null)
                    this.Initialize();
        }

        public event Action Initialize;

        public event Action Click;
    }

}
