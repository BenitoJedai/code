using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib;
using System.Xml.Serialization;
using System.Xml;

namespace jsc.Languages.JavaScript
{
    static class EntryPointProvider
    {
        static string SerializeToXML(this object e)
        {
            if (e == null)
                return "";

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

        static StreamWriter CreateFile(this DirectoryInfo dir, string filename)
        {
            FileInfo f = new FileInfo(dir.FullName + "/" + filename);

            if (f.Exists)
                f.Delete();

            StreamWriter x = new StreamWriter(new FileStream(f.FullName, FileMode.Create));


            return x;
        }

        static string ClickOnceSpawnLink(this Assembly ass, string BaseURL, string code)
        {
            var w = new StringWriter();

            //w.Write("((function(_7,_8){var _1=0,_2='onreadystatechange',_3=document.getElementsByTagName('HEAD')[0],_4,_5;for(_4 in _7){_5=document.createElement('SCRIPT');_5.src=_7[_4];_3.appendChild(_5);_5[_2 in _5?_2:'onload']=function(){var _6=_5.readyState;if(_6==null||_6=='loaded'||_6=='complete')if(++_1==_7.length)_8();};}})([");
            w.Write("((function(h,i){var a=0,b='onreadystatechange',c=document.getElementsByTagName('HEAD')[0],d,e;for(d in h){e=document.createElement('SCRIPT');e.src=h[d];c.appendChild(e);e[b in e?b:'onload']=function(){var f=e.readyState;if(f==null||f=='loaded'||f=='complete')if(++a==h.length)i();};}})([");

            var a = SharedHelper.LocalModulesOf(ass);

            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    w.Write(",");
                w.Write("'" + BaseURL + a[i] + ".js'");
            }

            w.Write("],function(){" + code + "}))");

            return w.ToString();
        }


        static void DefineSpawnPoint(this Assembly ass, StreamWriter w, string alias, string mime, string data)
        {

            w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine("<title>ScriptApplication</title>");

            w.WriteLine("<!-- created at " + System.DateTime.Now.ToString() + " -->");

            SharedHelper.DefineScript(w,
                SharedHelper.LocalModulesOf(ass)
            );

            w.WriteLine("<script></script>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");




            if (string.IsNullOrEmpty(data))
                w.WriteLine("<script type='" + mime + "' class='" + alias + "'></script>");
            else
                w.WriteLine("<script type='" + mime + "' class='" + alias + "'>\n" + data + "\n</script>");


            w.WriteLine("</body>");
            w.WriteLine("</html>");

        }

        static public void WriteEntryPoints(this Assembly a, DirectoryInfo dir)
        {
            // xxx

            foreach (var v in a.GetTypes())
            {
                var s = (ScriptApplicationEntryPointAttribute)v.GetCustomAttributes(typeof(ScriptApplicationEntryPointAttribute), false).SingleOrDefault();

                if (s != null)
                {
                    var field = v.GetField("DefaultData");

                    var data = field == null ? null : field.GetValue(null);

                    //DefineSpawnPointXML(e, v.Name, data);
                    //DefineSpawnPointJSON(e, v.Name, data);

                    Console.WriteLine("entrypoint: " + v.Name);

                    if (s.IsClickOnce)
                    {
                        using (var w = dir.CreateFile(v.Name + ".clickonce.htm"))
                        {
                            var done = "alert('done');";

                            if (data != null)
                            {
                                var ctor = (ConstructorInfo)v.GetConstructor(new[] { data.GetType() });

                                if (ctor != null)
                                {
                                    using (var x = new IdentWriter())
                                    {
                                        var h = new IL2ScriptWriterHelper(x);

                                        x.Write("new ");
                                        h.WriteWrappedConstructor(ctor);
                                        x.Write("(");

                                        x.Write(ScriptCoreLib.Tools.JSONSerializer.Serialize(data, '\''));
                                        //x.Write("null");

                                        x.Write(");");
                                        //done = "alert('" + x.ToString() + "');";

                                        done = x.ToString();
                                    }
                                }
                            }

                            w.Write("<a href=\"javascript:");
                            w.Write(a.ClickOnceSpawnLink("", done));
                            w.Write("\">Open this ScriptApplication</a>");
                        }

                        // ...
                    }
                    //else
                    //{
                    using (var w = dir.CreateFile(v.Name + ".xml.htm"))
                    {
                        a.DefineSpawnPoint(w, v.Name, "text/xml", data.SerializeToXML());
                    }

                    using (var w = dir.CreateFile(v.Name + ".json.htm"))
                    {
                        a.DefineSpawnPoint(w, v.Name, "text/json", ScriptCoreLib.Tools.JSONSerializer.Serialize(data));
                    }
                    //}
                }
            }
        }
    }
}
