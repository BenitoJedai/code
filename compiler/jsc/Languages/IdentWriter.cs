using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Globalization;

using ScriptCoreLib;

namespace jsc
{

	public class IdentWriter : StringWriter
	{
		/// <summary>
		/// set this property to true, if valic chars should be undecorated
		/// </summary>
		public bool UseBinaryEncoding = false;


		/// <summary>
		/// if true, additional meta comments will be written
		/// </summary>

		public bool UseHints = false;




		IL2ScriptWriterHelper _Helper;

		public IL2ScriptWriterHelper Helper
		{
			get
			{
				if (_Helper == null)
					_Helper = new IL2ScriptWriterHelper(this);

				return _Helper;
			}
		}

		public void ToFile(string filepath)
		{
			File.WriteAllText(filepath, ToString());
		}


		public void ToConsole()
		{
			Console.Write(this.ToString());
		}


		public int Ident;



		public void WriteIdent()
		{


			base.Write(new string(' ', Ident * 2));
		}

		public void WriteLiteral(string e)
		{
			this.Write("'");
			this.WriteDecoratedLiteralString(e);
			this.Write("'");
		}

		public void WriteCommentLine(string e)
		{

			WriteIdent();
			base.WriteLine("// " + e);

		}

		public void WriteCommentLine(string e, params object[] a)
		{
			WriteIdent();
			base.WriteLine("// " + e, a);

		}

		public void WriteBeginScope()
		{
			WriteIdent();
			WriteLine("{");

			this.Ident++;
		}

		public void WriteEndScope()
		{
			this.Ident--;
			WriteIdent();
			WriteLine("}");

		}

		public void EndScopeAndTerminate()
		{
			this.Ident--;
			WriteIdent();
			WriteLine("};");

		}

		public void WriteNumeric(double i)
		{
			NumberFormatInfo provider = new NumberFormatInfo();

			this.Write(Convert.ToString(i, provider));


		}

		public void WriteNumeric(long i)
		{
			NumberFormatInfo provider = new NumberFormatInfo();

			this.Write(Convert.ToString(i, provider));


		}

		public void WriteNumeric(float i)
		{
			NumberFormatInfo provider = new NumberFormatInfo();

			this.Write(Convert.ToString(i, provider));


		}

		public void WriteNumeric(int i)
		{
			this.Write(i);


		}


		internal static string GetParameterInfoNameField(ParameterInfo p)
		{
			return p.Name;
		}

		public void WriteParameterArray(ParameterInfo[] p)
		{
			ParameterInfo[] pi = p;

			for (int i = 0; i < pi.Length; i++)
			{
				if (i > 0)
					this.Write(", ");

				this.Write(pi[i].Name);
			}
		}

		public void WriteDecoratedParameterArray(ParameterInfo[] p)
		{
			ParameterInfo[] pi = p;

			for (int i = 0; i < pi.Length; i++)
			{
				if (i > 0)
					this.Write(", ");

				this.WriteDecoratedParameterInfo(pi[i]);
			}
		}



		public Func<bool> Override_WriteSelf;

		public void WriteSelf()
		{
			if (Override_WriteSelf != null && Override_WriteSelf())
				return;


			this.Write(GetSpecialLocalChar(0));
			// this.Write("this");
			// this.WriteDecorated(null, null);
		}

		public void WriteDecoratedMethodName(MethodBase m)
		{
			if (Script.CompilerBase.IsToStringMethod(m))
				Write("toString /* {0}.{1} */", m.DeclaringType.FullName, m.Name);
			else
			{
				WriteDecoratedMemberInfo(m);
			}
		}





		public MD5 md5 = null;
		//public MD5 md5 = new MD5CryptoServiceProvider();

		public AssamblyTypeInfo Session = new AssamblyTypeInfo();


		public void WriteMD5(string e, params object[] arg)
		{
			using (StringWriter w = new StringWriter())
			{
				w.Write(e, arg);

				byte[] data = ASCIIEncoding.ASCII.GetBytes(w.ToString());

				byte[] result = md5.ComputeHash(data);

				this.Write("__");
				foreach (byte b in result)
					this.Write("{0:x2}", b);

			}
		}

		#region decorated

		public bool Obfuscate = false;

		public static string GetDecoratedParameterInfo(ParameterInfo p)
		{
			//return GetSpecialChar(1) + (p.Position + 1);

			return GetSpecialLocalChar(p.Position + 1);
		}

		public void WriteDecoratedParameterInfo(ParameterInfo p)
		{
			Write(GetDecoratedParameterInfo(p));
		}

		public void WriteHint(string e, params object[] a)
		{
			if (UseHints)
				base.Write("/* " + e + " */", a);
		}



		bool IsNonBinary(char x)
		{
			return (
				(x >= 'a' && x <= 'z')
				|| (x >= 'A' && x <= 'Z')
				|| (x >= '0' && x <= '9')
				|| " ,.-_+-?#<>$%:;()[]}{=".IndexOf(x) > -1);

		}

		public static string GetSafeLiteral(string z)
		{
            // escaping language keywords?
            if (z == "this")
                return "__this";

			var w = new StringBuilder(z.Length);

			foreach (char x in z)
			{

				if (char.IsLetter(x) || char.IsNumber(x))
				{
					w.Append(x);
				}
				else
				{
					w.Append("_");
				}
			}

			return w.ToString();
		}

		public void WriteSafeLiteral(string z)
		{
			this.Write(GetSafeLiteral(z));
		}

		public void WriteDecoratedLiteralString(string z)
		{
			foreach (char x in z)
			{
				if (!UseBinaryEncoding && IsNonBinary(x))
				{
					this.Write(x);
				}
				else
				{
					if (x == '\"')
					{
						this.Write("\\\"");
						continue;
					}

					if (x == '\'')
					{
						this.Write("\\\'");
						continue;
					}

					this.Write(@"\u{0:x4}", (int)x);

					//this.Write(@"\x{0:x2}", (byte)x);
				}
			}
		}


		//public string GetDecoratedGuid(Guid g)
		//{
		//    return IdentWriter.GetDecoratedGuid(g);
		//}

		public static string GetDecoratedGuid(Guid g)
		{
			using (StringWriter w = new StringWriter())
			{
				w.Write(GetSpecialChar(1));

				byte[] b = g.ToByteArray();

				for (int i = 0; i < b.Length; i++)
					w.Write("{0:x2}", b[i]);

				return w.ToString();
			}
		}

		public void WriteDecoratedGuid(Guid g)
		{
			Write(GetDecoratedGuid(g));

		}


		public void WriteQoute(bool qoute, Action e)
		{
			if (qoute)
				Write("'");

			e();

			if (qoute)
				Write("'");
		}

		public string GetDecoratedType(Type x, bool qoute)
		{
			using (StringWriter w = new StringWriter())
			{
				if (qoute)
					w.Write("'");

				w.Write(GetDecoratedGuid(x.GUID));


				if (qoute)
					w.Write("'");

				return w.ToString();
			}

		}
		public void WriteDecoratedType(Type x, bool qoute)
		{
			WriteQoute(qoute, delegate { WriteGUID64(x); });

			WriteHint("[{0}] {1}", x.Module.Name, x.FullName);


		}

		public void WriteDecorated(Type x)
		{
			WriteDecoratedType(x, false);

		}

		#region SpecialChar
		public static string GetSpecialChar(int c)
		{
			return new string('_', c);
		}

		public void WriteSpecialChar()
		{
			WriteSpecialChar(1);
		}

		public void WriteSpecialChar(int c)
		{
			this.Write(GetSpecialChar(c));
		}
		#endregion


		/// <summary>
		/// local variables and parameters are local to the method
		/// currently javascript closures are not used thus
		/// there is no need to worry about nested naming clash - a thing
		/// to keep in mind in the future
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static string GetSpecialLocalChar(int offset)
		{
			int x = 'a';

			if (offset > 25)
				return "_" + offset;

			return "" + (char)(x + offset);
		}


		public T[] ToArray<T>(IEnumerable<T> e)
		{
			List<T> a = new List<T>();

			foreach (T v in e)
			{
				a.Add(v);
			}

			return a.ToArray();
		}

		public IEnumerable<T> Where<T>(IEnumerable<T> e, Func<T, bool> p)
		{
			foreach (T v in e)
			{
				if (p(v))
					yield return v;
			}
		}

		public void WriteDecoratedMemberInfo(MemberInfo x, bool q)
		{

			ScriptAttribute sa = ScriptAttribute.Of(x);
			ScriptAttribute ta = ScriptAttribute.Of(ToGenericDefinition(x.DeclaringType));



			bool ndec = (sa != null && sa.NoDecoration);

			if (!x.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Any() && IsSerializeableType(x, ndec))
				ndec = true;

	


			if (q)
				this.Write("'");

			if (ndec)
			{
			

				this.WriteSafeLiteral(x.Name);
			}
			else
			{
				if ((x is FieldInfo) && !((FieldInfo)x).IsStatic)
				{
					this.WriteSafeLiteral(x.Name);
				}
				else
				{
					//if (md5 == null)
					//{

					if (x.DeclaringType.IsSealed && x is MethodInfo)
					{
						MemberInfo[] members =
							ToArray(
								Where(x.DeclaringType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
									delegate(MemberInfo member) { return member.Name == x.Name; }
								)
							);

						if (members.Length == 1 && members[0] == x)
						{
							this.WriteSafeLiteral(x.Name);

							goto done;
						}

					}


					WriteGUIDAndToken64(x);



					//WriteGUIDAndToken(x);


					if (!q)
					{

						WriteHint("{0}.{1}", x.DeclaringType.FullName, x.Name);
					}

					//}
					//else
					//{

					//    WriteMD5("{0:x4}{1:x4}{2:x4}",
					//            x.Module.Assembly.GetHashCode(),
					//            x.DeclaringType.MetadataToken, x.MetadataToken);
					//}
				}
			}

		done:

			if (q)
				this.Write("'");
		}

		public static Type ToGenericDefinition(Type x)
		{
			return x.IsGenericType ?
					x.GetGenericTypeDefinition() :
					x;
		}



		public static string GetGUID64(Guid x)
		{
			MemoryStream m = new MemoryStream(x.ToByteArray());

			var e = m.ToArray();

			// sync to WriteSpecialBase64

			var name64 = Convert.ToBase64String(e).
				 Replace("+", "_a").
				 Replace("/", "_b").
				 Replace("=", "");

			if (!char.IsLetter(name64[0]))
				name64 = GetSpecialChar(1) + name64;

			return name64;
		}

		public static string GetGUID64(Type x)
		{
			return GetGUID64(ToGenericDefinition(x).GUID);

		}

		private void WriteGUID64(Type x)
		{
			Write(GetGUID64(x));

			//// 
			//MemoryStream m = new MemoryStream(ToGenericDefinition(x).GUID.ToByteArray());

			///*
			//if (this.Session.Options.KeepFullNames)
			//{
			//    WriteSpecialChar();
			//    WriteSafeLiteral(x.FullName);
			//    WriteSpecialChar();
			//}
			//*/

			//WriteSpecialBase64(m.ToArray());
		}


		private void WriteSpecialBase64(byte[] e)
		{
			string name64 = Convert.ToBase64String(e).
				Replace("+", "_a").
				Replace("/", "_b").
				Replace("=", "");

			if (!char.IsLetter(name64[0]))
				WriteSpecialChar();

			Write(name64);
		}

		private void WriteGUIDAndToken(MemberInfo x)
		{
			WriteDecoratedGuid(ToGenericDefinition(x.DeclaringType).GUID);
			WriteSpecialChar();
			Write("{0:x4}", x.MetadataToken);
		}

		private void WriteGUIDAndToken64(MemberInfo x)
		{


			MemoryStream m = new MemoryStream(
				ToGenericDefinition(x.DeclaringType).GUID.ToByteArray());

			if (this.Session.Options.KeepFullNames)
			{
				WriteSpecialChar();
				WriteSafeLiteral(ToGenericDefinition(x.DeclaringType).FullName);
				WriteSpecialChar();
				WriteSafeLiteral(x.Name);
				WriteSpecialChar();
			}

			int m_token = x.MetadataToken;

			do
			{
				m.WriteByte((byte)(m_token & 0xff));
			}
			while ((m_token >>= 8) > 0);

			WriteSpecialBase64(m.ToArray());
		}



		private static bool IsSerializeableType(MemberInfo x, bool ndec)
		{
			if (SerializableAttribute.IsDefined(x.DeclaringType, typeof(SerializableAttribute)))
			{
				if (x.MemberType == MemberTypes.Field)
				{
					MemberInfo[] mi = x.DeclaringType.GetMember(x.Name);

					if (mi.Length > 0)
					{
						ndec = true;
					}
					else
					{
						Script.CompilerBase.BreakToDebugger(" *** private field not supported on serialize");
					}
				}
			}
			return ndec;
		}

		public void WriteDecoratedMemberInfo(MemberInfo x)
		{
			WriteDecoratedMemberInfo(x, false);
		}


		public void WriteDecorated(MethodBase x, LocalVariableInfo v)
		{

			if (v == null)
			{
				Write(GetSpecialLocalChar(0));

				// WriteSpecialChar(1);
			}
			else
			{
				Write(GetSpecialLocalChar(v.LocalIndex + 1 + x.GetParameters().Length));

				//WriteSpecialChar(1);
				//Write(v.LocalIndex + 1 + x.GetParameters().Length);
				//WriteHint("{0}", v.LocalType);
			}
		}

		#endregion

		public void WriteSpace() { Write(" "); }

		/// <summary>
		/// writes parameter list for a methodbase
		/// </summary>
		/// <param name="p"></param>
		/// <param name="i"></param>
		/// <param name="s"></param>
		/// <param name="offset"></param>
		public void WriteParameters(ILBlock.Prestatement p, ILInstruction i, ILFlow.StackItem[] s, int offset, MethodBase m)
		{
			ParameterInfo[] pi = m == null ? null : m.GetParameters();

			for (int si = offset; si < s.Length; si++)
			{
				if (si > offset)
					this.Helper.WriteDelimiter();

				if (pi != null && IL2ScriptGenerator.OpCodeEmitStringEnum(this, s[si], pi[si - offset].ParameterType))
					continue;

				IL2ScriptGenerator.OpCodeHandler(this, p, i, s[si]);
			}
		}



	}

}
