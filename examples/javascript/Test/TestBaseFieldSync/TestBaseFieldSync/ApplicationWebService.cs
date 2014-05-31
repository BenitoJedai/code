using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace TestBaseFieldSync
{
    // can we do generic?
    // shall we also behave as attribute?

    //[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class xRow(public string Tag = "xdefault xTag", public XElement content = default(XElement)) : Attribute
    {
        // when will C# have xml literals and when can they be used as constants like strings can?
        // xml cannot be used for attributes yet

        // title could be special. automatically databound?
        // there cannot be byref fields in .net, yet in jsc we have field Observe?
        public string title;

        //public string Tag;

        public xRow() : this("default Tag")
        {

        }

        // we may end up with a full post back 
        // whats the screen size, etc?
        // when can we enable calling serverside delegates?

        // can the UI send as a delegate?
        // can we save a delegate into database? the ultimate serialization

        // should we have a special class to do remoting to browser?
        // chrome remote?
        // webviewremote?
        // as a base class it would enable to sync fields, yet limit our UI to webviews. what else is there? :P
        public Action close;
        // where is it being called?
        // can we also enable callbacks on any other data type we send in?

    }

    //public class ApplicationWebService : xRow { title = "???" }
    public class ApplicationWebService() : xRow(
        Tag: "???",

        // readonly elements where cant change div to anything else? DIVElement?
        content: new XElement("div",
            new XAttribute("style", "color: blue;"),
            "base field xml")
        ), IDisposable
    {



        //public Func<int, Task<int>> Compute =
        //    async input =>
        //    {
        //        // can the client call here yet?

        //        //this.

        //        return 42;
        //    };

        //public static readonly Func<ApplicationWebService, int, Task<int>> StaticCompute =
        //    async (service, input) =>
        //    {
        //        // can the client call here yet?

        //        return 42;
        //    };

        // X:\jsc.svn\examples\javascript\LINQ\test\TestGroupByThenOrderByThenOrderBy\TestGroupByThenOrderByThenOrderBy\Application.cs
        // if interfaces alloed fields we could ask the client to send us the window.close function

        /// <summary>
        /// <div>can we use XMLComments to init data?</div>
        /// </summary>
        public XElement content2;


        // when can we make it explicit?
        public void Dispose()
        {
            Console.WriteLine("");
            //Debugger.Break();
        }

        //[xRow(Tag: "look there is demo data!")]
        //[xRow(Tag: "look there is demo data!2")]
        //[xRow(Tag: "yay")]
        public async Task Invoke()
        {
            //e = "<div xmlns=\"http://www.w3.org/1999/xhtml\" style=\"color: blue;\" id=\"content\">base field xml<pre>{{ status = will call invoke }}</pre></div>"

            //         at System.Xml.XmlConvert.VerifyNCName(String name, ExceptionType exceptionType)
            //at System.Xml.XmlConvert.VerifyNCName(String name)
            //at System.Xml.Linq.XName..ctor(XNamespace ns, String localName)
            //at System.Xml.Linq.XNamespace.GetName(String localName, Int32 index, Int32 count)
            //at System.Xml.Linq.XNamespace.GetName(String localName)
            //at System.Xml.Linq.XName.Get(String expandedName)
            //at System.Xml.Linq.XName.op_Implicit(String expandedName)
            //at TestBaseFieldSync.ApplicationWebService.< Invoke > d__1.MoveNext()

            this.content.Add(
                new XElement("pre", "enter Invoke")
            );

            //Debugger.Break();

            // what if the server was able to send the client
            // there is more, eg. 204 and I have a debugger attached.

            close();
        }


        // where are the fields being serialized? ultra?
        // ultra library?
        // no.
        // its internal.
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs
        // ok where is the client deserializing the fields?
        // WebServiceForJavaScript
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebServiceForJavaScript.cs
        // um. where are they sent back to the server?
        // where is the server reading them?
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs
        // 940


    }


}
