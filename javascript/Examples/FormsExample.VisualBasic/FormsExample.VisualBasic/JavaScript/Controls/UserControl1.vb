<Script()> _
Public Class UserControl1

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.Button2.Enabled = Me.CheckBox1.Checked

    End Sub
End Class
