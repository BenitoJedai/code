using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web.UI;

using ScriptCoreLib;
using System.Web;

namespace jsc.server
{
    public static class WebTools
    {
        public static void CompileClientScript(Assembly a)
        {
            var moduleFile = new FileInfo(a.Location);
            var module = moduleFile.FullName;

            var dir = new FileInfo(module).Directory;

            var web = dir.CreateSubdirectory("web");

            Environment.CurrentDirectory = dir.FullName;


            jsc.Program.Main(new[] { module, "-js" });
        }

        static readonly object Token = new object();

        public static void CompileAndRegisterClientScript(this Page p, Assembly e)
        {
            lock (Token)
            {
                jsc.server.WebTools.CompileClientScript(e);

                foreach (var v in SharedHelper.ModulesOf(e))
                {
                    p.ClientScript.RegisterClientScriptInclude(v, string.Format("{0}.js", new System.IO.FileInfo(v).Name));
                }
            }
        }

        public static void CompileAndRegisterClientScript(this Page p)
        {
            CompileAndRegisterClientScript(p, Assembly.GetCallingAssembly());
        }

        public static bool VirtualRequest(this HttpContext e)
        {
            return VirtualRequest(e, Assembly.GetCallingAssembly());
        }

        public static bool VirtualRequest(this HttpContext e, Assembly a)
        {
            if (System.IO.File.Exists(e.Request.PhysicalPath))
            {
                return false;
            }

            string web = new System.IO.FileInfo(a.Location).Directory + "/web";

            string target = web + HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Substring(1);

            if (System.IO.File.Exists(target))
            {
                HttpContext.Current.Response.TransmitFile(target);
                //HttpContext.Current.Response.WriteFile(target);
                HttpContext.Current.Response.End();
                return true;
            }

            return false;
        }
    }
}
