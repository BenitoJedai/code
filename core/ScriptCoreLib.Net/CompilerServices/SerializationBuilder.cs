using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.IO;

namespace ScriptCoreLib.CompilerServices
{
	public class SerializationBuilder
	{
		public static Func<Type, MethodBuilder> GetEmitClassDeserializer(TypeBuilder w)
		{
			Func<Type, MethodBuilder> h = null;

			var c = new Dictionary<Type, MethodBuilder>();

			h = z =>
				{
					if (c.ContainsKey(z))
						return c[z];

					var a = new List<Type>();

					var kk = EmitClassDeserializer(w, a.Add, k => c[z] = k, h, z);

					foreach (var i in a)
					{
						h(i);
					}

					return kk;
				};

			return h;
		}

		public static Func<Type, MethodBuilder> GetEmitClassSerializer(TypeBuilder w)
		{
			Func<Type, MethodBuilder> h = null;

			var c = new Dictionary<Type, MethodBuilder>();

			h = z =>
			{
				if (c.ContainsKey(z))
					return c[z];

				var a = new List<Type>();

				var kk = EmitClassSerializer(w, a.Add, k => c[z] = k, h, z);

				foreach (var i in a)
				{
					h(i);
				}

				return kk;
			};

			return h;
		}


		static MethodBuilder EmitClassSerializer(TypeBuilder w, Action<Type> Enqueue, Action<MethodBuilder> EarlyReturn, Func<Type, MethodBuilder> Resolve, Type z)
		{
			Action<byte> BinaryWriter_WriteByte = new BinaryWriter(new MemoryStream()).Write;
			Action<int> BinaryWriter_WriteInt32 = new BinaryWriter(new MemoryStream()).Write;
			Action<char> BinaryWriter_WriteChar = new BinaryWriter(new MemoryStream()).Write;

			var name = "Serialize_" + z.MetadataToken.ToString("x8");

			var m = w.DefineMethod(name, System.Reflection.MethodAttributes.Public, System.Reflection.CallingConventions.Standard, null, new[] { typeof(BinaryWriter), z });

			EarlyReturn(m);

			foreach (var f in z.GetFields())
			{
				if (f.FieldType.IsClass)
				{
					Enqueue(f.FieldType);
				}
			}


			var il = m.GetILGenerator();

			var notnull = il.DefineLabel();

			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Brtrue, notnull);

			#region return w.WriteByte(0)
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldc_I4_0);
			il.EmitCall(BinaryWriter_WriteByte);
			il.Emit(OpCodes.Ret);
			#endregion


			il.MarkLabel(notnull);

			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldc_I4_1);
			il.EmitCall(BinaryWriter_WriteByte);

			foreach (var f in z.GetFields())
			{
				if (f.FieldType.IsClass)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldarg_2);
					il.Emit(OpCodes.Ldfld, f);
					il.EmitCall(OpCodes.Call, Resolve(f.FieldType), null);
				}
				else
				{
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Ldarg_2);
					il.Emit(OpCodes.Ldfld, f);

					if (f.FieldType == typeof(int))
					{
						il.EmitCall(BinaryWriter_WriteInt32);
					}
					else if (f.FieldType == typeof(char))
					{
						il.EmitCall(BinaryWriter_WriteChar);
					}
					else if (f.FieldType == typeof(byte))
					{
						il.EmitCall(BinaryWriter_WriteByte);
					}
					else throw new NotSupportedException(f.FieldType.FullName);
				}

			}

			il.Emit(OpCodes.Ret);

			return m;
		}

		static MethodBuilder EmitClassDeserializer(TypeBuilder w, Action<Type> Enqueue, Action<MethodBuilder> EarlyReturn, Func<Type, MethodBuilder> Resolve, Type z)
		{
			// this method might need to be rewritten to implement 2 pass

			Func<byte> BinaryReader_ReadByte = new BinaryReader(new MemoryStream()).ReadByte;
			Func<char> BinaryReader_ReadChar = new BinaryReader(new MemoryStream()).ReadChar;
			Func<int> BinaryReader_ReadInt32 = new BinaryReader(new MemoryStream()).ReadInt32;

			var name = "Deserialize_" + z.MetadataToken.ToString("x8");

			var m = w.DefineMethod(name, System.Reflection.MethodAttributes.Public, System.Reflection.CallingConventions.Standard, z, new[] { typeof(BinaryReader) });

			EarlyReturn(m);

			var il = m.GetILGenerator();

			foreach (var f in z.GetFields())
			{
				if (f.FieldType.IsClass)
				{
					Enqueue(f.FieldType);
				}
			}

			var notnull = il.DefineLabel();

			#region read 0/1
			il.Emit(OpCodes.Ldarg_1);
			il.EmitCall(BinaryReader_ReadByte);
			il.Emit(OpCodes.Brtrue, notnull);
			#endregion

			il.Emit(OpCodes.Ldnull);
			il.Emit(OpCodes.Ret);

			il.MarkLabel(notnull);

			il.Emit(OpCodes.Newobj, z.GetConstructor(Type.EmptyTypes));

			foreach (var f in z.GetFields())
			{
				il.Emit(OpCodes.Dup);

				if (f.FieldType.IsClass)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.EmitCall(OpCodes.Call, Resolve(f.FieldType), null);
				}
				else
				{
					if (f.FieldType == typeof(int))
					{
						il.Emit(OpCodes.Ldarg_1);
						il.EmitCall(BinaryReader_ReadInt32);
					}
					else if (f.FieldType == typeof(char))
					{
						il.Emit(OpCodes.Ldarg_1);
						il.EmitCall(BinaryReader_ReadChar);
					}
					else if (f.FieldType == typeof(byte))
					{
						il.Emit(OpCodes.Ldarg_1);
						il.EmitCall(BinaryReader_ReadByte);
					}
					else throw new NotSupportedException(f.FieldType.FullName);
				}

				il.Emit(OpCodes.Stfld, f);
			}

			il.Emit(OpCodes.Ret);

			return m;
		}
	}
}
