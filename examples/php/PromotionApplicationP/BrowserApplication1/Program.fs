// For more information visit:
// http://studio.jsc-solutions.net/

namespace BrowserApplication1

    open System

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =
            // Prepare the yield value for
            
//            do jsc.BrowserApplicationDiagnostics.Launch typeof<Application>
            do global.jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)

            0

