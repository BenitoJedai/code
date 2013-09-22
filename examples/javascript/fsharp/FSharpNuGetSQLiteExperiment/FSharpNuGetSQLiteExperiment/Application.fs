namespace FSharpNuGetSQLiteExperiment

    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.JavaScript
    open ScriptCoreLib.JavaScript.Components
    open ScriptCoreLib.JavaScript.DOM
    open ScriptCoreLib.JavaScript.DOM.HTML
    open ScriptCoreLib.JavaScript.Extensions
    open System
    open System.Linq
    open System.Text
    open System.Xml.Linq
    open System.Diagnostics
    open FSharpNuGetSQLiteExperiment.Design
    open FSharpNuGetSQLiteExperiment.HTML.Pages

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IApp) as me = 
        let this = me
        do ()
        let service = new ApplicationWebService()

        let sw = new Stopwatch()

        do sw.Start()

        // Send data from JavaScript to the server tier
        do service.WebMethod2(
            "A string from JavaScript.",
            fun value -> 
                // Show the server message as document title

                let p = new IHTMLPre()

                // Error	1	Type constraint mismatch. The type 
//    string    
//is not compatible with type
//    TimeSpan    
//The type 'string' is not compatible with the type 'TimeSpan'	X:\jsc.svn\examples\javascript\fsharp\FSharpNuGetSQLiteExperiment\FSharpNuGetSQLiteExperiment\Application.fs	41	38	FSharpNuGetSQLiteExperiment
// Error	1	The type 'int64' does not match the type 'string'	X:\jsc.svn\examples\javascript\fsharp\FSharpNuGetSQLiteExperiment\FSharpNuGetSQLiteExperiment\Application.fs	47	38	FSharpNuGetSQLiteExperiment

//
// statement cannot be a load instruction (or is it a bug?)
// [0x0012] ldarg.0    +1 -0

                let ElapsedMilliseconds = sw.ElapsedMilliseconds.ToString()

                // fsharp and C# params dont play well with jsc yet

                let gray = new IHTMLSpan()



                gray.Add("[");
                gray.Add(ElapsedMilliseconds);
                gray.Add("ms] ");
                gray.style.color <- "gray"

                p.Add(gray);

                p.Add(value);

//
//                ScriptCoreLib.JavaScript.Extensions.Extensions.AttachToDocument(
//                    p.AsNode()
//                ) |> ignore

                ()

        )

