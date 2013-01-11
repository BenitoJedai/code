using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace GravatarExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void Gravatar(string e, Action<string> avatar, Action<string> profile)
        {
            // http://en.gravatar.com/site/implement/hash/
            var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();



            avatar("http://www.gravatar.com/avatar/" + hash);
            profile("http://www.gravatar.com/" + hash);
        }

    }
}
