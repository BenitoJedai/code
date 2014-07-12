Imports VBXElementExample
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports System

Partial Public Class ApplicationControl
    Inherits UserControl
    Public Sub New()
        Me.InitializeComponent()
    End Sub


    Private Sub textBox1_TextChanged(sender As Object, e As System.EventArgs) Handles textBox1.TextChanged
        If PartialUpdate Then Return

        PartialUpdate = True

        textBox3.Text =
            New XElement("KeyValuePair",
                New XAttribute("Key", textBox1.Text),
                New XElement("Value", textBox2.Text)
            ).ToString()


        PartialUpdate = False

    End Sub

    Private Sub textBox2_TextChanged(sender As Object, e As System.EventArgs) Handles textBox2.TextChanged
        If PartialUpdate Then Return

        PartialUpdate = True

        textBox3.Text =
            New XElement("KeyValuePair",
                New XAttribute("Key", textBox1.Text),
                New XElement("Value", textBox2.Text)
            ).ToString()


        PartialUpdate = False
    End Sub

    Private Sub textBox3_TextChanged(sender As Object, e As System.EventArgs) Handles textBox3.TextChanged
        If PartialUpdate Then Return

        PartialUpdate = True

        Try
            Dim doc = XElement.Parse(textBox3.Text)

            textBox1.Text = doc.Attribute("Key").Value
            textBox2.Text = doc.Element("Value").Value

            textBox3.ForeColor = Color.Black
        Catch
            textBox3.ForeColor = Color.Red
        End Try

        PartialUpdate = False
    End Sub
    Dim PartialUpdate As Boolean


    Private Sub ApplicationControl_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        textBox1.Text = "foo"
        textBox2.Text = "bar"

    End Sub
End Class

