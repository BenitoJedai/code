using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace jsc.meta.Commands.Configuration
{
    public class ConfigurationIncrementApplicationVersion : CommandBase
    {
        public FileInfo ProjectFileName;

        public bool AutoApplicationRevision;

        public override void Invoke()
        {
            Console.WriteLine("ConfigurationIncrementApplicationVersion");

            var csproj = XDocument.Load(ProjectFileName.FullName);

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var nsPropertyGroup = ns + "PropertyGroup";
            var nsApplicationRevision = ns + "ApplicationRevision";
            var nsApplicationVersion = ns + "ApplicationVersion";

            // <ApplicationRevision>20110302</ApplicationRevision>
            // <ApplicationVersion>1.0.0.%2a</ApplicationVersion>

            if (AutoApplicationRevision)
            {
                foreach (var item in csproj.Root.Elements(nsPropertyGroup).Elements(nsApplicationRevision))
                {
                    item.Value = "" 
                        + (DateTime.Now.Hour * 60 + DateTime.Now.Minute);
                }

                foreach (var item in csproj.Root.Elements(nsPropertyGroup).Elements(nsApplicationVersion))
                {
                    item.Value = "1."

                        + DateTime.Now.ToString("yyyy") + "."
                        + DateTime.Now.ToString("MMdd") + "."
                        + (DateTime.Now.Hour * 60 + DateTime.Now.Minute);
                }
            }
            else
            {

                foreach (var item in csproj.Root.Elements(nsPropertyGroup).Elements(nsApplicationRevision))
                {
                    var n = "" + (int.Parse(item.Value) + 1);

                    Console.WriteLine("version: " + n);

                    item.Value = "" + n;
                }
            }

            csproj.Save(ProjectFileName.FullName);
        }
    }
}
