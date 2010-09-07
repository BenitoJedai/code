using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ScriptCoreLib.Ultra.BCLImplementation.System.Xml;

namespace ScriptCoreLib.Ultra.BCLImplementation.System.ServiceModel.Syndication
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.ServiceModel.Syndication.SyndicationFeed, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    internal class __SyndicationFeed
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        public __SyndicationFeed(
            string title,
            string description,
            Uri feedAlternateLink
        )
        {
            // RSS via WebClient and XElement?
            // http://www.nearinfinity.com/blogs/joe_ferner/performance_linq_to_sql_vs.html
            // http://weblogs.asp.net/scottgu/archive/2007/08/07/using-linq-to-xml-and-how-to-build-a-custom-rss-feed-reader-with-it.aspx

        }

        public static __SyndicationFeed Load(XmlReader reader)
        {
            var n = new __SyndicationFeed(null, null, null);

            var r = (__XmlReader)(object)reader;

            var doc = r.InternalDocument;

            return n;
        }
    }
}
