using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestServiceInterfacings
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component, iFoo
    {
        iFoo foo = new Foo();

        public Task<string> ReturnString(string s)
        {
            return foo.ReturnString();
        }
    }

    public interface iFoo
    {
        Task<string> ReturnString();
    }

    public class Foo : iFoo
    {
        //public async Task<string> ReturnString()
        //{
        //    return "Hello";
        //}
        async Task<string> iFoo.ReturnString(string s)
        {
            return "Hello";
        }
    }
}

