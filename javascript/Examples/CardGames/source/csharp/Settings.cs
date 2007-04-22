using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;

namespace CardGames.source.csharp
{
    static class Settings
    {


        static void DefineSpawnPoint(TextWriter w, string alias, string data)
        {
            var value = data;

            w.WriteLine("<input type='hidden' value='" + Convert.ToBase64String( Encoding.ASCII.GetBytes( value)  ) + "' class='" + alias + "' />");
        }

        static void DefineSpawnPoint(IEntryPoint e, string alias, string data)
        {
            var w = new TextWriter();


            w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            w.WriteLine("<html>");
            w.WriteLine("<body>");

            ScriptCoreLib.SharedHelper.DefineScript(w, ScriptCoreLib.SharedHelper.LocalModules);

            

            w.WriteLine("<script></script>");

            DefineSpawnPoint(w, alias,  data);

            w.WriteLine("</body>");
            w.WriteLine("</html>");

            e[alias + ".htm"] = w.Text;
        }


        public static void DefineEntryPoint(IEntryPoint e)
        {
            foreach (var v in CardGames.source.js.Settings.Controls)
            {
                DefineSpawnPoint(e, v.A, Environment.MachineName);
            }

        }
    }

}
