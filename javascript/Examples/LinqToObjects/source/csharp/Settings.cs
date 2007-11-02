using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;

namespace LinqToObjects.source.csharp
{
    interface IAssemblyReferenceToken : ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
    {
    }

    static class Settings
    {
        static void DefineSpawnPoint(IEntryPoint e, string alias, string data)
        {
            var w = new TextWriter();


            w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            w.WriteLine("<html>");
            w.WriteLine("<body>");


            SharedHelper.DefineScript(w, SharedHelper.LocalModules);

            w.WriteLine("<script></script>");

            SharedHelper.DefineSpawnPoint(w, alias, data);

            w.WriteLine("</body>");
            w.WriteLine("</html>");

            e[alias + ".htm"] = w.Text;
        }


        public static void DefineEntryPoint(IEntryPoint e)
        {
            foreach (var v in LinqToObjects.source.js.Settings.Controls)
            {
                DefineSpawnPoint(e, v.A, Environment.MachineName);
            }

        }

    }

}
