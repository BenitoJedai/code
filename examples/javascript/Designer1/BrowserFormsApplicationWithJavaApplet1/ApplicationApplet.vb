Imports java.applet
Imports ScriptCoreLib.Java.Extensions

Public NotInheritable Class ApplicationApplet
	Inherits Applet
	Public ReadOnly content As New ApplicationControl()

	Public Overrides Sub init()
		content.AttachTo(Me)
		content.AutoSizeTo(Me)
		Me.EnableVisualStyles()
	End Sub 


End Class

