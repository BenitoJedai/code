using System;
using System.Query;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.XLinq;


using ScriptCoreLib;


using PrimaryApplet = DemoApplet.source.java.DemoApplet;
using PrimaryAppletSettings = DemoApplet.source.java.DemoApplet.Settings;


namespace AppletTemplate.source.csharp
{
    class Setup
    {
        public const string SettingsFileName = "setup.settings.cmd";

        public static void DefineEntryPoint(IEntryPoint e)
        {

            var settings = new {
                ProjectName = PrimaryAppletSettings.Alias,
                CompilandNamespace0 = PrimaryAppletSettings.AliasNamespace.Replace(".", "/"),
                CompilandNamespace1 = PrimaryAppletSettings.AliasNamespace,
                AppletWebPage =  PrimaryAppletSettings.Alias + ".htm",
                CompilandType = typeof(PrimaryApplet).Name,
                CompilandFullName = PrimaryAppletSettings.AliasNamespace + "." + typeof(PrimaryApplet).Name,
                PackageName = typeof(PrimaryApplet).Name + "Package.jar",
            };

            using (var w = new StringWriter())
            {
                w.WriteLine(":: settings for current project modified at " + DateTime.Now);

                WriteSettings(w, settings);

                e[SettingsFileName] = w.ToString();
            }

            e[settings.AppletWebPage] =
                new XElement("body",
                    ToElementWithAttributes("applet",
                        new {
                            code = settings.CompilandFullName, 
                            codebase = "bin",
                            archive = settings.PackageName, 
                            width = PrimaryAppletSettings.DefaultWidth,
                            height = PrimaryAppletSettings.DefaultHeight,
                            mayscript="true"
                        }
                        , ToParameters(
                            new {
                                foo = "unused",
                                bar = "unused"
                            }
                        ).ToArray()
                    )
                ).Xml;
        }

        #region Applet HTML

        public static XElement ToElementWithAttributes(string tag, object attr, params object[] c)
        {
            return new XElement(tag,
                ToAttributes(attr).Concat(
                    new object[] {
                        ""
                        }
                ), c
            );
        }

        public static IEnumerable<XElement> ToParameters(object v)
        {
            foreach (PropertyInfo z in v.GetType().GetProperties())
            {
                yield return new XElement("param",
                        ToAttributes(
                            new {
                                name =z.Name,
                                value = z.GetValue(v, null)
                            }
                        )
                    );
            }
        }

        public static IEnumerable<object> ToAttributes(object v)
        {
            foreach (PropertyInfo z in v.GetType().GetProperties())
            {
                yield return new XAttribute(z.Name, z.GetValue(v, null));
            }
        }

        #endregion

        public static void WriteSettings(StringWriter w, object v)
        {
            foreach (PropertyInfo z in v.GetType().GetProperties())
            {
                w.WriteLine("set {0}={1}", z.Name, z.GetValue(v, null));
            }
        }
    }
}
