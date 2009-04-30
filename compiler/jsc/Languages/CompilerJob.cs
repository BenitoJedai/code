using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages
{
	using ScriptCoreLib;

	using Languages;
	using Script;

	public partial class CompilerJob
	{
		//public CompilerJob ParentJob;

		public FileInfo AssamblyFile;
		public Assembly AssamblyInfo;

		public List<ScriptNamespaceRenameAttribute> NamespaceRenameList = new List<ScriptNamespaceRenameAttribute>();

		public ScriptNamespaceRenameAttribute[] GetNamespaceRenameList()
		{
			return (ScriptNamespaceRenameAttribute[])Attribute.GetCustomAttributes(AssamblyInfo, typeof(ScriptNamespaceRenameAttribute), false);
		}

		public ScriptTypeFilterAttribute[] GetTypeFilterList()
		{
			return (ScriptTypeFilterAttribute[])Attribute.GetCustomAttributes(AssamblyInfo, typeof(ScriptTypeFilterAttribute), false);
		}

		public ScriptTypeFilterAttribute[] GetTypeFilterListByType(ScriptType e)
		{
			var u = new List<ScriptTypeFilterAttribute>();

			foreach (ScriptTypeFilterAttribute var in GetTypeFilterList())
			{
				if (var.Type == e)
					u.Add(var);
			}

			return u.ToArray();
		}

		public ScriptTypeFilterAttribute[] GetTypeFilterListByType(ScriptType e, Assembly context)
		{
			var u = GetTypeFilterListByType(e);

			if (u.Length == 0)
				if (ScriptAttribute.OfProvider(context).ScriptLibraries.Any(k => k.Assembly == this.AssamblyInfo))
					return new[] { new ScriptTypeFilterAttribute(e) };



			return u;
		}


		public static void Compile(string p, CompileSessionInfo sinfo)
		{
			sinfo.Logging.LogMessage("will compile '{0}'", p);

			// we need to build namespace rename and depends tree for function ommition

			CompilerJob j = new CompilerJob();

			j.AssamblyInfo = Assembly.LoadFile(p);
			j.AssamblyFile = new FileInfo(p);

			if (!j.AssamblyFile.Exists)
			{
				sinfo.Logging.LogMessage("file not found");


				return;
			}

			//Environment.CurrentDirectory = j.AssamblyFile.DirectoryName;

			Assembly.LoadFile(j.AssamblyFile.FullName);


			// we need all the namespace fixups from all assemblies
			foreach (var reference in ScriptCoreLib.SharedHelper.LoadReferencedAssemblies(j.AssamblyInfo, true))
			{
				var n = reference.GetCustomAttributes<ScriptNamespaceRenameAttribute>();

				// todo: deal with overlapping attributes here
				j.NamespaceRenameList.AddRange(n);
			}


			// we support java only at this time

			////bool _java = false;

			////foreach (ScriptTypeFilterAttribute var in j.GetTypeFilterListByType(ScriptType.Java))
			////{
			////    _java = true;

			////    sinfo.Logging.LogMessage(" * assambly contains '{0}'", var.FilterTypeName);
			////}

			// compile for language # java

			if (sinfo.Options.IsJava)
				if (j.GetTypeFilterListByType(ScriptType.Java).Any())
					CompileJava(j, sinfo);

			if (sinfo.Options.IsActionScript)
				if (j.GetTypeFilterListByType(ScriptType.ActionScript).Any())
					CompileActionScript(j, sinfo);

			if (sinfo.Options.IsCSharp2)
				if (j.GetTypeFilterListByType(ScriptType.CSharp2, Assembly.LoadFile(sinfo.Options.TargetAssembly.FullName)).Any())
					CompileCSharp2(j, sinfo);

			if (sinfo.Options.IsC)
				if (j.GetTypeFilterListByType(ScriptType.C).Any())
					CompileC(j, sinfo);

		}


		//private Type[] LoadTypes(ScriptType scriptType)
		//{
		//    return LoadTypes(scriptType, this.AssamblyInfo);
		//}

		public static Type[] LoadTypes(ScriptType scriptType, Assembly context)
		{
			List<Type> a = new List<Type>();

			foreach (Assembly x in SharedHelper.LoadReferencedAssemblies(context, true))
			{
				if (ScriptAttribute.OfProvider(x) == null)
				{
					// it better be a script library
					a.AddRange(x.GetTypes());
				}
				else
					a.AddRange(ScriptAttribute.FindTypes(x, scriptType));

			}


			return a.ToArray();
		}




		internal string NamespaceFixup(string _ns, Type context)
		{
			if (string.IsNullOrEmpty(_ns))
				return "";

			foreach (var v in this.NamespaceRenameList.OrderByDescending(i => i.NativeNamespaceName.Length))
			{

				if (_ns.StartsWith(v.NativeNamespaceName))
				{
					if (v.FilterToIsNative && !context.ToScriptAttributeOrDefault().IsNative)
						continue;

					_ns = v.VirtualNamespaceName + _ns.Substring(v.NativeNamespaceName.Length);

					if (_ns.StartsWith("."))
						_ns = _ns.Substring(1);

					break;
				}
			}

			return _ns;
		}

		public static void InvokeEntryPoints(DirectoryInfo dir, Assembly a)
		{
			foreach (Type v in a.GetTypes())
			{
				BindingFlags all = BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

				MethodInfo[] m = v.GetMethods(all);

				foreach (MethodInfo z in m)
				{
					if (z.IsStatic)
					{
						ParameterInfo[] p = z.GetParameters();

						if (p.Length == 1)
						{
							if (p[0].ParameterType == typeof(IEntryPoint))
							{
								z.Invoke(null, new object[] { new DefaultEntryPointWrapper(dir) });
							}
						}
					}
				}
			}
		}

		class DefaultEntryPointWrapper : IEntryPoint
		{
			DirectoryInfo dir;

			public DefaultEntryPointWrapper(DirectoryInfo e)
			{
				dir = e;
			}

			#region IEntryPoint Members

			public void Define(string filename, string content)
			{
				Console.WriteLine(filename);

				FileInfo f = new FileInfo(dir.FullName + "/" + filename);

				StreamWriter x = new StreamWriter(new FileStream(f.FullName, FileMode.Create));


				x.Write(content);
				x.Close();
			}

			public string this[string filename]
			{
				set { this.Define(filename, value); }
			}

			#endregion
		}



	}
}
