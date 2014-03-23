using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class InternalXMLExtensions
    {
        // 2011/11/27 we should have that by now. Testing needed :)

        // once all platforms implement xml then we may
        // skip these methods...

        // tested by
        // X:\jsc.svn\examples\javascript\DataTypesForWebServiceExperiment\DataTypesForWebServiceExperiment\ApplicationWebService.cs

        public static string ToXMLString(this string xml)
        {
            if ((object)xml == null)
                return null;


            return xml
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;")
            ;
        }

        public static string FromXMLString(this string xml)
        {
            if ((object)xml == null)
                return null;

            return xml
                .Replace("&apos;", "'")
                .Replace("&quot;", "\"")
                .Replace("&gt;", ">")
                .Replace("&lt;", "<")
                .Replace("&amp;", "&")
            ;
        }
    }
}
