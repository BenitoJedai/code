using System;

namespace jsc.Languages.Java
{

	partial class JavaCompiler
	{
		public void WriteOpCodesBox(Type TargetType, Func<bool> IsLoadLocal, Action EmitFirstOnStack, bool IsUnsignedSupport)
		{
			if (TargetType.IsEnum)
				TargetType = Enum.GetUnderlyingType(TargetType);


			#region byte
			if (TargetType == typeof(byte))
			{
				// short has 15 unsigned bits, we need 8
				Write("new ");
				Write(GetDecoratedTypeName(IsUnsignedSupport ? typeof(short) : typeof(byte), true, false));
				Write("(");

				if (!IsUnsignedSupport || IsLoadLocal())
				{
					EmitFirstOnStack();
				}
				else
				{
					Write("(short)");
					Write("(");
					EmitFirstOnStack();

					// this operator is either 16bit or 32bit, depends on VM
					Write(" & 0xff");
					Write(")");
				}

				Write(")");
				return;
			}
			#endregion

			#region ushort
			if (TargetType == typeof(ushort))
			{
				// short has 15 unsigned bits, we need 8
				Write("new ");
				Write(GetDecoratedTypeName(IsUnsignedSupport ? typeof(int) : typeof(short), true, false));
				Write("(");

				if (!IsUnsignedSupport || IsLoadLocal())
				{
					EmitFirstOnStack();
				}
				else
				{
					Write("(int)");
					Write("(");
					EmitFirstOnStack();

					Write(" & 0xffffL");
					Write(")");
				}

				Write(")");
				return;
			}
			#endregion

			#region uint
			if (TargetType == typeof(uint))
			{
				// short has 15 unsigned bits, we need 8
				Write("new ");
				Write(GetDecoratedTypeName(IsUnsignedSupport ? typeof(long) : typeof(int), true, false));
				Write("(");

				if (!IsUnsignedSupport || IsLoadLocal())
				{
					EmitFirstOnStack();
				}
				else
				{
					Write("(long)");
					Write("(");
					EmitFirstOnStack();

					Write(" & 0xffffffffL");
					Write(")");
				}

				Write(")");
				return;
			}
			#endregion


			#region IntPtr
			if (TargetType == typeof(IntPtr))
			{
				// IntPtr should never be boxed, because in our implementation
				// we only have classes
				EmitFirstOnStack();
			}
			else
			{
				//var TargetType = this.ResolveImplementation(e.i.TargetType) ?? e.i.TargetType;


				Write("new ");
				Write(GetDecoratedTypeName(TargetType, true, false));
				Write("(");
				EmitFirstOnStack();
				Write(")");
			}
			#endregion
		}

	}
}
