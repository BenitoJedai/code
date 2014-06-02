using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Linq
    {
    }
}


namespace System.Web
{
    public static class Helpers
    {
        //ConfirmUser.
    }

    public static class WebPages
    {
        // http://blog.sina.com.cn/s/blog_629847620100xy8j.html

        // RazorClassGenerator
        // http://visualstudiogallery.msdn.microsoft.com/1f6ec6ff-e89b-4c47-8e79-d2d68df894ec
        // do we need a special post build event to build cshtml?

        //        I think Express support can be added as follows to the manifest:

        //   <SupportedProducts>

        //     <VisualStudio Version = "10.0" >

        //       < Edition > Ultimate </ Edition >

        //       < Edition > Premium </ Edition >

        //       < Edition > Pro </ Edition >

        //       < Edition > Express_All </ Edition >

        //    </ VisualStudio >

        //   </ SupportedProducts >

        //This is my reference:

        //msdn.microsoft.com/.../ee822857.aspx

        public class __Context
        {
            // Error	2	'System.Web.WebPages.__Context' does not contain a definition for 'ApplicationInstance' and no extension method 'ApplicationInstance' accepting a first argument of type 'System.Web.WebPages.__Context' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset

            public object ApplicationInstance;
        }

        // Error	5	'object' does not contain a definition for 'ApplicationInstance' and no extension method 'ApplicationInstance' accepting a first argument of type 'object' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        public static __Context Context;

        //        Error	1	The type or namespace name 'Linq' does not exist in the namespace 'System' (are you missing an assembly reference?)	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        //Error   2	The type or namespace name 'Helpers' does not exist in the namespace 'System.Web' (are you missing an assembly reference?)	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        //Error   3	The name 'Context' does not exist in the current context X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset


        // Error	5	A using directive can only be applied to static classes or namespaces; 'System.Web.WebPages' is a non-static class	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        // Error	5	'System.Web.WebPages' is inaccessible due to its protection level	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        // Error	5	The type name 'Html' does not exist in the type 'System.Web.WebPages'	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset

        public static class Html
        {
        }

        // Error	4	'ASP._Page_Views_ConfirmUser_cshtml.Execute()': no suitable method found to override	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        // Error	5	'ASP._Page_Views_ConfirmUser_cshtml': cannot derive from static class 'System.Web.WebPages.WebPage'	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
        public class WebPage
        {
            // Error	5	'ASP._Page_Views_ConfirmUser_cshtml.Execute()': cannot override inherited member 'System.Web.WebPages.WebPage.Execute()' because it is not marked virtual, abstract, or override	X:\jsc.svn\examples\javascript\test\TestCSHTMLAsset\TestCSHTMLAsset\Views\ConfirmUser.cshtml	1	1	TestCSHTMLAsset
            public virtual void Execute()
            {
                //System.Linq.
            }
        }

    }
}
