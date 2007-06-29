using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Microsoft.Build.Utilities;

namespace jsc
{

    public class CompileSessionInfo
    {
        public class LoggingHelper
        {
            public TaskLoggingHelper TaskLogging;

            public void LogMessage(string e, params object[] args)
            {
                if (TaskLogging == null)
                    Console.WriteLine(e, args);
                else
                    TaskLogging.LogMessage(Microsoft.Build.Framework.MessageImportance.High, e, args);
            }
        }

        public readonly LoggingHelper Logging = new LoggingHelper();
        public CommandLineOptions Options;


    }


    public class CommandLineOptions : CommandLineOptionsBase<CommandLineOptions>
    {



        public CommandLineOptions()
        {

        }

        public CommandLineOptions(string[] args)
            : base(args)
        {
        }

        [CommandLineOption(Offset = 0, IsRequired = true,
            Description = "Target assembly filename in the working directory.")]
        public FileInfo TargetAssembly;

        [CommandLineOption(Flag = "nothreads", Description = "Multithreading will be disabled")]
        public bool IsNoThreads = true;

        [CommandLineOption(Flag = "q",
            Description = "Will load java only job.")]
        public bool IsJavaOnly;

        [CommandLineOption(Flag = "x")]
        public bool IsSmart;

        [CommandLineOption(Flag = "z",
            Description = "Will load all modules and compile all languages")]
        public bool IsAllModulesAllLanguages;

        [CommandLineOption(Flag = "java")]
        public bool IsJava;

        [CommandLineOption(Flag = "php")]
        public bool IsPHP;

        [CommandLineOption(Flag = "js")]
        public bool IsJavascript;

        [CommandLineOption(Flag = "ShowReferences")]
        public bool ShowReferences;
        
        [CommandLineOption(Flag = "ExtractAssets")]
        public bool ExtractAssets;

        [CommandLineOption(Flag = "KeepFullNames", Description="Emits namespace and member name")]
        public bool KeepFullNames;


        [CommandLineOption(
            Flag = "nologo",
            Description = "Suppress compiler copyright message")]
        public bool IsNoLogo;

        [CommandLineOption(
            Flag = "trace",
            Description = "Enables trace mode")]
        public bool IsTrace;

        [CommandLineOption(
            Flag = "noskip",
            Description = "Version info will be ignored, assembly will be forced build")]
        public bool DisableVersionCheck;

        [CommandLineOption(Flag = "enableremote", Description="Program will continue to run on a remote location like network share")]
        public bool EnableRemoteExecution;

        [CommandLineOption(
            Flag="jmc",
            Description="Will not emit source code of the referenced assemblies if flag is set"
            )]
        public bool JustMyCode;
    }

}
