﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace OrcasScriptApplication
{
    static class Setup
    {
        static Type Reference_ScriptCoreLib_Query = typeof(global::ScriptCoreLib.Shared.Query.AssemblyReferenceToken);
        static Type Reference_ScriptCoreLib = typeof(global::ScriptCoreLib.Shared.AssemblyReferenceToken);

        public static void DefineEntryPoint(IEntryPoint e)
        {
            DefineSpawnPoint(e, js.Class1.Alias, js.Class1.DefaultData);
            
        }

        static void DefineSpawnPoint(IEntryPoint e, string alias, string data)
        {
            var w = new TextWriter();

            w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine("<title>ScriptApplication</title>");

            w.WriteLine("<!-- created at " + System.DateTime.Now.ToString() + " -->");
            
            SharedHelper.DefineScript(w,
                SharedHelper.LocalModules
            );

            w.WriteLine("<script></script>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");

            SharedHelper.DefineSpawnPoint(w, alias, data);

            w.WriteLine("</body>");
            w.WriteLine("</html>");

            e[alias + ".htm"] = w.Text;
        }
    }
}
