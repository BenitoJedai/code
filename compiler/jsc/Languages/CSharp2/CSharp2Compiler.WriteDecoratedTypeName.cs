using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using System.IO;

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

        public void WriteDecoratedTypeNameOrImplementationTypeName(
            Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName,
            WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(timpv, favorPrimitives, favorTargetType, UseFullyQualifiedName, Mode, null, null);
        }

        public void WriteDecoratedTypeNameOrImplementationTypeName(
            Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName,
            WriteDecoratedTypeNameOrImplementationTypeNameMode Mode,
            Type context,
            Dual<Queue<Type>> GenericArguments
            )
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
                        WriteDecoratedTypeNameOrImplementationTypeName(
                            t.GetElementType(),
                            favorPrimitives,
                            favorTargetType,
                            UseFullyQualifiedName
                            );

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
                        WriteDecoratedTypeNameAndNested(context, GenericArguments, t);


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

            WriteGenericTypeName(context, subject);
            //WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject));

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


        void WriteDecoratedTypeNameAndNested(Type context, Dual<Queue<Type>> GenericArguments, Type z)
        {

            var t = z.DeclaringTypesToStack(true);


            var i = 0;

            foreach (var p in t)
            {
                if (i > 0)
                    Write(".");

                Write(GetShortName(p));

                if (p.IsGenericType)
                {
                    var a = new Queue<Type>(p.GetGenericTypeDefinition().GetGenericArguments());
                    var b = new Queue<Type>();

                    foreach (var v in a)
                    {
                        if (GenericArguments.Left.Count > 0)
                            if (v.GenericParameterPosition == GenericArguments.Left.Peek().GenericParameterPosition)
                            {
                                GenericArguments.Left.Dequeue();
                                b.Enqueue(GenericArguments.Right.Dequeue());
                            }

                    }

                    if (b.Count > 0)
                        WriteGenericTypeParameters(context, b.ToArray());
                }

                // z.DeclaringType.GetGenericArguments()[0].GenericParameterPosition
                // z.GetGenericTypeDefinition().GetGenericArguments()[0].GenericParameterPosition

                // WriteGenericTypeParameters(context, g, p.GetGenericArguments().Length);

                i++;
            }

            if (GenericArguments != null)
                if (GenericArguments.Right.Count > 0)
                {
                    DebugBreak();
                }
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


        public class Dual<T>
        {
            public T Left;
            public T Right;
        }

        private void WriteGenericTypeName(Type context, Type subject)
        {
            var ToBeWritten = default(Dual<Queue<Type>>);

            if (subject.IsGenericType)
            {
                ToBeWritten = new Dual<Queue<Type>>
                {
                    Left = new Queue<Type>(subject.GetGenericTypeDefinition().GetGenericArguments()),
                    Right = new Queue<Type>(subject.GetGenericArguments()),
                };
            }

            WriteQualifiedTypeName(context, ToBeWritten, subject);

            if (ToBeWritten != null)
                WriteGenericTypeParameters(context, ToBeWritten.Right.ToArray());
        }



        public void WriteGenericTypeParameters(Type context, MethodBase subject)
        {
            if (!subject.IsGenericMethod)
                return;

            var p = subject.GetGenericArguments();

            Write("<");

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    Write(", ");

                WriteGenericTypeName(context, p[i]);
            }


            Write(">");
        }



        public void WriteGenericTypeParameters(Type context, Type[] p)
        {
            if (p.Length == 0)
                return;

            Write("<");

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    Write(", ");

                WriteGenericTypeName(context, p[i]);
            }


            Write(">");

        }

        public void WriteGenericTypeParameters(Type context, Type subject)
        {
            if (!subject.IsGenericType)
                return;

            var p = subject.GetGenericArguments();

            if (p.Length == 0)
            {
                return;
            }

            Write("<");

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    Write(", ");

                WriteGenericTypeName(context, p[i]);
            }


            Write(">");

        }
    }
}
