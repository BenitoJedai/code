open System.Windows.Forms
open System

//            385
//385
//0

let x = new monese.experimental.MoneseWebServices()

Console.WriteLine "RegisterUserShortAsync before"

x.RegisterUserShortAsync("a@", "1234", 
    fun z ->

        Console.WriteLine "RegisterUserShortAsync"
        Console.WriteLine z
        Console.WriteLine "RegisterUserShortAsync done"

        Console.WriteLine "GetUserIDAsync before"

        x.GetUserIDAsync("a@", "1234",
            fun x ->
                Console.WriteLine "GetUserIDAsync"
                Console.WriteLine x
                Console.WriteLine "GetUserIDAsync done"
        );
)

//Console.WriteLine "any key to exit"
MessageBox.Show "exit api test"
