using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;

using ScriptCoreLib.Shared;

namespace SpaceInvaders.source.csharp
{
    static class Settings
    {
        static Type ScriptCoreLib_Query { get { return typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken); } }

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
            foreach (var v in SpaceInvaders.source.js.Settings.Controls)
            {
                DefineSpawnPoint(e, v.A, Environment.MachineName);
            }

            
            e["SpawnSpaceInvaders.htm"] = ClickOnceSpawnPage("http://jsc.sourceforge.net/examples/web/SpaceInvaders/", "SpawnSpaceInvaders");
        }

        static string ClickOnceSpawnPage(string BaseURL, string SpawnFunction)
        {
            var w = new TextWriter();

            var href = ClickOnceSpawnLink(BaseURL, SpawnFunction + "('" + BaseURL + "');");

            w.Write("<a href=\"javascript:");
            w.Write(href);
            w.Write("\">open</a>");

            return w.Text;
        }

        static string ClickOnceSpawnLink(string BaseURL, string code)
        {
            var w = new TextWriter();

            w.Write("((function(_7,_8){var _1=0,_2='onreadystatechange',_3=document.getElementsByTagName('HEAD')[0],_4,_5;for(_4 in _7){_5=document.createElement('SCRIPT');_5.src=_7[_4];_3.appendChild(_5);_5[_2 in _5?_2:'onload']=function(){var _6=_5.readyState;if(_6==null||_6=='loaded'||_6=='complete')if(++_1==_7.length)_8();};}})([");

            var a = SharedHelper.LocalModules;

            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    w.Write(",");
                w.Write("'" + BaseURL + a[i] + ".js.packed.js'");
            }

            w.Write("],function(){" + code + "}))");

            return w.Text;
        }
    }

}
