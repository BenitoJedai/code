using System;
using System.Collections.Generic;
using System.Linq;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using java.lang;
using android.content;
using java.net;
using java.io;
using java.util;

namespace AndroidAssetsServerActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // see also: y:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\com\lacas\testsocket\TestSocketActivity.java

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);



            var b2 = new Button(this);
            var uri = "http://" + getLocalIpAddress();

            uri += ":1111";

            b2.setText((java.lang.CharSequence)(object)uri);
            ll.addView(b2);

            this.setContentView(sv);



            serverThread = new ServerThread { mycontext = this };

            new Thread(serverThread).start();
        }

        public string getLocalIpAddress()
        {
            var value = "";

            try
            {
                for (Enumeration en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); )
                {
                    NetworkInterface intf = (NetworkInterface)en.nextElement();
                    for (Enumeration enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements(); )
                    {
                        InetAddress inetAddress = (InetAddress)enumIpAddr.nextElement();
                        if (!inetAddress.isLoopbackAddress())
                        {
                            if (value == "")
                                value = inetAddress.getHostAddress().ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            return value;
        }


        ServerThread serverThread;

        protected override void onDestroy()
        {
            base.onDestroy();

            if (serverThread.serversocket != null)
                serverThread.closeConnections();
        }
        public class ServerThread : Runnable
        {
            public Context mycontext;

            public bool isRunning = true;

            public ServerSocket serversocket;
            public Socket clientsocket;

            public BufferedReader input;
            public OutputStream output;

            public void run()
            {
                try
                {
                    int port = 1111;

                    serversocket = new ServerSocket(port);
                    serversocket.setReuseAddress(true);

                    while (isRunning)
                    {

                        clientsocket = serversocket.accept();
                        input = new BufferedReader(new InputStreamReader(clientsocket.getInputStream(), "ISO-8859-2"));
                        output = clientsocket.getOutputStream();

                        string sAll = getStringFromInput(input);

                        var i0 = sAll.IndexOf(" ", 0);
                        var i1 = sAll.IndexOf(" ", i0 + 1);


                        var path = ((java.lang.String)(object)sAll).substring(i0, i1);

                        if (path.Length > 0)
                            path = ((java.lang.String)(object)path).substring(2, path.Length);

                        var asset = openFileFromAssets(path, mycontext);

                        if (asset != null)
                        {
                            send(asset);
                        }
                        else
                        {
                            #region firstpage
                            string firstpage = "<body>";

                            firstpage += "<link rel=\"stylesheet\" type=\"text/css\" ";
                            firstpage += "href=\"/foo.css\" />";

                            firstpage += "<h1>";
                            firstpage += path;
                            firstpage += "</h1>";

                            firstpage += "<pre>";
                            firstpage += sAll;
                            firstpage += "</pre>";

                            firstpage += "First page! jsc! <a href='/foo.htm'>Next</a>";

                            var assets = mycontext.getResources().getAssets();

                            var collection = assets.list(path);

                            foreach (var item in collection)
                            {

                                firstpage += "<pre>";
                                firstpage += item;
                                firstpage += "</pre>";
                                firstpage += "<hr />";

                            }

                            firstpage += "</body>";

                            send(firstpage);
                            #endregion

                        }

                        input.close();
                        output.close();

                    }
                }
                catch
                {

                }
            }

            public static InputStream openFileFromAssets(string spath, Context mycontext)
            {
                InputStream value = null;
                try
                {
                    value = mycontext.getResources().getAssets().open(spath);
                }
                catch
                {
                    
                }
                return value;

            }

            void send(string s)
            {
                string header =
                        "HTTP/1.1 200 OK\n" +
                        "Connection: close\n" +
                        "Content-type: text/html; charset=utf-8\n" +
                    //"Content-Length: " + s.Length + "\n" +
                        "\n";

                var w = new OutputStreamWriter(output);

                try
                {
                    w.write(header + s);
                    w.flush();
                }
                catch
                {

                }


            }

            void send(InputStream fis, string contenttype = "application/octet-stream")
            {
                string header =
                     "HTTP/1.1 200 OK\n";

                header += "Content-type: ";
                header += contenttype;
                header += "\n";

                //header += "Content-Length: " + fis.available() + "\n" +
                header += "\n";
                try
                {
                    var w = new OutputStreamWriter(output);
                    w.write(header);
                    w.flush();

                    sbyte[] buffer = new sbyte[1024];
                    int bytes = 0;

                    bytes = fis.read(buffer);
                    while (bytes != -1)
                    {
                        output.write(buffer, 0, bytes);
                        bytes = fis.read(buffer);
                    }

                }
                catch
                {
                }
            }

            string getStringFromInput(BufferedReader input)
            {
                StringBuilder sb = new StringBuilder();
                string sTemp;

                try
                {
                    sTemp = input.readLine();

                    while (!(sTemp == ""))
                    {
                        sb.append(sTemp + "\n");
                        sTemp = input.readLine();
                    }
                }
                catch
                {
                    sb = new StringBuilder();
                }

                return sb.ToString();
            }

            public void closeConnections()
            {
                try
                {
                    serversocket.close();
                }
                catch
                {

                }
            }
        }
    }


}
