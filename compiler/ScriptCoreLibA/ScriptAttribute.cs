using System.Runtime.CompilerServices;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib.CSharp.Extensions;

namespace ScriptCoreLib
{






	[global::System.AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptParameterByRefAttribute : Attribute
	{

	}



	[global::System.AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptParameterByValAttribute : Attribute
	{

	}

	[global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptDelegateDataHintAttribute : Attribute
	{
		// this class shall be omitted from the future versions of jsc
		// jsc needs to infer this information by itself

		public enum FieldType
		{
			List,
			Target,
			Method
		}

		public readonly FieldType Value;

		public ScriptDelegateDataHintAttribute(FieldType Value)
		{
			this.Value = Value;
		}
	}

	[global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptMethodThrows : Attribute
	{
		public Type ThrowType;

		public ScriptMethodThrows(Type e)
		{
			ThrowType = e;
		}

		public static ScriptMethodThrows[] ArrayOfProvider(ICustomAttributeProvider m)
		{
			try
			{
				ScriptMethodThrows[] s = m.GetCustomAttributes(typeof(ScriptMethodThrows), false) as ScriptMethodThrows[];

				return s;
			}
			catch (Exception exc)
			{
				throw exc;
			}
		}
	}





	/// <summary>
	/// allows the compiler to detect wether it is out of date. If this value is higher than the one from the compiler the compile proccess fill halt with an error.
	/// </summary>
	[Obsolete]
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptVersionAttribute : Attribute
	{
		public int Value;

		public ScriptVersionAttribute(int e)
		{
			this.Value = e;
		}

	}

	// AllowMultiple should be set
	[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class ScriptAttribute : Attribute
	{
		public const string InternalConstructorDefault = "InternalConstructor";

		public const string Prototype = "prototype";

		public const string MetadataMember = "$0";

		public const string MetadataMemberTypeName = "$0";
		public const string MetadataMemberDefaultConstructor = "$1";

		/// <summary>
		/// When set to true, another class must define the implementation. This is useful when there is a shared class for several languages but its implementation varies within supported languages.
		/// </summary>
		public bool NotImplementedHere = false;

		/// <summary>
		/// provides inline concat operation / to be replaced with inline functions?
		/// </summary>
		public string StringConcatOperator = null;

		/// <summary>
		/// a constant is enclosed between { and }, also arguments are supported
		/// like {arg*}
		/// </summary>
		public bool UseCompilerConstants = false;

		/// <summary>
		/// overides default pointer name definiton in c
		/// </summary>
		public string PointerName = null;

		public ScriptAttribute()
		{

		}


		public string Header;

		/// <summary>
		/// system headers will be inside &lt; and &gt;, as of user provided headers will be in qoutes
		/// </summary>
		public bool IsSystemHeader;

		/// <summary>
		/// Defines a lib which will be called upon static constructor in java.
		/// In C this should point for example to winmm.lib.
		/// </summary>
		public string LibraryImport;


		/// <summary>
		/// Native class implementation is provided by runtime. The end compiler
		/// will need to be able to find their definition. 
		/// 
		/// Body of a native method will never be emitted.
		/// 
		/// While targeting C a delegate can be marked as IsNative. This delegate
		/// will then be treated as typeless static function pointer.
		/// </summary>
		public bool IsNative;

		/// <summary>
		/// if attached on a class, overrides new operator 
		/// (InternalConstructor can be set to true, but should 
		/// not be defined for external constructor specific signatures), 
		/// if attached to a static field then it overrides it
		/// renames static class
		/// </summary>
		public string ExternalTarget;

		/// <summary>
		/// provides a way to redirect emthods
		/// </summary>
		public Type Implements;

		/// <summary>
		/// global::System.IDisposable - csharp.IDisposableImplementation - null
		/// global::System.String - csharp.String - java.lang.String
		/// </summary>
		public Type ImplementationType;

		/// <summary>
		/// used in java compiler as JNI needs methods be marked to be native
		/// function body should be compiled as JNI C++ Native library
		/// </summary>
		public bool IsPInvoke;

		/// <summary>
		/// if set to true, the method will never be able to throw exceptions
		/// and all inner execptions will be caught and forgotten
		/// </summary>
		public bool NoExeptions;

		public string GetConstructorAlias()
		{
			if (InternalConstructor)
				return InternalConstructorDefault;

			return null;
		}

		/// <summary>
		/// setting this attruibute to true, tells the compiler, 
		/// it has no prototype and constructor code is relocated
		/// </summary>
		public bool InternalConstructor;

		/// <summary>
		/// This field will mark the primary scriptcorelib for the target language.
		/// In this assembly additional types may be written to support the runtime.
		/// </summary>
		public bool IsCoreLib;

		/// <summary>
		/// All types are now implicitly being attached to a default [Script]. 
		/// Every type referenced in this assembly must have a BCL implementation
		/// in its target language. Every defined type in this shall be converted
		/// to its target language
		/// </summary>
		//[Obsolete("You should mark your assembly via [assembly: Obfuscation(Feature = \"script\")] instead")]
		public bool IsScriptLibrary;

		/// <summary>
		/// Assemblies referenced with these types shall be treated as script libraries
		/// </summary>

		// this might be worth considered as an extra feature
		//[Obsolete("You should mark your assembly via [assembly: Obfuscation(Feature = \"script\")] instead")]

		public Type[] ScriptLibraries;

		/// <summary>
		/// This assembly level field can be used to define types which should not be considered as a script type.
		/// </summary>
		public Type[] NonScriptTypes;

		/// <summary>
		/// supports the ldlen opcode
		/// </summary>
		public bool IsArray;

		/// <summary>
		/// referenced type is the enumerator for the native array
		/// </summary>
		public bool IsArrayEnumerator;

		/// <summary>
		/// param 0 is target
		/// param 1 is method
		/// params 2 - n are arguments to be passed
		/// </summary>
		/// public bool     IsInvokeMemberWrapper;

		public bool IsDebugCode;
		public bool ILToConsole;



		/// <summary>
		/// defines a functionas out of bound member, which actually is a static member
		/// </summary>
		public bool DefineAsStatic;

		/// <summary>
		/// a static method is compiled as an instance method
		/// </summary>
		public bool DefineAsInstance;


		/// <summary>
		/// In some target languages method overload requires some name decoration in order to work. 
		/// In ActionScript you can apply this flag to a type or assembly to prevent the name decoration.
		/// As such using overloaded methods will result in compile error in a later step.
		/// </summary>
		public bool NoDecoration;




		bool _HasNoPrototype = false;
		/// <summary>
		/// instance members will not be declared when set to true, neither
		/// will prototype be declared.<br />
		/// System prototype might still exist
		/// </summary>
		public bool HasNoPrototype
		{
			get
			{
				// this may make rewriting attribtes difficult!
				return _HasNoPrototype || GetConstructorAlias() != null;
			}
			set
			{
				_HasNoPrototype = value;
			}
		}

		[Obsolete]
		public string BinaryOperator;

		/// <summary>
		/// inline source code in native language.
		/// 
		/// a constant is enclosed between { and }, also arguments are supported
		/// like {arg*}
		/// </summary>
		public string OptimizedCode;

		/// <summary>
		/// allows per assambly level html
		/// </summary>
		[Obsolete]
		public string InlineHTML;

		/// <summary>
		/// enum members get written as literals
		/// </summary>
		public bool IsStringEnum;


		public static ScriptAttribute Of(ICustomAttributeProvider m)
		{
			return OfProvider(m);
		}

		public class ScriptLibraryContext : IDisposable
		{
			// we are so breaking thread safety here...

			public readonly Assembly Context;

			Type[] InternalNonScriptTypes;

			public Type[] NonScriptTypes
			{
				get
				{
					if (InternalNonScriptTypes == null)
					{

						InternalNonScriptTypes = this.Context.ToScriptAttributeOrDefault().NonScriptTypes ?? new Type[0];
					}

					return InternalNonScriptTypes;
				}
			}

			public ScriptLibraryContext(Assembly value)
			{
				this.Context = value;

				OfProviderContext.Add(this);
			}

			#region IDisposable Members

			public void Dispose()
			{
				OfProviderContext.Remove(this);
			}

			#endregion
		}

		internal static List<ScriptLibraryContext> OfProviderContext = new List<ScriptLibraryContext>();

		/// <summary>
		/// A script library can now be marked via [assembly: Obfuscation(Feature = "script")]
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public static bool IsScriptLibraryViaObfuscationAttribute(ICustomAttributeProvider m)
		{
			var o = (ObfuscationAttribute)(m is Type ? (m as Type).Assembly : m).GetCustomAttributes(typeof(ObfuscationAttribute), false).FirstOrDefault();

			if (o != null)
				if (o.Feature == "script")
					return true;

			return false;
		}

		static Dictionary<ICustomAttributeProvider, ScriptAttribute> CachedOfProvider = new Dictionary<ICustomAttributeProvider, ScriptAttribute>();

		public static ScriptAttribute OfProvider(ICustomAttributeProvider m)
		{
			if (m == null)
				return null;

			try
			{
				if (m is Type)
				{
					// a context assembly can define any type to not be translated
					// we might want to cache this
					if (OfProviderContext.Any(k => k.NonScriptTypes.Contains(m)))
						return null;
				}

				ScriptAttribute[] s = m.GetCustomAttributes(typeof(ScriptAttribute), false) as ScriptAttribute[];

				var x = s.Length == 0 ? null : s[0];

				var t = m as Type;
				if (t != null)
				{
					if (t.Assembly.ToScriptAttributeOrDefault().IsScriptLibrary)
						x = new ScriptAttribute();

				}

				if (x == null)
				{
					if (IsScriptLibraryViaObfuscationAttribute(m))
						x = new ScriptAttribute();
				}


				// Whatif a corelib was marked as a IsScriptLib
				// this will confuse jsc currently...

				//if (x == null)

				if (x == null)
					if (t != null)
						if (Enumerable.Any(
							from p in OfProviderContext
							let ScriptLibraries = p.Context.ToScriptAttributeOrDefault().ScriptLibraries
							where ScriptLibraries != null
							from l in ScriptLibraries

							// A library which takes matters in its own hand
							// should keep doing that...

							where l.Assembly.GetCustomAttributes(typeof(ScriptAttribute), false).Length == 0
							where l.Assembly == t.Assembly

							select new { p, l }
							))
							x = new ScriptAttribute();


				return x;
			}
			catch (Exception exc)
			{
				throw exc;
			}
		}


		public static bool IsAnonymousType(Type z)
		{
			if (IsCompilerGenerated(z) && z.IsSealed && z.IsNotPublic && z.Namespace == null)
			{
				// while using it we have a solid type!


				// there should be not public declared fields
				if (z.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Length > 0)
					goto not_an_anonymous_type;



				return true;
			}

		not_an_anonymous_type:

			return false;
		}

		public static Type[] FindTypes(Assembly a, ScriptType _scripttype)
		{
			List<Type> MyTypes = new List<Type>();

			if (ScriptAttribute.OfProvider(a) == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				ScriptTypeFilterAttribute[] f = ScriptTypeFilterAttribute.ArrayOf(a, _scripttype);


				if (f.Length > 0)
				{
					#region filters are defined


					//Console.WriteLine("scripttype filters defined for assambly {0}", a);

					//foreach (ScriptTypeFilterAttribute fi in f)
					//    Console.WriteLine(fi);

					foreach (Type z in a.GetTypes())
					{
						// lets detect anonymous types
						if (IsAnonymousType(z))
						{
							// found one, yuppei!
							MyTypes.Add(z);
							continue;
						}

						// check for type filter, and if off bounds then skip


						bool bOutOfNamespace = true;


						foreach (ScriptTypeFilterAttribute fi in f)
							if (fi.MatchType(z))
							{
								//Console.WriteLine("+ {0} : {1} [{2}, {3}]", z.FullName, fi.Filter, fi.Type, _scripttype);
								bOutOfNamespace = false;
								break;
							}





						if (bOutOfNamespace)
						{
							//Console.WriteLine("- {0}", z.FullName);

							continue;
						}

						ScriptAttribute o = ScriptAttribute.Of(z, true);

						if (o != null)
						{


							MyTypes.Add(z);
							continue;
						}




						if (z.IsEnum /* || IsCompilerGenerated(z) */)
						{
							if (IsNestedTypeOfScriptType(z))
							{
								MyTypes.Add(z);
							}
							else
							{
								Console.WriteLine(" type ignored : {0}", z.FullName);

							}
						}
					}
					#endregion

				}
				else
				{
					var Obfuscation = (ObfuscationAttribute)a.GetCustomAttributes(typeof(ObfuscationAttribute), false).FirstOrDefault();

					if (Obfuscation != null)
					{
						if (Obfuscation.Feature == "script")
							return a.GetTypes();
					}
				}
			}

			return MyTypes.ToArray();
		}

		static bool IsNestedTypeOfScriptType(Type t)
		{
			Type x = t.DeclaringType;

			while (x != null)
			{
				if (ScriptAttribute.Of(x, false) != null)
					return true;

				x = x.DeclaringType;
			}

			return false;

		}

		public static bool IsCompilerGenerated(MethodBase t)
		{
			if (t == null)
				return false;

			object[] cc = t.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);


			if (cc.Length != 0)
			{
				if (cc[0] as CompilerGeneratedAttribute != null)
					return true;
			}
			else
				return IsCompilerGenerated(t.DeclaringType);


			return false;
		}

		public static bool IsCompilerGenerated(Type t)
		{
			if (t == null)
				return false;

			object[] cc = t.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);


			if (cc.Length == 0)
			{
				return IsCompilerGenerated(t.DeclaringType);
			}
			else
			{
				if (cc[0] as CompilerGeneratedAttribute != null)
					return true;
			}

			return false;
		}

		public static ScriptAttribute Of(MethodBase m)
		{
			return OfProvider(m);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="p">looks declaring types if not found at this type</param>
		/// <returns></returns>
		public static ScriptAttribute Of(Type type, bool p)
		{
			if (p)
			{
				try
				{
					Type x = type;

					while (x != null)
					{
						ScriptAttribute a = OfProvider(x);

						if (a != null)
							return a;

						x = x.DeclaringType;


					}
				}
				catch
				{
					return null;
				}

				return null;
			}
			else
				return OfProvider(type);
		}

		public static global::ScriptCoreLib.ScriptAttribute OfTypeMember(Type type, string name)
		{
			MemberInfo[] mi = type.GetMember(name);

			if (mi.Length == 1)
			{
				return OfProvider(mi[0]);
			}

			return null;
		}
	}



}
