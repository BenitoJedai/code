using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using ScriptCoreLib.GLSL;
using com.paypal.adaptive.exceptions;

#if !DEBUG
namespace com.paypal.adaptive.exceptions
{
    class AuthorizationRequiredException : java.lang.Exception
    {
        public string __AuthorizationUrl;

        public string getAuthorizationUrl()
        {
            return __AuthorizationUrl;
        }
    }


}
#endif

namespace TestExceptionCatch
{
    static class Program
    {
        // unreported exception java.lang.Throwable; must be caught or declared to be thrown
        [ScriptMethodThrows(typeof(java.lang.Throwable))]
        static void foo(bool thrownull)
        {
            if (thrownull)
                throw null;


#if !DEBUG
            var a = new AuthorizationRequiredException();
            a.__AuthorizationUrl = "http://foo";

            // Error	1	The type caught or thrown must be derived from System.Exception	
            throw (Exception)(object)a;
#endif
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("hi! vm:" + typeof(object).FullName);

            // http://msdn.microsoft.com/en-us/library/ms404228.aspx
            // http://jasonbock.net/JB/Default.aspx?blog=entry.9d6dbbd00d294f6fa2fee35f03e56ae8
            // http://stackoverflow.com/questions/9613413/gae-j-paypal-request-noclassdeffounderror-for-authorizationrequiredexception
            // http://paypalx-gae-toolkit.googlecode.com/svn/trunk/javadoc/com/paypal/adaptive/exceptions/AuthorizationRequiredException.html

            Action<bool> test =
                thrownull =>
                {
                    try
                    {
                        foo(thrownull);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(
                               "error info: " + new
                               {
                                   ex.Message,
                                   ex.StackTrace,
                                   ex.GetType().FullName
                               }
                       );


                        // Error	2	Cannot convert type 'System.Exception' to 'com.paypal.adaptive.exceptions.AuthorizationRequiredException' 
                        // via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion	
                        var aa = (object)ex as AuthorizationRequiredException;

                        if (aa != null)
                        {
                            var AuthorizationUrl = aa.getAuthorizationUrl();

                            Console.WriteLine(new { AuthorizationUrl });
                        }


                        if (aa == null)
                        {
                            Console.WriteLine("will rethrow now");

                            throw new Exception("only AuthorizationRequiredException expected!", ex);
                            //throw;
                        }
                    }
                };

            try
            {
                test(false);
                test(true);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(
                        "error: " + new
                        {
                            ex.Message,
                            ex.StackTrace

                        }
                );
            }

            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain();
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
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}

/*
 
 hi! vm:java.lang.Object
error info: { Message = , StackTrace = com.paypal.adaptive.exceptions.AuthorizationRequiredException
        at TestExceptionCatch.Program.foo(Program.java:105)
        at TestExceptionCatch.Program._Main_b__0(Program.java:71)
        at TestExceptionCatch.Program._1_Main_public_ldftn_0028(Program.java:112)
        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
        at java.lang.reflect.Method.invoke(Unknown Source)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:27)
        at TestExceptionCatch.Program.main(Program.java:52)
, FullName = com.paypal.adaptive.exceptions.AuthorizationRequiredException }
{ AuthorizationUrl = http://foo }
error info: { Message = , StackTrace = java.lang.NullPointerException
        at TestExceptionCatch.Program.foo(Program.java:102)
        at TestExceptionCatch.Program._Main_b__0(Program.java:71)
        at TestExceptionCatch.Program._1_Main_public_ldftn_0028(Program.java:112)
        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
        at java.lang.reflect.Method.invoke(Unknown Source)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:27)
        at TestExceptionCatch.Program.main(Program.java:53)
, FullName = java.lang.NullPointerException }
will rethrow now
error: { Message = , StackTrace = java.lang.RuntimeException
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:92)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:70)
        at ScriptCoreLib.Shared.BCLImplementation.System.__Action_1.Invoke(__Action_1.java:27)
        at TestExceptionCatch.Program.main(Program.java:53)
Caused by: java.lang.reflect.InvocationTargetException
        at sun.reflect.NativeMethodAccessorImpl.invoke0(Native Method)
        at sun.reflect.NativeMethodAccessorImpl.invoke(Unknown Source)
        at sun.reflect.DelegatingMethodAccessorImpl.invoke(Unknown Source)
        at java.lang.reflect.Method.invoke(Unknown Source)
        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:88)
        ... 3 more
Caused by: java.lang.RuntimeException: only AuthorizationRequiredException expected!
        at TestExceptionCatch.Program._Main_b__0(Program.java:88)
        at TestExceptionCatch.Program._1_Main_public_ldftn_0028(Program.java:112)
        ... 8 more
Caused by: java.lang.NullPointerException
        at TestExceptionCatch.Program.foo(Program.java:102)
        at TestExceptionCatch.Program._Main_b__0(Program.java:71)
        ... 9 more
 }
<hello>world</hello>

 */