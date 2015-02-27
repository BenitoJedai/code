using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestNullObjectFromWebService;
using TestNullObjectFromWebService.Design;
using TestNullObjectFromWebService.HTML.Pages;

namespace TestNullObjectFromWebService
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // enter { ConvertTypeName = TestNullObjectFromWebService.ConvertToString$0$<02000003> }

            if (base.foo == null)
                new IHTMLPre { "foo is null" }.AttachToDocument();
            else
                new IHTMLPre { "bugcheck: foo not null?" }.AttachToDocument();

            // GetInternalFields save to localstorage! { InternalFieldsCookieValue = field_foo= }
            // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Remoting\InternalWebMethodRequest.cs

            //0ms InternalFieldsFromTypeInitializer
            //2015-02-27 16:27:40.879 view-source:50192 30ms enter GetInternalFields
            //2015-02-27 16:27:40.883 view-source:50192 35ms GetInternalFields {{ InternalFieldsCookieValue = field_foo= }}
            //2015-02-27 16:27:40.892 view-source:50192 43ms GetInternalFields save to localstorage! {{ InternalFieldsCookieValue = field_foo= }}

            // who is calling it?
            //2015-02-27 16:27:40.910 view-source:50192 62ms NewInstanceConstructor restore fields..

            //2015-02-27 16:27:40.911 view-source:50192 62ms enter GetInternalFieldValue {{ FieldName = field_foo }}
            //2015-02-27 16:27:40.913 view-source:50192 64ms exit GetInternalFieldValue {{ FieldName = field_foo, FieldValue =  }}


            //0ms InternalFieldsFromTypeInitializer
            //2015-02-27 17:04:08.335 view-source:49845 33ms GetInternalFields save to localstorage! {{ InternalFieldsCookieValue = field_foo= }}
            //2015-02-27 17:04:08.353 view-source:49845 51ms NewInstanceConstructor restore fields..
            //2015-02-27 17:04:08.353 view-source:49845 51ms will restore foo
            //2015-02-27 17:04:08.354 view-source:49845 53ms enter { ConvertTypeName = TestNullObjectFromWebService.ConvertFromString0$<02000003> }
            //2015-02-27 17:04:08.355 view-source:49845 53ms before xml parse { ConvertTypeName = TestNullObjectFromWebService.ConvertFromString0$<02000003> }
            //2015-02-27 17:04:08.359 view-source:32878 Uncaught Error: ArgumentNullException: XElement.Parse Root element is missing.

            // the server should not be sending empty strings for fields that are null/
            // is it sending?
            
            
            // yes view souce is. why?
            // Set-Cookie:InternalFields=field_foo=; path=/
        }

    }
}
