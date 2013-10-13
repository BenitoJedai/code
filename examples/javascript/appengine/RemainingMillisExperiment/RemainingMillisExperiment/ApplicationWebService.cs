using com.google.apphosting.api;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RemainingMillisExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public string title = "before0";
        public int counter = 0;

       
        // jsc, can you turn AppEngine SDK into async yet?


        // client side needs to provide a proxy constructor for this to work
        //public async Task<T> CreateType<T>() where T : new()
        //{
        //    return new T { };
        //}

        public Task<string> RemainingMillisString
        {
            get
            {
                var RemainingMillis = this.RemainingMillis.Result;

                // make it slow!
                Thread.Sleep(500);

                return new { RemainingMillis }.ToString().ToTaskResult();
            }
        }
        public Task<long> RemainingMillis
        {
            get
            {
                Console.WriteLine("RemainingMillis enter " + this);

                // Set-Cookie:InternalFields=field_title=YmVmb3Jl&;  path=/
                // Set-Cookie:InternalFields=field_title=YmVmb3Jl&;  path=/
                this.title = "server";
                this.counter = 666;

                // http://stackoverflow.com/questions/13351563/how-to-impose-google-app-engines-deadlineexceededexception

                // The method or operation is not implemented.
                var en = ApiProxy.getCurrentEnvironment();

                var RemainingMillis = en.getRemainingMillis();

                // Send it back to the caller.
                return RemainingMillis.ToTaskResult();
            }
        }

        //public override string ToString()
        //{
        //    return new { this.counter, this.title }.ToString();

        //}

    }
}
