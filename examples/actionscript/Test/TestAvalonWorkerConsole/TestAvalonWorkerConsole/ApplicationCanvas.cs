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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TestAvalonWorkerConsole
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Opacity = 0.5;

            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });


            this.MouseLeftButtonUp += async delegate
            {
                // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs

                var sw = Stopwatch.StartNew();

                Console.WriteLine("enter click " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });

                // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
                // Progress
                // Task of Click?
                // Dispatcher

                var x = await Task.Run(
                    async delegate
                {
                    Console.WriteLine("threaded click " + new { Thread.CurrentThread.ManagedThreadId });

                    // if we were to run physic in worker,
                    // how would we update gpu?

                    await Task.Delay(500);


                    return "done";
                }
                );

                Console.WriteLine("exit click " + new { x, Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            };

        }

    }
}
