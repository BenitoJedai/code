using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataTypesForWebServiceExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public Action MakeYellow = delegate { };
        public Action MakeCyan = delegate { };

        public Action<string> set_backgroundColor = delegate { };

        public Task<int[]> WebMethod2(int e, Action<string> y)
        {
            MakeCyan();

            // Send it back to the caller.
            y(new { e }.ToString());

            var value = new[] {
                    (int)foo.b,
                    (int)foo.c
                };

            //var value = new[] {
            //         "a",
            //        "b"
            //    };


            foreach (var item in value)
            {
                Console.WriteLine(new { item });
            }

            return Task.FromResult(value);
        }

        public Task<data1[]> WebMethod4(int e, Action<string> y)
        {
            MakeYellow();

            // Send it back to the caller.
            y(new { e }.ToString());

            var value = new[] {
                // jsc why cant we return .this?
                   new data1 { e = "hello1!", CallbackCopy = new ApplicationWebService() },
                   new data1 { e = "hello2!", CallbackCopy = new ApplicationWebService() }
            };

            //var value = new[] {
            //         "a",
            //        "b"
            //    };


            foreach (var item in value)
            {
                Console.WriteLine(new { item });
            }

            return Task.FromResult(value);
        }

        public string xe = "default";

        public Task<ApplicationWebService[]> WebMethod8(int e, Action<string> y)
        {
            set_backgroundColor("rgba(0, 255, 0, 0.5)");

            // Send it back to the caller.
            y(new { e }.ToString());

            var value = new[] {
                   new ApplicationWebService { xe = this.xe + "/xe1"},
                   new ApplicationWebService { xe = this.xe + "/xe2"}
            };

            //var value = new[] {
            //         "a",
            //        "b"
            //    };


            foreach (var item in value)
            {
                Console.WriteLine(new { item });
            }

            return Task.FromResult(value);
        }

        public async Task<string> SpecialToString(data1 e)
        {
            // slow down
            Thread.Sleep(500);

            return "[ApplicationWebService] " + new { e };
        }

        // should jsc send all synchronious messages to the client?
        //public override string ToString()
        //{
        //    return base.ToString();
        //}
    }

    public class data1
    {
        public ApplicationWebService CallbackCopy;

        public string e;

        public override string ToString()
        {
            return new { e }.ToString();
        }

        public async Task<string> GetString()
        {
            if (CallbackCopy == null)
                return ToString();

            //// bugfix?
            //CallbackCopy.MakeYellow = delegate { };
            //CallbackCopy.MakeCyan = delegate { };
            //CallbackCopy.set_backgroundColor = delegate { };


            //await Task.Delay(300);

            return await CallbackCopy.SpecialToString(this);

            //return "[limbo] " + new
            //{
            //    @this = this,

            //    this.CallbackCopy.xe,
            //    this.CallbackCopy.MakeCyan,
            //    this.CallbackCopy.MakeYellow,
            //    this.CallbackCopy.set_backgroundColor

            //};


            //var service = new Appl
        }
    }




    public enum foo
    {
        a,
        b,
        c
    }
}
