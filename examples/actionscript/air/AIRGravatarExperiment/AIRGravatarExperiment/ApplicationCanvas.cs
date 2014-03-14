using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AIRGravatarExperiment
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
          

            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            var email = new TextBox { Text = "arvo.sulakatko@jsc-solutions.net" }.AttachTo(this);

            Action update = delegate
            {
                //0001 02000006 AIRGravatarExperiment.ApplicationSprite::AIRGravatarExperiment.ApplicationCanvas+<>c__DisplayClass7+<>c__DisplayClass9
                //script: error JSC1000: ActionScript : unable to emit newobj at 'MD5.MD5.set_ValueAsByte'#0016: Unable to transform overloaded constructors to a single constructor via optional parameters for MD5.MD5ChangingEventArgs

                //0001 02000009 AIRGravatarExperiment.ApplicationSprite::$ArrayType$256
                //script: error JSC1000: ActionScript :
                // BCL needs another method, please define it.
                // Cannot call type without script attribute :
                // System.UInt32 for System.String ToString(System.String) used at
                // MD5.Digest.ToString at offset 0014.

                var a = new MD5.MD5Type();

                //a.FingerPrint
                a.ValueAsByte = Encoding.UTF8.GetBytes(email.Text.ToLower());


                //var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();
                var hash = a.FingerPrint.ToLower();


                var src = ("http://www.gravatar.com/avatar/" + hash);
                var href = ("http://www.gravatar.com/" + hash);


                //srcu.tos

                var src_uri = new BitmapImage();

                src_uri.BeginInit();
                src_uri.UriSource = new Uri(src);
                src_uri.EndInit();

                // X:\jsc.svn\examples\javascript\Avalon\AvalonInteractiveSketchupWarehouse\AvalonInteractiveSketchupWarehouse\InteractiveSketchupWarehouseCanvas.cs

                src_uri.DownloadCompleted +=
                    delegate
                    {
                        new Image { Source = src_uri }.AttachTo(this);

                        //src_uri.toi
                        //src_uri.
                    };

            };
            email.TextChanged +=
                delegate
                {
                    update();
                };

            update();

        }

    }
}
