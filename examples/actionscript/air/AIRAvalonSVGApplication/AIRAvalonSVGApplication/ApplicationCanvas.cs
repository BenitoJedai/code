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

namespace AIRAvalonSVGApplication
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



            #region jsc need to learn http://svg2xaml.codeplex.com/
            //System.NotSupportedException was unhandled
            //  HResult=-2146233067
            //  Message=anonymous_logossinglewings.svg
            //  Source=ScriptCoreLib.Avalon
            //  StackTrace:
            //       at ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToImageSource(String ext, Stream s)
            //       at ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToSource(ManifestResourceEntry fileStream)
            //       at ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToSource(String e)
            //       at SVGAnonymous.Avalon.Images.Anonymous_LogosSingleWings..ctor()
            //       at AIRAvalonSVGApplication.ApplicationCanvas..ctor() in x:\jsc.svn\examples\actionscript\air\AIRAvalonSVGApplication\AIRAvalonSVGApplication\ApplicationCanvas.cs:line 27
            //       at AIRAvalonSVGApplication.Program.<Main>b__0() in x:\jsc.svn\examples\actionscript\air\AIRAvalonSVGApplication\AIRAvalonSVGApplication\Program.cs:line 16
            //       at ScriptCoreLib.Desktop.Extensions.DesktopAvalonExtensions.<>c__DisplayClassb`1.<Launch>b__1()
            //       at ScriptCoreLib.Desktop.Extensions.DesktopAvalonExtensions.<>c__DisplayClassb`1.<Launch>b__a()
            //       at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
            //       at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
            //       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
            //       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
            //       at System.Threading.ThreadHelper.ThreadStart()
            //  InnerException: 


            //var svg = new SVGAnonymous.Avalon.Images.Anonymous_LogosSingleWings();

            //svg.AttachTo(this);
            #endregion
        }

    }
}
