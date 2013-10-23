using android.content;
using android.net.wifi;
using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidBroadcastLogger
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        //public MyDataSource DataSource;

        // sending field as cookie - cuts of at ; inside xml escapes..

        public Task<MyDataSource> DataSource_poll(MyDataSource DataSource)
        {
            // first timer null
            if (DataSource == null)
                DataSource = new MyDataSource();

            DataSource.poll();

            // Set-Cookie:InternalFields=field_DataSource=<_02000006>%0d%0a  <_0400000b>1</_0400000b>%0d%0a  <_0400000c>&lt;DataTable TableName=""&gt;%0d%0a  &lt;Columns&gt;%0d%0a    &lt;DataColumn ReadOnly="true"&gt;xml&lt;/DataColumn&gt;%0d%0a  &lt;/Columns&gt;%0d%0a  &lt;DataRow&gt;%0d%0a    &lt;DataColumn&gt;&amp;lt;fake&amp;gt;data { last_id = 0, Count = 0 }&amp;lt;/fake&amp;gt;&lt;/DataColumn&gt;%0d%0a  &lt;/DataRow&gt;%0d%0a&lt;/DataTable&gt;</_0400000c>%0d%0a  <_0400000d>1000</_0400000d>%0d%0a  <_0400000e>10</_0400000e>%0d%0a  <_0400000f>30</_0400000f>%0d%0a</_02000006>; 
            // Cookie GetValues { value = field_DataSource=<_02000006>%0d%0a  <_0400000b>1</_0400000b>%0d%0a  <_0400000c>&lt }


            return DataSource.ToTaskResult();
        }

        public Task DataSource_addfake()
        {


            ApplicationWebServiceExtensions.History.Add(
                new XElement("fake", "data " + new { ApplicationWebServiceExtensions.History.Count })
            );

            return new object().ToTaskResult();
        }




        static ApplicationWebService()
        {
#if Android
            Console.WriteLine("ApplicationWebService cctor");

            if (__AndroidMulticast.value == null)
                __AndroidMulticast.value = new __AndroidMulticast(
                    value =>
                    {
                        Console.WriteLine(
                            "ApplicationWebService cctor: " +

                            new { value }

                        );

                        // I/System.Console( 7351): ApplicationWebService cctor: { value = <string c="1">Visit me at 192.168.43.252:25452</string> }
                        ApplicationWebServiceExtensions.History.Add(
                            XElement.Parse(value)
                       );


                        //I/System.Console( 7351): ApplicationWebService cctor: { value = <string c="1">Visit me at 192.168.43.252:24129</string> }
                        //I/System.Console( 7351): #13 POST /xml?WebMethod=0600000a HTTP/1.1
                        //D/dalvikvm( 7351): GC_CONCURRENT freed 437K, 7% free 8047K/8644K, paused 4ms+3ms, total 48ms
                        //I/System.Console( 7351): enter poll { last_id = 8 }
                        //I/System.Console( 7351): yield { xml = <string c="1">Visit me at 192.168.43.252:24129</string> }
                        //I/System.Console( 7351): before raise_ColumnChanged
                        //I/System.Console( 7351): #13 POST /xml?WebMethod=0600000a HTTP/1.1 error:
                        //I/System.Console( 7351): #13 java.lang.RuntimeException
                        //I/System.Console( 7351): #13 java.lang.RuntimeException
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XNode.InternalFixBeforeAdobt(__XNode.java:130)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XContainer.Add(__XContainer.java:103)
                        //I/System.Console( 7351):        at ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString(StringConversionsForDataTable.java:141)
                        //I/System.Console( 7351):        at AndroidBroadcastLogger._02000007____ConvertToString_.ConvertToString(_02000007____ConvertToString_.java:41)
                        //I/System.Console( 7351):        at AndroidBroadcastLogger.Global.Invoke(Global.java:201)
                        //I/System.Console( 7351):        at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(InternalGlobalExtensions.java:350)
                        //I/System.Console( 7351):        at AndroidBroadcastLogger.Global.Application_BeginRequest(Global.java:40)
                        //I/System.Console( 7351):        at AndroidBroadcastLogger.Activities.ApplicationWebServiceActivity___c__DisplayClass26._CreateServer_b__21(ApplicationWebServiceActivity___c__DisplayClass26.java:348)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invokeNative(Native Method)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invoke(Method.java:525)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:68)
                        //I/System.Console( 7351):        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_2.Invoke(__Action_2.java:27)
                        //I/System.Console( 7351):        at AndroidBroadcastLogger.Activities.ApplicationWebServiceActivity___c__DisplayClass26___c__DisplayClass2f._CreateServer_b__25(ApplicationWebServiceActivity___c__DisplayClass26___c__DisplayClass2f.java:31)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invokeNative(Native Method)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invoke(Method.java:525)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:68)
                        //I/System.Console( 7351):        at ScriptCoreLib.Shared.BCLImplementation.System.Threading.__ThreadStart.Invoke(__ThreadStart.java:27)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread___c__DisplayClass3.__ctor_b__1(__Thread___c__DisplayClass3.java:20)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invokeNative(Native Method)
                        //I/System.Console( 7351):        at java.lang.reflect.Method.invoke(Method.java:525)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:68)
                        //I/System.Console( 7351):        at ScriptCoreLib.Shared.BCLImplementation.System.__Action.Invoke(__Action.java:27)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread_RunnableHandler.run(__Thread_RunnableHandler.java:20)
                        //I/System.Console( 7351):        at java.lang.Thread.run(Thread.java:841)
                        //I/System.Console( 7351): Caused by: java.lang.NullPointerException
                        //I/System.Console( 7351):        at org.apache.xml.serializer.ToStream.writeAttrString(ToStream.java:2099)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.ToStream.processAttributes(ToStream.java:2079)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.ToStream.closeStartTag(ToStream.java:2623)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.ToStream.characters(ToStream.java:1410)
                        //I/System.Console( 7351):        at org.apache.xalan.transformer.TransformerIdentityImpl.characters(TransformerIdentityImpl.java:1126)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.TreeWalker.dispatachChars(TreeWalker.java:246)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.TreeWalker.startNode(TreeWalker.java:416)
                        //I/System.Console( 7351):        at org.apache.xml.serializer.TreeWalker.traverse(TreeWalker.java:145)
                        //I/System.Console( 7351):        at org.apache.xalan.transformer.TransformerIdentityImpl.transform(TransformerIdentityImpl.java:390)
                        //I/System.Console( 7351):        at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XNode.InternalFixBeforeAdobt(__XNode.java:126)
                        //I/System.Console( 7351):        ... 26 more
                        //D/dalvikvm( 7351): GC_CONCURRENT freed 405K, 7% free 8092K/8644K, paused 2ms+1ms, total 21ms



                    }
                );
#endif

        }
    }

    public class __AndroidMulticast
    {
        public static __AndroidMulticast value;



        WifiManager wifi;
        WifiManager.MulticastLock multicastLock;

        public __AndroidMulticast(

            Action<string> AtData

            )
        {


            new Thread(
                    delegate()
                    {
                        InitializeThread(AtData);
                    }
                )
            {

                Name = "client"
            }.Start();
        }

        private void InitializeThread(Action<string> AtData)
        {
            // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
            // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

            // Acquire multicast lock
            wifi = (WifiManager)
                ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
            multicastLock = wifi.createMulticastLock("multicastLock");
            //multicastLock.setReferenceCounted(true);
            multicastLock.acquire();

            System.Console.WriteLine("LANBroadcastListener ready...");
            try
            {
                // tweet sized incoming broadcasts!
                var b = new byte[0x100];

                // https://code.google.com/p/android/issues/detail?id=40003

                var socket = new MulticastSocket(40404); // must bind receive side
                socket.setBroadcast(true);
                socket.setReuseAddress(true);
                socket.setTimeToLive(30);
                socket.setReceiveBufferSize(0x100);


                var NetworkInterfaces = NetworkInterface.getNetworkInterfaces();

                var wlan = default(NetworkInterface);

                //I/System.Console( 6922): { NetworkInterfaceName = dummy0 }
                //I/System.Console( 6922): { NetworkInterfaceName = sit0 }
                //I/System.Console( 6922): { NetworkInterfaceName = ip6tnl0 }
                //I/System.Console( 6922): { NetworkInterfaceName = p2p0 }
                //I/System.Console( 6922): { NetworkInterfaceName = wlan0 }
                //I/System.Console( 6922): LANBroadcastListener joinGroup...
                //I/Web Console( 7351): DataGridView ready
                //I/Web Console( 7351):  at http://192.168.43.7:1440/view-source:28790
                //I/Web Console( 7351): Time to load fields from the cookie, were they even sent?
                //I/Web Console( 7351):  at http://192.168.43.7:1440/view-source:28790
                //E/Web Console( 7351): Uncaught Error: SYNTAX_ERR: DOM Exception 12 at http://192.168.43.7:1440/view-source:11710
                //I/System.Console( 7351): #4 GET /assets/ScriptCoreLib.Windows.Forms/DataGridNewRow.png HTTP/1.1
                //D/TilesManager( 7351): Starting TG #0, 0x63c079d0
                //D/TilesManager( 7351): new EGLContext from framework: 62720e40
                //D/GLWebViewState( 7351): Reinit shader
                //D/dalvikvm( 7351): GC_CONCURRENT freed 505K, 7% free 7984K/8552K, paused 2ms+4ms, total 34ms
                //D/GLWebViewState( 7351): Reinit transferQueue
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281
                //E/Web Console( 7351): Uncaught TypeError: Cannot call method 'nhwABlgwczGFxMM_a_a0rwGA' of null at http://192.168.43.7:1440/view-source:46281

                while (NetworkInterfaces.hasMoreElements())
                {
                    var xNetworkInterface = (NetworkInterface)NetworkInterfaces.nextElement();

                    var NetworkInterfaceName = xNetworkInterface.getName();

                    if (NetworkInterfaceName.Contains("wlan"))
                        wlan = xNetworkInterface;

                    Console.WriteLine(new { NetworkInterfaceName });
                }

                //socket.joinGroup(InetAddress.getByName("239.1.2.3"), wlan);
                socket.joinGroup(InetAddress.getByName("239.1.2.3"));

                System.Console.WriteLine("LANBroadcastListener joinGroup...");

                // workaround
                var forever = true;
                while (forever)
                {
                    DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                    socket.receive(dgram); // blocks until a datagram is received

                    var bytes = new MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                    var listen = Encoding.UTF8.GetString(bytes.ToArray());



                    //dgram.setLength(b.Length); // must reset length field!s

                    if (AtData != null)
                        AtData(listen);

                }
            }
            catch
            {
                System.Console.WriteLine("client error");
            }

            //           script: error JSC1000: return from within try block not yet supported:
            //assembly: W:\staging\clr\AndroidBroadcastLogger.ApplicationWebService.AndroidActivity.dll
            //type: AndroidBroadcastLogger.__AndroidMulticast, AndroidBroadcastLogger.ApplicationWebService.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //offset: 0x015c
            // method:Void InitializeThread(System.Action`1[System.String])


            var __workaround = new object();
        }
    }


    public static class DoEventsExtension
    {
        public static void DoEvents(this int wait)
        {
            var waitTimer = new Stopwatch();

            waitTimer.Start();

            while (waitTimer.ElapsedMilliseconds < wait)
            {

                //Implementation not found for type import :
                //type: System.Windows.Forms.Application
                //method: Void DoEvents()
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!

#if Android
                Thread.Sleep(100);
#else
                System.Windows.Forms.Application.DoEvents();
                Thread.Yield();
                //Thread.Sleep(1);
#endif
            }
        }
    }
    static class ApplicationWebServiceExtensions
    {
        // Error	3	The parameter modifier 'ref' cannot be used with 'this' 	X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs	31	38	AndroidBroadcastLogger

        public static List<XElement> History = new List<XElement>();

        public static void poll(this MyDataSource DataSource)
        {
            Console.WriteLine("enter poll " + new { DataSource.last_id });

            if (DataSource.last_id < 0)
            {
                DataSource.last_id = History.Count;
                return;
            }


            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = History.Count;



                if (id == DataSource.last_id)
                {
                    var wait = DataSource.sync_SelectContentUpdates_waitmin
                        + random.Next(0, DataSource.sync_SelectContentUpdates_waitrandom);

                    wait.DoEvents();


                }
                else
                {



                    History.ToArray().Skip(DataSource.last_id).Take(id - DataSource.last_id).WithEach(
                        xml =>
                        {

                            Console.WriteLine("yield " + new { xml });
                            DataSource.yield(xml);

                            // force end of stream for now.
                            // as we are not using event stream yet
                            sw.Stop();
                        }
                    );

                    DataSource.last_id = id;
                }

                if (sw.ElapsedMilliseconds >= DataSource.sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }
        }
    }

    public class MyDataSource
    {
        public int last_id = -1;

        public DataTable data;

        public int sync_SelectContentUpdates_timeout = 1000;
        public int sync_SelectContentUpdates_waitmin = 10;
        public int sync_SelectContentUpdates_waitrandom = 30;

        public void yield(XElement value)
        {
            if (data == null)
            {
                data = new DataTable { };
                data.Columns.Add(new DataColumn { ColumnName = "xml", ReadOnly = true });

            }

            // An exception of type 'System.IndexOutOfRangeException' occurred in System.Data.dll but was not handled in user code
            // Additional information: Cannot find column 0.

            var row = data.NewRow();

            row[0] = value.ToString();

            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataRowCollection.Add(System.Object[])]
            data.Rows.Add(row);
        }
    }
}
