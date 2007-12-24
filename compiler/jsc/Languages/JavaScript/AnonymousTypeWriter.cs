using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Languages.JavaScript
{


    static public class AnonymousTypeWriter
    {
        /*
           w.WriteVariableAssignment(
                IdentWriter.GetSpecialChar(1) + w.GetDecoratedGuid(assembly.ManifestModule.ModuleVersionId),
                new { FullName = assembly.FullName }
            );
    */
        static public void WriteVariableAssignment(this IdentWriter w, Guid name, object value)
        {
            WriteVariableAssignment(w, IdentWriter.GetGUID64(name), value);
        }

        static public void WriteVariableAssignment(this IdentWriter w, string name, object value)
        {
            w.WriteIdent();
            w.Write("var " + name);
            w.Helper.WriteAssignment();
            w.Write(value.SerializeToJSON());
            w.WriteLine(";");
        }

        static public void WriteMemberAssignment(this IdentWriter w, Guid name, object value)
        {
            WriteMemberAssignment(w, IdentWriter.GetGUID64(name), value);
        }

        static public void WriteMemberAssignment(this IdentWriter w, string name, object value)
        {
            foreach (var p in value.GetProperties())
            {
                w.WriteIdent();
                w.Write(name);
                w.Helper.WriteAccessor();
                w.Write(p.Key);
                w.Helper.WriteAssignment();
                w.Write(p.Value.SerializeToJSON());
                w.WriteLine(";");
            }
        }
    }
}
