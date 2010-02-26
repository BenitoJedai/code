﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using jsc;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToAssembly
	{
		// usage: c:\util\jsc\bin\jsc.meta.exe RewriteToAssembly /assembly:"$(TargetPath)"
		// usage: RewriteToAssembly /assembly:"C:\work\jsc.svn\examples\javascript\OrcasUltraWebApplication2\Rewrite1\bin\Debug\Rewrite1.dll"
		//

		// todo: generics, interfaces, and opcodes.leave need to be fixed!

		public string product;

		/// <summary>
		/// Types within these assemblies will be merged to the new primary assembly
		/// </summary>
		public MergeInstruction[] merge = new MergeInstruction[0];

		public class MergeInstruction
		{
			public string name;

			public static implicit operator MergeInstruction(string e)
			{
				return new MergeInstruction { name = e };
			}
		}

		public FileInfo assembly;


		public DirectoryInfo staging;

		public string type;

		public bool AttachDebugger;

		/// <summary>
		/// We should be translating complex IL to more simple IL.
		/// For example switch statements could be translated to
		/// if statements. We might want to use jsx and then after simplifing 
		/// IL run jsc on the generated assembly.
		/// 
		/// jsc is the default translation provider which just happens
		/// to understand our ScriptAttribute
		/// 
		/// jsx happens to be a more advanced IL reader than jsc
		/// 
		/// if in the future any vendor comes up with a better solution
		/// which we can implement we will consider them too.
		/// 
		/// Some target languages wont implement specific features
		/// for which we will need to simplify IL anyhow.
		/// 
		/// We could also be inlining methods and delete them.
		/// </summary>
		public bool simplify;

		/// <summary>
		/// We can provide obfuscation features. Simply by renaming all
		/// methods would do. We could also make the IL harder for disassamblers
		/// like reflector.
		/// </summary>
		public bool obfuscate = false;

		internal Delegate codeinjecton;
		internal Func<Assembly, object[]> codeinjectonparams;

		public class NamespaceRenameInstructions
		{
			// we could provide namespace renaming to provide 
			// brand support
			public string rule;

			public static implicit operator NamespaceRenameInstructions(string e)
			{
				return new NamespaceRenameInstructions { rule = e };
			}

			public string From
			{
				get
				{
					return rule.Substring(0, rule.IndexOf("->"));
				}
			}

			public string To
			{
				get
				{
					return rule.Substring(rule.IndexOf("->") + 2);
				}
			}
		}

		public NamespaceRenameInstructions[] rename;

		internal Assembly _assembly;

		VirtualDictionary<string, string> NameObfuscation = new VirtualDictionary<string, string>();

		public Type[] PrimaryTypes = new Type[0];


		public class AssemblyRewriteArguments
		{
			public jsc.Languages.IL.ILTranslationContext context;
			public ModuleBuilder Module;
			public AssemblyBuilder Assembly;

			ScriptResourceWriter _ScriptResources;
			public ScriptResourceWriter ScriptResourceWriter
			{
				get
				{
					if (_ScriptResources == null)
						_ScriptResources = new ScriptResourceWriter(Assembly, Module);

					return _ScriptResources;
				}
			}
		}

		public Action<AssemblyRewriteArguments> PostRewrite;
		public Action<AssemblyRewriteArguments> PreRewrite;


		public class TypeRewriteArguments : AssemblyRewriteArguments
		{
			public Type SourceType;
			public TypeBuilder Type;
		}

		public Action<TypeRewriteArguments> PostTypeRewrite;
		public Action<TypeRewriteArguments> PreTypeRewrite;

		public event Action<TypeRewriteArguments> TypeCreated;

		public class BeforeInstructionsArguments : TypeRewriteArguments
		{
			public MethodInfo SourceMethod;
			public MethodBuilder Method;

			public Func<ILGenerator> GetILGenerator;
		}

		// to be phased out
		public Action<BeforeInstructionsArguments> BeforeInstructions;

		public event Action<MethodBase, ILTranslationExtensions.EmitToArguments> AtILOverride;

		public FileInfo Output;

		public ILTranslationContext ExternalContext = new ILTranslationContext
		{
			ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>(),
			MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>(),
			TypeCache = new VirtualDictionary<Type, Type>(),
			TypeRenameCache = new VirtualDictionary<Type,string>(),
			FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>()
		};


	}
}
