namespace VS2012FSharp3WebApplication

    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
    open System

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
            0

