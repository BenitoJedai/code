using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ScriptCoreLib.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// A cross platform way to perform feed lookup.
        /// 
        /// Notice that .NET will use XmlReader which is not fully supported for 
        /// other platforms.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static SyndicationFeed ToSyndicationFeed(this Uri u)
        {
            var uri = u.ToString();
            return SyndicationFeed.Load(XmlReader.Create(uri));
        }
    }
}
