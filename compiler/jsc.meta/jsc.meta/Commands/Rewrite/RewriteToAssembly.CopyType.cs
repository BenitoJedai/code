using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;

namespace jsc.meta.Commands.Rewrite
{
		public partial class RewriteToAssembly
	{
		public void CopyType(
			Type source,
			AssemblyBuilder a,
			ModuleBuilder m,
			VirtualDictionary<Type, Type> TypeCache,
			VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
			VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
			VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
			TypeBuilder OverrideDeclaringType)
		{
			var t = default(TypeBuilder);

			// we might define as a nested type instead!
			if (source.IsNested)
			{
				t = (OverrideDeclaringType ?? ((TypeBuilder)TypeCache[source.DeclaringType])).DefineNestedType(source.Name, source.Attributes, source.BaseType, source.GetInterfaces());
			}
			else
			{
				t = m.DefineType(FullNameFixup(source.FullName), source.Attributes, source.BaseType, source.GetInterfaces());
			}

			TypeCache[source] = t;

			foreach (var f in source.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var ff = t.DefineField(f.Name, TypeCache[f.FieldType], f.Attributes);

				TypeFieldCache[source].Add(ff);
			}




			foreach (var k in source.GetConstructors(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = ConstructorCache[k];
			}

			foreach (var k in source.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var km = MethodCache[k];
			}

			foreach (var k in source.GetProperties(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				var kp = t.DefineProperty(k.Name, k.Attributes, null, null);

				var _SetMethod = k.GetSetMethod();
				if (_SetMethod != null)
					kp.SetSetMethod((MethodBuilder)MethodCache[_SetMethod]);

				var _GetMethod = k.GetGetMethod();
				if (_GetMethod != null)
					kp.SetGetMethod((MethodBuilder)MethodCache[_GetMethod]);

			}

			t.CreateType();
		}

	

		
	}
}
