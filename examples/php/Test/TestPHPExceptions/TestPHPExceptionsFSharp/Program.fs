// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace TestPHPExceptionsFSharp

    open System
    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : String[]) =
            // Prepare the yield value for
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
            0

