using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using java.applet;
using jsc.meta.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using jsc.meta.Tools;

namespace jsc.meta.Commands.Rewrite
{
	[Description("This command will tare an assembly to compile java and flash objects separatly.")]
	public partial class RewriteToJavaScriptDocument : CommandBase
	{

		/* How was this feature implemented in the long run?
		 * 
		 * 1. Adding a new command to the command chain
		 * 2. Test if the parameters are passed with a test project
		 * 3. Save to the svn
		 * 4. Rewrite components to their staging folders to be proccessed by the backend compilers
		 * 5. Get javascript to be compiled by jsc
		 * 6. Get flash to compile without alchemy
		 * 7. Get java to compile
		 */

		public override void Invoke()
		{
			this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));

			Console.WriteLine("RewriteToJavaScriptDocument: " + this.assembly.FullName);

			var assembly = Assembly.LoadFile(this.assembly.FullName);

			var targets = Enumerable.ToArray(
				from TargetType in assembly.GetTypes()

				let EntryPoint = TargetType.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault()

				where EntryPoint != null

				// what about Forms/Avalon ?
				let IsActionScript = typeof(Sprite).IsAssignableFrom(TargetType)
				let IsJava = typeof(Applet).IsAssignableFrom(TargetType)
				let IsJavaScript = !IsActionScript && !IsJava

				// possible name clash?
				let StagingFolder = this.staging.CreateSubdirectory(TargetType.FullName).ToConsole()

				select new { TargetType, EntryPoint, IsActionScript, IsJava, IsJavaScript, StagingFolder }
			);

			foreach (var k in targets)
			{
				// lets do a rewrite and inject neccesary bootstrap and proxy code

				var r = new RewriteToAssembly
				{
					assembly = this.assembly,
					staging = k.StagingFolder,

					PrimaryTypes = new[] { k.TargetType },

					product = k.TargetType.FullName,

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
						"jsc.meta->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
						"jsc->" +  Path.GetFileNameWithoutExtension( this.assembly.Name),
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
						"jsc.meta",
						"jsc"
					},
					#endregion

					
					PostTypeRewrite = 
						a =>
						{
							// we need to inject bootstrap code
							if (a.Type == a.context.TypeCache[k.TargetType])
							{
								if (k.IsJavaScript)
								{
									// look, we are injecting IL code :)
									// to bad jsc backend had to do this the ugly way in the past...
									InjectJavaScriptBootstrap(a);
								}
							}
						},

					PostRewrite =
						a =>
						{
							a.Assembly.DefineAttribute<ObfuscationAttribute>(
							   new
							   {
								   Feature = "script",
							   }
						    );

						}
				};

				r.Invoke();

				if (k.IsJava)
				{
					r.Output.ToJava(this.javapath, null, null, null, k.TargetType);
				}

				if (k.IsActionScript)
				{
					r.Output.ToActionScript(this.mxmlc, this.flashplayer, k.TargetType, null);
				}

				if (k.IsJavaScript)
				{
					r.Output.ToJavaScript();
				}
			}
		}


	}
}
