open System.Windows.Forms
open System
open System.Threading.Tasks
open monese.experimental

//            385
//385
//0

let r = async {
    // !!! full test suite shall run on every
    // version change and shall be part of the
    // monese.com penetration test task force
    // public opensource SDK and test cases?
    let x = new MoneseWebServices()

    Console.WriteLine "RegisterUserShortAsync before"

    let email = "hanna-mari@monese.com"

    let! z = x.RegisterUserShortFSharpAsync(email, "1234")

    Console.WriteLine z
    Console.WriteLine "RegisterUserShortAsync done"

    Console.WriteLine "GetUserIDAsync before"

    // http://msdn.microsoft.com/en-us/library/ee837067.aspx
    // let! xxx = Async.AwaitTask( x.GetUserID ( "a@", "1234"))
    let! xxx = x.GetUserIDFSharpAsync ( email, "1234")
                
    Console.WriteLine xxx
    Console.WriteLine "GetUserIDAsync done"
        
    Console.WriteLine "GetUserIDAsync after"


    //        x.GetUserIDAsync("a@", "1234",
    //            fun x ->

    //        );

}

// http://www.jaylee.org/post/2013/04/16/Cancellation-with-Async-Fsharp-Csharp-and-the-Reactive-Extensions.aspx
Async.StartAsTask(r).Wait()
