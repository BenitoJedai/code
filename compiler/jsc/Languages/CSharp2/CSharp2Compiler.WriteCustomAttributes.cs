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
		public void WriteCustomAttributes(Type z)
		{
			foreach (Attribute a in z.GetCustomAttributes(false).Where(k => !(k is ScriptAttribute)))
			{
				if (a is DefaultMemberAttribute)
					continue;

				WriteCustomAttribute(z, a);
			}
		}

		public void WriteCustomAttribute(Type z, Attribute a)
		{
			WriteIdent();
			Write("[");

			var x = a.GetType();

			WriteGenericTypeName(z, x);

			var Fields = x.GetFields(BindingFlags.Instance | BindingFlags.Public);

			if (Fields.Length > 0)
			{
				// this is not going to be simple.
				// looks like some attributes define only a special constructor
				// at this time we only know the values of the fields, but we need to
				// use the constructor to redefine the attribute

				var Constructors =
					from c in x.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
					let il = new ILBlock(c)
					let r = c.GetParameters().Select(p =>  WhichFieldIsSetByThisParameter(il, p)).ToArray()
					select new { c, r };

				// which constructor parameter will set which field?
				// if there are many constructors, which is best for us?

				var Constructor = Constructors.First();

				// if we have multiple constructors we'd better choos the best one
				// for now we take the first one

				var NamedFields = Fields.Where(
					k =>
					{
						if (Constructor.r.Any(
							q => q.DeclaringType == k.DeclaringType && q.Name == k.Name
							))
							return false;

						return true;
					}
				);

				bool WriteCommaFirstSkipped = false;

				Action WriteComma =
					delegate
					{
						if (!WriteCommaFirstSkipped)
						{
							WriteCommaFirstSkipped = true;
							return;
						}

						Write(", ");
					};

				Action<FieldInfo> WriteFieldValue =
					v =>
					{
						if (v.FieldType == typeof(string))
							WriteQuotedLiteral((string)v.GetValue(a));
						if (v.FieldType == typeof(int))
							MyWriter.Write((int)v.GetValue(a));
						if (v.FieldType == typeof(bool))
							WriteKeyword(((bool)v.GetValue(a)) ? Keywords._true : Keywords._false);
					};

				Write("(");

		
				foreach (var v in Constructor.r)
				{
					WriteComma();
					WriteFieldValue(v);
				}

				foreach (var v in NamedFields)
				{
					WriteComma();
					WriteSafeLiteral(v.Name);
					WriteAssignment();
					WriteFieldValue(v);
				}


				Write(")");
			}

			Write("]");
			WriteLine();
		}

		public FieldInfo WhichFieldIsSetByThisParameter(ILBlock b, ParameterInfo arg)
		{
			// jsc actually would know the anwser...

			var ld_arg_array = b.Instructrions.Where(i => i.IsLoadParameter && i.TargetParameter == arg).ToArray();

			// this is a dumbed down implementation
			// we look for either a call or a st_fld opcodes

			foreach (var ld_arg in ld_arg_array)
			{
				var p = ld_arg.Info.Prestatement;
				var i = p.Instruction;

				if (i.OpCode.FlowControl == FlowControl.Call)
				{
					// this field might be set within a subroutine

					var m = i.ReferencedMethod;
					var m_params = m.GetParameters();
					
					// match our opcode to a parameter

					var i_stack = i.StackBeforeStrict;

					for (int j = 0; j < m_params.Length; j++)
					{
						if (i_stack[i_stack.Length - 1 - j].SingleStackInstruction == ld_arg)
						{
							return WhichFieldIsSetByThisParameter(new ILBlock(m), m_params[m_params.Length - 1 - j]);
						}
					}
				}


				if (i.OpCode == OpCodes.Stfld)
				{
					// we found it

					return i.TargetField;
				}
			}

			throw new NotSupportedException();
		}
	}
}
