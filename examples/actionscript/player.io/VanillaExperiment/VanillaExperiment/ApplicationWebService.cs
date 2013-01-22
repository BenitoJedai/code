using PlayerIO.GameLibrary;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace VanillaExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        BasePlayer ref0;
        MyGame.GameCode ref1;

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        static AtHandler AtHandler;

        public void Hander(WebServiceHandler h)
        {
            if (AtHandler == null)
            {
                AtHandler = new AtHandler();
            }
        }
    }

    class AtHandler
    {

        public AtHandler()
        {
            new Thread(
                delegate()
                {
                    // (Uncomment line to start server and make it simulate the user 'bob' connecting for 30 seconds)
                    // (this is an easy way to debug serverside code)
                    //
                    // PlayerIO.DevelopmentServer.Server.StartWithDebugging("<Enter your gameid here>", "public", "MyCode", "bob", "", 30000);

                    // Start the server and wait for incomming connection


                    //  	Player.IO Development Server.exe!PlayerIO.ServerCore.PlayerIOClient.PlayerIOChannel.ProtobufHttp.startDnsQuery.AnonymousMethod__152(System.IAsyncResult result) + 0x6f bytes	
                }
            ).Start();

        }
    }
}

