using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace jsc.Languages.IL
{
	/// <summary>
	/// Here we are years later, actually rewriting IL for a later phase!
	/// </summary>
	public static partial class ILTranslationExtensions
	{


		public class EmitToArguments
		{
			public Action<ILRewriteContext> BeforeInstruction;
			public Action<ILRewriteContext> AfterInstruction;

			public readonly Dictionary<OpCode, Action<ILRewriteContext>> Configuration = new Dictionary<OpCode, Action<ILRewriteContext>>();



			public Func<Type, Type> TranslateTargetType =
				t => t;

			/// <summary>
			/// We are adding IL to a method. This method may already have defined some variables
			/// of which we are not aware of. This would allow
			/// us to enable method composition.
			/// </summary>
			public Func<int, int> TranslateLocalIndex = LocalIndex => LocalIndex;
			public Func<ILInstruction, int, int> TranslateBranchOffset = (i, o) => o;
			public Func<FieldInfo, FieldInfo> TranslateTargetField = TargetField => TargetField;
			public Func<MethodInfo, MethodInfo> TranslateTargetMethod = TargetMethod => TargetMethod;
			public Func<ConstructorInfo, ConstructorInfo> TranslateTargetConstructor = TargetConstructor => TargetConstructor;



			public EmitToArguments()
			{

				#region Stloc
				var Stloc_ = new[]
				{
					OpCodes.Stloc_0,
					OpCodes.Stloc_1,
					OpCodes.Stloc_2,
					OpCodes.Stloc_3
				};

				Action<ILRewriteContext, int> Stloc =
					(e, LocalIndex) =>
					{
						if (LocalIndex < Stloc_.Length)
						{
							e.il.Emit(Stloc_[LocalIndex]);
							return;
						}

						if (LocalIndex <= 0xFF)
						{
							e.il.Emit(OpCodes.Stloc_S, (byte)LocalIndex);
							return;
						}

						e.il.Emit(OpCodes.Stloc, LocalIndex);
					};

				Stloc_.ForEach((OpCode, n) => this[OpCode] = e => Stloc(e, TranslateLocalIndex(n)));

				this[OpCodes.Stloc_S] = e => Stloc(e, TranslateLocalIndex(e.i.OpParamAsInt8));
				this[OpCodes.Stloc] = e => Stloc(e, TranslateLocalIndex(e.i.OpParamAsInt32));
				#endregion


				#region Ldloc
				var Ldloc_ = new[]
				{
					OpCodes.Ldloc_0,
					OpCodes.Ldloc_1,
					OpCodes.Ldloc_2,
					OpCodes.Ldloc_3
				};

				Action<ILRewriteContext, int> Ldloc =
					(e, LocalIndex) =>
					{
						if (LocalIndex < Ldloc_.Length)
						{
							e.il.Emit(Ldloc_[LocalIndex]);
							return;
						}

						if (LocalIndex <= 0xFF)
						{
							e.il.Emit(OpCodes.Ldloc_S, (byte)LocalIndex);
							return;
						}

						e.il.Emit(OpCodes.Ldloc, LocalIndex);
					};

				Ldloc_.ForEach((OpCode, n) => this[OpCode] = e => Ldloc(e, TranslateLocalIndex(n)));

				this[OpCodes.Ldloc_S] = e => Ldloc(e, TranslateLocalIndex(e.i.OpParamAsInt8));
				this[OpCodes.Ldloc] = e => Ldloc(e, TranslateLocalIndex(e.i.OpParamAsInt32));
				#endregion

				this[OpCodes.Newobj] = e => e.il.Emit(OpCodes.Newobj, this.TranslateTargetConstructor(e.i.TargetConstructor));
				this[OpCodes.Stelem_Ref] = e => e.il.Emit(OpCodes.Stelem_Ref);


				// somebody said string encryption?
				// first level obfuscation starts here
				this[OpCodes.Ldstr] = e => e.il.Emit(OpCodes.Ldstr, e.i.TargetLiteral);

				this[OpCodes.Call] =
					e =>
					{
						var TargetMethod = e.i.TargetMethod;
						if (TargetMethod != null)
						{
							e.il.Emit(OpCodes.Call, TranslateTargetMethod(e.i.TargetMethod));
							return;
						}

						e.il.Emit(OpCodes.Call, TranslateTargetConstructor(e.i.TargetConstructor));
					};

				this[OpCodes.Callvirt] =
					e =>
					{
						var TargetMethod = e.i.TargetMethod;
						if (TargetMethod != null)
						{
							e.il.Emit(OpCodes.Callvirt, TranslateTargetMethod(e.i.TargetMethod));
							return;
						}

						e.il.Emit(OpCodes.Callvirt, TranslateTargetConstructor(e.i.TargetConstructor));
					};


				this[OpCodes.Ldvirtftn] =
					e =>
					{
						var TargetMethod = e.i.TargetMethod;
						if (TargetMethod != null)
						{
							e.il.Emit(OpCodes.Ldvirtftn, TranslateTargetMethod(e.i.TargetMethod));
							return;
						}

						e.il.Emit(OpCodes.Ldvirtftn, TranslateTargetConstructor(e.i.TargetConstructor));
					};

				this[OpCodes.Ldftn] =
					e =>
					{
						var TargetMethod = e.i.TargetMethod;
						if (TargetMethod != null)
						{
							e.il.Emit(OpCodes.Ldftn, TranslateTargetMethod(e.i.TargetMethod));
							return;
						}

						e.il.Emit(OpCodes.Ldftn, TranslateTargetConstructor(e.i.TargetConstructor));
					};

				this[OpCodes.Ldc_R4] =
					e =>
					{
						e.il.Emit(OpCodes.Ldc_R4, (float)e.i.TargetFloat);
					};
				this[OpCodes.Ldc_R8] =
				e =>
				{
					e.il.Emit(OpCodes.Ldc_R8, (double)e.i.TargetDouble);
				};
			
				this[OpCodes.Ldtoken] =
					e =>
					{
						var TargetField = e.i.TargetField;

						if (TargetField != null)
						{
							e.il.Emit(OpCodes.Ldtoken, this.TranslateTargetField(TargetField));
							return;
						}

						e.il.Emit(OpCodes.Ldtoken, this.TranslateTargetType(e.i.TargetType));

					};

				this[OpCodes.Castclass] = e => e.il.Emit(OpCodes.Castclass, this.TranslateTargetType(e.i.TargetType));


				this[i => this.TranslateTargetType(i.TargetType)] = new[] {
					OpCodes.Box,
					OpCodes.Newarr,
					OpCodes.Unbox_Any,
					OpCodes.Isinst
				};

				this[i => this.TranslateTargetField(i.TargetField)] = new[] {
					OpCodes.Stfld,
					OpCodes.Ldfld,
					OpCodes.Stsfld,
					OpCodes.Ldsfld,
					
				};

				this[i => i.OpParamAsInt32] = new[] {
					OpCodes.Ldc_I4,
					OpCodes.Ldloca
				};

				this[i => TranslateBranchOffset(i, i.OpParamAsInt32)] = new[] {
					OpCodes.Br,
					OpCodes.Brtrue,
				};

				this[i => i.OpParamAsInt8] = new[] {
					OpCodes.Ldc_I4_S,
					OpCodes.Ldarga_S,
					OpCodes.Ldarg_S,
					OpCodes.Starg_S,
					OpCodes.Ldloca_S
				};

				// http://msdn.microsoft.com/en-us/library/74b4xzyw(VS.71).aspx
				// If we translate the branch offset from byte to int
				// we got some problems!
				// For now we wont implement this but it will bite us later!
				this[i => 
					(byte)
						checked(
							(sbyte)TranslateBranchOffset(i, 
								unchecked(
									(sbyte)i.OpParamAsInt8)))
					] = new[] {
					OpCodes.Br_S,
					OpCodes.Brtrue_S,
					OpCodes.Brfalse_S,
					OpCodes.Bne_Un_S
				};



				// simple opcode translation without operand
				// the user can however override them
				// C# does not have Func<IL, void> now does it...
				new[] {
					OpCodes.Ldc_I4_M1,
					OpCodes.Ldc_I4_0,
					OpCodes.Ldc_I4_1,
					OpCodes.Ldc_I4_2,
					OpCodes.Ldc_I4_3,
					OpCodes.Ldc_I4_4,
					OpCodes.Ldc_I4_5,
					OpCodes.Ldc_I4_6,
					OpCodes.Ldc_I4_7,
					OpCodes.Ldc_I4_8,

					OpCodes.Ldarg_0,
					OpCodes.Ldarg_1,
					OpCodes.Ldarg_2,
					OpCodes.Ldarg_3,

				

					OpCodes.Ldelem_Ref,
					OpCodes.Ldelem_U1,
					OpCodes.Ldelem_U2,
					OpCodes.Ldelem_U4,
					OpCodes.Ldelem_I1,
					OpCodes.Ldelem_I2,
					OpCodes.Ldelem_I4,
					OpCodes.Ldelem_I8,
					OpCodes.Ldelem_R8,
					OpCodes.Ldelema,
					OpCodes.Ldelem,
					
					OpCodes.Stelem_Ref,
					OpCodes.Stelem_I1,
					OpCodes.Stelem_I2,
					OpCodes.Stelem_I4,
					OpCodes.Stelem_I8,
					OpCodes.Stelem_R8,
					OpCodes.Stelem,

					OpCodes.Ldnull,
					OpCodes.Pop,	
					OpCodes.Ret,
					OpCodes.Dup,

					OpCodes.Conv_I4,
					OpCodes.Conv_U1,
					OpCodes.Conv_U2,
					OpCodes.Conv_U4,
					OpCodes.Conv_I8,
					OpCodes.Conv_I2,
					OpCodes.Conv_R4,
					OpCodes.Conv_R8,

					OpCodes.Ldlen,
					OpCodes.Throw,
					OpCodes.Nop,
					OpCodes.Cgt,
					OpCodes.Clt,
					OpCodes.Ceq,

					OpCodes.Rem,
					OpCodes.Not,
					OpCodes.Sub,
					OpCodes.Shl,
					OpCodes.Shr,
					OpCodes.Shr_Un,
					OpCodes.Xor,
					OpCodes.Or,
					OpCodes.And,
					OpCodes.Add,
					OpCodes.Div,
					OpCodes.Mul,

					//OpCodes.Endfinally,
					
				}.ForEach(
					OpCode => this[OpCode] = e => e.il.Emit(OpCode)
				);


			}



			public OpCode[] this[Func<ILInstruction, Type> selector]
			{
				set { value.ForEach(OpCode => this[OpCode] = e => e.il.Emit(OpCode, selector(e.i))); }
			}

			public OpCode[] this[Func<ILInstruction, FieldInfo> selector]
			{
				set { value.ForEach(OpCode => this[OpCode] = e => e.il.Emit(OpCode, selector(e.i))); }
			}

			public OpCode[] this[Func<ILInstruction, int> selector]
			{
				set { value.ForEach(OpCode => this[OpCode] = e => e.il.Emit(OpCode, selector(e.i))); }
			}

			public OpCode[] this[Func<ILInstruction, byte> selector]
			{
				set { value.ForEach(OpCode => this[OpCode] = e => e.il.Emit(OpCode, selector(e.i))); }
			}

			public class ILRewriteContext
			{
				public ILInstruction i;
				public ILGenerator il;

				public Action Default;
			}


			public Action<ILRewriteContext> this[OpCode o]
			{
				set
				{
					var Default = Configuration.ContainsKey(o) ? Configuration[o] : default(Action<ILRewriteContext>);

					Configuration[o] = e => value(
						new ILRewriteContext
						{
							i = e.i,
							il = e.il,
							Default = () => Default(e)
						}
					);
				}
			}
		}



	}
}
