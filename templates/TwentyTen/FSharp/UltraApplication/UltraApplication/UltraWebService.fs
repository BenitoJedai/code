namespace UltraApplication


open global.System
open global.System.Reflection

[<Sealed>]
 type UltraWebService() =
    member 
        this.WebMethod1(x : string,  yieldreturn : Action<string>) =
     let y = x + x
     do yieldreturn.Invoke(y)