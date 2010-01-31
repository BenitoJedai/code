using System.Reflection;
using System.Reflection.Emit;

using ScriptCoreLib;

namespace jsc.Languages.Java
{

	partial class JavaCompiler
	{

		public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
		{
			DebugBreak(ScriptAttribute.Of(i.OwnerMethod));

			ScriptAttribute ma = ScriptAttribute.Of(m);

			bool IsExternalDefined = ma != null && ma.ExternalTarget != null;
			bool IsDefineAsInstance = ma != null && ma.DefineAsInstance;
			bool IsBaseConstructorCall = false;
			bool IsDefineAsStatic = ma != null && ma.DefineAsStatic;
			bool IsBaseMethodCall = false;

			// NonPrimitiveValueTypeConstructor
			ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

			var z_Implements = za != null ? za.Implements : null;
			var z_NonPrimitiveValueType = z_Implements != null && z_Implements.IsValueType && !z_Implements.IsPrimitive;


			if (m.IsInstanceConstructor())
			{
				// fixme: update the BCL resolving issue
				// the super ctor call gets lost otherwise

				if (z_NonPrimitiveValueType)
				{
					// yay we need to call a method instead a ctor
				}
				else if (m.DeclaringType == i.OwnerMethod.DeclaringType)
				{
					// ctor as this.ctor();
					IsBaseConstructorCall = true;
				}
				else if (i.IsBaseConstructorCall(m, k => ResolveImplementationMethod(k.DeclaringType, k), ResolveImplementation))
				{

					IsBaseConstructorCall = true;
				}
				else
					Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
			}
			else
			{
				if (i.OpCode == OpCodes.Call)
					if (!i.OwnerMethod.IsStatic)
						//if (i.TargetMethod.DeclaringType.Equals(i.OwnerMethod.DeclaringType.BaseType))
						if (i.TargetMethod.DeclaringType.IsAssignableFrom(i.OwnerMethod.DeclaringType.BaseType))
						{
							IsBaseMethodCall = true;
						}
			}


			ILFlow.StackItem[] s = i.StackBeforeStrict;

			int offset = 1;

			#region static call defined as instance call


			if (m.IsStatic && IsExternalDefined & IsDefineAsInstance)
			{
				// what?? string?

				Emit(p, s[0]);
				Write(".");
				WriteExternalMethod(ma.ExternalTarget, m);
				WriteParameterInfoFromStack(m, p, s, 1);

				return;
			}
			#endregion



			if ((m.IsStatic || IsDefineAsStatic) || IsBaseConstructorCall)
			{
				#region static
				//TODO: ???
				if (IsBaseConstructorCall)
				{
					//WriteTypeBaseType();
					//Write(".");

				}
				else
				{
					//ScriptAttribute ta = ScriptAttribute.Of(m.DeclaringType);

					if (IsExternalDefined)
					{
						//WriteBoxedComment("impl");



						this.Write(GetDecoratedTypeName(ScriptAttribute.Of(m.DeclaringType).ImplementationType, true, false, true, true));

						Write(".");

					}
					else
					{
						//WriteBoxedComment("ext");

						WriteTypeOrExternalTargetTypeName(m.DeclaringType, false);
						Write(".");
					}
				}
				#endregion

				offset = !m.IsStatic && (IsDefineAsStatic || IsBaseConstructorCall) ? 1 : 0;
			}
			else
			{

				// WriteBoxedComment("variable.call");

				// base. ?

				if (IsBaseMethodCall)
				{
					WriteKeyword(Keywords._super);
				}
				else
				{
					if (i.OpCode == OpCodes.Call &&
						s[0].SingleStackInstruction == OpCodes.Ldarg_0 &&
						i.OwnerMethod.DeclaringType.BaseType == m.DeclaringType)
					{
						if (i.IsBaseConstructorCall())
							WriteKeyword(Keywords._super);
						else
							WriteKeyword(Keywords._this);

					}
					else
					{
						Emit(p, s[0]);
					}
				}

				Write(".");
			}



			if (IsExternalDefined)
			{
				WriteExternalMethod(ma.ExternalTarget, m);
			}
			else
			{
				if (IsBaseConstructorCall)
				{
					if (i.IsBaseConstructorCall())
						WriteKeyword(Keywords._super);
					else
						WriteKeyword(Keywords._this);

				}
				else
				{
					if ((m.IsInstanceConstructor()) && z_NonPrimitiveValueType)
						Write("NonPrimitiveValueTypeConstructor");
					else
						WriteDecoratedMethodName(m, false);
				}
			}

			WriteParameterInfoFromStack(m, p, s, offset);

		}


	}
}
