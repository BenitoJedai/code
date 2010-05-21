using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Ultra.Library
{

    /// <summary>
    /// Whenever jsc converts an assembly it will emit a version file. This file could also contain the result
    /// from previous builds. This would enable pre built assemblies.
    /// </summary>
    public class CachedFileGeneratorBase
    {
        /* Motivation: Speed boost for new projects.
         * 
         * Implementation log:
         * 
         * 1. Let's start replacing jsc javascript version feature.
         * 2. A local token will be ~/web/version/assembly.language.version.txt"
         * 3. A remote token will be %App_Data%/jsc/assemblyhash/assembly.language.version.zip
         * 4. A missing local token means a rebuild 
         * 5. An existing remote token means a copy will be sufficient
         * 6. Remote tokens for ScriptCoreLib assemblies can be generated post build (like ngen? :))
         */

        /// <summary>
        /// The ~/web/version folder. 
        /// </summary>
        public readonly DirectoryInfo SourceVersionDir;


        public readonly Arguments ConstructorArguments;

        /// <summary>
        /// The local token.
        /// </summary>
        public readonly FileInfo SourceVersion;

        public class Arguments
        {
            public DirectoryInfo TargetDirectory;
            public FileInfo AssamblyFile;
            public ScriptType Language;
        }

        public delegate CachedFileGeneratorBase Constructor(Arguments Arguments);

        public static CachedFileGeneratorBase Create(Arguments Arguments)
        {
            return new CachedFileGeneratorBase( Arguments);
        }

        /// <summary>
        /// This implementation will not be implementing remote tokens, but will define the interface
        /// to be extended by ScriptCoreLib.Ultra.
        /// </summary>
        public CachedFileGeneratorBase(Arguments Arguments)
        {
            this.SourceVersionDir = Arguments.TargetDirectory.CreateSubdirectory("version");
            this.ConstructorArguments = Arguments;
            this.SourceVersion = new FileInfo(SourceVersionDir.FullName + "/" + Arguments.AssamblyFile.Name + "." + Arguments.Language.ToString() + ".version.txt");

        }

        public event Action AtValidate;

        /// <summary>
        /// A missing local token means a rebuild.  An existing remote token means a copy will be sufficient.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if (AtValidate != null)
                AtValidate();

            if (this.SourceVersion.Exists)
            {
                if (this.ConstructorArguments.AssamblyFile.LastWriteTime <= this.SourceVersion.LastWriteTime)
                {
                    // todo: check the dependencies...

                    return true;
                }
            }

            return false;
        }

        public class CachedFile
        {
            public string FileName;
            public string Content;
        }

        public readonly List<CachedFile> Files = new List<CachedFile>();

        public void Add(string FileName, string Content)
        {
            // at some point in the future we may need to adapt SolutionFile as input?

            this.Files.Add(
                new CachedFile
                {
                    FileName = FileName,
                    Content = Content
                }
            );
        }

        public event Action AtWriteTokens;

 
        public void WriteTokens()
        {
            WriteLocalTokens();

            // are we able to generate remote tokens?

            if (AtWriteTokens != null)
                AtWriteTokens();
        }

        public bool DisableOutput;

        public void WriteLocalTokens()
        {
            if (!DisableOutput)
            {
                foreach (var item in this.Files)
                {
                    File.WriteAllText(item.FileName, item.Content);
                }
            }

            File.WriteAllText(
                this.SourceVersion.FullName,
                this.ConstructorArguments.AssamblyFile.LastAccessTimeUtc.ToUniversalTime().ToString()
            );
        }
    }
}
