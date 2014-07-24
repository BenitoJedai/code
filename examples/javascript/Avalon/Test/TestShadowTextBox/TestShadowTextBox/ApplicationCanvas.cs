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

namespace TestShadowTextBox
{
    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {
            // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs

            // could we use Foo.xaml?
            // see also http://caniuse.com/shadowdom
            // http://jonrimmer.github.io/are-we-componentized-yet/
            // https://bugzilla.mozilla.org/show_bug.cgi?id=811542

            // would we be able to use shadow dom to be able to activate file:// code?


            var t = new TextBox { Width = 200, Text = "hello" };
            Canvas.SetTop(t, 30);

            t.AttachTo(this);


            var bu = new Button { Content = "multiline" }.AttachTo(this);

            bu.Click +=
                delegate
            {
                t.AcceptsReturn = true;
                t.Height = 200;

            };
        }

    }
}
