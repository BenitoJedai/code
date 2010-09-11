using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.Library.Analytics
{
    public class StatCounterUriBuilder
    {
        public string sc_project;
        public string security;

        public Uri ToUri()
        {
            return new Uri(
                "http://c.statcounter.com/" + this.sc_project + @"/0/" + this.security + "/0/"
            );
        }


    }

    public static class StatCounterUriBuilderExtensions
    {
        public static void Invoke(this StatCounterUriBuilder e, string message = "")
        {
            // should we encrypt it?
            var message0 = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));

            new TrivialWebRequest
            {
                Referer = "http://www.jsc-solutions.net/message/" + message0,
                Target = e.ToUri()
            }.Invoke();
        }
    }

    public static class StatCounterUriBuilderSilentExtensions
    {
        public static void InvokeSilent(this StatCounterUriBuilder e, string message = "")
        {
            new Thread(
                delegate()
                {
                    try
                    {
                        e.Invoke(message);
                    }
                    catch
                    {
                    }
                }
            ) { IsBackground = true }.Start();
        }
    }
}
