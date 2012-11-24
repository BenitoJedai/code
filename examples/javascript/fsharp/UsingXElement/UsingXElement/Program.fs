namespace UsingXElement

    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
    open ScriptCoreLib.Desktop.Extensions
    open System

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =

        // Could not load file or assembly 'jsc.meta, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. An attempt was made to load a program with an incorrect format.
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
            0

