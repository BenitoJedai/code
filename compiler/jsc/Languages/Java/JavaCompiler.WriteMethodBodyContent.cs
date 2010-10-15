
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        protected override void WriteMethodBodyContent(ILBlock xb, Action h)
        {
            // should we wrap our memory manager?

            this.WriteIndent();
            this.WriteKeyword(Keywords._try);
            this.WriteLine();

            using (this.CreateScope())
            {
                h();
            }

            this.WriteIndent();
            this.WriteKeyword(Keywords._finally);
            this.WriteLine();

            using (this.CreateScope())
            {
            }
        }

    }
}
