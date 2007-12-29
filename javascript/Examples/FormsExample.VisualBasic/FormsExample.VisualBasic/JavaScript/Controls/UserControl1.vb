Imports Microsoft.VisualBasic.Constants
Imports ScriptCoreLib


<Script()> _
Public Class UserControl1

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()


    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.TextBox2.Text += "RadioButton1" + vbNewLine




    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.TextBox2.Text += "RadioButton2" + vbNewLine

    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        Me.TextBox2.Text += "RadioButton4" + vbNewLine

    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Me.TextBox2.Text += "RadioButton3" + vbNewLine

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Me.TextBox2.Text += "CheckBox2" + vbNewLine

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.TextBox2.Text += "CheckBox1" + vbNewLine

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox2.Clear()

    End Sub
End Class
