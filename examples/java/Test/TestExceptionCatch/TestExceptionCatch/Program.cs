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
