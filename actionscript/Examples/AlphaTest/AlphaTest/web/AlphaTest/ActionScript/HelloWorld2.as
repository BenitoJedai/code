
package AlphaTest.ActionScript
{
        import flash.display.Sprite;
        import flash.display.Shape;
        import flash.display.Graphics;
        import flash.display.Sprite;
        import flash.text.TextField;
        import flash.filters.DropShadowFilter;
        import flash.display.*;
        import flash.events.*;
        import flash.net.*;
        import flash.utils.*;
        import flash.media.*;
        import mx.core.*;
    
        public class HelloWorld2 extends Sprite
        {
                [Embed(source="/assets/AlphaTest/Preview.png")]
		        private var PreviewImage:Class;
            
                [Bindable]
                [Embed(source="/assets/AlphaTest/rain_1.mp3")]
                private var c:Class;
                    
                // http://livedocs.adobe.com/flex/2/langref/mx/core/SoundAsset.html
                public var b:SoundAsset;
                
                public function s2(): SoundAsset { 
                    b = SoundAsset(new c()); 
                    
                    return b;
                }
            
                public function AddText():TextField
                {
                    var shad:DropShadowFilter = new DropShadowFilter (2, 45, 0x000000, 25, 3, 3, 2, 2);
                    var txt:TextField = new TextField();
                
                    txt.textColor = 0xFFFFFF;
                    txt.filters = [shad];
                    txt.width = 200;
                    txt.x = Math.random() * 300;
                    txt.y = Math.random() * 300;
                    txt.selectable = false;
                    txt.text = "Hello World welcome! [" + Math.round(txt.x) + "," + Math.round(txt.y) + "]";
                
                
                    addChild(txt);
                    
                    return txt;
                }
                
                
                private function drawColoredRectIn(target:Graphics, color:int):void 
                {
                    target.lineStyle(1, 0x000000);
                    target.beginFill(color);
                    target.drawRect(0, 0, 100, 100);
                }
                
                public function HelloWorld2()
                {
                    AddText();
                    
                    AddText();
                    
                    //  play(startTime:Number = 0, loops:int = 0, sndTransform:SoundTransform = null):SoundChannel  
                    
                    s2().play(0, 999);
                    
                    // rects
                    	var rect:Shape = new Shape();
                        drawColoredRectIn(rect.graphics, 0xFFFF00);
                        rect.x = 50;
                        rect.y = 50;
                        addChild(rect);
                    
                    addChild(new PreviewImage());
                }
        }
}