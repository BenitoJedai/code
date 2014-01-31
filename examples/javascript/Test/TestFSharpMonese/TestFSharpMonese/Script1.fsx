#r "X:\jsc.svn\examples\javascript\Test\TestFSharpMonese\packages\xmoneseAPI.1.0.0.0\lib\monese API.dll"
open System

let x = new monese.experimental.MoneseWebServices()

x.RegisterUserShortAsync("a@", "1234", 
    fun z ->
        do Console.WriteLine(z)
        do x.GetUserIDAsync("a@", "1234",
            fun x ->
                do Console.WriteLine(x)
        );
)
