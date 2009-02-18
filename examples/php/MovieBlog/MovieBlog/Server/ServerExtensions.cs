using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MovieBlog.Server
{
	[Script]
	public static class ServerExtensions
	{
		public static string ToLink(this string text, string href)
		{
			return "<a href='" + href + "'>" + text + "</a>";
		}

		public static void ToImageToConsole(this string src)
		{
			Console.WriteLine("<img src='" + src + "' />");
		}

		public static void ToImageToConsoleWithStyle(this string src, string style)
		{
			Console.WriteLine("<img src='" + src + "' style='" + style + "' />");
		}

		public static Func<T, T> ToChainedFunc<T>(this Func<T, T> e, int count)
		{
			return
				value =>
				{
					var p = e(value);


					for (int i = 1; i < count; i++)
					{
						p = e(p);
					}

					return p;
				};
		}

		public static Func<T, T> ToChainedFunc<T>(this Func<T, T> e, Func<T, T, bool> reinvoke)
		{
			return
				value =>
				{
					var x = value;
					var p = e(x);

					while (reinvoke(x, p))
					{
						x = p;
						p = e(x);
					}

					return p;
				};
		}

		[Script]
		public interface IParseAttributeToken_ParseAttribute : IParseAttributeToken_ParseContent, IParseAttributeToken_Parse
		{
			string Name { get; }
			


		}

		[Script]
		public interface IParseAttributeToken_ParseContent : IParseAttributeToken_Parse
		{
		
		}



		[Script]
		public interface IParseAttributeToken_Parse
		{
			IParseAttributeToken_ParseAttribute AttributeHandler { get; }

			Action<string> SetValue { get; }


			IParseAttributeToken_Context Context { get; }
		}

		[Script]
		public interface IParseAttributeToken_Context
		{
			string Source { get; }
		}

		[Script]
		public class ParseAttributeToken : IParseAttributeToken_ParseAttribute, IParseAttributeToken_Context
		{
			public string Name { get; set; }
			public Action<string> SetValue { get; set; }

			public string Source { get; set; }

			public IParseAttributeToken_Context Context { get; set; }

			public IParseAttributeToken_ParseAttribute AttributeHandler { get; set; }
		}

		public static IParseAttributeToken_ParseAttribute ParseAttribute(this string element, string name, Action<string> setvalue)
		{
			var Context = new ParseAttributeToken { Source = element };

			return new ParseAttributeToken
			{
				Name = name,
				SetValue = setvalue,
				Context = Context,
			};
		}

		public static IParseAttributeToken_ParseAttribute ParseAttribute(this IParseAttributeToken_ParseAttribute element, string name, Action<string> setvalue)
		{
			return new ParseAttributeToken
			{
				AttributeHandler = element,
				Name = name,
				SetValue = setvalue,
				Context = element.Context,
			};
		}

		public static IParseAttributeToken_Parse ParseContent(this IParseAttributeToken_ParseAttribute element, Action<string> setvalue)
		{
			return new ParseAttributeToken
			{
				AttributeHandler = element,
				SetValue = setvalue,
				Context = element.Context,
			};
		}

		public static void Parse(this IParseAttributeToken_Parse element)
		{
			var Source = element.Context.Source;

			var element_start = Source.IndexOf("<");

			var attributes_start = Source.IndexOf(" ", element_start);

			var attibutes_end = Source.IndexOf(">", element_start);

			var tag = "";

			if (attributes_start < attibutes_end)
			{
				tag = Source.Substring(element_start + 1, attributes_start - element_start - 1);

				// seek for attributes

				element.AttributeHandler.InternalParseAttributes(Source.Substring(attributes_start, attibutes_end - attributes_start));
			}
			else
			{
				tag = Source.Substring(element_start + 1, attibutes_end - element_start - 1);

				// seek for no attributes
			}

			var element_end = Source.IndexOf("</" + tag, attibutes_end);

			var content = Source.Substring(attibutes_end + 1, element_end - attibutes_end - 1);

			element.SetValue(content);
		}

		static void InternalParseAttributes(this IParseAttributeToken_ParseAttribute element, string data)
		{
			Action<string, string> SetValue =
				(name, value) =>
				{
					var p = element;

					while (p != null)
					{
						if (p.Name == name)
						{
							if (p.SetValue != null)
								p.SetValue(value);

							p = null;
						}
						else
						{
							p = p.AttributeHandler;
						}
					}
				};

			Func<int, int> ParseAttribute =
				offset =>
				{
					var equals = data.IndexOf("=", offset);

					if (equals < 0)
						return offset;

					var name = data.Substring(offset + 1, equals - offset - 1);

					var value_start = data.IndexOf("\"", equals);

					if (value_start < 0)
						return offset;

					var value_end = data.IndexOf("\"", value_start + 1);

					if (value_end < 0)
						return offset;

					var value = data.Substring(value_start + 1, value_end - value_start - 1);

					SetValue(name, value);

					return value_end + 1;
				};

			ParseAttribute.ToChainedFunc((x, y) => y > x)(0);
		}

		public static Func<A, T> ToAnonymousConstructor<T, A>(this T prototype, Func<A, T> ctor)
		{
			return ctor;
		}
	}
}
