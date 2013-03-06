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

        public Image i;



        public Image

         enter
         ;

        public int fingersize = 96;


        public ApplicationCanvas()
        {

            i = new Avalon.Images.Promotion3D_controller_720p();

            i.AttachTo(this);

            this.SizeChanged += (s, e) =>
            {
                i.MoveTo(
                        (this.Width - 1280) / 2,
                    (this.Height - 720) / 2
                );

            };

            enter = new Avalon.Images.start
            {
                Cursor = Cursors.Hand
            }.AttachTo(this);

            this.SizeChanged += (s, e) => enter.MoveTo(
                (this.Width - 128) * 0.6 + 64,
                (this.Height - 128) / 2);


            {
                Rectangle r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);
                r.MoveTo(0, 0);

                this.SizeChanged += (s, e) =>
                {
                    r.Width = this.Width;
                    r.Height = ((this.Height - 680) / 2).Max(0);
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

                    var Height = ((this.Height - 640) / 2).Max(0);

                    r.Height = Height;
                    r.MoveTo(0, this.Height - Height);
                };
            }

        }

    }
}
