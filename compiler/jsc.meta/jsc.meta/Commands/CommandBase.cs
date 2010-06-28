using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using jsc;

namespace jsc.meta.Commands
{
	public abstract class CommandBase
	{
		// Any field in this class will act as a commandline parameter

		public abstract void Invoke();


		public static implicit operator Action(CommandBase e)
		{
			return e.Invoke;
		}


        public Action AsNotification(Action<Action<string>> SetWithText)
        {
            var signal = new EventWaitHandle(false, EventResetMode.AutoReset);

            Action close = delegate { };

            var t = new Thread(
                 delegate()
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);


                     using (var n = new NotifyIcon())
                     {

                         //n.Icon = new Icon(
                         n.Icon = new Icon(typeof(CommandBase).Assembly.GetManifestResourceStream("jsc.meta.Documents.jsc.ico"));
                         n.Visible = true;
                         n.ContextMenuStrip = new ContextMenuStrip
                         {

                         };

                         n.Click +=
                             delegate
                             {
                                 n.ShowBalloonTip(500);
                             };

                         var s = new Queue<string>();

                         Action<string> WithText =
                              Text =>
                              {
                                  s.Enqueue(Text);

                                  if (s.Count > 5)
                                      s.Dequeue();

                                  n.BalloonTipText = string.Join(Environment.NewLine, s.ToArray());
                                  n.ShowBalloonTip(1000);
                              };


                         WithText.With(SetWithText);

                         close = n.Dispose;

                         n.Text = "jsc";
                         n.BalloonTipTitle = "jsc";

                         signal.Set();
                         Application.Run();

                     }

                 }
              )
            {
                ApartmentState = ApartmentState.STA,
                IsBackground = true
            };

            t.Start();

            signal.WaitOne();

            return close;
        }


        int __WriteDiagnostics_i;

        [Obsolete]
        public void WriteDiagnostics(string e)
        {
            __WriteDiagnostics_i++;

            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("j" + __WriteDiagnostics_i + ": " + e);
            Console.ForegroundColor = c;
        }
	}
}
