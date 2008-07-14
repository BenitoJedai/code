using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(timpv, favorPrimitives, favorTargetType, UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode.Default);
        }

        public enum WriteDecoratedTypeNameOrImplementationTypeNameMode
        {
            Default,
            IgnoreImplementationType
        }

        public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
        {

            if (timpv.IsGenericParameter)
            {
                WriteSafeLiteral(timpv.Name);
                return;
            }



            //[Script(Implements = typeof(global::System.Boolean),
            //    ImplementationType=typeof(java.lang.Integer))]

            if (NativeTypes.ContainsKey(timpv))
            {
                // write native
                Write(NativeTypes[timpv]);
                return;
            }


            var iType = MySession.ResolveImplementation(timpv);

            if (iType != null)
            {
                if (favorTargetType)
                {
                    var s = iType.ToScriptAttribute();

                    if (s.ImplementationType != null)
                        iType = s.ImplementationType;
                }
            }



            var WriteTypeName = default(Action<Type>);
            
            WriteTypeName =
                t =>
                {
                    if (t.IsArray)
                    {
                        WriteTypeName(t.GetElementType());
                        Write("[]");
                    }
                    else
                    {
                        var ns = NamespaceFixup(t.Namespace);

                        if (UseFullyQualifiedName && !string.IsNullOrEmpty(ns))
                        {
                            Write(ns);
                            Write(".");
                        }

                        //WriteSafeLiteral(GetDecoratedTypeName(t, true));
                        Write(GetDecoratedTypeName(t, true));
                    }
                };

            if (iType == null)
            {
                var s = timpv.ToScriptAttribute();

                if (!(Mode == WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType) && s != null && s.ImplementationType != null)
                    WriteTypeName(s.ImplementationType);
                else
                    WriteTypeName(timpv);

            }
            else
            {
                WriteTypeName(iType);
            }
        }



        public override void WriteDecoratedTypeName(Type context, Type subject)
        {
            // used by OpCodes.Newobj

            WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject));

        }

        public void WriteDecoratedTypeName(Type context, Type subject, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject), Mode);

        }

        public override void WriteDecoratedMethodName(MethodBase z, bool q)
        {
            if (q)
                throw new NotSupportedException();

            WriteSafeLiteral(z.Name);
        }

        public override string GetDecoratedTypeName(Type z, bool bExternalAllowed)
        {
            var p = z;
            var s = GetShortName(p);

            while (p.DeclaringType != null)
            {
                p = p.DeclaringType;
                s = GetShortName(p) + "." + s;
            }

            return s;
        }

        public override string GetDecoratedTypeNameWithinNestedName(Type z)
        {
            return GetDecoratedTypeName(z, false);
        }

        private string GetShortName(Type z)
        {
            if (z.IsGenericType)
            {
                var g = z.Name.IndexOf('`');

                if (g >= 0)
                    return GetSafeLiteral(z.Name.Substring(0, g));
            }

            return GetSafeLiteral(z.Name);
        }
    }
}
