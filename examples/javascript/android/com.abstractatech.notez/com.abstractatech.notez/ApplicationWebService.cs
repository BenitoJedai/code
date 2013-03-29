using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.notez
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
        public void get_LocalStorage(Action<string, string> add_localStorage, Action done)
        {

            #region default text
            var now = DateTime.Now;


            var yyyy = now.Year;
            var mm = now.Month;
            var dd = now.Day;


            var yyyymmdd = yyyy
                + mm.ToString().PadLeft(2, '0')
                + dd.ToString().PadLeft(2, '0');



            var InnerHTML = @"

<div><font face='Verdana' size='5' color='#0000fc'>" + yyyymmdd + @" Hello world</font></div><div><br /></div><blockquote style='margin: 0 0 0 40px; border: none; padding: 0px;'></blockquote><font face='Verdana'>This is your content.</font>

            ";
            #endregion

            add_localStorage(yyyymmdd + " Hello world", InnerHTML);

            // Send it back to the caller.
            done();
        }


        public void remove_LocalStorage(string key)
        {
            Console.WriteLine("remove_LocalStorage: " + new { key });
        }

        public void set_LocalStorage(string key, string value)
        {

            Console.WriteLine("set_LocalStorage: " + new { key, value });
        }
    }
}
