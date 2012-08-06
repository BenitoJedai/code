namespace FSharpAndroidHelloActivity.Activities

open android.app
open android.widget
open ScriptCoreLib.Android.Extensions

type ApplicationActivity()  = 
    inherit Activity() 

    override this.onCreate savedInstanceState = 
        base.onCreate(savedInstanceState)

        let sv =  new ScrollView(this)
        let ll =  new LinearLayout(this)

        do sv.addView (ll)

        let b = new Button(this)

        do b.setText("JSC / FSharp / Android")

        b.AtClick(
            fun v ->
                do b.setText("clicked")
        )

        do ll.addView(b)

        this.setContentView(sv)


module Program =
    [<Microsoft.FSharp.Core.EntryPoint>]
    let Main(args : string[]) =
        do global.jsc.AndroidLauncher.Launch(typeof<ApplicationActivity>)
        0
 