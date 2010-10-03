using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using jsc;
using jsc.Languages.IL;
using jsc.Library;
using jsc.meta.Library;

namespace jsc.meta.Commands.Rewrite
{
    partial class RewriteToAssembly
    {
        // usage: c:\util\jsc\bin\jsc.meta.exe RewriteToAssembly /assembly:"$(TargetPath)"
        // usage: c:\util\jsc\bin\jsc.meta.exe RewriteToAssembly /assembly:"$(TargetPath)" /CodeTrace:"$(TargetDir)$(TargetName).CodeTrace$(TargetExt)" /Output:"$(TargetDir)$(TargetName).Rewrite$(TargetExt)"
        // usage: RewriteToAssembly /assembly:"C:\work\jsc.svn\examples\javascript\OrcasUltraWebApplication2\Rewrite1\bin\Debug\Rewrite1.dll"
        // usage: RewriteToAssembly /Output:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\bin\Debug\UltraLibraryWithAssets.merged.dll" /AssemblyMerge:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\bin\Debug\UltraLibraryWithAssets.dll" /AssemblyMerge:"W:\jsc.svn\templates\Orcas\UltraLibraryWithAssets\UltraLibraryWithAssets\bin\Debug\UltraLibraryWithAssets.UltraSource.dll"


        // todo: generics, interfaces, and opcodes.leave need to be fixed!

        public string product;
        public string productExtension = ".dll";

        #region BCLImplementationMergeAssemblies
        /// <summary>
        /// These assemblies will be searched for BCLImplementations
        /// </summary>
        public BCLImplementationMergeAssembly[] BCLImplementationMergeAssemblies = new BCLImplementationMergeAssembly[0];

        public class BCLImplementationMergeAssembly
        {
            public string name;

            public static implicit operator BCLImplementationMergeAssembly(string e)
            {
                return new BCLImplementationMergeAssembly { name = e };
            }
        }
        #endregion

        #region MergeInstruction
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
        #endregion

        #region AssemblyMerge
        /// <summary>
        /// Multiple assemblies can be merged into single assembly. No types from these assemblies shall be omitted.
        /// </summary>
        public AssemblyMergeInstruction[] AssemblyMerge = new AssemblyMergeInstruction[0];

        public class AssemblyMergeInstruction
        {
            public string name;

            public static implicit operator AssemblyMergeInstruction(string e)
            {
                return new AssemblyMergeInstruction { name = e };
            }

            public override string ToString()
            {
                return name;
            }

            public override int GetHashCode()
            {
                return this.name.GetHashCode();
            }
        }
        #endregion



        public FileInfo assembly;


        public DirectoryInfo staging;

        public string type;

        public bool AttachDebugger;

     

        /// <summary>
        /// We can provide obfuscation features. Simply by renaming all
        /// methods would do. We could also make the IL harder for disassamblers
        /// like reflector.
        /// </summary>
        public bool obfuscate = false;

        [Obsolete]
        internal Delegate codeinjecton;
        [Obsolete]
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
                        _ScriptResources = new ScriptResourceWriter(Assembly, Module, context);

                    return _ScriptResources;
                }
            }
        }

        public Action<AssemblyRewriteArguments> PostAssemblyRewrite;
        public Action<AssemblyRewriteArguments> PreAssemblyRewrite;


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

        public FileInfo OutputStrongNameKeyPair;

        public ILTranslationContext ExternalContext = new ILTranslationContext
        {
            ConstructorCache = new VirtualDictionary<ConstructorInfo, ConstructorInfo>(),
            MethodCache = new VirtualDictionary<MethodInfo, MethodInfo>(),

            // Definition?
            TypeCache = new VirtualDictionary<Type, Type>(),
            TypeRenameCache = new VirtualDictionary<Type, string>(),
            FieldCache = new VirtualDictionary<FieldInfo, FieldInfo>()
        };



        /// <summary>
        /// By setting this argument to true, assemblies which are marked with
        /// [Obfuscation(Feature = "merge")]
        /// no longer get merged.
        /// </summary>
        public bool DisableIsMarkedForMerge;

        public bool DisableInvalidMergeCheck;

        /// <summary>
        /// To rewrite a loaded module this flag must be set.
        /// </summary>
        public bool EnableDelayedFileMove;


        public bool EnableSwitchRewrite;

        /// <summary>
        /// When a method is found with this name it is made an entrypoint.
        /// </summary>
        public string EntryPoint;

        public string EntryPointAssembly;

        public bool PEVerify;

        public DirectoryInfo MicrosoftVisualStudio = new DirectoryInfo(@"c:\Program Files\Microsoft Visual Studio 10.0\");
        public DirectoryInfo MicrosoftWindowsSDK = new DirectoryInfo(@"C:\Program Files\Microsoft SDKs\Windows\v7.0A");

        public bool Pause;
    }
}
