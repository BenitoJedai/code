using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.multimouse.Design;
using com.abstractatech.multimouse.HTML.Pages;
using SQLiteWithDataGridView.Library;
using ScriptCoreLib.JavaScript.Runtime;
using System.Diagnostics;

namespace com.abstractatech.multimouse
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            var layout = new global::MultiMouse.HTML.Pages.App();



            #region inehrit the static layout
            while (layout.Container.firstChild != null)
            {
                ((IHTMLElement)layout.Container.firstChild).Orphanize().AttachToDocument();
            }
            #endregion


            #region make it run
            var content = new global::MultiMouse.Application(layout);
            #endregion

            Action talkhandker =
                delegate
                {
                    Console.WriteLine("initiating communications...");

                    var mothershipcon = new ConsoleForm { Text = "mothership" };

                    mothershipcon.Show();
                    mothershipcon.WindowState = System.Windows.Forms.FormWindowState.Minimized;

                    var echosw = new Stopwatch();

                    echosw.Start();

                    var echolagcon = new ConsoleForm { Text = "echo" };
                    echolagcon.Show();
                    echolagcon.Left = mothershipcon.Right;
                    echolagcon.WindowState = System.Windows.Forms.FormWindowState.Minimized;

                    #region mothership_postXElement
                    Action<XElement> mothership_postXElement =
                        data =>
                        {
                            var now = DateTime.Now;
                            //var ticksms = (long)(now.Ticks / 10000);
                            var ticksms = (long)Math.Floor(now.Ticks / 10000.0);

                            var xml = new XElement("device",
                              new XAttribute("device_ticksms", "" + ticksms),
                              new XAttribute("device_id", "" + content.device_id),
                              data
                            );

                            //Console.WriteLine("to mothrship: " + xml);

                            // tell the server its us..
                            this.sync_Insert(xml);
                        };
                    #endregion


                    #region mothership_chat
                    Action<string> mothership_chat =
                        text =>
                        {
                            mothership_postXElement(
                                new XElement(
                                    "chat", text
                               )
                            );
                        };
                    #endregion

                    Action<int, XElement> at_mothership_echo =
                        (lag, xml) => { };


                    this.sync_SelectTransaction(
                        last_id =>
                        {
                            // either 
                            // get state from others 
                            // or everybody resets
                            // or neither

                            var echo_index = 0;

                            #region connect device with mothership
                            Action<XElement> at_mothership_message =
                                device_xml =>
                                {
                                    var device_ticksms = long.Parse(device_xml.Attribute("device_ticksms").Value);
                                    var device_id = int.Parse(device_xml.Attribute("device_id").Value);

                                    var now = DateTime.Now;
                                    var ticksms = (long)Math.Floor(now.Ticks / 10000.0);

                                    var lag = (int)(ticksms - device_ticksms);

                                    if (device_id == content.device_id)
                                    {
                                        //Console.WriteLine("lag " + TimeSpan.FromTicks(lag) + "ms");
                                        //echolagcon.WriteLine("#" + last_id + " echo lag " + lag + "ms " + device_xml.ToString().Length + "bytes");

                                        echo_index++;

                                        echolagcon.WriteLine(
                                            echosw.Elapsed +
                                            " #" + echo_index +
                                            " lag " + lag + "ms " + device_xml.ToString().Length + "bytes");

                                        device_xml.Elements().WithEach(
                                              xml =>
                                              {

                                                  at_mothership_echo(lag, xml);

                                              }
                                          );

                                        // ah, echo. we could tell the server we dont care about it..
                                        return;
                                    }



                                    mothershipcon.WriteLine("#" + last_id + " " + device_id + ":  dt " + lag + "ms " + device_xml.ToString().Length + "bytes");

                                    //Console.WriteLine("at_mothership_message: " + device_xml);

                                    device_xml.Elements().WithEach(
                                        xml =>
                                        {

                                            content.device_onmessage(device_id, xml);

                                        }
                                    );
                                };

                            content.device_bind(
                                xml =>
                                {

                                    mothership_postXElement(xml);
                                }
                            );
                            #endregion



                            #region sync_SelectContentUpdates
                            var loop_index = 0;
                            Action loop = null;

                            loop = delegate
                            {
                                loop_index++;

                                //talk.innerText = "#" + loop_index + " " + new { last_id };

                                new ScriptCoreLib.JavaScript.Runtime.Timer(
                                    delegate
                                    {
                                        this.sync_SelectContentUpdates_EventStream(
                                            last_id: last_id,
                                            yield: at_mothership_message,
                                            yield_last_id:
                                                id =>
                                                {
                                                    // in stream mode this make a while to reach here
                                                    last_id = id;

                                                    // this would cause stackoverflow, yet since we are in 
                                                    // clent-server "tail" call it aint.
                                                    loop();
                                                }
                                        );
                                    }
                                ).StartTimeout(150);
                            };

                            loop();
                            #endregion


                            mothership_chat("ping");

                            at_mothership_echo +=
                                (lag, xml) =>
                                {
                                    //Console.WriteLine("echo: " + new { xml, xml.Name.LocalName });

                                    if (xml.Name.LocalName == "chat")
                                    {
                                        // pong no earlier than every 1 sec
                                        new Timer(
                                            delegate
                                            {
                                                if (xml.Value == "ping")
                                                    mothership_chat("pong");
                                                else
                                                    mothership_chat("ping");

                                            }
                                        ).StartTimeout((500 - lag).Max(1));



                                    }
                                };

                        }
                    );
                };

            if (Native.window.opener == null)
                talkhandker();

            new IHTMLAnchor { innerText = "Next Tab", href = "/" }.AttachTo(layout.infocontent);

            Native.Document.location.href.ToDocumentTitle();
        }

    }
}
