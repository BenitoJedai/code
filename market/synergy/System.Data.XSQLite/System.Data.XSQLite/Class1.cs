using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSystem.Data.XSQLite
{
    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140321


        //rewriting... primary types: 329

        //0a8c:02:01 RewriteToAssembly error: System.InvalidOperationException: System.Data.XSQLite.Class1 ---> System.ArgumentOutOfRangeException: Length cannot be less than zero.
        //Parameter name: length
        //   at System.String.InternalSubStringWithChecks(Int32 startIndex, Int32 length, Boolean fAlwaysCopy)
        //   at System.String.Substring(Int32 startIndex, Int32 length)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.NamespaceRenameInstructions.get_From()
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalFullNameFixup(String n)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.FullNameFixup(String n, Type ContextType)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeDefinition.DefineType(TypeBuilder _DeclaringType, String TypeName, Type BaseType, TypeAttributes TypeAttributes)

    }
}
