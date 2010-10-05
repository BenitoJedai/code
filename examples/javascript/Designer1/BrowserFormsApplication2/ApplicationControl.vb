Imports BrowserFormsApplication2
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Public Class ApplicationControl

	Public Sub New()
		Me.InitializeComponent()
	End Sub 


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.TextBox1.Text &= "hello"

    End Sub
End Class

