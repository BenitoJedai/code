open System.Windows.Forms
open System

//            385
//385
//0

let x = new monese.experimental.MoneseWebServices()

x.RegisterUserShortAsync("a@", "1234", 
    fun z ->
        Console.WriteLine z

        x.GetUserIDAsync("a@", "1234",
            fun x ->
                Console.WriteLine x
        );
)

//Console.WriteLine "any key to exit"
MessageBox.Show "exit api test"
