namespace FSharpNuGetSQLiteExperiment

    open jsc.meta.Commands.Rewrite.RewriteToUltraApplication
    open System


//    Error	1	
// A function labeled with the 'EntryPointAttribute' attribute must be the last declaration 
// in the last file in the compilation sequence, and can only be used when compiling
//  to a .exe	
// X:\jsc.svn\examples\javascript\fsharp\FSharpNuGetSQLiteExperiment\FSharpNuGetSQLiteExperiment\Program.fs	11	13	FSharpNuGetSQLiteExperiment


    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    module Program =
        [<Microsoft.FSharp.Core.EntryPoint>]
        let Main(args : string[]) =
            do RewriteToUltraApplication.AsProgram.Launch(typeof<Application>)
            0

