using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;
using ScriptCoreLib.JavaScript.Runtime;


namespace ExposedFunctions.js
{
	// using SerializableAttribute will tell jsc to not obfuscate field names
	// also json objects do not contain any type information
	[Script, Serializable]
	public sealed class Data1
	{
		public string text;
		public int index;


		public IFunction onclick;
		public IFunction onmouseover;
		public IFunction onmouseout;

		public IFunction GetString;
	}

	[Script, ScriptApplicationEntryPoint]
	public class ExposedFunctions
	{
		public const string ExposedFunctions_AddData1_Example1 = "javascript:ExposedFunctions_AddData1({text: 'hello world', index: 100});";
		public const string ExposedFunctions_AddData1_Example2 = "javascript:ExposedFunctions_AddData1({text: 'the text', index: 42});";
		public const string ExposedFunctions_AddData1_Example3 = "javascript:ExposedFunctions_AddData1(null);";

		// using NoDecoration will enable us javascript:ExposedFunctions_AddData1({})
		// this will also work for php code
		[Script(NoDecoration = true)]
		public static void ExposedFunctions_AddData1(Data1 e)
		{

			if (e == null)
			{
				new IHTMLDiv { innerText = "yay, no data..." }.AttachTo(instructions);
				return;
			}

			Func<string> GetString = () => new { e.text, e.index }.ToString();

			// this function can now be used by external API
			e.GetString = IFunction.OfDelegate(GetString);

			var div = new IHTMLDiv
			{
				// using a real anonymous type, we get a nice key value string for display
				innerText = GetString()
			};

			#region attach events
			if (e.onclick != null)
			{
				div.style.cursor = IStyle.CursorEnum.pointer;
				div.onclick +=
					delegate
					{
						e.onclick.apply(e, div);
					};
			}

			if (e.onmouseover != null)
			{
				div.style.cursor = IStyle.CursorEnum.pointer;
				div.onmouseover +=
					delegate
					{
						e.onmouseover.apply(e, div);
					};
			}

			if (e.onmouseout != null)
			{
				div.style.cursor = IStyle.CursorEnum.pointer;
				div.onmouseout +=
					delegate
					{
						e.onmouseout.apply(e, div);
					};
			}
			#endregion

			div.AttachTo(instructions);
		}

		static IHTMLDiv instructions;

		public ExposedFunctions()
		{
			// this variable is static
			// if this application would be initiated multiple times
			// the last initiation will simply overwrite that variable
			instructions = new IHTMLDiv(
				 @"
<p>
	This C# to JavaScript project has exposed a method for scripting. 
	You can either click on the link or copy it's href to the adressbar.
</p>
<h3>Method exposed statically</h3>
<ul>
"

				// we will let the browser to parse this html snippet
				// as it contains a javascript href
				+ "<li>" + ExposedFunctions_AddData1_Example1.ToLink() + "</li>"
				+ "<li>" + ExposedFunctions_AddData1_Example2.ToLink() + "</li>"
				+ "<li>" + ExposedFunctions_AddData1_Example3.ToLink() + "</li>"
			+ @"
</ul>
			"
			);


			instructions.style.padding = "2em";



			// previously we defined a static method to be exposed
			// lets define one dynamically

			var MethodName = "ExposedFunctions_Dynamic1";

			var DynamicExample1 = "javascript:" + MethodName + "({text: 'hello', index: 1});";
			var DynamicExample2 = "javascript:" + MethodName + "({text: 'world', index: 2});";

			Action<Data1> DynamicEntry =
				data =>
				{
					// to prove the dynamic behaviour we shall modify the values
					// passed to us and we will in turn pass them to the static
					// entry method
					ExposedFunctions_AddData1(
						new Data1
						{
							index = data.index + 1,
							text = "dynamic: " + data.text
						}
					);
				};


			// In c# there will be dynamic object support which could
			// help with more magic.
			// Future code would look like this:
			// Expando.window[MethodName] = DynamicEntry

			// until then we must use more verbose code...
			Expando.Of(Native.Window).SetMember(MethodName, IFunction.OfDelegate(DynamicEntry));

			// lets also advertise this dynamic method

			// modifing innerHTML directly will invalidate any references we 
			// have to any of its childre, which we do not have at this time...
			instructions.innerHTML +=
				@"
<h3>Method exposed dynamically</h3>
<ul>
"

				// we will let the browser to parse this html snippet
				// as it contains a javascript href
				+ "<li>" + DynamicExample1.ToLink() + "</li>"
				+ "<li>" + DynamicExample2.ToLink() + "</li>"
			+ @"
</ul>";

			new IHTMLImage(Assets.Path + "/Preview.png").AttachTo(instructions);
			instructions.AttachToDocument();

		}


		static ExposedFunctions()
		{
			typeof(ExposedFunctions).SpawnTo(i => new ExposedFunctions());
		}

	}

}
