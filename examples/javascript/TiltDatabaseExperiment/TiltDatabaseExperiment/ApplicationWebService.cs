using jsc.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using TiltDatabaseExperiment.Controls;

namespace TiltDatabaseExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void AtFrame(
            string id,
            string frame,
            string dx,
            string dy
            //, Action<string> y
            )
        {
#if DEBUG

            ApplicationWebServiceClient.Clients[id].Text = new { frame, dx, dy }.ToString();
#endif

            // Send it back to the caller.
        }

    }

#if DEBUG
    partial class ApplicationWebServiceClient
    {

        public static VirtualDictionary<string, ApplicationWebServiceClient> Clients =
            VirtualDictionaryBase.Create<string, ApplicationWebServiceClient>(k => new ApplicationWebServiceClient(k));

        public Form Form;

        public string Text
        {
            set
            {
                serverui.Invoke(
                    new Action(
                    delegate
                    {
                        this.Form.Text = new { id, value }.ToString();
                    }
                    )
                );
            }
        }

        public string id;

        public ApplicationWebServiceClient(string id)
        {
            this.id = id;

            ApplicationWebServiceClient.Initialize();

            //Cross-thread operation not valid: Control '' accessed from a thread other than the thread it was created on.

            serverui.Invoke(
                new Action(
                delegate
                {
                    this.Form = new ClientForm
                    {
                        Text = id,
                        Owner = serverui,
                    };

                    this.Form.Show();
                }
                )
            );
        }

        static Thread ui;
        static Form serverui;

        public static object InitializeSync = new object();
        public static void Initialize()
        {
            lock (InitializeSync)
            {
                if (serverui != null)
                    return;

                var w = new AutoResetEvent(false);

                ui = new Thread(
                   delegate()
                   {
                       serverui = new ServerForm { Text = "I am the Server" };
                       serverui.Show();

                       serverui.FormClosing +=
                           delegate
                           {
                               SystemSounds.Beep.Play();
                               Environment.Exit(0);
                           };

                       w.Set();

                       System.Windows.Forms.Application.Run(serverui);
                   }
               );

                ui.SetApartmentState(ApartmentState.STA);
                ui.Start();

                w.WaitOne();
            }
        }
    }
#endif
}
