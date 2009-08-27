using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

using ScriptCoreLib;

using EntryPointProgram = OrcasNativeJavaConsoleApplication.source.java.Program;
using EntryPointSettings = OrcasNativeJavaConsoleApplication.source.java.Settings;



namespace AppletTemplate.source.csharp
{
   

    class Setup
    {
        public const string SettingsFileName = "setup.settings.cmd";

        public static void DefineEntryPoint(IEntryPoint e)
        {

            var settings = new
            {
                ProjectName = EntryPointSettings.Alias,
                CompilandNamespace0 = EntryPointSettings.AliasNamespace.Replace(".", "/"),
                CompilandNamespace1 = EntryPointSettings.AliasNamespace,
                CompilandType = typeof(EntryPointProgram).Name,
                CompilandFullName = EntryPointSettings.AliasNamespace + "." + typeof(EntryPointProgram).Name,
                PackageName = EntryPointSettings.Alias + ".jar",
            };

            using (var w = new StringWriter())
            {
                w.WriteLine(":: settings for current project modified at " + DateTime.Now);

                WriteSettings(w, settings);

                e[SettingsFileName] = w.ToString();
            }


            e["release/META-INF/MANIFEST.MF"] =
                "Main-Class: " + settings.CompilandFullName + Environment.NewLine +
                "Created-By: jsc.sf.net";
        }


        public static void WriteSettings(StringWriter w, object v)
        {
            foreach (PropertyInfo z in v.GetType().GetProperties())
            {
                w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
            }
        }
    }
}
