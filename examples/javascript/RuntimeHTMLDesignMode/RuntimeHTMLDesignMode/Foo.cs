using RuntimeHTMLDesignMode.HTML.Pages;
using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeHTMLDesignMode
{
    using FooApplicationWebService = ApplicationWebService;

    sealed class FooApplication
    {
        FooApplicationWebService service = new FooApplicationWebService();

        public FooApplication(IFoo foo)
        {
            Native.Window.alert("hey");


        }
    }

    class FooTable1
    {
        public long Id;
        public string ContentKey;
        public string ContentValue;


        public static IQueryable<FooTable1> Storage;
    }

    public sealed partial class ApplicationWebService
    {
        public static void FooMethod(string e, Action<string> y)
        {
            var q =
                from x in FooTable1.Storage
                where x.ContentKey.StartsWith("Foo")
                select new { x.Id, x.ContentValue };


        }
    }
}
