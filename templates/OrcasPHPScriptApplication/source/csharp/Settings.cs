using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;

namespace ScriptApplication.source.csharp
{
    static class Settings
    {
        public static void DefineEntryPoint(IEntryPoint e)
        {
            CreatePHPIndexPage(e, php.OrcasPHPScriptApplicationBackend.Filename, php.OrcasPHPScriptApplicationBackend.Entrypoint);
        }

        private static void CreatePHPIndexPage(IEntryPoint e, string file_name, string entryfunction)
        {
            var w = new TextWriter();

            w.WriteLine("<?");

            SharedHelper.PHPInclude(w, SharedHelper.LocalModules);

            w.WriteLine(entryfunction + "();");

            w.Write("?>");

            e[file_name] = w.Text;
        }
    }

}
