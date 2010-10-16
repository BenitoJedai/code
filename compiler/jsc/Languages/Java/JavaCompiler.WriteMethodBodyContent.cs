
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
using System.Linq;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;
using ScriptCoreLib.Shared;
using jsc.meta.Library;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        protected override void WriteMethodBodyContent(ILBlock xb, Action h)
        {
            var q = from i in xb.Instructrions
                    let m = i.ReferencedMethod
                    where m != null
                    where m.IsConstructor
                    where m.DeclaringType == typeof(IntPtr)
                    let p = m.GetParameters()
                    where p.Length == 1
                    where p[0].ParameterType == typeof(void*)
                    select new { i, m, p };

            if (!q.Any())
            {
                // no jni stuff needed..

                h();
                return;
            }

            var CreateCMallocCollector = ((Func<IDisposable>)PlatformInvocationServices.CreateCMallocCollector).Method;
            var Dispose = typeof(IDisposable).GetMethods().Single();

            // should we wrap our memory manager?
            this.WriteIndent();
            this.WriteDecoratedTypeName(Dispose.DeclaringType);
            this.WriteSpace();
            this.WriteSafeLiteral("_" + CreateCMallocCollector.MetadataToken.ToString("x8"));
            this.WriteAssignment();
            this.WriteDecoratedTypeName(CreateCMallocCollector.DeclaringType);
            this.Write(".");
            this.WriteDecoratedMethodName(CreateCMallocCollector, false);
            this.Write("(");
            this.Write(")");
            this.WriteLine(";");

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
                this.WriteIndent();
                this.WriteSafeLiteral("_" + CreateCMallocCollector.MetadataToken.ToString("x8"));
                this.Write(".");
                this.WriteDecoratedMethodName(Dispose, false);
                this.Write("(");
                this.Write(")");
                this.WriteLine(";");
            }
        }

    }
}
