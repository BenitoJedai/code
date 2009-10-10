﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Reflection;
using System.Reflection.Emit;
using jsc.meta.Library;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace jsc.meta.Commands.Reference
{
	/// <summary>
	/// So how do we go about using this command?
	/// 
	/// Use these steps to use this command:
	/// 1. Add project folder "MyContent.TextComponent"
	/// 2. Add a few named .txt files with content in it
	/// 3. Add a prebuild event - C:\util\jsc\bin\jsc.meta.exe ReferenceTextComponent /ProjectFileName:"$(ProjectPath)" /IsComponent:false
	/// 4. Rebuild and also allow to reload your project
	/// 5. Check for a new referenced assembly
	/// </summary>
	public class ReferenceTextComponent
	{
		/*
		 
http://msdn.microsoft.com/en-us/library/0c6xyb66(VS.71).aspx
		  
None - The file is not included in the project output group and is not compiled in the build process. An example is a text file that contains documentation, such as a Readme file.
Compile - The file is compiled into the build output. This setting is used for code files.
Content - The file is not compiled, but is included in the Content output group. For example, this setting is the default value for an .htm or other kind of Web file.
Embedded Resource - This file is embedded in the main project build output as a DLL or executable. It is typically used for resource files.
		 
		 */

		const string TextComponent = "TextComponent";

		// this is an example of how jsc.meta
		// could be used as a code generator
		// before any jsc kicks in

		/// <summary>
		/// This is this csproj file. We should also support VB project file
		/// as they should really not be that different.
		/// </summary>
		public FileInfo ProjectFileName;

		/// <summary>
		/// The generated type will inherit from global::System.ComponentModel.Component
		/// </summary>
		public bool IsComponent = true;

		/// <summary>
		/// We would need StringBuilder which may or may not be available
		/// on the target platform
		/// </summary>
		public bool ImplementToString = true;

		public void Invoke()
		{
			//Debugger.Launch();


			var csproj = XDocument.Load(ProjectFileName.FullName);
			var csproj_dirty = false;



			/*

<Project ToolsVersion="3.5" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
	<RootNamespace>AutoGeneratedReferences</RootNamespace>

  <ItemGroup>
	<Reference Include="System" />

  <ItemGroup>
	<None Include="Components\JohDoe.TextComponent" />
			*/

			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
			var nsItemGroup = ns + "ItemGroup";
			var nsRootNamespace = ns + "RootNamespace";
			var nsPropertyGroup = ns + "PropertyGroup";
			var nsNone = ns + "None";
			var nsContent = ns + "Content";
			var nsDependentUpon = ns + "DependentUpon";
			var nsReference = ns + "Reference";
			var nsHintPath = ns + "HintPath";
			var nsAssemblyName = ns + "AssemblyName";

			var SourceAssemblyName = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from AssemblyName in PropertyGroup.Elements(nsAssemblyName)
				 select AssemblyName.Value
			);

			var DefaultNamespace = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
				 select RootNamespace.Value
			);

			// bin is assumed to being ignored by svn
			// we need to stage it
			var Staging = this.ProjectFileName.Directory.CreateSubdirectory("bin/" + TextComponent + ".staging");

			Action<FileInfo, AssemblyName> AddReference =
				(AssemblyFile, Name) =>
				{

					/* add reference
<Reference Include="AutoGeneratedReferences.Components.JohDoe.TextComponent, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>bin\staging\AutoGeneratedReferences.Components.JohDoe.TextComponent.dll</HintPath>
</Reference>
					*/

					var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

					if (!Enumerable.Any(
						 from ItemGroup in csproj.Root.Elements(nsItemGroup)
						 from Reference in ItemGroup.Elements(nsReference)
						 from HintPath in Reference.Elements(nsHintPath)
						 where TargetHintPath == HintPath.Value
						 select new { HintPath, Reference, ItemGroup }
						))
					{
						var TargetItemGroup = Enumerable.First(
							from ItemGroup in csproj.Root.Elements(nsItemGroup)
							from Reference in ItemGroup.Elements(nsReference)
							select ItemGroup
						);

						TargetItemGroup.Add(
							new XElement(nsReference,
								new XAttribute("Include", Name.ToString()),
								new XElement(nsHintPath, TargetHintPath)
							)
						);

						csproj_dirty = true;

					}
				};

			#region take 1 - single file to single assembly
			foreach (var h in
			  from ItemGroup in csproj.Root.Elements(nsItemGroup)
			  from None in ItemGroup.Elements(nsNone)
			  let Include = None.Attribute("Include").Value
			  where Include.EndsWith("." + TextComponent)
			  let TargetName = DefaultNamespace + "." + Include.Replace("/", ".").Replace("\\", ".")
			  let Target = new FileInfo(Path.Combine(Staging.FullName, TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))
			  let File = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))
			  select new { ItemGroup, None, Include, File, TargetName, Target }
			  )
			{
				GenerateAssembly(SourceAssemblyName, h.Target, h.TargetName, Staging, new[] { h.File },
					Name => AddReference(h.Target, Name)
				);


			}
			#endregion

			#region take 2 - multiple files to single assembly

			var TextComponentFolders =
			  from ItemGroup in csproj.Root.Elements(nsItemGroup)
			  from None in ItemGroup.Elements(nsNone).Concat(ItemGroup.Elements(nsContent))
			  let Include = None.Attribute("Include").Value
			  let Directory = Path.GetDirectoryName(Include)
			  where Directory.EndsWith("." + TextComponent)

			  let TargetName = DefaultNamespace + "." + Directory.Replace("/", ".").Replace("\\", ".")
			  let Target = new FileInfo(Path.Combine(Staging.FullName, TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))

			  let File = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))
			  group new { ItemGroup, None, Include, File, Directory, TargetName, Target } by Directory;

			foreach (var h in TextComponentFolders)
			{

				GenerateAssembly(SourceAssemblyName,
					h.First().Target,
					h.First().TargetName,
					Staging, h.Select(k => k.File).ToArray(),
					Name => AddReference(h.First().Target, Name)
				);
			}

			#endregion

			foreach (var k in
				from ItemGroup in csproj.Root.Elements(nsItemGroup)
				from Reference in ItemGroup.Elements(nsReference)
				from HintPath in Reference.Elements(nsHintPath)
				let HintPathFile = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, HintPath.Value))
				where HintPathFile.Directory.FullName == Staging.FullName

				where !Enumerable.Any(
					from _ItemGroup in csproj.Root.Elements(nsItemGroup)
					from _None in _ItemGroup.Elements(nsNone)
					let _Include = _None.Attribute("Include").Value
					where _Include.EndsWith("." + TextComponent)
					let _TargetName = DefaultNamespace + "." + _Include.Replace("/", ".").Replace("\\", ".")
					let _Target = new FileInfo(Path.Combine(Staging.FullName, _TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))
					where _Target.FullName == HintPathFile.FullName
					select _None
				)

				where !Enumerable.Any(
					from k in TextComponentFolders
					where k.First().Target.FullName == HintPathFile.FullName
					select k
				)

				select new { Reference, HintPathFile }
				)
			{
				// cleanup - do we have any references to staged dll's whose
				// source is no longer available - remove it


				k.Reference.Remove();
				k.HintPathFile.Delete();

				csproj_dirty = true;
			}



			if (csproj_dirty)
				csproj.Save(this.ProjectFileName.FullName);
		}

		void GenerateAssembly(string SourceAssemblyName, FileInfo Target, string TargetName, DirectoryInfo Staging, FileInfo[] Sources, Action<AssemblyName> Dirty)
		{
			var name = new AssemblyName(Path.GetFileNameWithoutExtension(Target.Name));


			if (Target.Exists)
				if (Sources.All(s => Target.LastWriteTime > s.LastWriteTime))
				{
					Dirty(name);
					return;
				}



			var a = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, Staging.FullName);

			#region mark our generated assemblies as script aware

			a.DefineAttribute<ObfuscationAttribute>(
				new { Feature = "script" }
			);
			a.DefineAttribute<CompilerGeneratedAttribute>(new { });



			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					default(Func<string, string, System.CodeDom.Compiler.GeneratedCodeAttribute>).ToConstructorInfo(),
					new object[] { Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version.ToString() }
				)
			);

			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					default(Func<string, AssemblyDescriptionAttribute>).ToConstructorInfo(),
					new object[] { @"This assembly was generated by a pre build event and is internal to the calling assembly." }
				)
			);

			a.SetCustomAttribute(
				new CustomAttributeBuilder(
					default(Func<string, InternalsVisibleToAttribute>).ToConstructorInfo(),
					new object[] { SourceAssemblyName }
				)
			);

			#endregion


			var m = a.DefineDynamicModule(name.Name, Target.Name);

			var t = m.DefineType(
				TargetName.Substring(0, TargetName.Length - (TextComponent.Length + 1)),
				TypeAttributes.Public,
				IsComponent ?
					typeof(global::System.ComponentModel.Component)
					: typeof(object)
			);

			var Fields = Enumerable.ToArray(
				from s in Sources
				let FieldName = Sources.Length == 1 ?
				   "Text" : Path.GetFileNameWithoutExtension(s.Name)
				let Field = t.DefineField(FieldName, typeof(string), FieldAttributes.Public | FieldAttributes.InitOnly)
				select new { s, FieldName, Field }
			 );



			var t_ctor = t.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

			{
				var il = t_ctor.GetILGenerator();

				if (IsComponent)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, typeof(System.ComponentModel.Component).GetConstructor(new Type[0]));
				}

				foreach (var k in Fields)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldstr, File.ReadAllText(k.s.FullName));
					il.Emit(OpCodes.Stfld, k.Field);
				}

				il.Emit(OpCodes.Ret);
			}

			if (ImplementToString)
				t.DefineToStringMethod(from k in Fields select k.Field);

			t.DefineWriteToMethod(from k in Fields select k.Field);
		

			t.CreateType();



			a.Save(
				Target.Name
			);


			Dirty(name);
		}
	}
}
