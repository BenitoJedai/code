using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace jsc.Languages.Java
{
	static class DelegateImplementationProvider
	{

		const string IsExtensionMethod = "IsExtensionMethod";
		public const string AsExtensionMethod = "AsExtensionMethod";


		public static void WriteConstructor(JavaCompiler w, ConstructorInfo m)
		{

			w.WriteIdent();
			w.WriteKeyword(JavaCompiler.Keywords._super);
			w.Write("(");
			w.WriteDecoratedMethodParameter(m.GetParameters()[0]);
			w.Write(", ");
			w.WriteDecoratedMethodParameter(m.GetParameters()[1]);
			w.Write(")");
			w.WriteLine(";");
		}

		public static void WriteBeginInvoke(JavaCompiler w, MethodInfo m)
		{
			var z = m.DeclaringType;

			Type MulticastDelegate = w.MySession.ResolveImplementation(z.BaseType);
			Type Delegate = w.MySession.ResolveImplementation(z.BaseType.BaseType);
			Type IntPtr = w.MySession.ResolveImplementation(typeof(IntPtr));


			w.WriteIdent();
			w.WriteKeywordSpace(JavaCompiler.Keywords._return);
			w.WriteKeyword(JavaCompiler.Keywords._null);
			w.WriteLine(";");
		}

		public static void WriteInvoke(JavaCompiler w, MethodInfo m)
		{
			var z = m.DeclaringType;

			Type MulticastDelegate = w.MySession.ResolveImplementation(z.BaseType);
			Type Delegate = w.MySession.ResolveImplementation(z.BaseType.BaseType);
			FieldInfo Target = Delegate.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Single(k => k.FieldType == typeof(object));
			FieldInfo Method = Delegate.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Single(k => k.FieldType == typeof(MethodInfo));

			Action dummy = () => { };
			Func<object, object[], object> MethodInfo_Invoke = dummy.Method.Invoke;

			w.WriteIdent();

			if (m.ReturnType != typeof(void))
			{
				w.WriteKeywordSpace(JavaCompiler.Keywords._return);
				w.Write("(");
				w.WriteDecoratedTypeName(m.ReturnType);
				w.Write(")");
			}
	


			w.WriteKeyword(JavaCompiler.Keywords._this);
			w.Write(".");
			w.WriteSafeLiteral(Method.Name);
			w.Write(".");
			w.WriteDecoratedMethodName(MethodInfo_Invoke.Method, false);
			w.Write("(");

			#region Target
			w.WriteKeyword(JavaCompiler.Keywords._this);
			w.Write(".");
			w.WriteSafeLiteral(IsExtensionMethod);

			w.Write("?");

			w.WriteKeyword(JavaCompiler.Keywords._null);

			w.Write(":");

			w.WriteKeyword(JavaCompiler.Keywords._this);
			w.Write(".");
			w.WriteSafeLiteral(Target.Name);

			#endregion

			w.Write(", ");

			var p = m.GetParameters();

			#region Parameters
			w.WriteKeyword(JavaCompiler.Keywords._this);
			w.Write(".");
			w.WriteSafeLiteral(IsExtensionMethod);

			w.Write("?");


			w.WriteKeywordSpace(JavaCompiler.Keywords._new);
			w.WriteDecoratedTypeName(typeof(object[]));
			w.WriteSpace();
			w.Write("{");

			w.WriteKeyword(JavaCompiler.Keywords._this);
			w.Write(".");
			w.WriteSafeLiteral(Target.Name);
			

			for (int i = 0; i < p.Length; i++)
			{
				w.Write(", ");

				w.WriteDecoratedMethodParameter(p[i]);
			}
			w.Write("}");

			w.Write(":");

			w.WriteKeywordSpace(JavaCompiler.Keywords._new);
			w.WriteDecoratedTypeName(typeof(object[]));
			w.WriteSpace();
			w.Write("{");

			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
					w.Write(", ");

				w.WriteDecoratedMethodParameter(p[i]);
			}
			w.Write("}");
			#endregion

			w.Write(")");
			w.WriteLine(";");
		}

		public static void WriteExtensionMethodSupport(JavaCompiler w, Type z)
		{
			w.WriteIdent();
			w.WriteDecoratedTypeName(typeof(bool));

			w.WriteSpace();
			w.WriteSafeLiteral(IsExtensionMethod);
			w.WriteLine(";");

			w.WriteIdent();
			w.WriteKeywordSpace(JavaCompiler.Keywords._public);

			w.WriteDecoratedTypeNameOrImplementationTypeName(z);

			w.WriteSpace();
			w.WriteSafeLiteral(AsExtensionMethod);
			w.WriteLine("()");

			using (w.CreateScope())
			{
				w.WriteIdent();
				w.WriteSafeLiteral(IsExtensionMethod);
				w.WriteAssignment();
				w.WriteKeyword(JavaCompiler.Keywords._true);
				w.WriteLine(";");


				w.WriteIdent();
				w.WriteKeywordSpace(JavaCompiler.Keywords._return);
				w.WriteKeyword(JavaCompiler.Keywords._this);
				w.WriteLine(";");

			}

			
		}

		public static void WriteEndInvoke(JavaCompiler w, MethodInfo m)
		{
			w.WriteIdent();
			w.WriteKeywordSpace(JavaCompiler.Keywords._throw);
			w.WriteKeywordSpace(JavaCompiler.Keywords._new);
			w.Write("java.lang.RuntimeException(\"Not implemented\")");
			w.WriteLine(";");
		}
	}
}
