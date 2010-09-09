// Learn more about F# at http://fsharp.net

namespace A

open System
open System.Text

module Module1 =

    let Method1() =
        failwith "error 65"

    let MethodNotImplemented() =
        // http://en.wikibooks.org/wiki/F_Sharp_Programming/Exception_Handling
        // http://techiethings.blogspot.com/2009/02/f-exceptions.html
        raise (new NotImplementedException("not done yet"))



    let CollectErrors() =
        let a  = new StringBuilder()

        let a = a.Append("collecting error messages:")

  

        try
            do Method1() |> ignore
        with
            exn -> do a.Append("Method1: " + exn.Message + "; ") |> ignore


        try
            do MethodNotImplemented() |> ignore
        with  exn ->
                do a.Append("Method2: " + exn.Message + "; ") |> ignore


        a.ToString()


type ApplicationWebService() = member this.WebMethod2(x : string, y : Action<string>) = y.Invoke(x + Module1.CollectErrors() )
type ApplicationWebService2() as me = member this.WebMethod2(x : string, y : Action<string>) = y.Invoke(x + Module1.CollectErrors() )
