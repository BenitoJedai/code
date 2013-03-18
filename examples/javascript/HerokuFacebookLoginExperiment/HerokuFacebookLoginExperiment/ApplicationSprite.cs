using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HerokuFacebookLoginExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 1;
        public const int DefaultHeight = 1;

        LocalConnection tx = new LocalConnection();
        LocalConnection rx;

        public event Action<string> yield;

        public void Invoke(string connectionName, string data = "", string methodName = "Invoke")
        {
            // wait for ack!

            #region rx
            if (rx == null)
            {
                rx = new LocalConnection();


                var client = new DynamicContainer { Subject = new object() };

                Action<string> Invoke = ack_data =>
                {
                    // http://livedocs.adobe.com/flex/3/html/help.html?content=17_Networking_and_communications_4.html

                    if (yield != null)
                        yield(ack_data);

                    //content.r.Fill = Brushes.Green;
                };

                // http://stackoverflow.com/questions/6834455/as3-localconnection-errors
                client["Invoke"] = Invoke.ToFunction();
                rx.allowDomain("*");
                rx.allowInsecureDomain("*");
                rx.status += delegate
                { };

                rx.client = client.Subject;

                // ArgumentError: Error #2082: Connect failed because the object is already connected.
                // Error #2044: Unhandled StatusEvent:. level=error, code=

                // If the string for connectionName begins with an underscore (for example, _connectionName), Flash Player 
                // does not add a prefix to the string. This means the receiving and sending LocalConnection objects will 
                // use identical strings for connectionName. If the receiving object uses LocalConnection.allowDomain() to specify that connections from any domain will be accepted, you can move the SWF file with the receiving LocalConnection object to another domain without altering any sending LocalConnection objects.
                rx.connect(connectionName + "_ack");

                //content.MouseLeftButtonUp +=
                //    delegate
                //    {
            }
            #endregion


            #region tx
            tx.status +=
                delegate
                {

                };
            tx.allowDomain("*");
            tx.allowInsecureDomain("*");
            tx.send(connectionName, methodName, data);
            #endregion

        }
    }


}
