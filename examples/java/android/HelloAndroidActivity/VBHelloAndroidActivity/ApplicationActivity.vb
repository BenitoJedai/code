Imports ScriptCoreLib
Imports ScriptCoreLib.Delegates
Imports ScriptCoreLib.Extensions
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

Namespace Activities

    ''' <summary>
    ''' Your client side code running inside a web browser as JavaScript.
    ''' </summary>
    Public Class ApplicationActivity
        Inherits Activity

        Protected Overrides Sub onCreate(savedInstanceState As android.os.Bundle)
            MyBase.onCreate(savedInstanceState)

            Dim sv = New ScrollView(Me)
            Dim ll = New LinearLayout(Me)

            ll.setOrientation(LinearLayout.VERTICAL)

            sv.addView(ll)

            Dim b = New Button(Me)

            b.setText(CType(CType("JSC / Visual Basic / Android", Object), CharSequence))

            ll.addView(b)


            Me.setContentView(sv)

        End Sub

    End Class

End Namespace
