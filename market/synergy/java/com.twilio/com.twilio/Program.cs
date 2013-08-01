using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

namespace com.twilio
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // if jsc added manifest, this could be run with jvm also.
            // Error: Could not find or load main class com.twilio.exe
            // META-INF/MANIFEST.MF
            // Main-Class: 

            // http://www.twilio.com/docs/quickstart/java/sms
            // http://www.twilio.com/docs/quickstart/java/sms/sending-via-rest

            try
            {
                var ACCOUNT_SID = "AC123";
                var AUTH_TOKEN = "456bef";

                var client = new com.twilio.sdk.TwilioRestClient(ACCOUNT_SID, AUTH_TOKEN);

                var account = client.getAccount();

                var smsFactory = account.getSmsFactory();
                var smsParams = new java.util.HashMap();
                smsParams.put("To", "+14105551234");
                smsParams.put("From", "(410) 555-6789"); // Replace with a valid phone
                // number in your account
                smsParams.put("Body", "Where's Wallace?");
                var sms = smsFactory.create(smsParams);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }

            //            error: { Message = AccountSid 'AC123' is not valid.  It should be the 34 character unique identifier starting with 'AC', StackTrace = java.lang.IllegalArgumentException: AccountSid 'AC123' is not valid.  It should be the 34 character unique identifier starting with 'AC'
            //        at com.twilio.sdk.TwilioRestClient.validateAccountSid(TwilioRestClient.java:181)
            //        at com.twilio.sdk.TwilioRestClient.<init>(TwilioRestClient.java:129)
            //        at com.twilio.sdk.TwilioRestClient.<init>(TwilioRestClient.java:110)
            //        at com.twilio.Program.main(Program.java:40)
            // }
            //jvm: java.lang.Object
            //clr: System.Object


            //            002e log4j-1.2.16 create org.apache.log4j.ConsoleAppender
            //System.TypeLoadException: Derived method 'append' in type 'org.apache.log4j.ConsoleAppender' from assembly 'log4j-1.2.16, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' cannot reduce access.


            //            servlet-api-2.3 000d Create javax.servlet.ServletRequestWrapper
            //System.TypeLoadException: Method 'getLocalAddr' in type 'javax.servlet.ServletRequestWrapper' from assembly 'servlet-api-2.3, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
            //   at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
            //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
            //   at System.Reflection.Emit.TypeBuilder.CreateType()
            //   at jsc.meta.Library.CreateToJARImportNatives.<>c__DisplayClass8a.<>c__DisplayClassec.<InternalImplementation>b__7b(TypeBuilder SourceTypeBuilder) in x:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Library\CreateToJARImportNatives.cs:line 2387
            // SourceTypeName = "javax.net.ssl.SSLSession"
            // SourceTypeName = "java.lang.AutoCloseable"
            // SourceMethods.Length = 0x0000001b
            // Reflector_Type_GetImplicitAbstractMethods


            ("jvm: " + typeof(object)).WriteAndWaitAtCLR();

        }


    }


    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(

            )
        {
            System.Console.WriteLine(XML);


        }

        public static void WriteAndWaitAtCLR(this string e)
        {
            System.Console.WriteLine(e);
            System.Console.WriteLine("clr: " + typeof(object));
            System.Console.ReadKey();
        }
    }

}
