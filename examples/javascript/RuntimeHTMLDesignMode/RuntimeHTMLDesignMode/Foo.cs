using RuntimeHTMLDesignMode.HTML.Pages;
using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeHTMLDesignMode
{
    using FooApplicationWebService = ApplicationWebService;

    public sealed class FooApplication
    {
        FooApplicationWebService service = new FooApplicationWebService();

        public FooApplication(IFoo foo)
        {
            Native.window.alert("hey");


        }
    }

    public class FooApplicationFoo
    {
        public string ContentKey;
        public string ContentValue;

        // child nodes
        public List<FooApplicationFoo> Children = new List<FooApplicationFoo>();
    }

    public class FooApplicationData
    {
        // datasource: FooApplicationData
        // table: foo
        public List<FooApplicationFoo> foo = new List<FooApplicationFoo>();

        //public int Insert(object e)
        //{ 
        //    // any table?
        //}
    }


    public sealed partial class ApplicationWebService
    {
        public FooApplicationData data = new FooApplicationData();

        public void FooMethod(string e, Action<string> y)
        {


            var q =
                from x in data.foo
                where x.ContentKey.StartsWith("Foo")
                select new { Id = x.GetHashCode(), x.ContentValue };


        }
    }


}
