Imports System.Threading.Thread



Module Module1




    Sub Main()
        ' The namespace My is currently not supported
        WriteLine("OrcasVisualBasicSimpleJavaConsole. Crosscompiled from Visual Basic to Java.")

        WriteLine("---------------------------------")

        WriteLine("This console application can run at .net and java virtual machine!")
        WriteLine()

        WriteLine("running at: " + Environment.CurrentDirectory)
        WriteLine()


        ' lets test some inline event handlers
        Dim a As New MyIntegerProperty

        ' get some cool numbers
        ' http://www.google.ee/search?rlz=1C1GGLS_etEE292EE304&sourceid=chrome&ie=UTF-8&q=lost+sequence

        ' visual basic has its own conversion api... lets use the BCL versions this time instead!
        a.Changed = Function() WriteLine("sequence number: " & Convert.ToString(a.Value))

        Dim sequence As Integer() = {4, 8, 15, 16, 23, 42}



        For Each number In sequence
            a.Value = number

            Sleep(300)
        Next




    End Sub

    Public Delegate Function ObjectFunc() As Object


    ''' <summary>
    ''' A functional Console.WriteLine which can be used as event handlers too
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function WriteLine(Optional ByVal e As String = "") As Object

        Console.WriteLine(e)
        Return Nothing

    End Function


    Class MyIntegerProperty
        ' using events would mean java needs to support List<WeakReference>
        ' should it be supported for java 1.4?
        ' adding and removing events in .net 4 will change anyhow...

        Public Changed As ObjectFunc


        Private InternalValue As Integer
        Public Property Value() As Integer

            Get
                Return InternalValue
            End Get
            Set(ByVal value As Integer)
                InternalValue = value

                If Not Changed Is Nothing Then
                    Changed()
                End If
            End Set
        End Property

    End Class
End Module
