open System

/// <summary>
/// You can debug your application by hitting F5.
/// </summary>
module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    let Main(args : string[]) =

        let x = new monese.experimental.MoneseWebServices()


//            385
//385
//0



        do
            x.RegisterUserShortAsync("a@", "1234", 
                fun z ->
                    do Console.WriteLine(z)
                    do x.GetUserIDAsync("a@", "1234",
                        fun x ->
                            do Console.WriteLine(x)
                    );
            )


        do System.Windows.Forms.MessageBox.Show("ok")

        0
