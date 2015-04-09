using System;
using System.Collections.Generic;
using System.Text;
//using Reflection;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Linq;
using ScriptCoreLib.Shared.BCLImplementation.System.Reflection;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// this is the most popular type?

	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Type.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs
	// http://sourceforge.net/p/jsc/code/HEAD/tree/core/ScriptCoreLib/JavaScript/BCLImplementation/System/Type.cs
	// https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/type.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Type.cs
	// http://referencesource.microsoft.com/#mscorlib/system/type.cs
	// https://github.com/mono/mono/tree/master/mcs/class/corlib/System/Type.cs

	// X:\opensource\github\JSIL\Proxies\Reflection.cs
	// "X:\opensource\github\Netjs\mscorlib.ts"
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Type.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Type.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Type.cs

	[Script(Implements = typeof(global::System.Type))]
	public class __Type : __MemberInfo, __IReflect
	{
		//  // System.Type is appdomain agile type. Appdomain agile types cannot have precise static constructors. Make
		// sure to never introduce one here!

		// https://github.com/dotnet/corefx/blob/master/src/Microsoft.CSharp/src/Microsoft/CSharp/RuntimeBinder/Semantics/Types/PredefinedTypes.cs

		// https://msdn.microsoft.com/en-us/library/dn600644(v=vs.110).aspx
		// <Type Name="App1.AppClass`1" Browse="Required PublicAndInternal" />
		// would it be a good idea to add metadata by jsc if .net native directives are found in the project?
		// X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\XRuntimeDirectives.rd.xml

		// https://github.com/dotnet/coreclr/blob/master/Documentation/type-loader.md

		// http://developers.slashdot.org/story/15/02/21/0142230/the-robots-that-will-put-coders-out-of-work
		//Strong AI is the first "computer program" that has the potential to automate the act of creativity.
		// Everything less can be a compiler, a pattern recognizer, an Uber driver, and in general a tool that does what it is told.


		// http://thenewstack.io/why-you-should-care-about-the-new-open-source-net-core/

		// https://github.com/xen2/SharpLang/blob/master/src/SharpLang.Compiler/Type.cs

		// X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs
		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs



		// what about string enums and complex enums, what about encypted enums?
		// "X:\jsc.svn\examples\javascript\Test\TestTypeOfEnum\TestTypeOfEnum.sln"
		public virtual bool IsEnum
		{
			get
			{
				// jsc erases enum typeinfo for jvm.

				return false;
			}
		}


		public override string ToString()
		{
			// X:\jsc.svn\examples\javascript\test\TestTypeOfByteArray\TestTypeOfByteArray\Application.cs

			if (IsArray)
			{
				// X:\jsc.svn\examples\javascript\test\TestTypeOfArray\TestTypeOfArray\Application.cs
				return this.GetElementType() + "[]";
			}


			// set by?
			if (IsNative)
				return "[native] " + this.Name;

			// X:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\Library\ZeProperties.cs
			// mimic nameof?
			return this.Name;
		}

		[Script]
		internal sealed class __AttributeReflection
		{
			public IFunction Type;
			public object Value;
		}

		[Script]
		internal sealed class __TypeReflection
		{
			public IFunction GetAttributes;
		}

		public __Assembly Assembly
		{
			get
			{
				return new __Assembly
				{

					__Value = (__AssemblyValue)Expando.InternalGetMember(AsExpando().constructor, "Assembly")
				};
			}
		}

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
		[Obsolete("InternalIsNative")]
		public bool IsNative
		{
			get
			{
				var constructor = AsExpando().constructor;


				// tested by?
				// ScriptCoreLib has marked that type as Native?
				return (bool)Expando.InternalIsMember(constructor, "IsNative");
			}
		}


		// where is this used?


		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			// X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
			// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs

			return o.GetType().TypeHandle;
		}

		public RuntimeTypeHandle TypeHandle
		{
			get;
			set;
		}




		#region GetFields
		public FieldInfo GetField(string name)
		{
			Reflection.__FieldInfo r = null;

			if (this.IsNative)
			{
				// we do not have the type information. behave as if dynamic
				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131208-expression
				return r = new Reflection.__FieldInfo { _Name = name, InternalDeclaringType = this };
			}

			foreach (var m in global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(this.TypeHandle.Value).GetFields())
			{
				if (m.Name == name)
				{
					r = new Reflection.__FieldInfo { _Name = m.Name, InternalDeclaringType = this };

					break;
				}

			}

			return r;
		}



		//public abstract FieldInfo[] GetFields(BindingFlags bindingAttr)
		public virtual global::System.Reflection.FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			// X:\jsc.svn\examples\javascript\async\Test\TestDelegateObjectScopeInspection\TestDelegateObjectScopeInspection\Application.cs
			// do know how to treat binding attr?

			return GetFields();
		}

		public global::System.Reflection.FieldInfo[] GetFields()
		{
			var a = new List<global::System.Reflection.FieldInfo>();

			foreach (var m in AsExpando().GetFields())
			{
				a.Add(new Reflection.__FieldInfo { _Name = m.Name, InternalDeclaringType = this });

			}


			return a.ToArray();
		}
		#endregion




		public Expando AsExpando()
		{
			// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Activator.cs

			return global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(this.TypeHandle.Value);
		}


		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
		// ldtoken
		public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
		{
			// or called by object? 
			return new __Type { TypeHandle = TypeHandle };
		}


		// set by X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Uint8ClampedArray.cs
		//public static Type InternalTypeOfByteArrayViaMakeArrayType;
		//public static Type InternalTypeOfByteArrayViaGetType;

		public static implicit operator Type(__Type e)
		{
			return (Type)(object)e;
		}
		public static implicit operator __Type(Type e)
		{
			return (__Type)(object)e;
		}


		public override string Name
		{
			get
			{
				// X:\jsc.svn\examples\javascript\test\TestTypeOfByteArray\TestTypeOfByteArray\Application.cs
				// X:\jsc.svn\examples\javascript\forms\test\TestTypeActivatorRef\TestTypeActivatorRef\Class1.cs

				// http://msdn.microsoft.com/en-us/library/dd264736.aspx
				//dynamic constructor = AsExpando().constructor;

				//return constructor.TypeName;


				var c = AsExpando().constructor;


				var n = (string)Expando.InternalGetMember(
					c,
					"TypeName"
				);

				// X:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\Library\ZeProperties.cs
				if (string.IsNullOrEmpty(n))
				{
					return ("?" + c);
				}

				return n;
			}
		}

		public string Namespace
		{
			get
			{
				// would we be able to zipit and send it for a worker to search in?

				// jsc does not yet emit namespace info
				return "<Namespace>";
			}
		}

		public string FullName
		{
			get
			{
				return Namespace + "." + Name;
			}
		}


		// called by?
		__TypeReflection Reflection
		{
			get
			{
				return ((__TypeReflection)(object)AsExpando().constructor);
			}
		}



		public Type[] GetInterfaces()
		{
			// does jsc keep interface type info yet? should have it by now yes..

			// X:\jsc.svn\examples\javascript\Test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs

			// wo do dont we, how else the as is works?
			return new Type[0];
		}

		public global::System.Reflection.ConstructorInfo GetConstructor(Type[] t)
		{
			// X:\jsc.svn\examples\javascript\linq\visualized\VisualizeWhere\VisualizeWhere\Application.cs
			// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs

			var c = new __ConstructorInfo
			{
				// do we support the primaryConstructor?

				InternalDeclaringType = this,
				InternalParameterTypes = t
			};

			return c;
		}





		// tested by?
		#region GetCustomAttributes
		public override object[] GetCustomAttributes(bool inherit)
		{
			return GetCustomAttributes(null, false);
		}


		public override object[] GetCustomAttributes(Type x, bool inherit)
		{
			if (inherit)
				throw new NotSupportedException();

			if (Reflection.GetAttributes == null)
				return new object[0];

			var i = new List<object>();

			foreach (var v in (__AttributeReflection[])Reflection.GetAttributes.apply(Reflection))
			{
				var c = true;

				if (x != null)
					if (!object.ReferenceEquals(v.Type.prototype, x.TypeHandle.Value))
						c = false;

				// todo: rebuild to known type
				if (c)
					i.Add(v.Value);
			}

			return i.ToArray();
		}
		#endregion





		#region InternalEquals
		public bool Equals(Type o)
		{
			return InternalEquals(this, (__Type)(object)o);
		}

		public static bool operator !=(__Type left, __Type right)
		{
			return !InternalEquals(left, right);
		}

		public static bool operator ==(__Type left, __Type right)
		{
			return InternalEquals(left, right);
		}

		public bool Equals(__Type e)
		{
			return InternalEquals(this, e);
		}

		private static bool InternalEquals(__Type x, __Type e)
		{
			// X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.DataSource.cs

			object xx = x;
			object ee = e;

			if (xx == null)
			{
				if (ee == null)
					return true;

				return false;
			}

			if (ee == null)
			{
				return false;
			}

			object a = x.TypeHandle.Value;
			object b = e.TypeHandle.Value;

			return a == b;
		}
		#endregion


		// when should we keep the nested type info?
		public override Type DeclaringType
		{
			get { throw new NotImplementedException(); }
		}


		#region GetTypeIndex
		// used by X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
		public static object GetTypeIndex(string Name, Type TargetType)
		{
			// X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ApplicationWebService.cs

			var AllMemberNames = Expando.Of(Native.self).GetMemberNames();

			var TargetTypeHandle = TargetType.TypeHandle;
			var prototype = (object)TargetTypeHandle.Value;


			//function IzkeSBiD_aTGMsWPjgYVYEg() {}
			//IzkeSBiD_aTGMsWPjgYVYEg.TypeName = "IDataParameter";
			//IzkeSBiD_aTGMsWPjgYVYEg.Assembly = _7ryscGGN80KExNOXH5xlgw;
			//IzkeSBiD_aTGMsWPjgYVYEg.Interfaces = 
			//{
			//  f7G82WqfyzOLoZ_b8v0KVxw: 1
			//};

			//var type$IzkeSBiD_aTGMsWPjgYVYEg = IzkeSBiD_aTGMsWPjgYVYEg.prototype;
			//type$IzkeSBiD_aTGMsWPjgYVYEg.constructor = IzkeSBiD_aTGMsWPjgYVYEg;


			var prototype_constructor = Expando.InternalGetMember(prototype, "constructor");
			if (prototype_constructor == null)
				return null;


			//0:4257ms { Name = foo, prototype_constructor_TypeName =  } 

			var prototype_constructor_TypeName = Expando.InternalGetMember(prototype_constructor, "TypeName");
			if (prototype_constructor_TypeName == null)
				return null;

			//Console.WriteLine(new { Name, prototype_constructor_TypeName });

			var TargetTypeIndex = AllMemberNames.FirstOrDefault(
				  item =>
				  {
					  // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
					  dynamic self = Native.self;
					  object value = self[item];

					  if (value == prototype)
					  {
						  //Console.WriteLine(new { item, f });
						  return true;
					  }

					  return false;
				  }
			);


			// what are we to return?
			return TargetTypeIndex;
		}
		#endregion






		// X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs

		public virtual bool IsAssignableFrom(Type TargetType)
		{
			// jsc rewriter could replace it with is operator for static cases?

			// X:\jsc.svn\examples\javascript\test\TestIsAssignableFrom\TestIsAssignableFrom\Application.cs
			// X:\jsc.svn\examples\javascript\test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs

			// IAsyncStateMachine vs c

			//o33aimgYBj_aJ1Gk6clAOrw.Interfaces = 
			//  {
			//    f7G82WqfyzOLoZ_b8v0KVxw: 1
			//  };

			//// TestIsAssignableFrom.foo
			//function o33aimgYBj_aJ1Gk6clAOrw() { }
			//o33aimgYBj_aJ1Gk6clAOrw.TypeName = "foo";
			//  o33aimgYBj_aJ1Gk6clAOrw.Assembly = YQ3YcnWoDE6CyQ7lsi20IQ;

			var TargetTypeHandle = TargetType.TypeHandle;
			var prototype = (object)TargetTypeHandle.Value;

			if (prototype == null)
			{
				//throw new Exception("IsAssignableFrom:482 prototype null " + new { TargetType });
				return false;
			}

			var prototype_constructor = Expando.InternalGetMember(prototype, "constructor");
			if (prototype_constructor == null)
				return false;


			//0:4257ms { Name = foo, prototype_constructor_TypeName =  } 

			var prototype_constructor_Interfaces = Expando.InternalGetMember(prototype_constructor, "Interfaces");
			if (prototype_constructor_Interfaces == null)
				return false;

			// now if we are an interface, then there will be a match

			var this_prototype = (object)this.TypeHandle.Value;

			if (this_prototype == null)
			{
				return false;
			}
			//throw new Exception("IsAssignableFrom:501 this_prototype null " + new { TargetType });

			var this_prototype_constructor = Expando.InternalGetMember(this_prototype, "constructor");

			// we should not compare names, we should resolve and compare refs

			var any = Expando.InternalGetMemberNames(prototype_constructor_Interfaces).AsEnumerable().Any(
				item =>
				{
					dynamic self = Native.self;
					object value = self[item];

					if (value == this_prototype_constructor)
					{
						//Console.WriteLine(new { item, f });
						return true;
					}
					return false;
				}
			);


			return any;
		}

		public virtual bool IsInstanceOfType(object o)
		{
			if (o == null)
				return false;

			return IsAssignableFrom(o.GetType());
		}



		// how would we know its a byte array?
		public bool IsArray { get; set; }

		public virtual Type MakeArrayType()
		{
			// X:\jsc.svn\examples\javascript\test\TestDynamicToArray\TestDynamicToArray\Application.cs

			return new __Type
			{
				InternalElementType = this,

				IsArray = true
			};
		}

		public Type InternalElementType;

		public Type GetElementType()
		{
			// X:\jsc.svn\examples\javascript\test\TestTypeOfArray\TestTypeOfArray\Application.cs
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401

			// tested by?
			// typeofArray?

			// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
			// X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Query\Experimental\QueryExpressionBuilder.AsEnumerable.cs
			// MakeArray ?

			return InternalElementType;
		}
	}
}
