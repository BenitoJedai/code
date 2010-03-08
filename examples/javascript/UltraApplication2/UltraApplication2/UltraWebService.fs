namespace UltraApplication2


open global.System
open global.System.Reflection

[<Sealed>]
 type UltraWebService() =
    member 
        this.WebMethod1(x : string,  yieldreturn : Action<string>) =
     let y = x + DateTime.Now.ToString()
     do yieldreturn.Invoke(y)