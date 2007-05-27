using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

[assembly: ScriptResources("assets/WebCalculator")]

namespace FormsExample
{
    static class Setup
    {
        static IEnumerable<Type> References
        {
            get
            {
                yield return typeof(ScriptCoreLib.JavaScript.Windows.Forms.AssemblyReferenceToken);
                yield return typeof(ScriptCoreLib.JavaScript.Drawing.AssemblyReferenceToken);
                yield return typeof(ScriptCoreLib.JavaScript.AssemblyReferenceToken);
            }
        }
        
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

            var n = SharedHelper.LocalModules;

            for (int i = 0; i < n.Length; i++)
            {
                //if (i == n.Length - 1)
                //    w.WriteLine("<script>var basector$eX9CB8IfMja19AK1G6ROGA = basector$DB95z3a_b_bjaciaa25RopEA;</script>");

                SharedHelper.DefineScript(w, n[i]);    
            }
            

            
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
