namespace BrowserAvalonApplication3

    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
    open ScriptCoreLib.Desktop.Extensions
    open System

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =
#if DEBUG
            do DesktopAvalonExtensions.Launch(
                fun ()  -> 
                    new ApplicationCanvas()
                    

            ) |> ignore
#else
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
#endif
            0

