using ScriptCoreLib;

namespace ScriptCoreLib.PHP
{
    [Script]
    public static partial class Native
    {
        static Native()
        {
            SetDefaultTimezone();
        }

        [Script(IsNative=true)]
        static public void echo(object e) { }

        public static string ScriptFileName
        {
            get
            {
                return Native.API.basename(Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.SCRIPT_FILENAME]);
            }
        }

        public static string QueryString
        {
            get
            {
                return Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.QUERY_STRING];
            }
        }


        public static void Link(string e, string href, string target)
        {
            Native.echo("<a href='" + href + "' target='" + target + "'>" + e + "</a>");

        }

        public static void Link(string e, string href)
        {
            Native.echo("<a href='" + href + "'>" + e + "</a>");
        }

        public static void Script(string e)
        {
            echo("<script src='" + e + "'></script>");
        }

        public static void Error(string e)
        {
            if (e == null)
                return;

            Error(e, true);
        }

        public static void Error(string e, bool bEscape)
        {
            Message("Error", e, "red", bEscape);

        }

        public static void Message(string c, object e)
        {
            Message(c, "" + e);
        }

        public static void Message(string c, string e)
        {
            Message(c, e, "black", true);
        }

        public static void Message(string c, string e, string bgColor, bool bEscape)
        {
            echo("<div style='font-family: Verdana; border: 1px solid #808080;'>");

            if (c != null)
            {
                echo("<div class='Web.Fx.CollapsedMessage' style='color: white; background: " + bgColor + "; padding: 4px;'><code>");
                echo(c);
                echo("</code></div>");
            }

            echo("<pre style='padding: 8px;'>");

            if (e == null || e == "")
                echo("<i>No text</i>");
            else
            {
                if (bEscape)
                    echo(HTMLEscape(e));
                else
                    echo(e);
            }

            echo("</pre>");

            echo("</div>");

            Native.API.flush();
        }

        public static string HTMLEscape(string e)
        {
            string r = e.Replace("<", "&lt;");

            r = r.Replace(">", "&gt;");

            return r; ;
        }

        public static void Message(string e)
        {
            echo("<pre style='border: 1px solid #808080; padding: 8px;'>");

            echo(HTMLEscape(e));

            echo("</pre>");

            Native.API.flush();

        }

        
        public static void Dump(string c, object e)
        {
            Message(c, DumpToString(e));
            
        }

        public static void Dump(object e, bool bAsMessage)
        {
            if (bAsMessage)
                Message(DumpToString(e));
            else
                echo(DumpToString(e));

        }

        public static void Dump(object w)
        {
            Message(DumpToString(w));
        }

        public static string DumpToString(object e)
        {
            API.ob_start();

            API.var_dump(e);

            return API.ob_get_clean();
        }


        #region void header

        // TODO: handle optional parameters or paramlist of header
        /// <summary>
        /// undefined
        /// </summary>
        /// <param name="_string">string string</param>
        [Script(IsNative = true)]
        public static void header(string _string) { }

        #endregion

        #region void exit

        /// <summary>
        /// The exit() function terminates execution of the script. It prints status just before exiting. 
        /// </summary>
        /// <param name="_status">string status</param>
        [Script(IsNative = true)]
        public static void exit(string _status) { }

        #endregion

        #region void exit

        /// <summary>
        /// The exit() function terminates execution of the script. It prints status just before exiting. 
        /// </summary>
        [Script(IsNative = true)]
        public static void exit() { }

        #endregion
    }
}
