using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;

namespace jsc.meta.Commands.Extend
{
	public class ExtendToAvalonEverywhere
	{
		public FileInfo assembly;
		public string type;
		public DirectoryInfo staging;
		public FileInfo zip;
		public DirectoryInfo flexpath;

		/// <summary>
		/// Setting this field to false disables javascript generation.
		/// </summary>
		public bool javascript = true;

		/// <summary>
		/// The swf file shall be converted to an exe file.
		/// </summary>
		public bool flashprojector = false;

		public void Invoke()
		{
			if (this.staging == null)
				this.staging = this.assembly.Directory.CreateSubdirectory("staging");
			else if (!staging.Exists)
				this.staging.Create();

			staging.DefinesTypes(
				typeof(ScriptCoreLib.ScriptAttribute),
				typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Net.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Query.IAssemblyReferenceToken),
				typeof(ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken)
			);

			var assembly = this.assembly.LoadAssemblyAtWithReferences(staging);



			new Builder
			{
				context = this,
				assembly = assembly
			}.Invoke();
		}

		public class Builder
		{
			const string MetaScript = "MetaScript";

			public ExtendToAvalonEverywhere context;

			public Assembly assembly;

			public Type assembly_type;

			public void Invoke()
			{
				if (context.type == null)
					assembly_type = assembly.EntryPoint.DeclaringType;

				if (assembly_type == null)
				{
					// we actually need to look at who will be called
					// with or .ToWindow function
					// and we need to mark this type as NonScript
					assembly_type = assembly.GetType(context.type);
				}

				if (assembly_type == null)
					throw new InvalidOperationException("entrypoint type is missing");
			}
		}
	}
}
