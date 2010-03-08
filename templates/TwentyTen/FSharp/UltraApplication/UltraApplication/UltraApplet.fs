
namespace UltraApplication

open java.applet
open java.awt
open System;
open ScriptCoreLib.Ultra.Library.Extensions

[<Sealed>]
 type UltraApplet() =
    inherit Applet()

    override  this.paint(g : Graphics) = do
        let w = this.getWidth()
        let h = this.getHeight()

        (* 
        note:
        'for' loop generates 'do while' which jsc does not yet support
        'while' loop generates 'while (true)' which jsc currently does support 

        http://en.wikibooks.org/wiki/F_Sharp_Programming/Control_Flow

        we need to extend jsc.meta to rewrite/simplify IL 
        
        for now we can reuse methods defined in another assembly written in C#
        *)

        LoopExtensions.For(0, h / 2,
            fun (i) ->
                do
                    g.setColor(new Color(0xff - 0xff * i / h))
                    g.drawLine(0, i, w, i)   
        ) 
                
        g.setColor(new Color(0x7f))
        g.fillRect(0, h / 2, w,  h / 2)    

        let text = "jsc-solutions.net";

        g.setFont(new Font("Verdana", 0, 24));
        g.setColor(new Color(0))
        g.drawString(text, 8 + 1, h - 32 + 1);

        g.setFont(new Font("Verdana", 0, 24));
        g.setColor(new Color(0xffffff))
        g.drawString(text, 8, h - 32);