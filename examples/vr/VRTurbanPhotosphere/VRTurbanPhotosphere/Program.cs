using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Text;

namespace VRTurbanPhotosphere
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = new ApplicationWebService();
            var xml = x.Header.ToString();
            var bytes = Encoding.UTF8.GetBytes(xml);
            var base64 = Convert.ToBase64String(bytes);


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
