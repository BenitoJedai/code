using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace FormsAvalonAnimation
{
    public class AnimationControl : UserControl
    {
        public AnimationControl()
        {
            var c = new Canvas();

            this.Content = c;

            //var w = new WebBrowser();

            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/61a901d3-3273-4d8e-8e08-9441dc11010f/wpf-webbrowser-in-a-transparent-window
            // http://blogs.msdn.com/b/dwayneneed/archive/2013/02/26/mitigating-airspace-issues-in-wpf-applications.aspx
            //// http://www.jonathanantoine.com/2011/09/24/wpf-4-5-%E2%80%93-part-8-no-more-airspace-problems-integrating-wpf-with-win32/
            //w.CompositionMode = System.Windows.Interop.CompositionMode.Full;
            //w.IsRedirected = true;

            //w.AttachTo(c);
            //w.Navigate("http://example.com");
            //w.SizeTo(400, 200);

            var cc = new AnimationCanvas();
            cc.AttachTo(c);

        }
    }
}
