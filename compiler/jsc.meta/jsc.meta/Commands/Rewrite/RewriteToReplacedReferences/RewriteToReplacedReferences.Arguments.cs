using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Rewrite.RewriteToReplacedReferences
{
    partial class RewriteToReplacedReferences
    {
        public FileInfo Assembly;
        public FileInfo Output;

        public ReferenceInfo[] References = new ReferenceInfo[0];

        public bool DefaultToOrcas;

        public ReferenceInfo[] ReferencesForOrcas = new[]
        {
            new ReferenceInfo { Name = "mscorlib", Version = "2:0:0:0", PublicKeyToken = "B7 7A 5C 56 19 34 E0 89" },
            new ReferenceInfo { Name = "System", Version = "2:0:0:0", PublicKeyToken = "B7 7A 5C 56 19 34 E0 89" },
            new ReferenceInfo { Name = "System.Core", Version = "3:5:0:0", PublicKeyToken = "B7 7A 5C 56 19 34 E0 89" },
            new ReferenceInfo { Name = "Microsoft.VisualBasic", Version = "8:0:0:0", PublicKeyToken = "B0 3F 5F 7F 11 D5 0A 3A" },
        };

        public bool UseReferencesForSilverlight;

        public class ReferenceInfo
        {
            public string Name;

            public string Version;
            public string PublicKeyToken;

            public static implicit operator ReferenceInfo(string e)
            {
                const string s = ";";

                return new ReferenceInfo
                {
                    Name = e.TakeUntilOrEmpty(s),
                    Version = e.SkipUntilOrEmpty(s).TakeUntilOrEmpty(s),
                    PublicKeyToken = e.SkipUntilOrEmpty(s).SkipUntilOrEmpty(s),
                };
            }
        }

        public FileInfo ilasm20 = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\ilasm.exe");

        public FileInfo ilasm = new FileInfo(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe");

        public FileInfo ildasm = new FileInfo(@"C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\ildasm.exe");

        public FileInfo PEVerify = new FileInfo(@"C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\PEVerify.exe");

        public bool AttachDebugger;


        /*
         
         
         * Google suggests:
         * http://www.nokola.com/trycsharp/HowToBuild.aspx
         * http://forums.silverlight.net/forums/p/102774/234470.aspx
         * http://stackoverflow.com/questions/1159800/silverlight-support-for-dynamic-code
         * http://www.codeproject.com/KB/silverlight/SLAssemblies.aspx
         * http://biofractal.blogspot.com/2009/03/convert-dotnet-assemblies-to.html
         * http://www.netfxharmonics.com/2008/12/Reusing-NET-Assemblies-in-Silverlight
         * http://forums-silverlight-dit.neudesic.com/forums/p/4495/171796.aspx
         * http://timross.wordpress.com/category/silverlight/
         * http://forums.silverlight.net/forums/t/23489.aspx
         * http://neilmosafi.blogspot.com/2008/03/silverlight-projects-vs-normal-projects.html
         */
    }
}
