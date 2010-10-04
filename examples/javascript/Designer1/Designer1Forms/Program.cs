using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;

namespace Designer1Forms
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            InternalMain(() => new ApplicationControl());
#else
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

        private static void InternalMain(Func<UserControl> CreateApplicationControl)
        {
            var t = new Thread(
                delegate()
                {
                    global::System.Windows.Forms.Application.EnableVisualStyles();

                    var f = new Form();

                    var c = CreateApplicationControl();

                    f.ClientSize = new System.Drawing.Size(c.Width, c.Height);

                    f.ClientSizeChanged +=
                        delegate
                        {
                            c.Width = f.ClientSize.Width;
                            c.Height = f.ClientSize.Height;
                        };

                    f.Controls.Add(c);

                    f.ShowDialog();
                }
            )
            {
                ApartmentState = ApartmentState.STA
            };

            t.Start();
            t.Join();
        }
    }
}
