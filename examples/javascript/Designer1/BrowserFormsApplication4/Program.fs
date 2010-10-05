namespace BrowserFormsApplication4

    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
    open ScriptCoreLib.Desktop.Forms.Extensions
    open System

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =
#if DEBUG
            do DesktopFormsExtensions.Launch(
                fun ()  -> 
                    new ApplicationControl()
                    

            ) |> ignore
#else
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
#endif
            0

