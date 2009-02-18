using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.CodeModel
{
	public class TypeInfo
	{
		public Type Value;
		public Type ResolvedValue;

		public TypeInfo(Type e)
		{
			Value = e;

			ReferencedBaseTypes = GetReferencedBaseTypes(e).ToArray();

		}

		public readonly Type[] ReferencedBaseTypes;
		public Type[] ResolvedBaseTypes;

		static IEnumerable<Type> GetReferencedBaseTypes(Type e)
		{
			if (e.BaseType != null)
				yield return e.BaseType;

			foreach (var i in e.GetInterfaces())
			{
				yield return i;

				foreach (var j in GetReferencedBaseTypes(i))
				{
					yield return j;
				}
			}
		}

		public bool IsCompilerGenerated
		{
			get
			{
				return ScriptAttribute.IsCompilerGenerated(Value);
			}
		}

		public bool IsScript
		{
			get
			{
				return ScriptAttribute.Of(Value) != null;
			}
		}

		public Func<TypeInfo, string> TargetFileNameHandler;

		public string TargetFileName
		{
			get
			{

				return TargetFileNameHandler(this);
			}
		}

		public string AssamblyFileName
		{
			get
			{
				return new FileInfo(Value.Assembly.Location).Name;
			}
		}

		public string Win32TypeName
		{
			get
			{
				//if (Value.FullName == null)
				{

					Type p = Value;

					string u = CompilerBase.GetSafeLiteral(Value.Name, CompilerBase.DefaultIsSafeLiteralChar);

					if (Value.IsNested)
					{
						int token = p.MetadataToken;

						while (p.IsNested)
						{
							p = p.DeclaringType;

							//token ^= p.MetadataToken;

							if (!p.IsNested)
							{
								u = CompilerBase.GetSafeLiteral(p.Name, CompilerBase.DefaultIsSafeLiteralChar) + "_" + token.ToString("x8");
							}
						}

					}

					if (p.Namespace != null)
						u = CompilerBase.GetSafeLiteral(p.Namespace, CompilerBase.DefaultIsSafeLiteralChar) + "." + u;

					return Helper.GetSafeWin32FileName(u);
				}

				//return Helper.GetSafeWin32FileName(Value.FullName);
			}
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
}
