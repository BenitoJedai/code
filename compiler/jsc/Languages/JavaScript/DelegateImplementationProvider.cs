using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc.Languages.JavaScript.legacy
{
    class DelegateImplementationProvider
    {
        static string GetLambadaTitle(MethodInfo invoke)
        {
            StringBuilder w = new StringBuilder();

            ParameterInfo[] p = invoke.GetParameters();

            w.Append("(");

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    w.Append(", ");

                w.Append(p[i].Name);
            }

            w.Append(") => ");

            w.Append(invoke.ReturnType.Name);

            return w.ToString();
        }

        static T Single<T>(Func<T[]> e)
        {
            return Single(e());
        }

        static T Single<T>(T[] e)
        {
            if (e.Length != 1)
                throw new ArgumentException();

            return e[0];
        }

        static string MethodParamsAsString(MethodBase e)
        {
            StringBuilder w = new StringBuilder();

            ParameterInfo[] p = e.GetParameters();

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0)
                    w.Append(", ");

                w.Append(IdentWriter.GetDecoratedParameterInfo(p[i]));
            }

            return w.ToString();
        }

        static IEnumerable<KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>> ToArray(Type e)
        {

            foreach (FieldInfo v in e.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                object[] vhint = v.GetCustomAttributes(typeof(ScriptCoreLib.ScriptDelegateDataHintAttribute), false);

                if (vhint.Length == 1)
                {
                    yield return new KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>((ScriptCoreLib.ScriptDelegateDataHintAttribute)vhint[0], v);
                }
            }
        }

        /// <summary>
        /// writes the implementation for delegates that the Excecution Engine is responsible for
        /// </summary>
        /// <param name="w"></param>
        /// <param name="z"></param>
        public static void Write(IdentWriter w, Type z)
        {
            Type MulticastDelegate = w.Session.ResolveImplementation(z.BaseType);
            Type Delegate = w.Session.ResolveImplementation(z.BaseType.BaseType);

            FieldInfo FieldList = null;
            FieldInfo FieldTarget = null;
            FieldInfo FieldMethod = null;

            DelegateHint.Resolve(MulticastDelegate, Delegate,
                out FieldList,
                out FieldTarget,
                out FieldMethod
            );
            
            ConstructorInfo Constructor = Single<ConstructorInfo>(z.GetConstructors);
            MethodInfo Invoke = z.GetMethod("Invoke");

            w.WriteCommentLine("delegate: " + GetLambadaTitle(Invoke));

            
            w.Helper.DOMDefineNamedType(z, null);
            w.Helper.DefineAndAssignPrototype(z);

            // delegate ctor

            // events cannot be inherited
            // w.Helper.DefineTypeInheritanceConstructor(z, MulticastDelegate);


            ConstructorInfo MulticastDelegateConstructor = Single<ConstructorInfo>(MulticastDelegate.GetConstructors);

            w.Helper.DefineTypeMemberMethodAs(z, Constructor, MulticastDelegateConstructor);
            w.Helper.DefineTypeInheritanceConstructor(z, Constructor, MulticastDelegate);


            w.Helper.DefineTypeMemberMethodHeader(z, Invoke);
            w.WriteBeginScope();

            #region WriteForEach
            Action<__handler> WriteForEach = delegate(__handler f)
            {
                w.WriteIdent();
                w.WriteLine(string.Format("for (var i = 0; i < this.{0}.length; i++)", FieldList.Name));

                w.WriteBeginScope();

                f();
                
                w.WriteEndScope();
            };
            #endregion


            if (Invoke.ReturnType == typeof(void))
            {
                WriteForEach(
                    delegate
                    {

                        w.WriteIdent();
                        w.WriteLine(string.Format("var f = this.{0}[i];", FieldList.Name));
                        w.WriteIdent();
                        w.WriteLine(string.Format("f.{1}[f.{2}]({0});", MethodParamsAsString(Invoke), FieldTarget.Name, FieldMethod.Name));


                    }
                );

       
            }
            else
            {
                w.WriteIdent();
                w.WriteLine("var _ = void(0);");

                WriteForEach(
                   delegate
                   {
                       w.WriteIdent();
                       w.WriteLine(string.Format("var f = this.{0}[i];", FieldList.Name));
                       w.WriteIdent();
                       w.WriteLine(string.Format("_ = f.{1}[f.{2}]({0});", MethodParamsAsString(Invoke), FieldTarget.Name, FieldMethod.Name));

                   }
                );

                w.WriteIdent();
                w.WriteLine("return _;");
            }

            

            w.EndScopeAndTerminate();
            

            w.WriteLine();
        }
    }
}
