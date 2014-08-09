using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace FlashHeatZeeker.Lobby
{
    public class ApplicationCanvas : Canvas
    {
        // "X:\jsc.svn\examples\javascript\WebGL\HeatZeekerRTSOrto\HeatZeekerRTSOrto.sln"
        // http://stackoverflow.com/questions/1420253/viewing-collada-with-wpf

        // what about Collada, webgl, 3d?

        public Image i, ii;



        public Image

         enter
         ;

        public AnimatedOpacity<Image> entero;

        public int fingersize = 96;

        public Canvas Container720;
        //public AnimatedOpacity<Canvas> Container720A;

        public AnimatedOpacity<Image> iA;


        public ApplicationCanvas()
        {

            var Container720X = new Canvas().AttachTo(this);

            Container720 = new Canvas().AttachTo(Container720X);
            //Container720A = Container720.ToAnimatedOpacity();

            i = new Avalon.Images.Promotion3D_controller_720p();
            i.AttachTo(Container720);
            iA = i.ToAnimatedOpacity();
            iA.Opacity = 0.8;

            var ShadowOverlay = new Rectangle { Fill = Brushes.Black };
            var ShadowOverlayA = ShadowOverlay.ToAnimatedOpacity();


            ShadowOverlay.AttachTo(Container720);
            ShadowOverlay.SizeTo(1280, 720);

            ii = new Avalon.Images.Promotion3D_controller_android_720();
            ii.AttachTo(Container720);
            var iiA = ii.ToAnimatedOpacity();


            var Title = new Avalon.Images.Title();
            Title.AttachTo(Container720X);





            this.SizeChanged += (s, e) =>
            {
                Container720X.MoveTo(
                        (this.Width - 1280) / 2,
                    (this.Height - 720) / 2
                );


                Console.WriteLine(new { this.Width, this.Height });


            };

            enter = new Avalon.Images.start
            {
                Cursor = Cursors.Hand
            }.AttachTo(this);

            entero = enter.ToAnimatedOpacity();


            this.SizeChanged += (s, e) => enter.MoveTo(
                (this.Width - 128) * 0.8 + 64,
                (this.Height - 128) / 2 + 32);

            #region Black
            {
                Rectangle r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);
                r.MoveTo(0, 0);

                this.SizeChanged += (s, e) =>
                {
                    r.Width = this.Width;
                    r.Height = ((this.Height - 720) / 2).Max(0);
                };
            }

            {
                Rectangle r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);
                r.MoveTo(0, 0);

                this.SizeChanged += (s, e) =>
                {
                    r.Width = this.Width;

                    //                NotImplementedException
                    //at ScriptCoreLib.ActionScript.BCLImplementation.System.Windows::__UIElement/InternalGetHeight_100663344()
                    //at ScriptCoreLib.ActionScript.BCLImplementation.System.Windows::__UIElement/VirtualGetHeight_100663342()
                    //at ScriptCoreLib.ActionScript.BCLImplementation.System.Windows::__FrameworkElement/get Height()

                    var Height = ((this.Height - 720) / 2).Max(0);

                    r.Height = Height;
                    r.MoveTo(0, this.Height - Height);
                };
            }
            #endregion

            var hotzone = new Rectangle { Fill = Brushes.Green, Opacity = 0 }.AttachTo(Container720X);

            hotzone.Cursor = Cursors.Hand;

            hotzone.MoveTo(1280 / 3, 720 * 2 / 3);
            hotzone.SizeTo(1280 / 3, 720 / 3);


            hotzone.MouseEnter +=
                delegate
                {
                    ShadowOverlayA.Opacity = 0.4;
                    iiA.Opacity = 1.0;
                    entero.Opacity = 0.2;

                    //Container720A.Opacity = 1;
                };


            hotzone.MouseLeave +=
                delegate
                {
                    ShadowOverlayA.Opacity = 0;
                    iiA.Opacity = 0;
                    entero.Opacity = 0.8;

                    //Container720A.Opacity = VideoPlayingOpacity;
                };


            hotzone.MouseLeftButtonUp +=
                delegate
                {
                    new Uri("http://young-beach-4377.herokuapp.com/android").NavigateTo();
                };

            #region enter
            enter.MouseEnter +=
                delegate
                {
                    entero.Opacity = 1;
                    ShadowOverlayA.Opacity = 0.4;

                    //Container720A.Opacity = 1;
                };


            enter.MouseLeave +=
             delegate
             {
                 entero.Opacity = 0.8;
                 ShadowOverlayA.Opacity = 0;

                 // got video?
                 //Container720A.Opacity = VideoPlayingOpacity;
                 //Container720A.Opacity = 1.0;
             };
            #endregion



            ShadowOverlayA.Opacity = 0;
            iiA.Opacity = 0;
            entero.Opacity = 0.8;

        }

        public double VideoPlayingOpacity = 1.0;
    }
}
