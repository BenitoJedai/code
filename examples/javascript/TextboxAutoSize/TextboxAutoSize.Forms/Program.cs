// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using TextboxAutoSize.Forms.Design;
using System.Windows.Forms;

namespace TextboxAutoSize.Forms
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            var f = new Form();

            f.AutoSize = true;

            f.Controls.Add(new UserControl1());

            f.ShowDialog();
#else
            // Prepare the yield value for
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
