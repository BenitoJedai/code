using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace TextScreenSaver
{
    class IAssemblyReferenceToken :
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
    }

    static class Setup
    {
        // http://weblogs.asp.net/cazzu/archive/2004/01/23/62141.aspx
       

        static string SerializeToXML(this object e)
        {
            var s = new XmlSerializer(e.GetType());
            var w = new StringWriter();
            var z =
                XmlWriter.Create(
                    w,
                    new XmlWriterSettings
                    {
                        
                        Indent = true,
                        OmitXmlDeclaration = true
            
                    }
                );
            

            s.Serialize(z, e);

            return w.ToString();
        }

        public static void DefineEntryPoint(IEntryPoint e)
        {
            DefineSpawnPointXML(e, js.Class1.Alias, js.Class1.DefaultData);
        }

        static void DefineSpawnPointXML(IEntryPoint e, string alias, object data)
        {
            var w = new ScriptCoreLib.Shared.TextWriter();

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




            // http://msdn2.microsoft.com/en-us/library/ms766512.aspx
            w.WriteLine("<script type='text/xml' class='" + alias + "'>\n" + data.SerializeToXML() + "\n</script>");


            w.WriteLine("</body>");
            w.WriteLine("</html>");

            e[alias + ".htm"] = w.Text;
        }

        static void DefineSpawnPoint(IEntryPoint e, string alias, string data)
        {
            var w = new ScriptCoreLib.Shared.TextWriter();

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
