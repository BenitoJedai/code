
using System;

using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;

using ScriptCoreLib;

using jsc.Script;
using jsc.Languages.JavaScript;

namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlow.StackItem;
	using jsc.Languages.JavaScript.legacy;


	partial class IL2ScriptGenerator
	{
		static bool OpCode_newobj_override(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			Type _decl_type = i.TargetConstructor.DeclaringType;

			#region custom implementation



			//ScriptAttribute a = ScriptAttribute.Of(_decl_type);

			//#region ExternalTarget fixup
			//if (a != null && a.ExternalTarget != null)
			//{

			//    w.Write("new ");
			//    w.Write(a.ExternalTarget);
			//    w.Write("(");
			//    w.WriteParameters(p, i, s, 0, null);
			//    w.Write(")");

			//    return true;
			//}
			//#endregion




			MethodBase _impl_type_ctor = w.Session.ResolveImplementation(_decl_type, i.TargetConstructor);

			if (_impl_type_ctor != null)
			{
				//w.WriteCommentLine(_impl_type_ctor.DeclaringType.FullName + "." + _impl_type_ctor.Name);

				WriteCreateType(w, p, i, s, _impl_type_ctor);

				/*
				w.Helper.DOMCreateAndInvokeConstructor(

					_impl_type_ctor.DeclaringType,
					_impl_type_ctor, p, i, s);
				 */

				return true;
			}


			#endregion


			return false;
		}



		static void OpCode_newobj(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			MethodBase m = i.TargetConstructor;

			if (m.DeclaringType == typeof(object))
			{
				w.Write("{}");

				return;
			}


			if (ScriptAttribute.IsAnonymousType(m.DeclaringType))
			{
				goto TryDefault;
			}



			//if (ScriptAttribute.IsCompilerGenerated(m.DeclaringType))
			if (ScriptAttribute.OfProvider(m.DeclaringType) == null
				&& w.Session.ResolveImplementation(m.DeclaringType) == null)
			{
				w.Write("/* DOMCreateType */");
				w.Helper.DOMCreateType(m.DeclaringType);

				return;
			}


			if (OpCode_newobj_override(w, p, i, s))
			{
				if (i.TargetConstructor.DeclaringType.IsDelegate())
				{
					var TargetIsNotNull = i.StackBeforeStrict[0].SingleStackInstruction != OpCodes.Ldnull;
					var TargetMethodIsStatic = i.StackBeforeStrict[1].SingleStackInstruction.TargetMethod.IsStatic;

					if (TargetMethodIsStatic)
						if (TargetIsNotNull)
						{
							w.Write(".");
							w.Write(DelegateImplementationProvider.AsExtensionMethod);
							w.Write("()");
						}
				}

				return;
			}

			var m_type_attribute = ScriptAttribute.Of(m.DeclaringType, true);



			#region missing script attribute
			if (m_type_attribute == null)
			{
				if (m.DeclaringType.IsArray)
				{
					// when is this hit? arrays are implemented!

					Task.Error("new array not implemented");
					Task.Fail(i);
				}

				using (StringWriter sw = new StringWriter())
				{
					// static? in js?

					if (m.IsStatic)
						sw.Write("static ");

					sw.Write("{0}.{1}", m.DeclaringType.FullName, m.Name);
					sw.Write("(");
					int pix = 0;
					foreach (ParameterInfo pi in m.GetParameters())
					{
						if (pix++ > 0)
							sw.Write(", ");

						sw.Write(pi.ParameterType.FullName);
					}

					sw.Write(")");

					Task.Error("Missing Script Attribute? Native constructor was invoked, please implement [{0}]", sw.ToString());

					w.Session.ResolveImplementationTrace(m.DeclaringType, m);

					Task.Fail(i);
				}
				Debugger.Break();
			}
			#endregion

		TryDefault:

			WriteCreateType(w, p, i, s, m);


		}
	}

}