
namespace UltraApplication

open java.applet
open java.awt
open System;

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
        *)

        
        g.setColor(new Color(0xff));
        g.fillRect(0, 0, w, h / 2)    
                
        g.setColor(new Color(0x7f));
        g.fillRect(0, h / 2, w,  h / 2)    

