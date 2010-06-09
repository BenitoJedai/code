using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.meta.Commands.Rewrite;
using System.IO;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
    public class IDLCompiler
    {
        public string DefaultNamespace;
        public XElement BodyElement;
        public RewriteToAssembly r;
        public Func<string, FileInfo> GetLocalResource;

        public void Define()
        {
            (from a in BodyElement.XPathSelectElements("//a")
             let href = a.Attribute("href").Value
             where href.EndsWith(".idl")
             let f = GetLocalResource(href)
             select new { a, f }
            ).WithEach(
                k =>
                {
                    // { 
                    // a = {<a href="CanvasRenderingContext2D.idl">CanvasRenderingContext2D</a>}, 
                    // f = {W:\jsc.svn\core\ScriptCoreLib.Ultra.Components\ScriptCoreLib.Ultra.Components.IDL\Design\IDLFiles\CanvasRenderingContext2D.idl} 
                    // }
                }
            );

        }
    }
}
