using android.content;
using android.net.wifi;
using java.net;
using JVMCLRBroadcastLogger;
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


        // X:\jsc.svn\examples\javascript\android\AndroidNotificationWebActivity\AndroidNotificationWebActivity\ApplicationWebService.cs


        static ApplicationWebService()
        {
#if Android
            Console.WriteLine("ApplicationWebService cctor");

            if (__AndroidMulticast.value == null)
            {

                WifiManager wifi;
                WifiManager.MulticastLock multicastLock;

                // Acquire multicast lock
                wifi = (WifiManager)
                    ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
                multicastLock = wifi.createMulticastLock("multicastLock");
                //multicastLock.setReferenceCounted(true);
                multicastLock.acquire();

                __AndroidMulticast.value = new __AndroidMulticast(
                    value =>
                    {
                        Console.WriteLine(
                            "ApplicationWebService cctor: " +

                            new { value }

                        );

                        try
                        {
                            var xml = XElement.Parse(value);
                            // ApplicationWebService cctor: { value = <string reason="shake" c="8" preview="data:image/png;base64,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" n="com.abstractatech.gamification.gir">Visit me at 192.168.43.7:25814</string> }


                            Console.WriteLine(new { xml });

                            // I/System.Console( 7351): ApplicationWebService cctor: { value = <string c="1">Visit me at 192.168.43.252:25452</string> }
                            ApplicationWebServiceExtensions.History.Add(
                                xml
                           );
                        }
                        catch (Exception ex)
                        {
                            // client error
                            System.Console.WriteLine(
                                "ApplicationWebService cctor error " +
                                new { ex.Message, ex.StackTrace }
                            );
                        }

                   

                        // http://grepcode.com/file/repository.springsource.com/org.apache.xalan/com.springsource.org.apache.xml.serializer/2.7.1/org/apache/xml/serializer/ToStream.java#2099


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
            }

#endif

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
