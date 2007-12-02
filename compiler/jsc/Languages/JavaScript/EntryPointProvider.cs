﻿using System;
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
        #region ClickOnceTemplate
        static string _ClickOnceTemplate;

        static public string ClickOnceTemplate
        {
            get
            {
                if (_ClickOnceTemplate == null)
                    _ClickOnceTemplate = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.Languages.JavaScript.$clickonce$.js")).ReadToEnd()
                        .Replace("\r\n", "")
                        .Replace("\t", "")
                        .ReplaceSpace("{", "}", ",", "=");

                return _ClickOnceTemplate;
            }
        }
        #endregion


        static string ClickOnceSpawnLink(this Assembly ass, string BaseURL, string code)
        {
            var w = new StringWriter();

            //w.Write("((function(_7,_8){var _1=0,_2='onreadystatechange',_3=document.getElementsByTagName('HEAD')[0],_4,_5;for(_4 in _7){_5=document.createElement('SCRIPT');_5.src=_7[_4];_3.appendChild(_5);_5[_2 in _5?_2:'onload']=function(){var _6=_5.readyState;if(_6==null||_6=='loaded'||_6=='complete')if(++_1==_7.length)_8();};}})([");
            //w.Write("((function(h,i){var a=0,b='onreadystatechange',c=document.getElementsByTagName('HEAD')[0],d,e;for(d in h){e=document.createElement('SCRIPT');e.src=h[d];c.appendChild(e);e[b in e?b:'onload']=function(){var f=e.readyState;if(f==null||f=='loaded'||f=='complete')if(++a==h.length)i();};}})([");

            var a = SharedHelper.LocalModulesOf(ass);

            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    w.Write(",");
                w.Write("'" + BaseURL + a[i] + ".js'");
            }

            //w.Write("],function(){" + code + "}))");

            return ClickOnceTemplate
                .ReplaceSpace("    ", "   ", "  ", " ")
                .Replace("$references$", w.ToString())
                .Replace("$done$", code);
        }

        static void WriteEntryPointHTMLTemplate(StreamWriter w, Action WriteScript, Action WriteBody)
        {
            w.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine("<title>ScriptApplication</title>");

            w.WriteLine("<!-- created at " + System.DateTime.Now.ToString() + " -->");

            WriteScript();

            w.WriteLine("<script></script>");

            w.WriteLine("</head>");
            w.WriteLine("<body>");
            w.WriteLine("<noscript>ScriptApplication cannot run without JavaScript!</noscript>");


            WriteBody();


            w.WriteLine("</body>");
            w.WriteLine("</html>");
        }


        static void DefineSpawnPoint(this Assembly ass, StreamWriter w, string alias, string mime, string data)
        {
            WriteEntryPointHTMLTemplate(w,
                () => SharedHelper.DefineScript(w, SharedHelper.LocalModulesOf(ass)),
                () => w.WriteLine("<script type='" + mime + "' class='" + alias + "'>" + (string.IsNullOrEmpty(data) ? "" : "\n" + data + "\n") + "</script>")
            );



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
                            var done = "/* ctor not found */";
                            var ctor = default(ConstructorInfo);

                            #region ctor(typeof(DefaultData))
                            if (data != null && (ctor = v.GetConstructor(field.FieldType)) != null)
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
                            #endregion
                            #region ctor()
                            else if ((ctor = v.GetConstructor()) != null)
                                using (var x = new IdentWriter())
                                {
                                    var h = new IL2ScriptWriterHelper(x);

                                    x.Write("new ");
                                    h.WriteWrappedConstructor(ctor);
                                    x.Write("();");

                                    done = x.ToString();
                                }
                            #endregion


                            WriteEntryPointHTMLTemplate(
                                w, () => { },
                                delegate
                                {
                                    w.Write("<a href=\"javascript:");
                                    w.Write(a.ClickOnceSpawnLink("", done));
                                    w.Write("\">Click to open <b>" + v.Name + "</b></a>");
                                }
                            );
                        }

                        // ...
                    }


                    if ((s.Format & SerializedDataFormat.xml) == SerializedDataFormat.xml)
                        using (var w = dir.CreateFile(v.Name + ".xml.htm"))
                        {
                            a.DefineSpawnPoint(w, v.Name, "text/xml", data.SerializeToXML());
                        }

                    if ((s.Format & SerializedDataFormat.json) == SerializedDataFormat.json)
                        using (var w = dir.CreateFile(v.Name + ".json.htm"))
                        {
                            a.DefineSpawnPoint(w, v.Name, "text/json", data == null ? null : data.SerializeToJSON());
                        }
                }
            }
        }
    }
}
