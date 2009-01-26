using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace HintPathFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Z:\work\code.google\avalonugh\AvalonUgh\AvalonUgh.Code\AvalonUgh.Code.csproj

            var f = @"Z:\work\code.google\avalonugh\AvalonUgh";


            FixSolution(f);

        }

        private static void FixSolution(string f)
        {

            foreach (var csproj in Directory.GetDirectories(f).SelectMany(k => Directory.GetFiles(k, "*.csproj")))
            {
                Console.WriteLine(csproj);
                FixProject(csproj);
            }
        }

        private static void FixProject(string f)
        {
            var doc = XDocument.Load(
                f
            );

            // /Project/ItemGroup/Reference/HintPath/
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

            foreach (var h in
                from ItemGroup in doc.Root.Elements(ns + "ItemGroup")
                from Reference in ItemGroup.Elements(ns + "Reference")
                from HintPath in Reference.Elements(ns + "HintPath")
                select HintPath
                    )
            {
                // "..\\..\\..\\..\\..\\util\\jsc\\bin\\ScriptCoreLib.dll"

                Console.WriteLine(h.Value);

                h.Value = h.Value.Replace("..\\..\\..\\..\\..\\util\\jsc\\bin\\", @"c:\util\jsc\bin\");
            }

            doc.Save(f);
        }
    }
}

