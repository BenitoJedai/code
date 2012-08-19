using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using java.util;
using java.net;
using System.Net;

namespace APKWebServer.Activities
{
    /*

     * W/Smack/Packet(  639): notify conn break (IOEx), close connection
D/ConnectivityService(  351): handleInetConditionHoldEnd: net=1, condition=0, published condition=100
F/APKWebServer( 2045): #3
D/dalvikvm( 2045): GC_CONCURRENT freed 434K, 11% free 7368K/8199K, paused 13ms+4ms, total 43ms
I/GTalkService/c(  639): [AndroidEndpoint@1096709400] connect: acct=1000000, state=CONNECTING
I/GTalkService/c(  639): [GTalkConnection@1096900640] connect: acct=2, state=CONNECTING
I/GTalkService/c(  639): [GTalkConnection@1096621520] connect: acct=1, state=CONNECTING
E/GTalkService(  639): connectionClosed: no XMPPConnection - That's strange!
E/GTalkService(  639): connectionClosed: no XMPPConnection - That's strange!
E/GTalkService(  639): connectionClosed: no XMPPConnection - That's strange!
D/ConnectivityService(  351): handleInetConditionHoldEnd: net=1, condition=0, published condition=0
     * 
     
     */
    public class ApplicationActivity : Activity
    {
        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );

            //var b2 = new Button(this);
            //b2.setText("The other button!");
            //ll.addView(b2);

            var ipa = Dns.GetHostAddresses(getLocalIpAddress())[0];

            var port = 8080;

            b.WithText(ipa + ":" + port);

            ClassLibrary1.Class1Shared.CreateServer(
                ipa,
                port,
                x =>
                {
                    //b.WithText(x);
                    android.util.Log.wtf("APKWebServer", x);
                }
            ).Start();

            this.setContentView(sv);
        }

        public static string getLocalIpAddress()
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

                        //Log.wtf("getLocalIpAddress", inetAddress.getHostAddress().ToString());

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

    }


}
