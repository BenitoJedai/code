using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using System.Diagnostics;
using ScriptCoreLib.YAML;

namespace YAMLExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class YAMLExample : Sprite
	{
		public YAMLExample()
		{
			KnownEmbeddedResources.Default["assets/YAMLExample/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);

			TestMethod1();
		}

		[Script]
		sealed class TestMethod1_Type
		{
			public string Field1;
			public string Field2;
		}

		public void TestMethod1()
		{
			var a = YAMLDocument.WriteMappingsSequence(
				typeof(TestMethod1_Type),
				new TestMethod1_Type { Field1 = "1", Field2 = "2" },
				new TestMethod1_Type { Field1 = "3", Field2 = "4" }
			);

			var t = new TextField { multiline = true, text = a, width = 400, height = 400 }.AttachTo(this);

			var n = (TestMethod1_Type[])
				YAMLDocument.FromMappingsSequence(
					typeof(TestMethod1_Type), a
				);

			Trace.Assert(n[0].Field1 == "1");
			Trace.Assert(n[0].Field2 == "2");

			Trace.Assert(n[1].Field1 == "3");
			Trace.Assert(n[1].Field2 == "4");

		}
	}

}