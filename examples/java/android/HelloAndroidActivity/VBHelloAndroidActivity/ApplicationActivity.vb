Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
Imports ScriptCoreLib.Android.Extensions

Imports ScriptCoreLib.JavaScript
Imports ScriptCoreLib.JavaScript.Components
Imports ScriptCoreLib.JavaScript.DOM
Imports ScriptCoreLib.JavaScript.DOM.HTML
Imports ScriptCoreLib.JavaScript.Extensions
Imports System
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq
Imports VBHelloAndroidActivity.Design
Imports VBHelloAndroidActivity.HTML.Pages
Imports android.app
Imports android.widget
Imports java.lang
Imports android.view

Namespace Activities

    ''' <summary>
    ''' Your client side code running inside a web browser as JavaScript.
    ''' </summary>
    Public Class ApplicationActivity
        Inherits Activity

        Dim ref0 As View


        Protected Overrides Sub onCreate(savedInstanceState As android.os.Bundle)
            MyBase.onCreate(savedInstanceState)

            Dim sv = New ScrollView(Me)
            Dim ll = New LinearLayout(Me)

            ll.setOrientation(LinearLayout.VERTICAL)

            sv.addView(ll)

            Dim b = New Button(Me)

            b.setText("JSC / Visual Basic / Android")

            ll.addView(b)

            'Dim a As Action(Of View) = Sub(v) b.setText("clicked")

            b.AtClick(Sub(v) b.setText("clicked"))

            Me.setContentView(sv)

        End Sub

    End Class

End Namespace
