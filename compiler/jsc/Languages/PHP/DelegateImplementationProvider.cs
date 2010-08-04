using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc.Languages.PHP
{
    public delegate T Func<A, B, T>(A a, B b);
    public delegate void Action<A, B>(A a, B b);

    class DelegateImplementationProvider
    {


        public static void Write(jsc.Script.PHP.PHPCompiler w, Type z)
        {
            Type MulticastDelegate = w.MySession.ResolveImplementation(z.BaseType);
            Type Delegate = w.MySession.ResolveImplementation(z.BaseType.BaseType);

            w.WriteImport(w.TypeInfoOf(MulticastDelegate));

            FieldInfo FieldList = null;
            FieldInfo FieldTarget = null;
            FieldInfo FieldMethod = null;

            DelegateHint.Resolve(MulticastDelegate, Delegate,
                out FieldList,
                out FieldTarget,
                out FieldMethod
            );

            //ConstructorInfo Constructor = Single<ConstructorInfo>(z.GetConstructors);

            MethodInfo Invoke = z.GetMethod("Invoke");

            w.WriteCommentLine("delegate: " + z.Name);

            w.WriteIndent();
            w.Write("class ");
            w.WriteDecoratedTypeName(z);
            w.Write(" extends ");
            w.WriteSpace();
            w.WriteDecoratedTypeName(MulticastDelegate);
            w.WriteLine();

            using (w.CreateScope())
            {
                w.WriteIndent();
                w.WriteLine("function __construct($object, $method) { parent::__construct($object, $method); }");


                w.WriteMethodSignature(z, Invoke, false);


                using (w.CreateScope())
                {

                    #region WriteForEach
                    Action<Action> WriteForEach = delegate(Action f)
                    {
                        w.WriteIndent();
                        w.WriteLine(string.Format("foreach ($this->{0} as $f)", FieldList.Name));

                        using (w.CreateScope()) f();
                    };
                    #endregion


                    Func<string> GetParams =
                        delegate
                        {
                            StringBuilder s = new StringBuilder();

                            ParameterInfo[] p = Invoke.GetParameters();

                            for (int i = 0; i < p.Length; i++)
                            {
                                if (i > 0)
                                    s.Append(", ");

                                s.Append(w.GetDecoratedMethodParameter(p[i]));
                            }
                            


                            return s.ToString();
                        };

                    Action<string, string> st_loc =
                        delegate (string local, string value)
                        {
                            w.WriteIndent();
                            w.WriteLine("$" + local + " = " + value + ";");
                        };
                    
                    Func<string, string, string> ld_fld =
                        delegate (string local, string field)
                        {
                            return "$" + local + "->" + field; 
                        };

                    
                    if (Invoke.ReturnType == typeof(void))
                    {
                        WriteForEach(
                            delegate
                            {

                                st_loc("t", ld_fld("f", FieldTarget.Name));
                                st_loc("m", ld_fld("f", FieldMethod.Name));

                                w.WriteIndent();
                                w.WriteLine(string.Format("if ($t) $t->$m({0}); else $m({0});",
                                    GetParams()
                                    ));

                            }
                        );
                    }
                    else
                    {
                        w.WriteIndent();
                        w.WriteLine("$_ = NULL;");

                        WriteForEach(
                            delegate
                            {

                                st_loc("t", ld_fld("f", FieldTarget.Name));
                                st_loc("m", ld_fld("f", FieldMethod.Name));

                                w.WriteIndent();
                                w.WriteLine(string.Format("if ($t) $_ = $t->$m({0}); else $_ = $m({0});",
                                 GetParams()
                                 ));

                            }
                        );

                        w.WriteIndent();
                        w.WriteLine("return $_;");
                    }
                }

            }
        }
    }
}
