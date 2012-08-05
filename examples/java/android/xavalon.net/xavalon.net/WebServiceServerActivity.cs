using System;
using System.Collections.Generic;
using System.Linq;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using java.lang;
using android.content;
using java.net;
using java.io;
using java.util;
using android.webkit;
using android.util;

namespace xavalon.net
{
    public abstract class WebServiceServerActivity : Activity
    {
        // see also: y:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\com\lacas\testsocket\TestSocketActivity.java

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;
        Inet6Address __hack;


        public WebView webview;
        public ProgressDialog progressBar;
        public AlertDialog alertDialog;

        public override bool onKeyDown(int keyCode, KeyEvent e)
        {
            // http://android-coding.blogspot.com/2011/08/handle-back-button-in-webview-to-back.html

            if (keyCode == KeyEvent.KEYCODE_BACK)
            {
                if (webview.canGoBack())
                {
                    webview.goBack();
                    return true;
                }
            }
            return base.onKeyDown(keyCode, e);
        }

        public string uri;
        public int height;
        public int width;


        public string ApplicationFile = "Application.htm";

//        [Script(OptimizedCode = @"
//            
////            View v = findViewById(android.R.id.view_id);
////                v.setSystemUiVisibility(View.STATUS_BAR_HIDDEN);
////            
////         getWindow().getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_LOW_PROFILE);
////         getWindow().getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_HIDE_NAVIGATION);
//         getWindow().setSystemUiVisibility(View.SYSTEM_UI_FLAG_HIDE_NAVIGATION);
//
//            ")]
//        public static void hidestatusbar()
//        {
//        }



        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            height = getWindowManager().getDefaultDisplay().getHeight();
            width = getWindowManager().getDefaultDisplay().getWidth();

            if (width > height)
            {
                //getWindow().requestFeature(Window.FEATURE_ACTION_BAR_OVERLAY);
                getWindow().requestFeature(0x00000009);
                //hidestatusbar();

                // http://stackoverflow.com/questions/8469112/hide-ics-back-home-task-switcher-buttons
                // http://developer.android.com/reference/android/view/View.OnSystemUiVisibilityChangeListener.html
                // http://stackoverflow.com/questions/9131790/android-lights-out-mode-not-working
                // http://baroqueworksdev.blogspot.com/2012/02/request-that-visibility-of.html
            }

            this.ToFullscreen();

    

            //actionBar = getActionBar();
            //actionBar.setBackgroundDrawable(null);

            //}

            var r = new System.Random();
            var port = r.Next(1024, 32000);


            uri = "http://" + getLocalIpAddress();

            uri += ":";
            uri += ((object)(port)).ToString();

            this.setTitle((java.lang.CharSequence)(object)uri);

            serverThread = new ServerThread { mycontext = this, port = port };

            new Thread(serverThread).start();



            // http://stackoverflow.com/questions/8955228/webview-with-an-iframe-android
            // http://www.chrisdanielson.com/tag/webviewclient/

            this.alertDialog = new AlertDialog.Builder(this).create();

            this.progressBar = ProgressDialog.show(this, (CharSequence)(object)"look here!", (CharSequence)(object)"Loading...");


            this.webview = new WebView(this);


            setContentView(webview);



            webview.getSettings().setSupportZoom(true);
            webview.getSettings().setLoadsImagesAutomatically(true);
            webview.getSettings().setJavaScriptEnabled(true);
            webview.getSettings().setBuiltInZoomControls(true);
            //webview.setInitialScale(1);

            webview.setWebViewClient(new MyWebViewClient { __this = this });
            webview.getSettings().setSupportZoom(false);
            webview.setScrollBarStyle(WebView.SCROLLBARS_INSIDE_OVERLAY);

            //webview.getSettings().setJavaScriptEnabled(true);

            // no flash in emulator?
            // works on my phone!
            // no Flash since android 4.1.0!!!
            //webview.getSettings().setPluginsEnabled(true);
            //webview.getSettings().setPluginState(android.webkit.WebSettings.PluginState.ON);



            // OR, you can also load from an HTML string:
            //var summary = "<html><body>You scored <b>192</b> points.</body></html>";
            //webview.loadData(summary, "text/html", null);
            //Log.i(TAG, "loadUrl");
            webview.loadUrl(uri);

            //this.setContentView(sv);





        }

        public override void onCreateContextMenu(ContextMenu ContextMenu0, View View1, ContextMenu_ContextMenuInfo ContextMenu_ContextMenuInfo2)
        {
            //var menu = ContextMenu0.add((CharSequence)(object)"jsc");
        }


        class MyWebViewClient : WebViewClient
        {
            public WebServiceServerActivity __this;

            public override bool shouldOverrideUrlLoading(WebView view, string url)
            {
                Func<string, string, bool> Contains = (item, ex) =>
                {
                    var xx = !(((java.lang.String)(object)item).indexOf(ex, 0) < 0);

                    return xx;
                };


                if (Contains(url, ".apk"))
                    return false;

                //Log.i(TAG, "Processing webview url click...");
                view.loadUrl(url);
                return true;
            }

            public override void onPageFinished(WebView view, string url)
            {
                //Log.i(TAG, "Finished loading URL: " + url);
                if (__this.progressBar.isShowing())
                {
                    __this.progressBar.dismiss();
                }
            }

            public override void onReceivedError(WebView view, int errorCode, string description, string failingUrl)
            {
                //Log.e(TAG, "Error: " + description);

                //__this.ShowToast("Oh no! " + description);

                //Toast.makeText(__this, "Oh no! " + description, Toast.LENGTH_SHORT).show();
                //__this.alertDialog.setTitle((CharSequence)(object)"Error");
                //__this.alertDialog.setMessage(description);
                //__this.alertDialog.setButton((CharSequence)(object)"OK", new DialogInterface.OnClickListener() {
                //    public void onClick(DialogInterface dialog, int which) {
                //        return;
                //    }
                //});
                //__this.alertDialog.show();
            }
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

                        Log.wtf("getLocalIpAddress", inetAddress.getHostAddress().ToString());

                        var v6 = inetAddress is Inet6Address;

                        if (v6)
                        {
                        }
                        else if (!inetAddress.isLoopbackAddress())
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

            if (value == "")
            {
                // no wifi
                value = "127.0.0.1";
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

        public class XThread : Runnable
        {
            public readonly Thread Thread;

            public XThread()
            {
                this.Thread = new Thread(this);
            }

            public Action Handler;

            public void run()
            {
                Handler();
            }
        }

        public class ServerThread : Runnable
        {
            public int port = 1111;

            public WebServiceServerActivity mycontext;

            public bool isRunning = true;

            public ServerSocket serversocket;
            public Socket clientsocket;


            public void run()
            {
                try
                {

                    serversocket = new ServerSocket(port);
                    serversocket.setReuseAddress(true);

                    while (isRunning)
                    {

                        clientsocket = serversocket.accept();

                        AtConnection();

                    }
                }
                catch
                {

                }
            }

            public void AtConnection()
            {
                Func<string, string, bool> Contains = (item, ex) =>
                {
                    var xx = !(((java.lang.String)(object)item).indexOf(ex, 0) < 0);

                    return xx;
                };


                BufferedReader input = null;
                OutputStream output = null;

                try
                {
                    input = new BufferedReader(new InputStreamReader(clientsocket.getInputStream(), "ISO-8859-2"));
                    output = clientsocket.getOutputStream();
                }
                catch
                {
                }

                #region send_text
                Action<string> send_text = (s) =>
                {
                    string header =
                            "HTTP/1.1 200 OK\n" +
                            "Connection: close\n" +
                            "Content-type: text/html; charset=utf-8\n";
                    //"Content-Length: " + s.Length + "\n" +
                    header += "\n";

                    var w = new OutputStreamWriter(output);

                    try
                    {
                        w.write(header + s);
                        w.flush();
                    }
                    catch
                    {

                    }


                };
                #endregion

                #region send_stream
                Action<InputStream, string> send_stream = (fis, contenttype) =>
                {
                    string header =
                         "HTTP/1.1 200 OK\n";

                    header += "Expires: Sun, 17 Jan 2038 19:14:07 GMT\n";

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
                };
                #endregion


                Action Handler =
                    delegate
                    {

                        try
                        {


                            string sAll = getStringFromInput(input);

                            var i0 = sAll.IndexOf(" ", 0);
                            var i1 = sAll.IndexOf(" ", i0 + 1);


                            var path = ((java.lang.String)(object)sAll).substring(i0, i1);


                            if (path.Length > 0)
                                path = ((java.lang.String)(object)path).substring(2, path.Length);

                            if (path.Length > 1)
                            {
                                var last = ((java.lang.String)(object)path).substring(path.Length - 1, path.Length);

                                if (last == "/")
                                {
                                    path = ((java.lang.String)(object)path).substring(0, path.Length - 1);

                                }
                            }

                            if (mycontext.width > mycontext.height)
                                if (path == "")
                                    path = mycontext.ApplicationFile;

                            Log.i("jsc get", path);

                            var asset = openFileFromAssets(path, mycontext);



                            if (asset != null)
                            {
                                if (Contains(path, ".gif"))
                                    send_stream(asset, "image/gif");
                                if (Contains(path, ".htm"))
                                    send_stream(asset, "text/html");
                                else
                                    send_stream(asset, "application/octet-stream");
                            }
                            else
                            {
                                string firstpage = "<body style='padding: 0; margin: 0;'>";

                                #region AttachQRToElement
                                Action<string, string> AttachQRToElement =
                                    (itemuri, Container) =>
                                    {
                                        firstpage += "<script>";
                                        firstpage += "\n";
                                        firstpage += "var i = document.getElementById('";
                                        firstpage += Container;
                                        firstpage += "').appendChild( qr.image(";
                                        firstpage += "\n";
                                        firstpage += "{value:'";



                                        firstpage += itemuri;
                                        firstpage += "'}";
                                        firstpage += "\n";
                                        firstpage += "));";
                                        firstpage += "\n";
                                        firstpage += "</script>";

                                    };
                                #endregion


                                #region firstpage

                                firstpage += "<script src='/qr.js'></script>";
                                firstpage += "<center>";

                                firstpage += "<div style='background-color: black; color: white; padding: 2em;'>";
                                firstpage += "&laquo; Rotate your device to left to <b>launch</b>";
                                firstpage += "</div>";

                                firstpage += "<h1>";
                                firstpage += path;
                                firstpage += "</h1>";


                                firstpage += "<div  id='newdevice'>";


                                firstpage += "</div>";
                                AttachQRToElement(mycontext.uri, "newdevice");




                                firstpage += "<br />";

                                firstpage += "<div>";
                                firstpage += "Connect any other device on the same network to";
                                firstpage += "</div>";

                                firstpage += "\n";
                                firstpage += "<div>";
                                firstpage += "<code>";
                                firstpage += mycontext.uri;
                                firstpage += "</code>";
                                firstpage += "</div>";
                                firstpage += "\n";


                                firstpage += "<div style='padding: 1em; margin: 0; overflow: hidden;'>";

                                //ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden




                                var assets = mycontext.getResources().getAssets();

                                var collection = assets.list(path);

                                var index = 0;

                                #region AtItem
                                Action<string> AtItem =
                                    item =>
                                    {
                                        index++;

                                        if (!Contains(item, "."))
                                        {
                                            item += "/";
                                        }

                                        firstpage += "<div style='border-top: 0.3em solid black; padding: 1em; '>";


                                        firstpage += "<a";
                                        firstpage += " href='";
                                        firstpage += item;
                                        firstpage += "' id='item";
                                        firstpage += ((object)index).ToString();
                                        firstpage += "'";
                                        firstpage += ">";

                                        var path_preview = "assets.preview/";

                                        path_preview += item;
                                        path_preview += ".png";

                                        var asset_preview = openFileFromAssets(path_preview, mycontext);
                                        if (asset_preview != null)
                                        {
                                            firstpage += "<div>";
                                            firstpage += "<img  src='";
                                            firstpage += path_preview;
                                            firstpage += "' />";
                                            firstpage += "</div>";
                                        }

                                        firstpage += "<h4>";
                                        firstpage += item;
                                        firstpage += "</h4>";
                                        firstpage += "\n";

                                        #region WithImage
                                        var WithImage = Contains(item, ".gif");

                                        WithImage |= Contains(item, ".png");

                                        if (WithImage)
                                        {
                                            firstpage += "<div>";

                                            firstpage += "<img src='";
                                            firstpage += item;
                                            firstpage += "' />";
                                            firstpage += "</div>";

                                        }
                                        #endregion


                                   
                                        firstpage += "</a>";

                                        firstpage += "</div>";

                                        #region WithQR
                                        var WithQR = Contains(item, ".htm");

                                        WithQR |= Contains(item, ".apk");





                                        if (WithQR)
                                        {
                                            var ContainerID = "item";

                                            ContainerID += ((object)index).ToString();

                                            var itemuri = mycontext.uri + "/";
                                            itemuri += item;

                                            AttachQRToElement(itemuri, ContainerID);
                                        }
                                        #endregion

                                        firstpage += "\n";


                                    };
                                #endregion

                                foreach (var xitem in collection)
                                {
                                    AtItem(xitem);
                                }

                                firstpage += "</center>";


                                firstpage += "<pre>";
                                firstpage += sAll;
                                firstpage += "</pre>";

                                firstpage += "</div>";
                                firstpage += "</body>";

                                send_text(firstpage);
                                #endregion

                            }

                            input.close();
                            output.close();
                        }
                        catch
                        {

                        }
                    };

                var x = new XThread
                {
                    Handler = Handler
                };

                x.Thread.start();
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


    [Script(Implements = typeof(global::System.Random))]
    internal class __Random
    {
        public virtual int Next()
        {
            return Next(0, int.MaxValue);
        }

        public virtual int Next(int min, int max)
        {
            var len = max - min;
            var r = global::java.lang.Math.floor(java.lang.Math.random() * len);

            int ri = (int)r;
            return ri + min;
        }

        public virtual double NextDouble()
        {
            return java.lang.Math.random();
        }


    }

}
