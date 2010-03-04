// Learn more about F# at http://fsharp.net

module Module1


// error FS0841: This attribute is not valid for use on this language element.
//  Assembly attributes should be attached to a 'do ()' declaration, if necessary
//  within an F# module.

// http://stackoverflow.com/questions/2269625/using-assembly-attributes-in-f
[<assembly: System.Reflection.Obfuscation(Feature = "merge")>] 
do ()

let square x = x * x