Imports VBAsyncExperiment
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading.Tasks
Imports System

Partial Public Class ApplicationControl
    Inherits UserControl
    Public Sub New()
        Me.InitializeComponent()
    End Sub


    Private Async Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Await FooAsync()

    End Sub



    Async Function FooAsync() As Task
        ' http://blogs.msdn.com/b/pfxteam/archive/2012/09/11/forking-in-async-methods.aspx

        Console.WriteLine("Starting FooAsync")
        Dim t1 = (Async Function()
                      Console.WriteLine("Starting first async block")
                      Await Task.Delay(1000)
                      Console.WriteLine("Done first block")

                      Return "a"
                  End Function)()
        Dim t2 = (Async Function()
                      Console.WriteLine("Starting second async block")
                      Await Task.Delay(2000)
                      Console.WriteLine("Done second block")

                      Return "b"
                  End Function)()
        Dim x = Await Task.WhenAll(t1, t2)
        Console.WriteLine("Done FooAsync")
    End Function
End Class

