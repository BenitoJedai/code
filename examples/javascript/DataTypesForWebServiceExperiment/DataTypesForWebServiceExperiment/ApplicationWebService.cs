using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
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
                   new data1 { e = "e1"},
                   new data1 { e = "e2"}
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
    }

    public class data1
    {
        public string e;
    }




    public enum foo
    {
        a,
        b,
        c
    }
}
