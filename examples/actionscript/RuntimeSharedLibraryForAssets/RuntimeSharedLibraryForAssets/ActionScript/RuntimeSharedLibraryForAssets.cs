using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;

namespace RuntimeSharedLibraryForAssets.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[ScriptImportsType("flash.utils.getDefinitionByName")]
	public class RuntimeSharedLibraryForAssets : Sprite
	{

		// http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getDefinitionByName()
		[Script(OptimizedCode = "return flash.utils.getDefinitionByName(e);")]
		public static object getDefinitionByName(string e)
		{
			return default(object);
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		public RuntimeSharedLibraryForAssets()
		{

			//for (var j = 0.0; j < 1; j += 0.1)
			//{
			//    this.graphics.beginFill(0xff0000, j);
			//    this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
			//    this.graphics.endFill();
			//}

			//var step = 100;
			//for (int i = 0; i < 4; i++)
			//{
			//    addChild(
			//       new TextField
			//       {
			//           text = "hello world",
			//           x = step * i,
			//           y = 20,
			//           textColor = 0x00ff00,
			//           sharpness = 400
			//       });
			//}

		    var t = new TextField
		    {
		        text = "powered by jsc",
		        x = 20,
		        y = 40,
		        selectable = false,
		        sharpness = -400,
		        textColor = 0xffffff
		    }.AttachTo(this);

			//var a = ((Class)getDefinitionByName("SomeGenericAssets.ActionScript.Assets")).CreateType();

			// http://books.google.ee/books?id=r4Xl06bwKyAC&pg=PA21&lpg=PA21&dq=flex+file-specs&source=web&ots=9ELbZmqKep&sig=1uUN7L-T-xzqW8PHI6Hv1ImhxCU&hl=et&sa=X&oi=book_result&resnum=1&ct=result#PPA32,M1
			// http://www.nabble.com/Bizarre-link-report-and-load-externs-problems-in-FB3-plugin-td15904495.html
			// http://livedocs.adobe.com/flex/3/html/help.html?content=rsl_02.html#220679

			t.click +=
				delegate
				{
					var a = new SomeGenericAssets.ActionScript.Assets();

					a[SomeGenericAssets.ActionScript.Assets.Path + "/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);

				};
		}

	}
}