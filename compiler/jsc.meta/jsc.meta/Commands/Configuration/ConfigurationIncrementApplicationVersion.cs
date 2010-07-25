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

        public override void Invoke()
        {
            Console.WriteLine("ConfigurationIncrementApplicationVersion");

            var csproj = XDocument.Load(ProjectFileName.FullName);

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var nsPropertyGroup = ns + "PropertyGroup";
            var nsApplicationRevision = ns + "ApplicationRevision";

            foreach (var item in csproj.Root.Elements(nsPropertyGroup).Elements(nsApplicationRevision))
            {
                var n = int.Parse(item.Value) + 1;
                Console.WriteLine("version: " + n);

                item.Value = "" + n;
            }

            csproj.Save(ProjectFileName.FullName);
        }
    }
}
