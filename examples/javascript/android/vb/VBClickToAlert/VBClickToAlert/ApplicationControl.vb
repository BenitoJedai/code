Imports VBClickToAlert
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Partial Public Class ApplicationControl
    Inherits UserControl
    Public Sub New()
        Me.InitializeComponent()
    End Sub



    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        MessageBox.Show("hello world")
    End Sub
End Class

