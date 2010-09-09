// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace TestPHPExceptionsFSharp

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Delegates

    module Module1 =

        let Method1() =
            failwith "error 65"

        let MethodNotImplemented() =
            // http://en.wikibooks.org/wiki/F_Sharp_Programming/Exception_Handling
            // http://techiethings.blogspot.com/2009/02/f-exceptions.html
            raise (new NotImplementedException("not done yet"))



        let Collect1 (a:StringBuilder) =
            try
                do Method1() |> ignore
            with
                exn -> do a.Append("Method1: " + exn.Message + "; ") |> ignore
            ()

        let Collect2 (a:StringBuilder) =
            try
                do MethodNotImplemented() |> ignore
            with  exn ->
                    do a.Append("Method2: " + exn.Message + "; ") |> ignore
            ()


        let CollectErrors() =
            let a  = new StringBuilder()

            let a = a.Append("collecting error messages:")

            Collect1 a
            Collect2 a



            a.ToString()

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() = 
//        Warning	2	The recursive object reference 'me' is unused. 
// The presence of a recursive object reference adds runtime initialization 
// checks to members in this and derived types. Consider removing this 
// recursive object reference.	


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        member this.WebMethod2(e : String, y : StringAction) =
            // Send it back to the caller.
            do y.Invoke(e + Module1.CollectErrors())
            ()

