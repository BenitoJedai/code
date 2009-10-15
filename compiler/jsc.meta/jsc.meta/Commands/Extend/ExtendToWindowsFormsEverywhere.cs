using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using jsc.meta.Library;
using jsc.meta.Tools;
using ScriptCoreLib;
using System.Linq;
using ScriptCoreLib.Archive.ZIP;
using System.Diagnostics;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToWindowsFormsEverywhere
	{
		public FileInfo assembly;
		public string type;
		public DirectoryInfo staging;
		public DirectoryInfo javapath;

		/// <summary>
		/// Setting this field to false disables javascript generation.
		/// </summary>
		public bool javascript = true;

		/// <summary>
		/// When set to true the jar file is appended to the exe file
		/// thus creating a dual launchable. This option can be used
		/// if there is only one jar file. With multiple jar files
		/// this approach needs a special classloader.
		/// </summary>
		public bool javafusion = false;

		public bool operawidget;

		public bool jnlp;

		public void Invoke()
		{
			//Debugger.Launch();

			// todo: compare to http://tirania.org/blog/archive/2009/Jul-28.html

			// think windows forms but on javascript, followed by flash and java


			// http://social.msdn.microsoft.com/Forums/en-US/vbide/thread/0e946e63-a481-45b1-990d-af727914ff15

			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();


			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken),

				// in case we are converting windows forms to
				// java swing...
				typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
				typeof(ScriptCoreLibJava.Drawing.IAssemblyReferenceToken),
				typeof(ScriptCoreLibJava.Windows.Forms.IAssemblyReferenceToken)
			);

			var assembly = this.assembly.LoadAssemblyAtWithReferences(staging);


			new ExtendToWindowsFormsEverywhereBuilder
			{
				context = this,
				assembly = assembly
			}.Build();
		}
	}

	class ExtendToWindowsFormsEverywhereBuilder
	{
		const string MetaScript = "MetaScript";

		public ExtendToWindowsFormsEverywhere context;

		public Assembly assembly;

		public Type assembly_type;

		public void Build()
		{

			if (context.type == null)
				assembly_type = assembly.EntryPoint.DeclaringType;

			if (assembly_type == null)
				assembly_type = assembly.GetType(context.type);

			if (assembly_type == null)
				throw new InvalidOperationException("entrypoint type is missing");

			var JavaEntrypoint = default(MethodInfo);
			var JavaScriptEntrypoint = default(Type);

			// 2
			var MetaScript = InternalBuild(k => JavaEntrypoint = k,
				
				k => JavaScriptEntrypoint = k);
			// 3


			if (this.context.javascript)
			{
				MetaScript.ToJavaScript();
			}

			if (this.context.javapath != null)
			{
				//Debugger.Launch();

				var Fusion = new FileInfo(
					Path.Combine(
						MetaScript.Directory.FullName,
						Path.GetFileNameWithoutExtension(MetaScript.Name) + "Fusion" + Path.GetExtension(MetaScript.FullName)
					)
				);

				MetaScript.ToJava(context.javapath, JavaEntrypoint,
					this.context.javafusion ? Fusion : null
				);
			}

			#region web to .js.zip
			// zip files could be appended to exe files

			var staging_web = new DirectoryInfo(Path.Combine(this.context.staging.FullName, "web"));

			if (staging_web.Exists)
				if (this.context.operawidget)
				{
					var zip = new ZIPFile();

					foreach (var file in staging_web.CreateSubdirectory("assets").GetFiles("*", SearchOption.AllDirectories))
					{
						zip.Add(file.FullName.Substring(staging_web.FullName.Length + 1), File.ReadAllBytes(file.FullName));
					}

					foreach (var file in staging_web.GetFiles("*.js", SearchOption.TopDirectoryOnly))
					{
						zip.Add(file.FullName.Substring(staging_web.FullName.Length + 1), File.ReadAllBytes(file.FullName));
					}

					var index_html = staging_web.GetFiles().FirstOrDefault(
						k => k.Name == JavaScriptEntrypoint.Name + ".htm"
					);

					zip.Add("index.html",
						@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
<title>OrcasSimpleWindowsFormsApplication</title>
<!-- created at 1.09.2009 15:45:38 -->
<script type='text/javascript' src='ScriptCoreLib.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Drawing.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Query.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Avalon.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Windows.Forms.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Avalon.Integration.dll.js'></script>
<script type='text/javascript' src='ScriptCoreLib.Net.dll.js'></script>
<script type='text/javascript' src='MyContext.MyLocation.TextComponent.dll.js'></script>
<script type='text/javascript' src='MyContext.MyInformation.TextComponent.dll.js'></script>
<script type='text/javascript' src='MyContext.MyPanel.TextualUserControl.dll.js'></script>
<script type='text/javascript' src='OrcasMetaWindowsForms.exe.js'></script>
<script type='text/javascript' src='OrcasMetaWindowsFormsMetaScript.exe.js'></script>
<script></script>
</head>
<body style='margin: 0; overflow: hidden; -apple-dashboard-region: dashboard-region(control rectangle); '><noscript>ScriptApplication cannot run without JavaScript!</noscript>

<script type='text/xml' class='OrcasMetaWindowsFormsDocument'></script>
<script>
	window.resizeTo( screen.width, screen.height );
	window.moveTo( 0, 0 );
</script>

</body>
</html>

");

					zip.Add("config.xml",
						@"<?xml version='1.0' encoding='UTF-8'?>
<widget network='public'>
  <widgetname>OrcasMetaWindowsFormsDocument</widgetname>
  <description>Powered by jsc</description>
  <width>400</width>
  <height>300</height>
  <author>
    <name>Arvo Sulakakto</name>
    <email>dadeval@gmail.com</email>
    <link>http://zproxy.wordpress.com</link>
    <organization></organization>
  </author>
  <id>
    <host>jsc.sourceforge.net</host>
    <name>zproxy</name>
    <revised>2009-09</revised>
  </id>
  <icon>assets/ScriptCoreLib.Windows.Forms/App.ico</icon>
</widget>
"
					);

					var zzm = new MemoryStream();
					using (var w = new BinaryWriter(zzm))
					{
						zip.WriteTo(w);
					}

					File.WriteAllBytes(this.context.assembly.FullName + ".wgt", zzm.ToArray());

					//File.WriteAllBytes(this.context.zip.FullName, zzm.ToArray());
				}
			#endregion

		}


		FileInfo InternalBuild(Action<MethodInfo> AnnounceJavaEntrypoint, Action<Type> AnnounceJavaScriptEntrypoint)
		{
			// Main
			// C# project template in .net 4 has internal Main... who knew...
			//var assembly_type_Main = assembly_type.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			var assembly_type_Main = assembly_type.Assembly.EntryPoint;

			var name = new AssemblyName(assembly.GetName().Name + MetaScript);
			var Product = new FileInfo(Path.Combine(this.context.staging.FullName, name.Name + ".exe"));
			if (Product.Exists)
				Product.Delete();
			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, this.context.staging.FullName);
			var m = a.DefineDynamicModule(name.Name, name.Name + ".exe");


			// yay attributes
			var ScriptAttribute = typeof(ScriptCoreLib.ScriptAttribute);


			var AssemblyScriptAttribute = new ScriptAttribute
			{
				IsScriptLibrary = true,
				ScriptLibraries = new[] {
					assembly_type,
					typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Windows.Forms.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken),
					typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken),

					// in case we are converting windows forms to
					// java swing...
					typeof(ScriptCoreLibJava.IAssemblyReferenceToken),
					typeof(ScriptCoreLibJava.Drawing.IAssemblyReferenceToken),
					typeof(ScriptCoreLibJava.Windows.Forms.IAssemblyReferenceToken)							//typeof(ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken)
					},
				NonScriptTypes = assembly.GetTypes().Where(
					k =>
						k.Namespace != null && (
							k.Namespace.EndsWith(".My") ||
							k.Namespace.EndsWith(".My.Resources")
						)
	
				).ToArray()
			};

			a.DefineScriptAttribute(
				new
				{
					AssemblyScriptAttribute.IsScriptLibrary,
					AssemblyScriptAttribute.ScriptLibraries,
					AssemblyScriptAttribute.NonScriptTypes
				}
			);

		

			var DefineScriptTypeFilterAttribute = default(Func<ScriptType, string, ScriptTypeFilterAttribute>).DefineAttributeAt(a);


			// why did we need a redirect?
			// it is for .net entrypoint as it cannot be in another library
			// the redirect wont be translated
			#region CLR entrypoint
			{
				var t = m.DefineType(assembly_type.Namespace + ".CLR." + assembly_type.Assembly.GetName().Name + "Document");

				var main = t.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();


				if (assembly_type_Main.GetParameters().Length > 0)
					main_il.Emit(OpCodes.Ldarg_0);

				//main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);

				default(Func<STAThreadAttribute>).DefineAttributeAt(main)();

				t.CreateType();


				a.SetEntryPoint(main);
			}
			#endregion

			#region Java entrypoint
			{
				var t_redirect = m.DefineType(assembly_type.Namespace + ".Java." + assembly_type.Assembly.GetName().Name + "Document");

				default(Func<ScriptAttribute>).DefineAttributeAt(t_redirect)();

				var main = t_redirect.DefineMethod("Main", MethodAttributes.Static, CallingConventions.Standard, typeof(void), new[] { typeof(string[]) });
				var main_il = main.GetILGenerator();


				if (assembly_type_Main.GetParameters().Length > 0)
					main_il.Emit(OpCodes.Ldarg_0);

				//main_il.Emit(OpCodes.Ldnull);
				main_il.EmitCall(OpCodes.Call, assembly_type_Main, null);

				main_il.Emit(OpCodes.Ret);

				t_redirect.CreateType();

				DefineScriptTypeFilterAttribute(ScriptType.Java, t_redirect.Namespace);

				AnnounceJavaEntrypoint(main);

			}
			#endregion

			#region JavaScript entrypoint
			{
				var t = m.DefineType(assembly_type.Namespace + ".JavaScript." + assembly_type.Assembly.GetName().Name + "Document");

				var DefineScriptAttribute = default(Func<ScriptAttribute>).DefineAttributeAt(t);
				var DefineScriptApplicationEntryPointAttribute = default(Func<ScriptApplicationEntryPointAttribute>).DefineAttributeAt(t);

				DefineScriptAttribute();
				DefineScriptApplicationEntryPointAttribute();


				var t_ctor = t.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig, CallingConventions.HasThis, new Type[0]);
				var t_ctor_il = t_ctor.GetILGenerator();


				//L_0000: ldarg.0 
				//L_0001: call instance void [mscorlib]System.Object::.ctor()
				t_ctor_il.Emit(OpCodes.Ldarg_0);
				t_ctor_il.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));

				// could pass some params around...
				if (assembly_type_Main.GetParameters().Length > 0)
					t_ctor_il.Emit(OpCodes.Ldnull);

				t_ctor_il.EmitCall(OpCodes.Call, assembly_type_Main, null);
				t_ctor_il.Emit(OpCodes.Ret);


				#region spawn

				// we are going to show GUI from cctor.. this wont look nice under .net virtual machine! :)
				// it is only for jsc compiler at this time...
				var cctor = t.DefineTypeInitializer();
				var cctor_il = cctor.GetILGenerator();

				// L_0000: nop 
				//L_0001: ldtoken WindowsFormsApplication1Document.js.WindowsFormsApplication1Document
				//L_0006: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
				//L_000b: call void [ScriptCoreLib]ScriptCoreLib.JavaScript.Extensions.Extensions::Spawn(class [mscorlib]System.Type)
				//L_0010: nop 
				//L_0011: ret 

				cctor_il.Emit(OpCodes.Ldtoken, t);
				cctor_il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
				cctor_il.EmitCall(OpCodes.Call, ((Action<Type>)ScriptCoreLib.JavaScript.Extensions.Extensions.SpawnEntrypointWithBrandning).Method, null);

				cctor_il.Emit(OpCodes.Ret);

				#endregion



				var tt = t.CreateType();


				AnnounceJavaScriptEntrypoint(tt);

				DefineScriptTypeFilterAttribute(ScriptType.JavaScript, tt.Namespace);

			}
			#endregion




			a.Save(
				Product.Name
			);

			Product.Refresh();

			return Product;

		}
	}

}
