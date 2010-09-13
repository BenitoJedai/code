using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Delegates;

namespace jsc.meta.Library.Mashups
{
    using Secure1 = UltraWebServiceWithInternalServiceInternal;

    [Location("/secure")]
    internal sealed class UltraWebServiceWithInternalServiceApplication_secure
    {
        public UltraWebServiceWithInternalServiceApplication_secure(object p)
        {
            var secure = new Secure1();

            secure.SecureNonPublicMethod("hello",
                y => { }
            );
        }
    }

    [Location("/")]
    internal sealed class UltraWebServiceWithInternalServiceApplication
    {
        public UltraWebServiceWithInternalServiceApplication(object p)
        {
            var service = new UltraWebServiceWithInternalService();

            service.Method1Complete += y => { };

            service.Method1("client data", y => { } );


        }
    }

    [Location("www.example.com", "public.example.com")]
    public class UltraWebServiceWithInternalService
    {
        public event StringAction Method1Complete;

        public void Method1(string input, StringAction yield)
        {
            var secure = new UltraWebServiceWithInternalServiceInternal();

            yield("working on it!");

            secure.SecureNonPublicMethod(input,
                c =>
                {
                    // we better have the Session on the server set up to store this call
                    // and the client polling or on wait for events

                    Method1Complete(c);
                    yield(c);
                }
            );
        }
    }

    [Location("internal1.example.com")]
    public class UltraWebServiceWithInternalServiceInternal
    {
        public void SecureNonPublicMethod(string input, StringAction yield)
        {
            // this method may not be able to be accessed directly from the public client
        }
    }

    /// <summary>
    /// A DNS name where the client will be able to access this method
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class LocationAttribute : Attribute
    {
        public string[] Host { get; set; }
        public LocationAttribute(params string[] Location)
        {

        }
    }
}
