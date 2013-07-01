Module Module1

    Sub Main()

        Try

            Throw New NotImplementedException("hi")

        Catch ex As NotImplementedException When ex.Message = "hi"

            Console.WriteLine(ex.Message)


        End Try
    End Sub

End Module
