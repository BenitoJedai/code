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
            // http://farazmahmood.wordpress.com/projects/md5-implementation-in-c/

            // http://en.gravatar.com/site/implement/hash/
            var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();

            // android needs attention
            // Implementation not found for type import :
            // type: System.Security.Cryptography.MD5CryptoServiceProvider
            // method: Void .ctor()
            // Did you forget to add the [Script] attribute?
            // Please double check the signature!

            // type: ScriptCoreLib.Ultra.Library.Extensions.CryptographyExtensions
            // offset: 0x0001
            //  method:Byte[] ToMD5Bytes(Byte[])
            //System.NotSupportedException:

            // Implementation not found for type import :
            // type: System.Security.Cryptography.MD5CryptoServiceProvider
            // method: Void .ctor()
            // Did you forget to add the [Script] attribute?
            // Please double check the signature!



            avatar("http://www.gravatar.com/avatar/" + hash);
            profile("http://www.gravatar.com/" + hash);
        }

    }
}
