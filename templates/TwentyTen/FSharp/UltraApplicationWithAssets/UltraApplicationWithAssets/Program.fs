namespace UltraApplicationWithAssets


   
// For more information visit:
// http://studio.jsc-solutions.net
 
// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project
 
// View as Visual CSharp project
// http://do.jsc-solutions.net/View-as-Visual-CSharp-project
 

    open System
    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
 
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    module Program =
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
         [<Microsoft.FSharp.Core.EntryPoint>]
         let Main(args : string[]) =
            // Prepare the yield value for
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
            0
 

