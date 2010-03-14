using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using jsc.meta.Library.Templates;
using System.Reflection;
using System.Collections;
using jsc.Languages.IL;

namespace jsc.meta.Library
{
	public static class CostumAttributeBuilderExtensions
	{
		public static Func<ILTranslationContext, CustomAttributeBuilder> ToCustomAttributeBuilder(this object a)
		{
			var x = Enumerable.FirstOrDefault(
				from t in new[] { a.GetType() }

				// is there a constructor which saves all parameters to fields?

				let ctor = Enumerable.First(

					from c in t.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
					let args = c.GetParameters()
					orderby args.Length descending
					let il = new ILBlock(c)

					let ldarg_stfld = Enumerable.ToArray(
						from i in il.Instructrions
						let p = i.TargetParameter
						where p != null
						let stfld = i.NextInstruction
						let f = stfld.TargetField
						where f != null
						group f by p
					)

					where ldarg_stfld.Length == args.Length


					select new { c, args, ldarg_stfld }
				)

				let ctor_params = ctor.ldarg_stfld.Select(k => k.First().GetValue(a)).ToArray()

				let def = Activator.CreateInstance(t, ctor_params)

				let properties = Enumerable.ToArray(
					from p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					where p.CanRead
					where p.CanWrite
					let value = p.GetValue(a, null)
					where !Equals(value, p.GetValue(def, null), p.PropertyType)
					select new { p, value }
				)

				let fields = Enumerable.ToArray(
					from p in t.GetFields(BindingFlags.Public | BindingFlags.Instance)
					where !p.IsInitOnly
					where !ctor.ldarg_stfld.Any(k => k.First() == p)
					let value = p.GetValue(a)
					where !Equals(value, p.GetValue(def), p.FieldType)
					select new { p, value }
				)

				select new { ctor, ctor_params, properties, fields }
			);


			return context =>
			{
				Func<object[], object[]> ff =
					e => e.Select(k => k is Type ? context.TypeCache[(Type)k] : k).ToArray();

				// Property must be on the same type of the given ConstructorInfo.

				return new CustomAttributeBuilder(
					context.ConstructorCache[x.ctor.c],
					ff(x.ctor_params),

					x.properties.Select(k => context.PropertyCache[k.p]).ToArray(),
					ff(x.properties.Select(k => k.value).ToArray()),

					x.fields.Select(k => context.FieldCache[k.p]).ToArray(),
					ff(x.fields.Select(k => k.value).ToArray())

				);
			};
		}

		internal static bool Equals(object a, object b, Type t)
		{
			// are we doing the right thing here?
			// we need to compare primitives and objects...

			var Comparer = typeof(Comparer<>).MakeGenericType(t);
			var Default = (IComparer)Comparer.GetProperty("Default").GetValue(null, null);

			return Default.Compare(a, b) == 0;
		}

		public static void TestFeature()
		{
			var i = typeof(MyClass).GetCustomAttributes(false).Select(k => ToCustomAttributeBuilder(k)).ToArray();
		}
	}
	namespace Templates
	{
		[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
		sealed class MyAttribute : Attribute
		{
			// See the attribute guidelines at 
			//  http://go.microsoft.com/fwlink/?LinkId=85236
			readonly string positionalString;

			// This is a positional argument
			public MyAttribute(string positionalString)
			{
				this.positionalString = positionalString;

			}

			public MyAttribute(string positionalString, int i)
			{
				this.positionalString = positionalString;

			}

			public string PositionalString
			{
				get { return positionalString; }
			}

			// This is a named argument
			public int NamedInt { get; set; }

			public int Field1;
		}

		[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
		sealed class My2Attribute : Attribute
		{
			// See the attribute guidelines at 
			//  http://go.microsoft.com/fwlink/?LinkId=85236
			readonly string positionalString;

			// This is a positional argument

			public My2Attribute()
			{

			}

			public My2Attribute(string positionalString)
			{

				this.positionalString = positionalString;

			}

			public string PositionalString
			{
				get { return positionalString; }
			}

			// This is a named argument
			public int NamedInt { get; set; }

			public int Field1;
		}

		[My("Y", Field1 = 1, NamedInt = 2)]
		class MyClass
		{

		}
	}
}
